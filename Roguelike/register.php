<?php
include 'db_config.php';

//get the username and password from Unity
$username = $_POST['username'];
$password = $_POST['password'];

//hash the password
$hashed_password = password_hash($password, PASSWORD_DEFAULT);

//insert user into database
$sql = "INSERT INTO users (username, password) VALUES ('$username', '$hashed_password')";

if ($conn->query($sql) === TRUE) {
    echo "Registration successful";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>
