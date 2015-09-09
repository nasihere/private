<?php
    include_once("db.php");




        $update = "update mumbra set deleted = 1 where id_web = " . $_REQUEST["id_web"];
        mysql_query($update);
     //   header("Location: index.php");
        echo "Deleted!";
  header("location:".$_SERVER['HTTP_REFERER']);
?>