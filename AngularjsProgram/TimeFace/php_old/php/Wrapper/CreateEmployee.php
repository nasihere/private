<?php
   $ip = $_SERVER["REMOTE_ADDR"];
  	
	 
	 $Emailid = $p->email;
	 $password = $p->password;
     $code  = $p->PIN;
	 $name = $p->employeename;
	 if ($name == "") $name = "Unknown";
    
			$session_id = $p->session_id;
			if (CheckCode($session_id,$code) == "404")
			{
				print "405";
				
			}
			else
			{
				$InsertLogQuery = "INSERT INTO  eemployee (  
					_id,
					session_id,
					employee_name, 
					employee_email, 
					employee_id, 
					employee_perhour, 
					employee_code, 
					timestamp,
					ip,
					pannedin,
					plannedout) VALUES (
						   NULL,
						  '".$session_id."',  
						  '".$name."',  
						  '".$p->email."',  
						  '".$p->employeeid."',  
						  '".$p->hourly."',  
						  '".$code."',  
						  NOW(),  
						  '".$ip."',
						  '".$p->PlannedIn."',
						  '".$p->PlannedOut."'
					);";

			   
			   
			  
				$result =mysql_query($InsertLogQuery);			   
				if (!$result) {
					print "505"; // error in creating new member
				}
				else
				{
					echo "1001"; //Congrats !! successfull . add to shareed preference
					echo $name . "~" . $code . "~" . "0" . "@#@#@#";
				} 
			}
    

	function CheckCode($sessionid,$code)
	{
		$q =  "SELECT employee_code FROM `eemployee` where session_id = '".$sessionid."' and employee_code = '".$code."'";
		$res = mysql_query($q);
		$num_rows = mysql_num_rows($res);
		 if ($num_rows)
		 {
			return "404";
		 }
		 else
		 {
			return "1001";
		 }
	}
	
   
   ?>	