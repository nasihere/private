var login_det = null;
login_det = {};
login_det.session_id = "04317bb9dd679c65503fbe9399443b26";
app.controller("LoginCtrl",function($scope, $http, $state){
	$scope.Loading = false;
	
	$scope.LoginBtn = function(user){

		$scope.Loading = true;
		var url = "php/WebService/Request.php";
		$http({
		      method: "POST",
		      url: url,
		      data: user,
		      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
		  }).
		    success(function(response) {
		    	login_det = response;
				
		    	$state.go("Dashboard");
			}).
		    error(function(response) {
		    
			});
			
		
	}
});



/*				Create Employee ***/

app.controller("EmployeeCtrl",function($scope, $http, $state){
	$scope.Loading = false;
	
	$scope.BtnSubmit = function(user){

		if(login_det == null){
			$state.go("Login");
			return;
		}
		$scope.Loading = true;
		
		user.session_id = login_det.session_id;
		var url = "php/WebService/Request.php";
		$http({
		      method: "POST",
		      url: url,
		      data: user,
		      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
		  }).
		    success(function(response) {
		    	$state.go("AccountEmployee");
			}).
		    error(function(response) {
		    
			});
			
		
	}
});



/*				Account Employee ***/

app.controller("AccountEmpCtrl",function($scope, $http, $state){
	$scope.Loading = false;
	if(login_det == null){
		$state.go("Login");
		return;
	}
	$scope.search = "";
	
	$scope.Report = function(item){
		login_det.keycode = item.employee_code;
		$state.go("Reports");
	}
	
	$scope.getLastLoginTimeStamp = function(data, col){
		for (i = 0; i <= data.length - 1; i++){
			//console.log(data[i].devicetime + " == " + data[i].employee_id + " == " + col);
			
			if ( undefined  != data[i].employee_id && data[i].employee_id == col){
				return data[i].devicetime;
				break;
			}
		}
		return "";
	}
	
	
	$scope.getLastLoginAction = function(data, col){
		
		for (i = 0; i <= data.length - 1; i++){
			
			if (undefined  != data[i].employee_id && data[i].employee_id == col){
				if (data[i].action == 1)
					return true;
				else 
					return false;
				break;
			}
		}
		return null;
	}
	var user  = {};
	 user.type = "AccountEmployee";
	user.session_id = login_det.session_id;
	var url = "php/WebService/Request.php";
	$http({
	      method: "POST",
	      url: url,
	      data: user,
	      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
	  }).
	    success(function(response) {
			$scope.data = response;
		}).
	    error(function(response) {
	    
		});
		
		
	$scope.BtnSubmit = function(user){

		if(login_det == null){
			$state.go("Login");
			return;
		}
		
			
		
	}
});

/*				Call Center ***/

app.controller("CallCenterCtrl",function($scope, $http, $state){
	
	
	$scope.DoRet = function(id, val, itemobj){
		var user  = {};
		 user.type = "UpdateCallCenter";
		 user.id = id;
		 user.retval = val;
		user.session_id = login_det.session_id;
		$http({
		      method: "POST",
		      url: url,
		      data: user,
		      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
		  }).
		    success(function(response) {
				//$scope.data = response;
				//location.reload();
				if (itemobj.returned == 0){
					itemobj.returned = 1;
				}
				else{
					itemobj.returned = 0;
				}
			}).
		    error(function(response) {
	    
			});
	}
	$scope.user = {};
	$scope.user.val_ashish = "";
	$scope.user.val_hamza = "";
	$scope.user.val_nasir = "";	
	$scope.user.val_imran = "";	
	$scope.SaveComment = function(id, col, val,itemobj){

		var user  = {};
		 user.type = "UpdateCallCenter_1";
		 user.id = id;
		 user.col = col;
		 user.val = val;
		user.session_id = login_det.session_id;
		$http({
		      method: "POST",
		      url: url,
		      data: user,
		      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
		  }).
		    success(function(response) {
				//$scope.data = response;
			//	location.reload();
				itemobj = itemobj  + val;
				alert("Comment Saved! After refresh list will update");
			}).
		    error(function(response) {
	    
			});
	}
	
	
	var url = "php/WebService/Request.php";
	var user  = {};
	 user.type = "CallCenter";
	user.session_id = login_det.session_id;
	
	$http({
	      method: "POST",
	      url: url,
	      data: user,
	      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
	  }).
	    success(function(response) {
			$scope.data = response;
			
		}).
	    error(function(response) {
	    
		});
		
		
		$scope.SumTotal = 0;
		$scope.SumTotal = function(data){
			var sumtt = 0;
			
			for(i=0;i<=data.length -1 ;i++){
				var value = data[i].name;
				value = value.replace("$","");
			
				sumtt += parseInt(value);
			}
			return sumtt;
		}
		
	
	
});



/*				Call Center ***/

app.controller("ReportCtrl",function($scope, $http, $state){
	
	
	$scope.user = {};
	var url = "php/WebService/Request.php";
	var user  = {};
	user.type = "GetEmployeeName";
	user.session_id = login_det.session_id;
	user.keycode = login_det.keycode;
	$http({
	      method: "POST",
	      url: url,
	      data: user,
	      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
	  }).
	    success(function(response) {
			$scope.data = response;
		}).
	    error(function(response) {		});
		
		$scope.RunReport = function(report){
			var user  = {};
			 user.type = "Reports";
			 user.report = report;
			user.session_id = login_det.session_id;
			//user.keycode = '1003';
		
			$http({
			      method: "POST",
			      url: url,
			      data: user,
			      headers: {'Content-Type': 'application/x-www-form-urlencoded'}
			  }).
			    success(function(response) {
					$scope.dataReport =response;
				}).
			    error(function(response) {
	    
				});
		}	
		
	    $(function() {
	      $( "#fromdatepicker,#todatepicker" ).datepicker();
	    });	
	

	
});


function printDiv(divName) {
     var printContents = document.getElementById(divName).innerHTML;
     var originalContents = document.body.innerHTML;

     document.body.innerHTML = printContents;

     window.print();

     document.body.innerHTML = originalContents;
}


var openDia = function(){
	console.log("nasir");
	//document.getElementById(id+index).open = true;
}


