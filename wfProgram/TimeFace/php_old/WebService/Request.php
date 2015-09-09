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
			echo	DoAccountEmployee($p);
			    break;
				
			
				case "GetEmployeeName":
					 echo DoEmployeeName($p);
					 break;
		case "Reports":
			 echo DoReports($p);
			 break;	
	    default:
			echo "break point is missing Request Fail";
	}
	
?>