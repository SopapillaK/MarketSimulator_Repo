<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "marketsimulator";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$sql = "SELECT id, username, money FROM users WHERE username=? AND password=?";
$stmt = $conn->prepare($sql);
$stmt->bind_param("ss", $loginUser, $loginPass);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
  $row = $result->fetch_assoc();
  header('Content-Type: application/json');
  echo json_encode([
    'userID' => $row['id'],
    'username' => $row['username'],
    'money' => $row['money']
  ]);
} else {
  echo json_encode(['error' => 'Wrong Credentials']);
}

$conn->close();
?>
