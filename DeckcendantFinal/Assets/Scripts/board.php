<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "deckcendant";
//$numberOfRowsToRead;
//$currentRow;
$name = $_POST["name"];
$score = $_POST["score"];


$conn = new mysqli($servername,$username,$password,$dbname);
if ($conn->connect_error){
    die("Connection Failed: ". $conn->connect_error);

}
//echo "Connected successfully";
if(isset($name) && isset($score)){
    
    $newsql = "INSERT INTO scores (PlayerName, Score) VALUES ('". $name."','".$score."')";

} else{
    echo "PlayerName or Score do not hold values <br>";
}

$add = $conn ->query($newsql);
$sql = "SELECT PlayerName, Score FROM scores";

$result = $conn->query($sql);



if($result->num_rows >0){
    while($row = $result->fetch_assoc()){
        echo "Name: ". $row["PlayerName"]."\t"."Score: ".$row["Score"]."<br>";
    } 
       
    

}else{
echo "0 results";
} 


$conn ->close();