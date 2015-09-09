<?php
   $ip = $_SERVER["REMOTE_ADDR"];
	 $Emailid = $p->email;
	 $password = $p->password;
     $code  = $p->PIN;
	 $employee_name = $p->employeename;
	 $mobileNumber = $p->mobileNumber;
	 $notificationSms = $p->notificationSms;
	 $notificationEmail = $p->notificationEmail;
	$session_id = $p->session_id;
	function sendSms($ses_id,$emo_id,$emp_name,$mbl_no,$msg_text)
	{
					$url = 'http://49.50.69.90/api/smsapi.aspx?username=nasir&password=nasir123&to='.$mbl_no.'&from=DEMOUT&message='.$msg_text;
					$ch=curl_init();//intializing
					curl_setopt($ch,CURLOPT_URL,$url);
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true); 
					$message_id = curl_exec($ch);
					$message_array =explode("\n",$message_id);
					curl_close($ch);
					$message_id = $message_array[0];
					if($message_id != '') smsReport($ses_id,$emo_id,$emp_name,$mbl_no,$message_id,$msg_text);
	}
	function smsReport($ses_id,$emo_id,$emp_name,$mbl_no,$msg_id,$msg_text)
	{
					$url = 'http://49.50.69.90/api/smsstatus.aspx?username=nasir&password=nasir123&messageid='.$msg_id;
					$ch=curl_init();//intializing
					curl_setopt($ch,CURLOPT_URL,$url);
					curl_setopt($ch, CURLOPT_RETURNTRANSFER, true); 
					$status = curl_exec($ch);
					$status_array = explode("\n",$status);
					curl_close($ch);
					$status = preg_replace('<br />', '',$status_array[0]); 
					if($status != '')
					{
						$InsertSmsLogQuery = "INSERT INTO  esmslog (  
									_id,
									employee_id, 
									employee_name,
									session_id, 
									message_id,
									status,
									message_text) VALUES (
										   NULL,
										   '".$emo_id."',
										  '".$emp_name."',
										  '".$ses_id."',
										  '".$msg_id."',  
										  '".$status."',  
										  '".$msg_text."'
									);";
								$result =mysql_query($InsertSmsLogQuery);
								echo $InsertSmsLogQuery;
								if (!$result) 
								{
										//print "505"; // error in creating new member
								}
								else
								{		
										//echo "1001"; //Congrats !! successfull . add to shareed preference				} 
								}
					}
	
	}
	 if ($employee_name == "") $employee_name = "Unknown";
			/*if (CheckCode($session_id,$code) == "404")
			{
				print "405";
				
			}
			else
			{*/
				$InsertLogQuery = "INSERT INTO  eemployee (  
					_id,
					session_id,
					employee_name, 
					employee_email,
					mobile_number,
					employee_id, 
					employee_perhour, 
					employee_code, 
					timestamp,
					ip,
					pannedin,
					plannedout,
					notification_sms,
					notification_email) VALUES (
						   NULL,
						  '".$session_id."',  
						  '".$employee_name."',  
						  '".$p->email."',
						  '".$mobileNumber."',
						  '".$p->employeeid."',  
						  '".$p->hourly."',  
						  '".$code."',  
						  NOW(),  
						  '".$ip."',
						  '".$p->PlannedIn."',
						  '".$p->PlannedOut."',
						  '".$notificationSms."',
						  '".$notificationEmail."'
					);";

				$result =mysql_query($InsertLogQuery);
				if (!$result) {
					print "505"; // error in creating new member
				}
				else
				{
					if($notificationSms == 1)
					{
						sendSms($session_id,$p->employeeid,$employee_name, $mobileNumber,"hello"); //pass parameter like sendSms($mobileNumber,messgetext)
					}
					echo "1001"; //Congrats !! successfull . add to shareed preference
					echo $name . "~" . $code . "~" . "0" . "@#@#@#";
				} 
			//}
    
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