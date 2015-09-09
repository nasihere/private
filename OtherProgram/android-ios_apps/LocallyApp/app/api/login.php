<?php
    session_start(); 
    $ses_id = session_id();
    $link = mysql_connect('naszus.ipagemysql.com', 'nasz', 'Nasir@1234'); 
    mysql_select_db(shifakent); 
    if (!$link) { 
        die('Could not connect: ' . mysql_error()); 
    } 
  
 if ($_REQUEST['lid'])
 {
    $s = "select sessionid from tbl_user where sessionid = '".$_REQUEST['lid']."'";
    $result = mysql_query($s);
    $row = mysql_fetch_row($result); 
    print $row[0];
    $s = "UPDATE tbl_user SET login = login + 1   WHERE sessionid = '".$_REQUEST['lid']."'";
    $result = mysql_query($s);
 }
      
        else
        {
            $q = "INSERT INTO tbl_user (sessionid,datetime,login) values ('".$ses_id ."',NOW(),1)";
            mysql_query($q);
            print $ses_id;
        }
    
   
?>