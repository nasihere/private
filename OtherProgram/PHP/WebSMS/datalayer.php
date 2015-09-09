<?php
	
	
function sendSms($ses_id,$emo_id,$emp_name,$mbl_no,$msg_text)
{
	echo	$url = "http://49.50.69.90/api/smsapi.aspx?username=XXXX&password=XXXX&message=".$msg_text."&from=DEMOUT&to=".$mbl_no;
	
				$ch=curl_init();//intializing
				curl_setopt($ch,CURLOPT_URL,$url);
				curl_setopt($ch, CURLOPT_RETURNTRANSFER, true); 
				$message_id = curl_exec($ch);
				print_r($message_id);
				$message_array =explode("\n",$message_id);
				curl_close($ch);
				echo $message_id = $message_array[0];
				if($message_id != '') smsReport($ses_id,$emo_id,$emp_name,$mbl_no,$message_id,$msg_text);
}
function smsReport($ses_id,$emo_id,$emp_name,$mbl_no,$msg_id,$msg_text)
{
			echo	$url = 'http://49.50.69.90/api/smsstatus.aspx?username=XXXX&password=XXXX&messageid='.$msg_id;
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
									print "505"; // error in creating new member
							}
							else
							{		
									echo "1001"; //Congrats !! successfull . add to shareed preference				} 
							}
				}

}


?>