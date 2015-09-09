<?php
    include_once("db.php");
    include_once("param.php");

    $today = date("F j, Y, g:i a");
          $time = microtime();
//Dont need to do echo to this query becasue concat not work in sqlite
     $query = "update mumbra set book = CONCAT(Book,  '" . $_REQUEST["mobile"] . "-,-". $_REQUEST["comment"]  . "-,-". $today . "-:-'), json = '".$time."'  where id_web = " . $_REQUEST["id_web"];
    $result = mysql_query($query);
 echo $query = "update mumbra set book = Book ||  '" . $_REQUEST["mobile"] . "-,-". $_REQUEST["comment"]  . "-,-". $today . "-:-', json = '".$time."'  where id_web = " . $_REQUEST["id_web"];

?>