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

$sql = "SELECT username, money FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "username: " . $row["username"]. " - money: " . $row["money"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>