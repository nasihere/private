wmg.controller('complianceController', ['$scope', 'stateService', 'wmgCommonService', 'Enums', 'goAheadService', 'wmgDataService', '$timeout',
    function ($scope, stateService, wmgCommonService, Enums, goAheadService, wmgDataService, $timeout) {

        //START - Helper functions
        //NOTE: This below function is used to validate if the WMG JSON has any error, if any error it will route to Error Page.
        $scope.isApplicationError = function () {
            if (wmgDataService.applicationError == true) {
                stateService.goToView(Enums.wmgViews.Error);
                return false;
            }
        };
        $scope.isApplicationError();

        //Helper Function
        //This is required, just in case we have PB34Z only one product and the AUM < 1m then there is no point of showing the Wisconsin Disclaimer
        var wisconsinToShow = function () {
            var productCount = wmgCommonService.getTotalProductCount();
            var validAumProudct = wmgCommonService.isProductFound("PB34Z");
            if ((productCount == 1) && (validAumProudct == true) && ($scope.aumWarning == true)) {
                return false; //No Need to show wisconsin Disclaimer
            } else {
                return true;
            }
        };
        //END - Helper functions


        //START - Common application variable scope binding
        var init = function () {
            if (wmgDataService.applicationError == true) {
                return;
            }

            $scope.applicationInfo = Root.applicationInfo;
            $scope.applicantInfoList = Root.applicationInfo.applicantInfoList;

            $scope.additionalInfo = Root.applicationInfo.additionalContact;
            $scope.bankerInfo = Root.applicationInfo.bankerInfo;

            $scope.applicant = wmgCommonService.getApplicant("P");
            $scope.coapplicant = wmgCommonService.getApplicant("S");

            $scope.businessTypeInfo = Root.applicationInfo.businessTypeInfo;
        };
        init();
        $scope.wisconsinApplicable = false;
        //END - Common application variable scope binding

        //START - AUM Validation section

        $scope.applyAumLogic = function () {
            stateService.disableProductByCode('PB34Z', false);
            //stateService.skipProductRouting($scope.routingStates, 'PB34Z');
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            $scope.aumWarningPopupClose();

            wmgCommonService.setAumWarningPopUpFlag(true);

            if (wisconsinToShow() == false) {
                goAheadService.setFlag(true);
                stateService.disableFromAndMoveTo(Enums.wmgViews.Product1, Enums.wmgViews.SubmitApp);
                $scope._routedFromCDScreen.status = true;
            } else {

                if (((Root.applicationInfo.isWIDisclosureApplicable == 'Y') && (($scope.applicant.maritalStatus == 'M') || ($scope.applicant.maritalStatus == 'S')))
                    || (((Root.applicationInfo.isWIDisclosureApplicable == 'Y') && (Root.applicationInfo.isCoAppAvailable == 'Y')
                        && (($scope.coapplicant.maritalStatus == 'M') || ($scope.coapplicant.maritalStatus == 'S'))))) {
                    goAheadService.setFlag(false);
                    $scope.applyDisclosureLogic();

                } else {
                    goAheadService.setFlag(true);
                    $scope.moveNext();
                }

            }
        };

        $scope.showAumNotification = function () {
            if (wmgCommonService.isAumWarningApplicable($scope.applicationInfo.AUM) == true) {
                $scope.aumWarning = true;
            } else {
                $scope.aumWarning = false;
            }
        };
        $scope.aumWarningPopupClose = function () {
            $("#aumWarningPopup").hide();
        };

        //END - AUM Validation section 


        //START - Business Type Information validation Section
        //FSD-FR.83
        $scope.isTrustInBusinessTypeApplicable = function () {
          
            var prods = "PB251, PB252, PB25G, PB25H, PB281, PB282, PB28G, PB28Y, PB31I, PB34I, PB34Z";
            return wmgCommonService.isProductFound(prods);
        };
        //FSD-FR.86
        $scope.isOthersInBusinessTypeApplicable = function () {
            var prods = "PB22X, PB22Y, PB37I, PB37K";
            return wmgCommonService.isProductFound(prods);
        };
        //FSD-FR.86
        $scope.isMemberOfOfficeTypeApplicable = function () {
            var prods = "PB22X, PB22Y, PB37I, PB37K";
            return wmgCommonService.isProductFound(prods);
        };
        //Hide the section header
        $scope.isBusinessTypeApplicable = function () {
           /* if (($scope.isTrustInBusinessTypeApplicable() == false) && ($scope.isOthersInBusinessTypeApplicable() == false) && ($scope.isMemberOfOfficeTypeApplicable() == false))*/
            if (($scope.isOthersInBusinessTypeApplicable() == false) && ($scope.isMemberOfOfficeTypeApplicable() == false))
            {
                $scope.businessTypeSectionApplicable = false;
            } else {
                $scope.businessTypeSectionApplicable = true;
            }
        };
        //below is self call so that based on FSD the section will not be displayed
        $scope.isBusinessTypeApplicable();
        //END - Business Type Information validation Section


        //FSD. 
        //START - Members Officers Section validation
        //FSD-Page-20
        if (Root.applicationInfo.businessTypeInfo != undefined) {
            $scope.membersOfficers = Root.applicationInfo.businessTypeInfo.membersOfficersList;
            $scope.membersOfficers = wmgCommonService.removeIfEmptyItemFound($scope.membersOfficers);
        }

        $scope.membersOfficer = { role: "", title: "", name: "" };
        $scope.position = 0;
        $scope.editClicked = false;

        $scope.refreshDropdowns = function () {
            setTimeout(function () {
                svpApp.util.buildSelects();
            }, 50);
        };

        var validateInput = function () {
            $("select[ng-model='membersOfficer.role']").change();
            $("select[ng-model='membersOfficer.title']").change();
            $("input[ng-model='membersOfficer.name']").change();

            var f1 = $("select[ng-model='membersOfficer.role']").hasClass("ng-invalid");
            var f2 = $("select[ng-model='membersOfficer.title']").hasClass("ng-invalid");
            var f3 = $("input[ng-model='membersOfficer.name']").hasClass("ng-invalid");
            if (f1 == true || f2 == true || f3 == true) {
                return false;
            }
            return true;
        };

        $scope.addMembers = function (object) {
            if (validateInput() == false) return;

            if ((object.role == "") || (object.title == "") || (object.name == "")) {
                return;
            }

            $scope.membersOfficers.push(object);
            $scope.membersOfficer = { role: "", title: "", name: "" };
            $scope.refreshDropdowns();
        };

        $scope.editMembers = function (object, index) {
            $scope.editClicked = true;
            $scope.position = index;
            $scope.membersOfficer = angular.copy(object);
            $scope.refreshDropdowns();
        };

        $scope.deleteMembers = function (index) {
            $scope.editClicked = false;
            $scope.membersOfficers.splice(index, 1);
            $scope.membersOfficer = { role: "", title: "", name: "" };
            $scope.refreshDropdowns();
        };

        $scope.updateMembers = function (object) {
            if (validateInput() == false) return;

            if ((object.role == "") || (object.title == "") || (object.name == "")) {
                return;
            }
            $scope.editClicked = false;
            $scope.membersOfficer = { role: "", title: "", name: "" };
            $scope.membersOfficers.splice($scope.position, 1, object);
            $scope.refreshDropdowns();
        };

        $scope.clearMembers = function () {
            $scope.editClicked = false;
            $scope.membersOfficer = { role: "", title: "", name: "" };
            $scope.refreshDropdowns();
            if (validateInput() == false) return;
        };
        $scope.getDescription = function (list, key) {
            return wmgCommonService.findDescription(list, key);
        };
        //END - Members Officers Section validation

        //START - Marital Status validation
        //FSD-FR.178
        $scope.isMaritalStatusApplicable = function () {
            var prods = "PB251,PB252,PB25G,PB25H,PB30Y,PB31I,PB31K,PB34Z,PB36K";
            return wmgCommonService.isProductFound(prods);
        };
        //END - Marital Status validation

        //START - Security questions 
        //FSD-FR.77
        $scope.isSecurityWarningApplicable = function () {
            if (wmgCommonService.getApplicant("P") != undefined) {
                $scope.appBackgroundQuestions = wmgCommonService.getApplicant("P").backgroundInfoQuesList.backgroundInfoQues;

                if (($scope.appBackgroundQuestions.bankruptcyInd == 'Y') || ($scope.appBackgroundQuestions.bankruptcyInd == 'NP') || ($scope.appBackgroundQuestions.felonyInd == 'Y') || ($scope.appBackgroundQuestions.felonyInd == 'NP')
                    || ($scope.appBackgroundQuestions.lawsuitInd == 'Y') || ($scope.appBackgroundQuestions.lawsuitInd == 'NP')) {
                    return true;
                }
            }
            if (wmgCommonService.getApplicant("S") != undefined) {
                $scope.coAppBackgroundQuestions = wmgCommonService.getApplicant("S").backgroundInfoQuesList.backgroundInfoQues;
                if (($scope.coAppBackgroundQuestions.bankruptcyInd == 'Y') || ($scope.coAppBackgroundQuestions.bankruptcyInd == 'NP') || ($scope.coAppBackgroundQuestions.felonyInd == 'Y') || ($scope.coAppBackgroundQuestions.felonyInd == 'NP') || ($scope.coAppBackgroundQuestions.lawsuitInd == 'Y') || ($scope.coAppBackgroundQuestions.lawsuitInd == 'NP')) {
                    return true;
                }
            }
            return false;
        };
        $scope.isSecurityWarningApplicable();
        //END - Security questions 

        //START - Wisconsin Disclosure logic
        $scope.applyDisclosureLogic = function () {
            if ((Root.applicationInfo.isWIDisclosureApplicable == 'Y') && (($scope.applicant.maritalStatus == 'M') || ($scope.applicant.maritalStatus == 'S'))
                   || ((Root.applicationInfo.isWIDisclosureApplicable == 'Y') && (Root.applicationInfo.isCoAppAvailable == 'Y') && (($scope.applicant.maritalStatus == 'M') || ($scope.applicant.maritalStatus == 'S')))) {
                if ($scope.applicationInfo.isWIDisclosureConfirmed != "Y") {
                    goAheadService.setFlag(false);
                    $('#wisconsin').show();
                    var widthContainer = 750;
                    var bodyContainer = $('body').width();

                    $("#wisconsin .modalcontainer").css({ width: widthContainer + "px" });
                    var leftSpace = (bodyContainer - widthContainer) / 2;
                    $("#wisconsin .modalcontainer").css({ left: leftSpace + "px" });

                    setTimeout(function () {
                        svpApp.util.buildSelects();
                    }, 500);

                } else {
                    goAheadService.setFlag(true);
                }
            } else {
                goAheadService.setFlag(true);
            }
        };

        $scope.setWIDisclousure = function () {
            $scope.applicationInfo.isWIDisclosureConfirmed = "Y";
            $scope.wisconsinWarningPopupClose();
            $scope.moveNext(); //In IE it struks
        };
        $scope.wisconsinWarningPopupClose = function () {
            $('#wisconsin').hide();
        };
        //END - Wisconsin disclosure logic



        $scope.$on("CreditDetail", $scope.validateBeforeContinue);
        $scope.$on("CreditDetail", $scope.validateBeforeContinue = function (event, data) {
            goAheadService.setFlag(false);
            if ((wmgCommonService.isAumWarningApplicable($scope.applicationInfo.AUM) == true) && (wmgCommonService.aumWarningPopupShown == false)) {
                $("#aumWarningPopup").show();
                var widthContainer = 750;
                var bodyContainer = $('body').width();

                $("#aumWarningPopup .modalcontainer").css({ width: widthContainer + "px" });
                var leftSpace = (bodyContainer - widthContainer) / 2;
                $("#aumWarningPopup .modalcontainer").css({ left: leftSpace + "px" });

                setTimeout(function () {
                    svpApp.util.buildSelects();
                }, 500);

            } else {
                $scope.applyDisclosureLogic();
            }

            ////Add check to make sure if any product exists... 
            //var activeProducts = wmgCommonService.getAllActiveProducts();
            //if (activeProducts == undefined || activeProducts.length == 0) {
            //    goAheadService.setFlag(true);
            //    $scope._routedFromCDScreen.status = true;
            //    stateService.goToView(Enums.wmgViews.SubmitApp);
            //}
        });
    }]);