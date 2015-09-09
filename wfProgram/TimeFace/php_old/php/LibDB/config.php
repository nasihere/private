<?php
error_reporting(0);
    $p = $_REQUEST;     
	$p = json_decode(file_get_contents("php://input"));
	
    if ($_SERVER['SERVER_NAME'] == "localhost"){
		$link = mysql_connect('localhost', 'root', ''); 
    	
    }
	else{
	    	$link = mysql_connect('naszus.ipagemysql.com', 'dev', '123456'); 
	}
    mysql_select_db(elogger); 
    if (!$link) { 
        die('Could not connect: ' . mysql_error()); 
    }
	
?>
