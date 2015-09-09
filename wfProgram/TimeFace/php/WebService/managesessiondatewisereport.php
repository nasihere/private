<?php
if( ! ini_get('date.timezone') )
{
    date_default_timezone_set('GMT');
}
  	$summary['employer'] = getEmployerName($param['session_id']);	
	$rate = getPerHour($param['session_id'],$param['keycode']);
	$member_name = getName($param['session_id'],$param['keycode']);	
	$summary['membername'] = $member_name;
	$summary['rate'] = $rate;
	if ($param['month'] != "") $monthqry = " and devicetime >= '".$param['month_from']."' AND devicetime < '".$param['month_to']."'";
		$q =  "SELECT  _id,date(devicetime) as onlydate,action,devicetime,employee_id,remark
				FROM  `elog` 
				WHERE session_id =  '".$param['session_id']."' and  employee_id =  '".$param['keycode']."'" . $monthqry . "
				order by devicetime asc";

     $res = mysql_query($q);
     $i = 0;
	 $skip = false;
	 $samedate = "";
	 $num_rows = mysql_num_rows($res);
	 if ($num_rows)
     {
		while($row = mysql_fetch_array($res))
		 {
			if ($row['action'] == 0 ) 
			{
				if ($samedate != $row['onlydate'])
				{
					$samedate = $row['onlydate'];
					$starttime = $row['devicetime'];
					$startAt =  date("h:i A", strtotime($row['devicetime']));
					$endAt = "";
				}
				else
				{
					$skip = true;
				}
			}
			else if ($row['action'] == 1)
			{
				$skip = false;
				$samedate= "";
			//	$data1[$i - 1] = null;
				$endTime = $row['devicetime'];
				$endAt =  date("h:i A", strtotime($row['devicetime']));
				
			}
			if ($skip == false)
			{
				
				$data1[$i]['timeago'] = $row['onlydate']; 
				$data1[$i]['startAt'] = $startAt;
				$data1[$i]['_id'] = $row['_id'];
				$data1[$i]['remark'] = $row['remark'];
				$data1[$i]['endAt'] = $endAt;
				//$data1[$i]['time'] = date("h:i A", strtotime($row['devicetime']));; //time_ago1($row['devicetime']); 
				$data1[$i]['code'] = $row['employee_id']; 
				$data1[$i]['action'] = $row['action']; 
				$data1[$i]['name'] = $member_name;
				$data1[$i]['imageurl'] = getImageURL($row['devicetime']."N-A-N".$row['employee_id']."N-A-N".$param['session_id']); 
				if ($endAt != "")
				{
					$data1[$i]['workinghour'] =  (strtotime($endTime) - strtotime($starttime)) / 3600;
					$data1[$i]['workinghour'] = number_format((float)$data1[$i]['workinghour'], 2, '.', '');  // Outputs -> 105.00
					$summary['sumworkinghour'] = $summary['sumworkinghour'] + $data1[$i]['workinghour'];
					$data1[$i]['rate'] = $summary['rate'] * $data1[$i]['workinghour'];
					$summary['totalrate'] = $summary['totalrate'] + $data1[$i]['rate'];
				}
				
				if ($startAt == 0 || $startAt == "") 
				{
					$data1[$i]['startAt'] = '<a href="http://timeface.nasz.us/addeditime.php?code='.$row['employee_id'].'&session_id='.$param['session_id'].'&date='.$row['onlydate'].'&inout=0">Add Time</a>';
				}
				if ($endAt == 0 || $endAt == "") 
				{
					$data1[$i]['endAt'] = '<a href="http://timeface.nasz.us/addeditime.php?code='.$row['employee_id'].'&session_id='.$param['session_id'].'&date='.$row['onlydate'].'&inout=1">Add Time</a>';
				}
				//echo $data[$i]['timeago'] . "@#@" . $data[$i]['time'] . "@#@" . $data[$i]['code'] . "@#@" . $data[$i]['action'] . "@#@" . $data[$i]['name'] . "@#@" . $data[$i]['imageurl'] . "-#@#@#@-";
				$i++;
			}
		 }
	 }
	 
	return $data1;
	
		
	function getPerHour($sess,$emp)
	{
		$q =  "SELECT employee_perhour FROM `eemployee` where employee_code = '".$emp."' and session_id = '".$sess."' ORDER BY `_id`  DESC";

		$res = mysql_query($q);
		$row = mysql_fetch_array($res);
		return $row[0];
	}
	function getEmployee_UniqueID($sess,$emp)
	{
		$q =  "SELECT _id FROM `eemployee` where employee_code = '".$emp."' and session_id = '".$sess."' ORDER BY `_id`  DESC";
		$res = mysql_query($q);
		$row = mysql_fetch_array($res);
		return $row[0];
	}
	function getEmployerName($sess)
	{
		$q =  "SELECT first FROM `eregister` where session_id = '".$sess."'";

		$res = mysql_query($q);
		$row = mysql_fetch_array($res);
		return $row[0];
	}
	function getName($sess,$emp)
	{
		$q =  "SELECT employee_name FROM `eemployee` where employee_code = '".$emp."' and session_id = '".$sess."' ORDER BY `_id`  DESC";

		$res = mysql_query($q);
		$row = mysql_fetch_array($res);
		return $row[0];
	}
	function getImageURL($str)
	{
		$str = str_replace(":","-",$str);
		$str = str_replace(" ","",$str);
		$str = "http://kent.nasz.us/elog_php/img/" . $str;
		$str = $str . ".jpg";
		return $str;
	}
	
