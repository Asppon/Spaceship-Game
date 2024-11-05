<?php
$servername = "localhost";
$username = "root";
$password = "";//database connection parameters
$database = "leaderboarddata";
$conn = new mysqli($servername, $username, $password, $database);
//create connection
if ($conn->connect_error) {//check connection
    die("Connection failed: " . $conn->connect_error);
}
//SQL query to retrieve the top 5 scores
$sql = "SELECT Name, Score FROM leaderboard ORDER BY Score DESC LIMIT 5";
$result = $conn->query($sql);
//check if there are any rows returned
if ($result->num_rows > 0) {
    //output data of each row
    $scores = array();
    while($row = $result->fetch_assoc()) {
        $scores[] = $row;
    }
    //convert the array to JSON format
    echo json_encode($scores);
} else {
    echo "No scores found";
}
$conn->close();
?>


