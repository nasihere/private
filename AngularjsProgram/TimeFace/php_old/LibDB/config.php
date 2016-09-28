
<?php
	date_default_timezone_set('Africa/Lagos');
    $p = $_REQUEST;     
	$p = json_decode(file_get_contents("php://input"));
	
    if ($_SERVER['SERVER_NAME'] == "tf.local"){
		$link = mysql_connect('localhost', 'root', '123'); 
		
    	
    }
	else{
	    	$link = mysql_connect('naszus.xxxxx.com', 'dev', '123456'); 
	}
	
    mysql_select_db(elogger); 
    if (!$link) { 
        die('Could not connect: ' . mysql_error()); 
    }
	
?>
