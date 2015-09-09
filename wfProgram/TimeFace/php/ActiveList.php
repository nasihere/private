<?php
	if ($_REQUEST['code'] != "7861") 
	{
		echo "Security reason you cannot entered to this page";
		return;
	}
    $p = $_REQUEST;     
	if ($ran == 0)
         $link = mysql_connect('naszus.ipagemysql.com', 'nasz', 'Nasir@1234'); 
    else
         $link = mysql_connect('naszus.ipagemysql.com', 'nasz'.$ran, 'Nasir@1234'); 
    mysql_select_db(elogger); 
    if (!$link) { 
        die('Could not connect: ' . mysql_error()); 

    }
	function getImageURL($str)
	{
		$str = str_replace(":","-",$str);
		$str = str_replace(" ","",$str);
		$str = "http://kent.nasz.us/elog_php/img/" . $str;
		$str = $str . ".jpg";
		return $str;
	}
		//b97f11aa293b6f2d9d9b18c52cb0a5f7
		 $active_id = "";
		 $data = ""; 
		 $i = 0;
		 $qry = "SELECT distinct session_id  FROM `elog`  ORDER BY _id desc limit 200";
		 $res = mysql_query($qry);
		 $num_rows = mysql_num_rows($res);
		 if ($num_rows)
		 {
			while($row2 = mysql_fetch_array($res))
			 {
			 	//$active_id .= "'". $row['session_id']  ."',";
			    
				$qry3 = "SELECT devicetime, employee_id,session_id FROM `elog` where session_id = '".$row2['session_id']."'  ORDER BY _id desc ";
				$res3 = mysql_query($qry3);
				$row = mysql_fetch_array($res3);
				
			 	//$active_session_id .= "'".$row['session_id']  ."',";
			    $data1[$row['session_id']]['imageurl'] = getImageURL($row['devicetime']."N-A-N".$row['employee_id']."N-A-N".$row['session_id']); 
				
				
				 $qry1 = "Select * from eregister where session_id = '".$row['session_id']."' ";
				 $res1 = mysql_query($qry1);
				 $row1 = mysql_fetch_array($res1);
				 $data[$i] = $row1;
				 $i++;
				 
			 }
		 }
		 //$active_id .= "'0'";
		 
		
		
//		 $active_session_id = "";
		  
		 		
			
//		 $active_session_id .= "'0'";
		
		
		
				
			
	 
	 
	
?>
<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
    <title>TimeFace</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
     <!-- Required -->
    <link href="css/global-style.css" rel="stylesheet" type="text/css" media="screen">
    <link rel="icon" href="images/favicon.png" type="image/png">
</head>
<body>

<div class="wrapper">


<div class="top-header">
	<div class="container">
        <div class="row">
            <div class="col-sm-12">
            	<span class="aux-text hidden-xs">
                    Welcome to TimeFace: nasir.sayed.us@gmail.com or (US: +1 323-300-4756) or (INDIA: +91 9869111483)
                </span>
            	<nav class="top-header-menu">
                    <ul class="menu">
							
                        
                        
                    </ul>
				</nav>
            </div>
        </div>
    </div>
</div>
<header>    
	<div id="navOne" class="navbar navbar-wp" role="navigation">
        <div class="container">
            <div class="navbar-header">
            	<button type="button" class="navbar-toggle navbar-toggle-aside-menu">
                    <i class="fa fa-outdent icon-custom"></i>
                </button>
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html" title="Boomerang | One template. Infinite solutions">
                	<img src="images/boomerang-logo-dark.png" alt="Boomerang | One template. Infinite solutions">
                </a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                	<li>
                    	<a href="http://timeface.nasz.us" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">Home</a>
                        
                	</li>
                    <li class="active">
                    						<a href="Admin.php" class="dropdown-toggle">Login</a>
                        
                	</li>

                </ul>
               
            </div><!--/.nav-collapse -->
        </div>
    </div>
</header>	    
    <section class="slice bg-3 animate-hover-slide">
        <div class="w-section inverse blog-grid">
            <div class="container">
                <div class="row">
                    
                    <div class="col-md-9">
                    	<!-- List styles -->
                    	<div class="wp-example" id="list-styles">
                            
                            <hr>
                            <div class="row">
                            	
                            	<div class="col-md-6">
                                	<h3 class="section-title">Organization List</h3>
									<a href="signup.php">Add Organization</a>
                                	<ul class="popular">
										<?php 
										foreach($data as $item){ ?>
                                        <li>
											<img src="<?php  echo $data1[$item['session_id']]['imageurl']; ?>" class="img-thumbnail pull-left" />
                                            <p>
												
                                                <a href="http://timeface.nasz.us/MemberList.php?session_id=<?php echo $item['session_id']; ?>"><?php echo $item['first'];?></a>
                                                <i>Name: <?php echo $item['last'] ?></i>
												<i>Email: <?php echo $item['emailid'] ?></i>
												<i>Password: <?php echo $item['password'] ?></i>
												<i>Member: <?php echo $item['timestamp'] ?></i>
												<i>IP: <?php echo $item['ip'] ?></i>
												<a href="http://timeface.nasz.us/DeleteAccount.php?session_id=<?php echo $item['session_id']; ?>">Delete</a>
                                            </p>
                                            
                                        </li>
										<?php } ?>
                                        
                                    </ul>
									
                                </div>
                            </div>
                        </div>
                                            	
                        
                    </div>
                </div>
            </div>
        </div>
    </section>
    
</div>

</html>
