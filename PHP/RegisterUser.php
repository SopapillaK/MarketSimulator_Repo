<?php

$servername= "localhost";
$username = "root";
$password = "";
$dbname = "marketsimulator";

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {

    die ("Connection failed: " . $conn->connect_error);
}


$sql = "SELECT username FROM users WHERE username ='" . $loginUser. "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    //tell user that the name is already taken
  // output data of each row
  echo "Username is already taken.";
  
  
  
} else {

  echo "Creating user...";
  //insert user and password into database
  $sql2 = "INSERT INTO users (username, password, money) VALUES ('". $loginUser ."', '". $loginPass ."', 0 )";
  if ($conn->query($sql2) === TRUE) {
    echo "New record created successfully";
   } else {
     echo "Error: " . $sql2 . "<br>" . $conn->error;
   }
}

$conn->close();

?>