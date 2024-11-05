<?php
include 'db_config.php';

// Get the username and password from Unity
$username = $_POST['username'];
$password = $_POST['password'];

// Check if username exists
$sql = "SELECT * FROM users WHERE username='$username'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Username exists, verify password
    $row = $result->fetch_assoc();
    if (password_verify($password, $row['password'])) 
    {
        echo "Login successful";
    } else {
        echo "Incorrect password";
    }
} else {
    echo "User not found";
}

$conn->close();
?>

