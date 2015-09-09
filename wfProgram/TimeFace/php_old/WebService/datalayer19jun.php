<?php
	
function DoLogin($username, $password){
	$qry = create_query("eregister","emailid = '$username' and password = '$password'");
	return json_encode(get_col_array($qry));
	
}
	
function DoCreateEmployee($p){
	include_once ("../Wrapper/CreateEmployee.php");
}


function DoAccountEmployee($p){
	$qry = create_query("eemployee","session_id = '".$p->session_id ."'");
	$data = get_col_array($qry);
	$employee_code = "";
	for($i=0; $i <= sizeOf($data) -1; $i++){
		$employee_code .= $data[$i]["employee_code"] . ",";
	}
	$data["elog"] = DoAccountLog($employee_code, $p->session_id);
	
	return json_encode($data);
	
}

function DoAccountLog($PIN, $session_id){
	 $qry = create_query("elog","session_id = '".$session_id ."' and employee_id in (".$PIN." 0)  group by employee_id");
	return get_col_array($qry);
	
	 
}



function DoEmployeeName($p){
	$qry = create_query_col("eemployee","session_id = '".$p->session_id ."'","employee_name");
	$data = get_col_array($qry);
	
	return json_encode($data);
	
}
function DoProcessReportManage($p){
    $param['session_id'] = $p->session_id;
	$param['keycode'] = 1019;
	$param['month']=  $p->report->datefrom;
	$param['month_from'] = $p->report->datefrom;
	$param['month_to'] = $p->report->dateto;
	$data = include_once ("managesessiondatewisereport.php");
	return json_encode($data);
	
}

function DoProcessReport($p){
	$qry = "SELECT * FROM `elog` WHERE `session_id` = '".$p->session_id ."' GROUP by `employee_id` order by _id desc";
	$data = get_col_array($qry);
	return $data;
}
function DoReports($p){
	//return DoProcessReportManage($p);
	$qry = "SELECT * FROM `elog` WHERE `session_id` = '".$p->session_id ."' GROUP by `employee_id` order by _id desc";
	$data[0] = get_col_array($qry);
	$concatId = "";
	foreach($data[0] as $val){
		 $concatId .= $val['employee_id'] . ",";
	}
	$empID = rtrim($concatId,",");
	
	$qry = "SELECT * FROM `eemployee` where session_id = '".$p->session_id ."'";
	$data[1] = get_col_array($qry);
	$data[2] = "";
	$i == 0;
	foreach($data[1] as $employee){
		foreach($data[0] as $elog){
			if ($elog['employee_id'] == $employee['employee_code']){

				$data[2][$i]['report']['elog'] =$elog; 
				$data[2][$i]['report']['employee'] =$employee; 
				$i++;
				break;
			}
			
				
		}
	}
	
	
	if ($p->report->repname = "Attendance Overview"){
		
		return json_encode($data[2]);
			
	}
	else
	{
		$data = DoProcessReport($p);
		return json_encode($data);
	}
		
	
}

?>