wmg.controller('PersonalFinanceController', ['$scope', 'goAheadService', 'wmgCommonService', function ($scope, goAheadService, wmgCommonService) {

    //START - Basic Initialization of the Controller
    $scope.Errormsg = "";
    $scope.ownProperty = "";
    $scope.assetInfo = Root.applicationInfo.WealthData.assetInfo;

    $scope.realEstateHoldings = Root.applicationInfo.WealthData.assetInfo.realEstateOwnedList;
    $scope.realEstateHoldings = wmgCommonService.removeIfEmptyItemFound($scope.realEstateHoldings);

    $scope.assets = Root.applicationInfo.WealthData.assetInfo.assets;                         //Initialized in BasicXML.js
    $scope.liabilities = Root.applicationInfo.WealthData.assetInfo.liabilities;               //Initialized in BasicXML.js

    //$scope.notesReceivable = notesReceivableList;   //Initialized in BasicXML.js
    //Root.applicationInfo.WealthData.assetInfo.assets = $scope.assets;
    //Root.applicationInfo.WealthData.assetInfo.liabilities = $scope.liabilities;
    //Root.applicationInfo.WealthData.assetInfo.notesReceivableList = $scope.notesReceivable;

    //$scope.assets = Root.applicationInfo.WealthData.assetInfo.assets;
    //$scope.liabilities = Root.applicationInfo.WealthData.assetInfo.liabilities;
    $scope.notesReceivable = Root.applicationInfo.WealthData.assetInfo.notesReceivableList;

    $scope.sumAssets = Root.applicationInfo.WealthData.assetInfo.totalAssets;
    $scope.getTotalAssets = function () {


        var sum = 0;
        for (var i = 0; i < $scope.assets.length ; i++) {
            if ($scope.assets[i].balance == undefined || $scope.assets[i].balance == "")
                sum += 0;
            else
                sum += parseInt($scope.assets[i].balance);
        }
        $scope.assetInfo.totalAssets = parseInt(sum);
        $scope.getNetWorth();
        return sum;

    };
    Root.applicationInfo.WealthData.assetInfo.totalAssets = $scope.sumAssets;

    $scope.sumLiabilities = Root.applicationInfo.WealthData.assetInfo.totalLiabilities;
    $scope.getTotalLiabilities = function () {

        var sum = 0;
        for (var i = 0; i < $scope.liabilities.length ; i++) {
            if ($scope.liabilities[i].balance == undefined || $scope.liabilities[i].balance == "")
                sum += 0;
            else
                sum += parseInt($scope.liabilities[i].balance);
        }
        $scope.assetInfo.totalLiabilities = parseInt(sum);
        $scope.getNetWorth();
        
        return sum;


    };
    $scope.getNetWorth = function () {

      
        $scope.assetInfo.netWorth = parseInt($scope.assetInfo.totalAssets) - parseInt($scope.assetInfo.totalLiabilities);
        


    };
    $scope.getTotalMonthlyTaxes = function () {

        var sum = 0;
        for (var i = 0; i < $scope.realEstateHoldings.length ; i++) {
            if ($scope.realEstateHoldings[i].reoMonthlyTaxes == undefined || $scope.realEstateHoldings[i].reoMonthlyTaxes == "")
                sum += 0;
            else
                sum += parseInt($scope.realEstateHoldings[i].reoMonthlyTaxes);
        }

        return sum;


    };
    $scope.getTotalPresentMktValue = function () {

        var sum = 0;
        for (var i = 0; i < $scope.realEstateHoldings.length ; i++) {
            if ($scope.realEstateHoldings[i].reoPresentMktValue == undefined || $scope.realEstateHoldings[i].reoPresentMktValue == "")
                sum += 0;
            else
                sum += parseInt($scope.realEstateHoldings[i].reoPresentMktValue);
        }

        return sum;


    };
    $scope.getTotalGrossRentalIncome = function () {

        var sum = 0;
        for (var i = 0; i < $scope.realEstateHoldings.length ; i++) {
            if ($scope.realEstateHoldings[i].reoGrossRentalIncome == undefined || $scope.realEstateHoldings[i].reoGrossRentalIncome == "")
                sum += 0;
            else
                sum += parseInt($scope.realEstateHoldings[i].reoGrossRentalIncome);
        }

        return sum;


    };
    $scope.getTotalInsurancePayment = function () {

        var sum = 0;
        for (var i = 0; i < $scope.realEstateHoldings.length ; i++) {
            if ($scope.realEstateHoldings[i].reoHOInsurance == undefined || $scope.realEstateHoldings[i].reoHOInsurance == "")
                sum += 0;
            else
                sum += parseInt($scope.realEstateHoldings[i].reoHOInsurance);
        }

        return sum;


    };
    //END - Basic Initialization of the Controller

    //START - Helper Functions
    $scope.getDescription = function (list, key) {
        return wmgCommonService.findDescription(list, key);
    };

    $scope.selection = [{ checked: false }, { checked: false }, { checked: false }];

    if ($scope.assetInfo.propertTypeInd == "0") {
        $scope.selection[0].checked = true;
    }
    else if ($scope.assetInfo.propertTypeInd == "1") {
        $scope.selection[1].checked = true;
    }
    else if ($scope.assetInfo.propertTypeInd == "2") {
        $scope.selection[2].checked = true;
    }
    $scope.selectOne = function (current, index) {
        angular.forEach($scope.selection, function (item) {
            item.checked = false;
        });
        current.checked = true;
        $scope.assetInfo.propertTypeInd = index;
    };
    //END - Helper Functions


    //START - Schedule-2 Section
    $scope.inputNore2Receivable = $scope.notesReceivable;

    $scope.TotalUnPaidBalance = function () {
        var sum = 0;
        for (var i = 0; i < $scope.inputNore2Receivable.length ; i++) {
            if ($scope.inputNore2Receivable[i].unPaidBalance == undefined || $scope.inputNore2Receivable[i].unPaidBalance == "")
                sum += 0;
            else
                sum += parseInt($scope.inputNore2Receivable[i].unPaidBalance);
        }

        return sum;
    };

    $scope.TotalAnnual = function () {
        var sum = 0;
        for (var i = 0; i < $scope.inputNore2Receivable.length ; i++) {
            if ($scope.inputNore2Receivable[i].annual == undefined || $scope.inputNore2Receivable[i].annual == "")
                sum += 0;
            else
                sum += parseInt($scope.inputNore2Receivable[i].annual);
        }

        return sum;

    };

    var validateInput = function () {
        $("input[ng-model='input.nameOfDebtor']").change();
        $("input[ng-model='input.collateralType']").change();
        $("input[ng-model='input.maturityDate']").change();
        $("input[ng-model='input.annual']").change();
        $("input[ng-model='input.unPaidBalance']").change();

        var f1 = $("input[ng-model='input.nameOfDebtor']").hasClass("ng-invalid");
        var f2 = $("input[ng-model='input.collateralType']").hasClass("ng-invalid");
        var f3 = $("input[ng-model='input.maturityDate']").hasClass("ng-invalid");
        var f4 = $("input[ng-model='input.annual']").hasClass("ng-invalid");
        var f5 = $("input[ng-model='input.unPaidBalance']").hasClass("ng-invalid");
        if (f1 == true || f2 == true || f3 == true || f4 == true || f5 == true) {
            return false;
        }
        return true;
    };

    $scope.Note2ReceivableAddNewRow = function (input, index) {


        if (input.anchorclass == "") {
            if (validateInput() == true) {
                input.anchorclass = "open";
                $scope.inputNore2Receivable.push(angular.copy({
                    NameOfDebtor: "",
                    CollateralType: "",
                    MaturityDate: "",
                    Annual: "",
                    UnPaidBalance: "", anchorclass: "", error: false, errorNo: 0
                }));
            }
        }
        else {
            if ($scope.inputNore2Receivable.length == 1) return false;
            $scope.inputNore2Receivable.splice(index, 1);
        }
    };
    //END - Schedule-2 Section


    //START - Schedule-3 Section
    $scope.editModalPopup = function (index) {

        $scope.reoList.reo = angular.copy($scope.realEstateHoldings[index]);
        $scope.modalPopup(index);

    };

    $scope.modalPopup = function (index) {
        if (index == undefined) {
            if ($scope.reoList.reo != undefined) {
                $scope.reoList.reo = wmgCommonService.emptyGivenObject($scope.reoList.reo);

            }
            index = null;
        }

        EditIndex = index;
        $("#dvSchedule3Modal").show();
        wmgCommonService.getCursorTop();
        var widthContainer = 1105;
        var bodyContainer = $('body').width();

        $("#dvSchedule3Modal .modalcontainer").css({ width: widthContainer + "px" });
        var LeftSpace = (bodyContainer - widthContainer) / 2;
        $("#dvSchedule3Modal .modalcontainer").css({ left: LeftSpace + "px" });

        setTimeout(function () {
            svpApp.util.buildSelects();
        }, 500);
    };

    $scope.modalPopupSchedule3Close = function () {
        EditIndex = null;
        $("#dvSchedule3Modal").hide();
    };

    $scope.reoList = [];

    $scope.saveRealEstateHoldings = function () {
        if (pageValidator("modal") == false) {
            return;
        }

        if (EditIndex != null) {
            $scope.realEstateHoldings[EditIndex] = angular.copy($scope.reoList.reo);
        } else {

            $scope.realEstateHoldings.push(angular.copy($scope.reoList.reo));
        }
        $scope.modalPopupSchedule3Close();


    };

    $scope.removeRealEstateHolding = function (index) {
        $scope.realEstateHoldings.splice(index, 1);
    };

    $scope.editRealEstateHolding = function (index) {
        $scope.reoList.reo = angular.copy($scope.realEstateHoldings[index]);
        $scope.reoList.reo.index = index;

        $scope.CDSaving.Form = angular.copy($scope.PathCdSaving[index]);
        $scope.CDSaving.Form.collOwnerInd = "Y";
        $scope.CDSaving.Form.index = index;
        $scope.modalPopupCdSaving(index);

    };

    //END - Schedule-3 Section


    $scope.$on("PersonalFin", $scope.validateBeforeContinue);

    $scope.$on("PersonalFin", $scope.validateBeforeContinue = function (event, data) {
        goAheadService.setFlag(true);
    });
}]);