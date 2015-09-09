

MaturingTools.controller("maturingController", ['$scope', '$routeParams', 'heDataService', 'apiUrl', '$location', 'constants', '$timeout', function ($scope, $routeParams, heDataService, apiUrl, $location, constants, $timeout) {

    $scope.reNumber = constants.reNumber;
    $scope.reBalanceFormat = constants.reBalanceFormat;
    $scope.reAmountFormat = constants.reAmountFormat;
    $scope.rePaymentAmountFormat = constants.rePaymentAmountFormat;
    $scope.reRateFormat = constants.reRateFormat;

    init();

    $scope.showLoader = true;

    $scope.butonClicked = false;
    $scope.butonSaveClicked = false;
    $scope.butonSubmitForProcessingClicked = false;

    function init() {

        if (angular.isDefined($routeParams["actionType"])) {

            $scope.showLoader = true;

            var data;

            if ($routeParams["actionType"] == "Detail") {
                data = { "accountNum": $routeParams["accountNum"], "moguid": $routeParams["moGuid"], "CreatedBy": $routeParams["userId"], "userType": $routeParams["userType"], "mnlsrid": $routeParams["mloId"], "agentName": $routeParams["agentName"] };
            }
            else {
                data = { "CreatedBy": $routeParams["userId"], "userType": $routeParams["userType"], "mnlsrid": $routeParams["mloId"], "agentName": $routeParams["agentName"] };
            }

            heDataService.getData(apiUrl + '/GetMaturingOptionsData', data, function (response) {
                $scope.heData = response;
                $scope.showLoader = false;
            }, function (error) {
                console.log(error);
                $scope.showLoader = false;
            });
        }

        $scope.heData = { "createdBy": $routeParams["userId"] };
    };

    $scope.btnRetrieveData = function (accountNo) {

        $scope.butonSaveClicked = false;
        $scope.butonSubmitForProcessingClicked = false;
        $scope.showLoader = true;

        data = { "accountNum": accountNo, "CreatedBy": angular.element(userId).val(), "userType": angular.element(userType).val(), "mnlsrid": angular.element(mloId).val(), "agentName": angular.element(agentName).val() };

        heDataService.getData(apiUrl + '/GetMaturingOptionsData', data, function (response) {
            $scope.heData = response;
            $scope.showLoader = false;
        }, function (error) {
            console.log(error);
            $scope.showLoader = false;
        });
    };

    $scope.btnCancel = function (src) {

        $scope.butonSaveClicked = false;
        $scope.butonSubmitForProcessingClicked = false;

        $scope.showLoader = true;
        $scope.heData.logSource = "UI." + src.label;
        $scope.heData.moStatus = src.status;
        //$scope.showLoader = true;

        //data = { "accountNum": "", "CreatedBy": angular.element(userId).val(), "userType": angular.element(userType).val(), "mnlsrid": angular.element(mloId).val(), "agentName": angular.element(agentName).val() };

        heDataService.getData(apiUrl + '/CancelMaturingToolsData', $scope.heData, function (response) {
            $scope.heData = response;
            $scope.showLoader = false;
        }, function (error) {
            console.log(error);
            $scope.showLoader = false;
        });
    }

    $scope.btnSave = function (src) {
        if ((angular.equals(angular.lowercase(src.label.replace(" ", "")), "sendemail")) || (angular.equals(angular.lowercase(src.label.replace(" ", "")), "sendletter"))) {
            $scope.butonSubmitForProcessingClicked = true;
        }
        $scope.butonSaveClicked = true;


        $scope.butonClicked = true;

        $timeout(function () {
            if ($scope.maturingToolForms.$valid) {
                $scope.showLoader = true;
                $scope.heData.logSource = "UI." + src.label;
                $scope.heData.moStatus = src.status;

                heDataService.getData(apiUrl + '/SubmitMaturingToolsData', $scope.heData, function (response) {
                    $scope.heData = response;
                    $scope.showLoader = false;
                    $scope.butonSaveClicked = false;
                }, function (error) {
                    console.log(error);
                    $scope.showLoader = false;
                    $scope.butonSaveClicked = true;
                });
            }
        }, 500);

        //alert($scope.maturingToolForms.$valid);
        //return;
    };

    $scope.checkForSaveClick = function () {
        return $scope.butonSaveClicked;
    }

    $scope.checkForButtonProcessingClick = function (isChecked) {
        return (isChecked && $scope.butonSubmitForProcessingClicked);
    }

    $scope.backtoStatus = function () {
        //clear assignto if no activity is done
        if (angular.isDefined($scope.heData.moguid) && $scope.heData.moguid != null && $scope.heData.moguid != "") {
            heDataService.getData(apiUrl + '/ClearAssignToData', $scope.heData, function (response) {
                backToStatusScreen();
            }, function (error) {
                console.log("error");
            });
        } else {
            backToStatusScreen();
        }
    }

    function backToStatusScreen() {
        $location.path("");
        $location.hash("");
        $location.search("");
        $location.path('/StatusScreen');
    }

    $scope.checkAccountTypeForEPD = function () {
        var isDisabled = $scope.heData.accountType != 'EOD';
        if (isDisabled && $scope.heData.isepdP1 == true) {
            $scope.heData.isepdP1 = false;
        }
        if (isDisabled && $scope.heData.isepdP2 == true) {
            $scope.heData.isepdP2 = false;
        }
        if (isDisabled && $scope.heData.isepdP3 == true) {
            $scope.heData.isepdP3 = false;
        }
        if (isDisabled && $scope.heData.isepdP4 == true) {
            $scope.heData.isepdP4 = false;
        }
        return isDisabled;
    };

    $scope.checkEPDProgram1 = function () {
        return $scope.heData.isepdP1 == false;
    }

    $scope.checkEPDProgram2 = function () {
        return $scope.heData.isepdP2 == false;
    }

    $scope.checkEPDProgram3 = function () {
        return $scope.heData.isepdP3 == false;
    }

    $scope.checkEPDProgram4 = function () {
        return $scope.heData.isepdP4 == false;
    }

    $scope.checkEmailDocumentsFlag = function () {
        return $scope.heData.isEmail == false;
    }

    $scope.checkFieldsForPrinting = function () {
        return $scope.heData.payoffBalance == "" || $scope.heData.payoffBalance == null || $scope.heData.payoffAsOfDate == "" || $scope.heData.payoffAsOfDate == null;
    };

    $scope.checkRepaymentIndicator = function () {
        if (angular.equals(angular.uppercase(angular.element(userType).val()), "OSU")) {
            return $scope.heData.isRepayment && $scope.butonSubmitForProcessingClicked;
        } else {
            return false;
        }
    };

    $scope.checkPayoffIndicator = function () {
        return $scope.heData.isPayoff && $scope.butonSubmitForProcessingClicked;
    }

    $scope.checkAccountTypeForRepayment = function () {
        return $scope.heData.accountType != 'EOD';
    }

    $scope.checkNewHomeLoanDate = function () {
        var cfgDateStr = angular.element(endDate).val();
        var sDate = yyyymmdd_to_date(cfgDateStr)
        var currDate = new Date();
        return currDate > sDate;
    }

    function yyyymmdd_to_date(d) {
        return new Date(d.substr(0, 4) + "/" + d.substr(4, 2) + "/" + d.substr(6, 2));
    }

    $scope.changeButtonLabel = function (isEmail) {
        //Allow send letter only for OSU users
        if (angular.equals(angular.uppercase(angular.element(userType).val()), "OSU")) {
            if (isEmail) {
                $scope.heData.btnSubmitForProcessing.label = "Send Email";
                $scope.heData.btnSubmitForProcessing.status = "Delivered";
            } else {
                $scope.heData.btnSubmitForProcessing.label = "Send Letter";
                $scope.heData.btnSubmitForProcessing.status = "ForDelivery";
            }
        }
    }
}]);