<?php
    include_once("db.php");
    include_once("param.php");
        $time = microtime();
      $query = "update mumbra set  edit = concat(edit, '".$_REQUEST["edit"].",".$_REQUEST["session_id"]."'), json = '".$time."' where id_web = " . $_REQUEST["id_web"];
     $result = mysql_query($query);

?>