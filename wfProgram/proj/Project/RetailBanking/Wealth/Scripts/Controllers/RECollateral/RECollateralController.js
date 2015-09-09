wmg.controller("RECollateralController", ['$scope', 'goAheadService', 'stateService', 'Enums', 'wmgCommonService',
    function ($scope, goAheadService, stateService, Enums, wmgCommonService) {


        //START - Basic Initialization for Controller
        $scope.rec = Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral;
        $scope.isCoAppAvailable = Root.applicationInfo.isCoAppAvailable;
        $scope.addressText = "";
        if (wmgCommonService.getApplicant("S") != undefined) {
            $scope.coapplicant = wmgCommonService.getApplicant("S");
        }
        //END - Basic Initialization for Controller


        //START - Collateral Property Address Section
        $scope.getAddress = function () {
            for (var i = 0; i < Root.applicationInfo.DynamicData.addressList.length; i++) {
                if (Root.applicationInfo.DynamicData.addressList[i].addressType == 'CR') {
                    $scope.address = Root.applicationInfo.DynamicData.addressList[i];
                }
            }
        };
        $scope.getAddress();

        $scope.setPropertyAddress = function () {
            for (var key in $scope.address) {
                if ($scope.address.hasOwnProperty(key)) {
                    if (key != 'addressType') {
                        if ($scope.address[key] != undefined) {
                            $scope.addressText = $scope.addressText + ' ' + $scope.address[key];
                        }
                    }
                }
            }
            $scope.propertyAddress = [];
            $scope.collateralProperty = { type: "" };

            $scope.propertyAddress.push({ key: "", value: "Select" });
            $scope.propertyAddress.push({ key: "ADR", value: $scope.addressText });
            $scope.propertyAddress.push({ key: "OTH", value: "Other" });
        };
        $scope.setPropertyAddress();

        $scope.getOtherAddress = function () {
            if ($scope.collateralProperty.type == 'OTH') {
                $scope.rec.propertyAddress.addressLine = "";
                $scope.rec.propertyAddress.addressLine2 = "";
                $scope.rec.propertyAddress.city = "";
                $scope.rec.propertyAddress.state = "";
                $scope.rec.propertyAddress.zip = "";
                $scope.purchasePriceRequired = true;
            } else if ($scope.collateralProperty.type == 'ADR') {
                $scope.hmdaAddress = "";
                $scope.rec.propertyAddress.addressLine = $scope.address.addressLine;
                $scope.rec.propertyAddress.addrLine2 = "";
                $scope.rec.propertyAddress.city = $scope.address.city;
                $scope.rec.propertyAddress.state = $scope.address.state;
                $scope.rec.propertyAddress.zip = $scope.address.zip;
                $scope.purchasePriceRequired = false;
            }
            setTimeout(function () {
                svpApp.util.buildSelects();
            }, 50);
        };

        $scope.isPurchasedIn5Years = function () {
            var millsecondsPerDay = 1000 * 60 * 60 * 24;
            var purchaseDate = $scope.rec.purchaseDate;
            if (purchaseDate == undefined) {
                return false;
            }
            var currentDate = new Date();
            var diffInMilliSeconds = Date.parse(currentDate) - Date.parse(purchaseDate);
            var diffInDays = Math.floor(diffInMilliSeconds / millsecondsPerDay);
            return (diffInDays <= 1826);  //1826 Days = 5 Years 
        };
        //END - Collateral Property Address Section

        //START - Existing Mortgage Section
        $scope.mortgageInformation = $scope.rec.mortgageInformation;
        $scope.mortgages = $scope.mortgageInformation.mortgageList;

        if (angular.isArray($scope.mortgages)) {
            if ($scope.mortgages.length > 1) {
                $scope.firstMortgage = $scope.mortgageInformation.mortgageList[0];
                $scope.secondMortgage = $scope.mortgageInformation.mortgageList[1];
            } else {
                $scope.firstMortgage = $scope.mortgageInformation.mortgageList[0];
                $scope.secondMortgage = angular.copy($scope.firstMortgage);
                for (var prop in $scope.secondMortgage) {
                    if ($scope.secondMortgage.hasOwnProperty(prop)) {
                        $scope.secondMortgage[prop].value = "";
                    }
                }
                $scope.mortgageInformation.mortgageList.push($scope.secondMortgage);
            }
        }

        //Only pass object not nested object
        var isObjectSet = function (obj) {
            var dirty = false;
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop)) {
                    if (obj[prop] != "") {
                        dirty = true;
                    }
                }
            }
            return dirty;
        };

        $scope.setMortgageType = function () {
            if ($scope.mortgageInformation.mortgageList == undefined) {
                return;
            }

            for (var i = 0; i < $scope.mortgageInformation.mortgageList.length; i++) {

                if (isObjectSet($scope.mortgageInformation.mortgageList[i]) == false) {
                    continue;
                }

                if (i == 0) {
                    $scope.mortgageInformation.mortgageList[0].type = "MTG1";
                    $scope.mortgageInformation.mortgageList[0].mortgageType = "MTG1";
                }
                if (i == 1) {
                    $scope.mortgageInformation.mortgageList[1].type = "MTGA";
                    $scope.mortgageInformation.mortgageList[1].mortgageType = "MTG1";
                }
            }
        };

        //$scope.addSecondMortgage = function () {
        //    if ($scope.mortgageInformation.anotherMortgageFlag == 'Y') {

        //    }
        //};
        //END - Existing Mortgage Section

        //START - Check is HMDA Screen Applicable 
        //This is as per the requirement we need to decide to show / hide the HMDA Screen
        //based on the values entered in RECollateral screen
        $scope.isHmdaApplicable = function () {
            $scope.products = wmgCommonService.getAllProductrs();

            //Just in case user clicks previous, we need to reset the status and run again
            $scope._showAppRaceEthnicity.status = false;
            $scope._showCoAppRaceEthnicity.status = false;

            var result = false;
            //FSD-FR.51.3, FR.51.3.A, FR.51.3.B
            for (var i = 0; i < $scope.products.length; i++) {
                if ($scope.products[i].isProductRemoved == 'Y') {
                    continue;
                }
                if ((wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB30Y"))
                        && (($scope.products[i].loanPurpose == "REFN") || ($scope.products[i].loanPurpose == "PMSD")) 
                        && ($scope.rec.occupancyType == 'P')
                        && ($scope.rec.appCollateralFlag == 'Y')) {

                    $scope._showAppRaceEthnicity.status = true;
                    result = true;
                }
                if ((wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB30Y"))
                        && (($scope.products[i].loanPurpose == "REFN") || ($scope.products[i].loanPurpose == "PMSD"))
                        && ($scope.rec.occupancyType == 'P')
                        && ($scope.rec.coAppCollateralFlag == 'Y')
                        && ($scope.isCoAppAvailable == 'Y')) {
                    $scope._showCoAppRaceEthnicity.status = true;
                    result = true;
                }
                if ((wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB34Z,PB36K"))
                        && (($scope.products[i].loanPurpose == "REFN"))
                        && ($scope.rec.occupancyType == 'P')
                        && ($scope.rec.appCollateralFlag == 'Y')) {
                    $scope._showAppRaceEthnicity.status = true;
                    result = true;
                }
                if ((wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB34Z,PB36K"))
                        && (($scope.products[i].loanPurpose == "REFN"))
                        && ($scope.rec.occupancyType == 'P')
                        && ($scope.rec.coAppCollateralFlag == 'Y')
                        && ($scope.isCoAppAvailable == 'Y')) {
                    $scope._showCoAppRaceEthnicity.status = true;
                    result = true;
                }
                if (wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y")) {
                    if ($scope.products[i].loanPurpose != undefined && $scope.products[i].loanPurpose == "HIMP") {
                        $scope._showAppRaceEthnicity.status = true;
                        if ($scope.coapplicant != null) {
                            $scope._showCoAppRaceEthnicity.status = true;
                        }

                        result = true;
                    }
                }
            }
            if ($scope._isHmdaApplicable.status == true) {
                return true;
            }
            return result;
        };

        $scope.validateHMDAApplicable = function () {
            if ($scope.isHmdaApplicable() == false) {
                stateService.disableView(Enums.wmgViews.Hmda);
                $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            } else {
                stateService.enableView(Enums.wmgViews.Hmda);
                $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            }
        };

        //Check is HMDA Screen Applicable - END

        $scope.$on("RECollateral", $scope.validateBeforeContinue);

        $scope.$on("RECollateral", $scope.validateBeforeContinue = function (event, data) {
            $scope.validateHMDAApplicable();
            $scope.setMortgageType();
            if ($scope.address.addressLine2 != undefined) {
                $scope.address.addressLine = $scope.address.addressLine + ' ' + $scope.address.addressLine2;
            }
            goAheadService.setFlag(true);
        });


    }]);