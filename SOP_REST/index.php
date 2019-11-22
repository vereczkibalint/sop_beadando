<?php
require_once 'db.php';

$db = new Database();
$connection = $db->openConnection();

$request = $_SERVER["REQUEST_METHOD"];

switch($request){
    case "GET":
        if(empty($_GET["id"])){
            get_all_todos();
        }else{
            $id = intval($_GET["id"]);
            get_todo($id);
        }
    break;
    case "POST":
        insert_todo();
    break;
    case "PUT":
        $id = intval($_GET["id"]);
        update_todo($id);
    break;
    case "DELETE":
        $id = intval($_GET["id"]);
        delete_todo($id);
    break;
    default:
        header("HTTP/1.0 405 Method Not Allowed");
    break;
}

function get_all_todos(){
    global $connection;

    $query = "SELECT * FROM todos";
    $response = array();
    $result = mysqli_query($connection, $query);
    while($row = mysqli_fetch_assoc($result)){
        $response[] = $row;
    }
    header("Content-Type: application/json");
    echo json_encode($response);
}

function get_todo($id = 0){
    global $connection;

    $query = "SELECT * FROM todos";
    if($id != 0){
        $query .= " WHERE todo_id=".$id." LIMIT 1";
    }
    $response = array();
    $result = mysqli_query($connection, $query);
    while($row = mysqli_fetch_assoc($result)){
        $response[] = $row;
    }
    header("Content-Type: application/json");
    echo json_encode($response);
}

function insert_todo(){
    global $connection;
    $data = json_decode(file_get_contents("php://input"), true);

    $todo_title = $data["todo_title"];
    $todo_body = $data["todo_body"];
    $todo_author = $data["todo_author"];
    $todo_deadline = $data["todo_deadline"];
    $todo_priority = $data["todo_priority"];

    $query = "INSERT INTO todos (todo_title, todo_body, todo_author, todo_deadline, todo_priority) VALUES ('".$todo_title."','".$todo_body."','".$todo_author."', '".$todo_deadline."', '".$todo_priority."')";
    if(mysqli_query($connection, $query)){
        $response = array(
            "status" => 1,
            "message" => "Sikeres adatfelvitel"
        );
    }else{
        $response = array(
            "status" => 0,
            "message" => "Sikertelen adatfelvitel"
        );
    }
    header("Content-Type: application/json");
    echo json_encode($response);
}

function update_todo($id){
    global $connection;

    global $connection;
    $data = json_decode(file_get_contents("php://input"), true);

    $todo_title = $data["todo_title"];
    $todo_body = $data["todo_body"];
    $todo_author = $data["todo_author"];
    $todo_deadline = $data["todo_deadline"];
    $todo_priority = $data["todo_priority"];

    $query = "UPDATE todos SET todo_title = '".$todo_title."',todo_body = '".$todo_body."', todo_author = '".$todo_author."', todo_deadline = '".$todo_deadline."', todo_priority = '".$todo_priority."' WHERE todo_id = ".$id;
    if(mysqli_query($connection, $query)){
        $response = array(
            "status" => 1,
            "message" => "Sikeres adatmódosítás"
        );
    }else{
        $response = array(
            "status" => 0,
            "message" => "Sikertelen adatmódosítás"
        );
    }
    header("Content-Type: application/json");
    echo json_encode($response);
}

function delete_todo($id){
    global $connection;

    $query = "DELETE FROM todos WHERE todo_id=".$id;

    if(mysqli_query($connection, $query)){
        $response = array(
            "status" => "1",
            "message" => "Sikeres törlés"
        );
    }else{
        $response = array(
            "status" => "0",
            "message" => "Sikertelen törlés"
        );
    }
    header("Content-Type: application/json");
    echo json_encode($response);
}
?>