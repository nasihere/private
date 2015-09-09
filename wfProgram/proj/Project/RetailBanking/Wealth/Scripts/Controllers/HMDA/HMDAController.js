wmg.controller("HMDAController", ['$scope', 'wmgCommonService', 'goAheadService', function ($scope, wmgCommonService, goAheadService) {

    $scope.isCoApplicantAvailable = Root.applicationInfo.isCoAppAvailable;
    $scope.applicantRaceList = angular.copy(Common.Race);
    $scope.coApplicantRaceList = angular.copy(Common.Race);
    $scope.sameAsResidence = { selected: false };
    $scope.allProducts = wmgCommonService.getAllProductrs();

    $scope.setAddress = function () {
        if ($scope.sameAsResidence.selected == true) {
            $scope.hmdaAddress = $scope.getResidenceAddress();
        } else {
            $scope.hmdaAddress.addressLine = "";
            $scope.hmdaAddress.addrLine2 = "";
            $scope.hmdaAddress.city = "";
            $scope.hmdaAddress.state = "";
            $scope.hmdaAddress.zip = "";
        }
        $scope.setProductHmdaAddress();
        setTimeout(function () {
            svpApp.util.buildSelects();

        }, 50);
    };

    $scope.setProductHmdaAddress = function () {
        var prodList = wmgCommonService.getAllProductrs();
        for (var i = 0; i < prodList.length; i++) {
            if ($scope.isHmdaAddressApplicableForProduct(prodList[i]) == true) {
                prodList[i].HMDA.propertyImproved.addrLineList.addrLine1 = $scope.hmdaAddress.addressLine;
                prodList[i].HMDA.propertyImproved.addrLineList.addrLine2 = $scope.hmdaAddress.addrLine2;
                prodList[i].HMDA.propertyImproved.city = $scope.hmdaAddress.city;
                prodList[i].HMDA.propertyImproved.state = $scope.hmdaAddress.state;
                prodList[i].HMDA.propertyImproved.zip = $scope.hmdaAddress.zip;
            } else {
                prodList[i].HMDA.propertyImproved.addrLineList.addrLine1 = "";
                prodList[i].HMDA.propertyImproved.addrLineList.addrLine2 = "";
                prodList[i].HMDA.propertyImproved.city = "";
                prodList[i].HMDA.propertyImproved.state = "";
                prodList[i].HMDA.propertyImproved.zip = "";
            }
        }
    };
    //Called from UI - View
    $scope.isHmdaAddressApplicableForProduct = function (product) {
        if (product == undefined) {
            return false;
        }

        //if ((wmgCommonService.isProductExistsIn(product.productID, "PB30Y,PB34Z,PB36K"))
        //               && (product.loanPurpose == "HIMP")
        //               && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
        //               && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.appCollateralFlag == 'Y')) {
        //    return true;
        //}

        //if ((wmgCommonService.isProductExistsIn(product.productID, "PB30Y,PB34Z,PB36K"))
        //        && (product.loanPurpose == "HIMP")
        //        && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
        //        && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.coAppCollateralFlag == 'Y')
        //        && (Root.applicationInfo.isCoAppAvailable == 'Y')) {
        //    return true;
        //}

        var result = wmgCommonService.isProductExistsIn(product.productID, "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
        //if (result == true) {
        //    $scope._showAppRaceEthnicity.status = true;
        //}
        if ((result == true) && (product.loanPurpose == "HIMP")) {
            return true;
        }

        return false;
    };

    $scope.isHmdaAddressApplicable = function () {
        if ($scope.allProducts == undefined) {
            return false;
        }

        for (var i = 0; i < $scope.allProducts.length; i++) {
            //if ((wmgCommonService.isProductExistsIn($scope.allProducts[i].productID, "PB30Y,PB34Z,PB36K"))
            //&& ($scope.allProducts[i].loanPurpose == "HIMP")
            //&& (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
            //&& (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.appCollateralFlag == 'Y')) {
            //    return true;
            //}

            //if ((wmgCommonService.isProductExistsIn($scope.allProducts[i].productID, "PB30Y,PB34Z,PB36K"))
            //    && ($scope.allProducts[i].loanPurpose == "HIMP")
            //    && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
            //    && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.coAppCollateralFlag == 'Y')
            //    && (Root.applicationInfo.isCoAppAvailable == 'Y')) {
            //    return true;
            //}

            var result = wmgCommonService.isProductExistsIn($scope.allProducts[i].productID, "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
            //if (result == true) {
            //    $scope._showAppRaceEthnicity.status = true;
            //}
            if ((result == true) && ($scope.allProducts[i].loanPurpose == "HIMP")) {
                return true;
            }
        }
        return false;
    };
    $scope.isHmdaAddressApplicable();

    $scope.getResidenceAddress = function () {
        return angular.copy(Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.propertyAddress);
    };

    $scope.isInformationForBankerApplicable = function () {
        return wmgCommonService.isProductFound("PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
    };

    if (wmgCommonService.getApplicant("P") != undefined) {
        var app = wmgCommonService.getApplicant("P");
        $scope.applicant = wmgCommonService.getApplicant("P").HMDA;
        $scope.applicationMethod = app.applicationMethod;
        $scope.appInPersonIndicator = app.inPersonInd;
        
        //The below variables are used in Summary Screen
        applicationMethod = app.applicationMethod;
    }
    
    if (wmgCommonService.getApplicant("S") != undefined) {
        var app = wmgCommonService.getApplicant("S");
        $scope.coapplicant = wmgCommonService.getApplicant("S").HMDA;
        $scope.coApplicationMethod = app.applicationMethod;
        $scope.coAppInPersonIndicator = app.inPersonInd;
        
        //The below variables are used in Summary Screen
        coApplicationMethod = app.applicationMethod;
    }

    //$scope.initAppMethod = function () {
    //    $scope.appMethods = Root.applicationInfo.applicationMethod;

    //    if ($scope.appMethods.indexOf(',') != -1) {
    //        $scope.appMethods = $scope.appMethods.split(',');

    //        $scope.applicationMethod = $scope.appMethods[0];
    //        $scope.coApplicationMethod = $scope.appMethods[1];

    //        //The below variables are used in Summary Screen
    //        applicationMethod = $scope.appMethods[0];
    //        coApplicationMethod = $scope.appMethods[1];
    //    } else {
    //        $scope.applicationMethod = Root.applicationInfo.applicationMethod;
    //        $scope.coApplicantionMethod = "";
    //        //The below variables are used in Summary Screen
    //        applicationMethod = Root.applicationInfo.applicationMethod;
    //        coApplicationMethod = "";
    //    }
    //};

    $scope.setDropDown = function () {

        $scope.applicantEthnicity = angular.copy(Common.ddEthnicity);
        $scope.applicantGender = angular.copy(Common.ddGender);
        $scope.coApplicantEthnicity = angular.copy(Common.ddEthnicity);
        $scope.coApplicantGender = angular.copy(Common.ddGender);
        
        if ($scope.applicationMethod != 'F') {
            $scope.applicantEthnicity.push({ key: "3", value: "Does not wish to provide" });
            $scope.applicantGender.push({ key: "3", value: "Does not wish to provide" });
        }
        if ($scope.coApplicationMethod != 'F') {
            $scope.coApplicantEthnicity.push({ key: "3", value: "Does not wish to provide" });
            $scope.coApplicantGender.push({ key: "3", value: "Does not wish to provide" });
        }
    };

    $scope.applicantDisable = false;
    $scope.validateApplicantRace = function () {
        if ($scope.applicant.race6 != '') {
            $scope.applicant.race1 = '';
            $scope.applicant.race2 = '';
            $scope.applicant.race3 = '';
            $scope.applicant.race4 = '';
            $scope.applicant.race5 = '';
            $scope.applicantDisable = true;
        } else {
            $scope.applicantDisable = false;
        }
    };

    $scope.coApplicantDisable = false;
    $scope.validateCoApplicantRace = function () {
        if ($scope.coapplicant.race6 != '') {
            $scope.coapplicant.race1 = '';
            $scope.coapplicant.race2 = '';
            $scope.coapplicant.race3 = '';
            $scope.coapplicant.race4 = '';
            $scope.coapplicant.race5 = '';
            $scope.coApplicantDisable = true;
        } else {
            $scope.coApplicantDisable = false;
        }
    };


    
    $scope.AppRaceList = function (ValueRace, AppRace) {
        var target = angular.element('#AppRaceValid')[0];
        if (AppRace == "")
            target.value += ValueRace;
        else
            target.value = target.value.replace(ValueRace,"");
        

    }
    $scope.CoAppRaceList = function (ValueRace, AppRace) {
        var target = angular.element('#CoAppRaceValid')[0];
        if (AppRace == "")
            target.value += ValueRace;
        else
            target.value = target.value.replace(ValueRace, "");


    }
    
    $scope.$on("Hmda", $scope.validateBeforeContinue);
    $scope.$on("Hmda", $scope.validateBeforeContinue = function (event, data) {
        goAheadService.setFlag(true);
    });
}]);