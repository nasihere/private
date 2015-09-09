wmg.controller("ProcessAppController", ['$scope', 'APIService',
    function ($scope, APIService) {

       // alert(fotterButtons);
      
        $scope.process = true;
      
           var appSubmitted = "Y";
            var appSaved = "N";

            //Note: The status will be set only when the user has PB34Z product and not meeting the AUM Logic
            if ($scope._routedFromCDScreen.status == true) {
                appSubmitted = "N";
                appSaved = "N";
            }
       
        /*
        APIService.SubmitData(function (data) {
                if (data.Errors == null) {
                    var wealthData = { "wealthData": { "isAppSubmitted": appSubmitted, "isAppSaved": appSaved } };
                    try {
                        document.domain = "wellsfargo.com";
                        parent.ContinueOnWealth(sessionIdData, wealthData);
                    } catch (e) {
                        alert("Error in redirecting to SCS screen " + e);
                    }
                }
            });
           */
        if (SubmitDataResponse == null) {
            var wealthData = { "wealthData": { "isAppSubmitted": appSubmitted, "isAppSaved": appSaved } };
            try {
                document.domain = "wellsfargo.com";
                parent.ContinueOnWealth(sessionIdData, wealthData);
            } catch(e) {
                alert("Error in redirecting to SCS screen " + e);
                $scope.process = false;
            }
        } else {
            alert("Error in Submit Application Response " + SubmitDataResponse);
            $scope.process = false;
        }
    }]);