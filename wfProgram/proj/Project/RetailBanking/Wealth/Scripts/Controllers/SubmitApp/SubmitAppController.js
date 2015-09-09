
wmg.controller("SubmitAppController", ['$scope', 'APIService',
    function ($scope, APIService) {
        var appSubmitted = "Y";
        var appSaved = "N";

        //Note: The status will be set only when the user has PB34Z product and not meeting the AUM Logic
        if ($scope._routedFromCDScreen.status == true) {
            appSubmitted = "N";
            appSaved = "N";
        }
        APIService.SubmitData(function (data) {
            SubmitDataResponse = data.Errors;
          
        });
    }]);