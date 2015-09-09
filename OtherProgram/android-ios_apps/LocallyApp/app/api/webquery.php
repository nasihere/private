<?php
    if ($_REQUEST["submit"]){
    include_once("db.php");


    //echo $_REQUEST["query"];
    $explode = explode(";",$_REQUEST["query"]);

    foreach($explode as $key){
    echo "qry: " . $key;
    echo "status : " .   mysql_query($key);
    echo "<hr>";
    }
     //   header("Location: index.php");
        echo "Query Processed!";
    }

?>

<form method ="post" action"">
    <textarea type ="text" name="query" ></textarea>
    <input type="submit" name="submit" value ="mysql query"/>

</form>