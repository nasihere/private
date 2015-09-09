wmg.controller("KYCController", ['$scope', 'goAheadService', function ($scope, goAheadService) {

    $scope.kyc = Root.applicationInfo.WealthData.KYC;
    
    $scope.showOfflineForm = function() {
        $('#info').click();
    };

    $scope.showOfflineForm = function() {
        if ($scope.kyc.completeOnline == "N") {
            $("#offlineForm").show();
            var widthContainer = 650;
            var bodyContainer = $('body').width();

            $("#offlineForm .modalcontainer").css({ width: widthContainer + "px" });
            var leftSpace = (bodyContainer - widthContainer) / 2;
            $("#offlineForm .modalcontainer").css({ left: leftSpace + "px" });

            setTimeout(function() {
                svpApp.util.buildSelects();
            }, 500);
        } else {
            $('#offlineForm').hide();
        }
    };
   

    $scope.offlineFormClose = function () {
        $('#offlineForm').hide();
    };

    $scope.$on("Kyc", $scope.validateBeforeContinue);
    
    $scope.$on("Kyc", $scope.validateBeforeContinue = function (event, data) {
        goAheadService.setFlag(true);
    });

}]);
