<?php
    include_once("db.php");
    include_once("param.php");

     $time = date("Y-m-d h:i:s");


       $update = "update mumbra set selected = replace(selected,'".$_REQUEST['selected'].",','') where id_web = " . $_REQUEST["id_web"];
              mysql_query($update);
      $query = "update mumbra set selected = concat(selected, '".$_REQUEST['selected'].",'), json = '" . $time ."'  where id_web = " . $_REQUEST["id_web"];
     $result = mysql_query($query);
  $query = "update mumbra set selected = selected || '".$_REQUEST['selected'].",', json = '" . $time ."'  where id_web = " . $_REQUEST["id_web"];

?>