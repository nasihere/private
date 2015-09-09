<?php
  include_once("db.php");
  foreach($_REQUEST as $key => $val){
    $error .= $key . " = " . $val .", \n";
  }
    $query = "insert into error (_id,error,datetime) values " .
                                 "(NULL,'".$error."',NOW());";
                          $result = mysql_query($query);

?>