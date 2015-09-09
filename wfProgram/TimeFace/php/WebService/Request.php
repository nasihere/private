<?php
	include ("../LibDB/dbhelper.php");
	include ("datalayer.php");

	switch ($p->type) {
	    case "Login":
			echo DoLogin($p->email, $p->password);
		    break;
	    case "CreateEmployee":
			DoCreateEmployee($p);
		    break;
		case "AccountEmployee":
			echo DoAccountEmployee($p);
			    break;
				case "GetEmployeeName":
					 echo DoEmployeeName($p);
					 break;
		case "Reports":
			echo DoReports($p);
			 break;	
		case "Notification":
			echo DoUpdateNotification($p);
			 break;	
		 
		case "SMSTest":
		
			 echo sendSms('f0f633ec-63d2-444a-974c-999e50269d7d',1003,'uttam',9727486616,"My%20Mac%20Machine");
			 break;
	    default:
	
	 	echo "break point is missing or reached... Request Fail";
	}
	
?>