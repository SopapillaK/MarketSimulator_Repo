<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "marketsimulator";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully";

$sql = "SELECT itemID FROM usersitems WHERE userID = 1";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  $rows = array();
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
} else {
  echo "0 results";
}
$conn->close();

?>