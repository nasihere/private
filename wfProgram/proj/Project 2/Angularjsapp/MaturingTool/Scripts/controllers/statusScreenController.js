MaturingTools.controller("statusScreenController", ['$scope', '$timeout','$http', '$location', 'heDataService', 'apiUrl', '$filter', function ($scope, $timeout, $http, $location, heDataService, apiUrl, $filter) {

    init();
	$scope.loadingflag = false;
    $scope.showLoader = true;
	$scope._selectedValue = "Pending Review";
	$scope.selectedValue = "Pending Review";
	
	
	$scope.loading = function()
	{
			$scope.showLoader = true;
			$timeout(function(){
				$scope.selectedValue = $scope._selectedValue; 
            }, 500).then(function (result) {
			    $scope.showLoader = false;
				
            });
			
		
	}
	

    function init() {
        $scope.showLoader = true;
        $scope.sortField = 'screenDisplay';
        heDataService.getDataList(apiUrl + '/MaturingToolsDataList', function (data) {
            $scope.statusItems = data;
            $scope.showLoader = false;
            $scope.filterItems = [
				{screenDisplay:'Saved'},
				{screenDisplay:'Pending Review'},
				{screenDisplay:'Pending QC'},
				{screenDisplay:'Cancelled'},
				{screenDisplay:'Waiting for delivery'},
				{screenDisplay:'Delivery failed'},
				{screenDisplay:'Delivered'},
				{screenDisplay:'Confirmed'}
				
			];
            $scope.groupByItems = function () {
                angular.forEach(data, function (item) {
                    //if (!$scope.containsObject(item)) {
                        //$scope.filterItems.push(item);
                    //}
                });
            };
            //$scope.groupByItems();
        }, function (error) {
            $scope.error = error;
            $scope.showLoader = false;
        });
    }

    $scope.newAccount = function () {
        if (angular.isDefined(angular.element(userId).val()) && angular.element(userId).val() != null && angular.element(userId).val() != "") {
            $location.path('/NewAccount').search({ actionType: "New", accountNum: "", moGuid: "", userId: angular.element(userId).val(), userType: angular.element(userType).val(), mloId: angular.element(mloId).val(), agentName: angular.element(agentName).val() });
        }
        else {
            alert("Unauthorized, Please log back in");
        }
    }

    $scope.detailAccount = function (accountNum, moGuid) {
        if (angular.isDefined(angular.element(userId).val()) && angular.element(userId).val() != null && angular.element(userId).val() != "") {
            $location.path('/AccountDetails').search({ actionType: "Detail", accountNum: accountNum, moGuid: moGuid, userId: angular.element(userId).val(), userType: angular.element(userType).val(), mloId: angular.element(mloId).val(), agentName: angular.element(agentName).val() });
        }
        else {
            alert("Unauthorized, Please log back in");
        }
    }

    $scope.order = function (predicate, reverse) {
        $scope.statusItems = $filter('orderBy')($scope.statusItems, predicate, reverse);
    };

    $scope.containsObject = function (obj) {
	//return false;
        var i;

        if ($scope.filterItems.length == 0) {
            return false;
        }

        for (i = 0; i < $scope.statusItems.length; i++) {

            if (!angular.isDefined($scope.filterItems[i])) {
                return false;
            }

            if (angular.equals($scope.filterItems[i].screenDisplay, obj.screenDisplay)) {
                return true;
            }
        }

        return false;
    };
}]);