?>
<?php



    /********************************** Time to go Conversion **********************/ 
     function time_ago1($date)
    {
        if(empty($date)) {
            return "No date provided";
        }
        $periods = array("sec", "min", "hr", "day", "week", "month", "year", "decade");
        $lengths = array("60","60","24","7","4.35","12","10");
        $now = time();
        $unix_date = strtotime($date);
        // check validity of date
        if(empty($unix_date)) {
         return "Bad date";
         }
         // is it future date or past date
         if($now > $unix_date) {
         $difference = $now - $unix_date;
         $tense = "ago";
         } else {
         $difference = $unix_date - $now;
        $tense = "from now";}
         for($j = 0; $difference >= $lengths[$j] && $j < count($lengths)-1; $j++) {
        $difference /= $lengths[$j];
        }
        $difference = round($difference);
        if($difference != 1) {
        $periods[$j].= "s";
        }
        return "$difference $periods[$j] {$tense}";
    }
function GetDays($sStartDate, $sEndDate){
  // Firstly, format the provided dates.
  // This function works best with YYYY-MM-DD
  // but other date formats will work thanks
  // to strtotime().
  $sStartDate = gmdate("Y-m-d", strtotime($sStartDate));
  $sEndDate = gmdate("Y-m-d", strtotime($sEndDate));

  // Start the variable off with the start date
  $aDays[] = $sStartDate;

  // Set a 'temp' variable, sCurrentDate, with
  // the start date - before beginning the loop
  $sCurrentDate = $sStartDate;

  // While the current date is less than the end date
  while($sCurrentDate < $sEndDate){
    // Add a day to the current date
    $sCurrentDate = gmdate("Y-m-d", strtotime("+1 day", strtotime($sCurrentDate)));

    // Add this new day to the aDays array
    $aDays[] = $sCurrentDate;
  }

  // Once the loop has finished, return the
  // array of days.
  return $aDays;
}
	
	//print_r($data1);
	//print_r($summary);
	
   ?>
   
   
   
   
   
   
   
   
   
   
   <?/*
   
   
   <!DOCTYPE html>
<html>
<head>
<!-- CSS goes in the document HEAD or added to your external stylesheet -->
<style type="text/css">
table.gridtable {
	font-family: verdana,arial,sans-serif;
	font-size:11px;
	color:#333333;
	border-width: 1px;
	border-color: #666666;
	border-collapse: collapse;
}
table.gridtable th {
	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #666666;
	
	background-color: #dedede;
}
table.gridtable td {
	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #666666;
	background-color: #ffffff;
}
</style>

</head>
<body>
	<?php include("../../timeface/header.php"); ?>
	<a href="http://timeface.nasz.us/MemberList.php?session_id=<?php echo $param['session_id']; ?>">Show All Members </a>
	<form action="" method="get">
	  Monthwise Log (month and year): <input type="month" name="month">
	  <input type="hidden" name="keycode" value="<?php echo $_REQUEST['keycode']; ?>" />
	  <input type="hidden" name="session_id" value="<?php echo $_REQUEST['session_id']; ?>" />
	  <input type="submit">
	</form>

	<h3><?php echo ucwords($summary['employer']);?> </h3>
	<h4>Employee Name: <?php echo ucwords($summary['membername']);?> </h4>
	<?php
	$_idUnique = getEmployee_UniqueID($param['session_id'],$param['keycode']);	
	echo "<b><span>Per Hour Rate: </span></b> ".$rate." <a href='http://timeface.nasz.us/addedirate.php?_id=".$_idUnique."&rate=".$rate."'> Edit Rate</a>";

	
	?>
	<br>
	<a href="Export-To-Xls.php?keycode=<?php echo $param['keycode'];?>&session_id=<?php echo $param['session_id'];?>">Export to Excel</a>
	
	<table class="gridtable">
		<tr>
			<th>Day</th>
			<th>Date</th>
			<th>In</th>
			<th>Out</th>
			<th>No. Hour</th>
			<th>Calculate
				<?php if ($rate == 0 || $rate == "")
				{
					echo "<br><a href='http://timeface.nasz.us/addedirate.php?_id=".$_idUnique."&rate=".$rate."'>Add Rate</a>";
				} ?>
			</th>
			<th>TimeFace</th>
			<th>Remark</th>
		</tr>
		<?php foreach($data1 as $key => $val) { if ($val == null) continue; ?>
		<tr>
			<td><?php echo date_format(date_create($val['timeago']), 'l'); ?></td>
			<td><?php echo $val['timeago'];?></td>
			<td><?php echo $val['startAt'];?></td>
			<td><?php echo $val['endAt'];?></td>
			<td><?php echo $val['workinghour'];?></td>
			<td><?php echo $val['rate'];?></td>
			<td><img src="<?php echo $val['imageurl'];?>" height = "50px" width="50px" /></td>
			<td>
			<?php 
				if ($val['remark'] == "")
					echo "<a href='http://timeface.nasz.us/addeditremark.php?id=".$val['_id']."&remark=".$val['remark']."'>Add Remark</a>";
				else
				{
					echo $val['remark'] ;
					echo "<br>";
					echo "<a href='http://timeface.nasz.us/addeditremark.php?id=".$val['_id']."&remark=".$val['remark']."'>Edit Remark</a>";
				}
			?>
			</td>
		</tr>
		<?php } ?>
		
		<tr>
			<td></td>
			<td></td>
			<td></td>
			<td></td>
			<td><?php echo $summary['sumworkinghour'];?></td>
			<td><?php echo $summary['totalrate'];?></td>
			<td></td>
			<td></td>
		</tr>
		
		
	</table>
	
</body>
</html>

*/
?>