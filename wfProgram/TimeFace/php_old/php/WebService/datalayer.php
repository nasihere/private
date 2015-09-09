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
	$qry = create_query_col("eemployee","session_id = '".$p->session_id ."'","employee_code,employee_name");
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

	//Add By uttam

	if($p->report->duration == 'Manual Date')
	{
			$startDate = $p->report->datefrom;
	
			$endDate = $p->report->dateto;
			
	}
	else if($p->report->duration == 'Last Week')
	{	
			$date = strtotime("-7 day", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	else if($p->report->duration == 'Last 2 Week')
	{
			$date = strtotime("-14 day", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	else if($p->report->duration == 'Last Month')
	{
			$date = strtotime("-1 month", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");	
	}
	else if($p->report->duration == 'Last 2 Month')
	{
			$date = strtotime("-2 month", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	else if($p->report->duration == 'Last Quater')
	{
			$date = strtotime("-3 month", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	else if($p->report->duration == 'Last Half Year')
	{
			$date = strtotime("-6 month", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	else if($p->report->duration == 'Last Year')
	{
			$date = strtotime("-1 year", strtotime(date("Y-m-d")));
			$startDate = date("Y-m-d",$date);
			$endDate = date("Y-m-d ");
	}
	
	//Return day and inTime
	$qry = "SELECT employee_id,DAYNAME(devicetime) as day,min(devicetime) as inTime FROM elog WHERE action = 0 AND employee_id = '".$p->report->empname."' AND session_id = '".$p->session_id ."' AND  '".$startDate."' <= date(devicetime) AND  '".$endDate."' >= date(devicetime)  GROUP BY date(devicetime) ";
	$data[0]=get_col_array($qry);
	//Return day and In Time
	$qry = "SELECT employee_id,max(devicetime) as outTime FROM elog WHERE action = 1 AND employee_id = '".$p->report->empname."' AND session_id = '".$p->session_id ."' AND '".$startDate."' <= date(devicetime) AND  '".$endDate."' >= date(devicetime) GROUP BY date(devicetime) ";
	$data[1]=get_col_array($qry);
	$qry="SELECT employee_name FROM eemployee WHERE employee_code='".$p->report->empname."' AND session_id='".$p->session_id ."'";
	$employee_name=get_col_array($qry);
	$qry="SELECT first FROM eregister WHERE session_id='".$p->session_id ."'";
	$company_name=get_col_array($qry);
	$j == 0;
	$j++;
	foreach($data[0] as $v1)
	{
		$date1=date('Y-m-d', strtotime($v1['inTime']));
		foreach($data[1] as $v2)
		{
			$date2=date('Y-m-d', strtotime($v2['outTime']));
			if($date1 == $date2)
			{
				$qry = "SELECT TIMEDIFF(max('".$v2['outTime']."'),min('".$v1['inTime']."')) as noOfhours" ;
				$hours=get_col_array($qry);
				$dataOfDepartSummary[$j]['report']['day']=$v1['day'];
				$dataOfDepartSummary[$j]['report']['date']=$date1;
				//$dataOfDepartSummary[$i]['report']['date2']=$date2;
				$dataOfDepartSummary[$j]['report']['inTime']=date('h:i:s a', strtotime($v1['inTime']));
				$dataOfDepartSummary[$j]['report']['outTime']=date('h:i:s a', strtotime($v2['outTime']));
				$dataOfDepartSummary[$j]['report']['noOfhours']=$hours[0]['noOfhours'];
				$dataOfDepartSummary[$j]['report']['pic'] = getImageURL($v2['outTime']."N-A-N".$v1['employee_id']."N-A-N".$p->session_id);
				$j++;
			}
		}
	}
	$dataOfDepartSummary['report']['employee_name']=$employee_name[0]['employee_name'];
	$dataOfDepartSummary['report']['company_name']=$company_name[0]['first'];
	if ($p->report->repname == "Attendance Overview"){
		return json_encode($data[2]);
	}
	else if($p->report->repname == "Department Summary")
	{
		return json_encode($dataOfDepartSummary);
	}
	else
	{
		$data = DoProcessReport($p);
		return json_encode($data);
	}
	
	
}

?>