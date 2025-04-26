<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "marketsimulator";

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

if (isset($_POST['userID']) && isset($_POST['itemID'])) {
  $userID = intval($_POST['userID']);
  $itemID = intval($_POST['itemID']);

  //get item name and price
  $itemQuery = "SELECT name, price FROM item WHERE id = $itemID";
  $itemResult = $conn->query($itemQuery);

  if ($itemResult->num_rows > 0) {
    $itemRow = $itemResult->fetch_assoc();
    $itemName = $itemRow['name'];
    $price = $itemRow['price'];

    //get user money
    $userQuery = "SELECT money FROM users WHERE id = $userID";
    $userResult = $conn->query($userQuery);

    if ($userResult->num_rows > 0) {
      $userRow = $userResult->fetch_assoc();
      $money = $userRow['money'];

      if ($money >= $price) {
        //deduct money and insert item
        $conn->query("UPDATE users SET money = money - $price WHERE id = $userID");
        $conn->query("INSERT INTO usersitems (userID, itemID) VALUES ($userID, $itemID)");

        //return updated money as JSON
        $newMoneyResult = $conn->query("SELECT money FROM users WHERE id = $userID");
        $newMoney = $newMoneyResult->fetch_assoc()['money'];

        echo json_encode(array("newCoins" => $newMoney));
      } else {
        echo json_encode(array("error" => "Not enough money"));
      }
    } else {
      echo json_encode(array("error" => "User not found"));
    }
  } else {
    echo json_encode(array("error" => "Item not found"));
  }
} else {
  echo json_encode(array("error" => "Missing parameters"));
}

$conn->close();
?>
