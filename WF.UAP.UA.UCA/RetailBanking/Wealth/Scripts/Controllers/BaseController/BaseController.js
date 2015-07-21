
wmg.controller('baseController', ['$scope', 'stateService', 'Enums', 'goAheadService', 'appInitService', 'routingStates', 'APIService', '$http', 'wmgDataService', 'AccordionGate', '$timeout', 'wmgCommonService',
    function ($scope, stateService, Enums, goAheadService, appInitService, routingStates, APIService, $http, wmgDataService, AccordionGate, $timeout, wmgCommonService) {


        // - Nasir : Purpose is to see the debug message on Top.. Becasue many time IE does not support Console.log so we created own console.log for run time debugging purpose...
        $scope.NC = nW;

        $scope.Local = isLocalDebug();
        


        $scope.fotterButtons = fotterButtons;


        //:::IMPORTANT:::START - The below method/condition is the one will decide to configure the 
        //Application setup based on the error exists or not
        if (wmgDataService.applicationError == false) {
            appInitService.appInitialize();
            appInitService.checkIfKycApplicable();
        } else if (wmgDataService.applicationError == true) {
            $scope._appErrorStatus = true;
            $scope._wmgAppErrorInfo = wmgDataService.errorInfo;
            return;          // ---> the reason for return is we no need to configure the BaseController if any error, this will go to Error page.
        }
        //:::IMPORTANT:::END - The below method/condition is the one will decide to configure the 




        //Global Variables
        $scope._isChecksApplicable = false;
        $scope._isProductFeaturesApplicable = false;
        $scope._isRECollateralApplicable = { status: false };
        $scope._isPersonalFinanceApplicable = false;
        $scope._isHmdaApplicable = { status: false };
        $scope._isCollateral = { given: true };
        $scope._appErrorStatus = false; //This is global status can be used in any controller (Read Only)
        $scope._routedFromCDScreen = { status: false };
        $scope._showAppRaceEthnicity = { status: false };
        $scope._showCoAppRaceEthnicity = { status: false };

        //Global Initialization
        $scope.common = Common;                  //Common items example: yes/no, dropdown
        $scope.jsonPreview = Root;
        $scope.SessionIDPreview = sessionIdData; // For debug purpose
        $scope.failurePreview = pageError;
       
        $scope.routingStates = stateService.getAllViews(true); //setting as 'true' will return products header. for the breadcrumb list item needed
       
        $scope.CurrentViewName = stateService.getCurrentView();

        //Set the Saved Screen
        if ((Root != undefined) && (Root != "")) {
            $scope.savedAppScreenName = Root.applicationInfo.savedScreen;
        }

        //Check if saved app exists
        $scope.isSavedAppLoadRequired = function () {
            if ($scope.savedAppScreenName == "" || $scope.savedAppScreenName == undefined) {
                return false;
            } else {
                return true;
            }
        };

        //This even will load the saved app and route to that view
        $scope.savedAppLoaded = false;
        $scope.$on('$stateChangeSuccess', function (event, toState) {
            if ((callIsFromSCSPrevious == "Y") && (wmgDataService.applicationError == false)) {
                stateService.goToView(Enums.wmgViews.Summary);
                callIsFromSCSPrevious = "N";
                $scope.savedAppScreenName = "";

            } else if ($scope.isSavedAppLoadRequired() == true) {
                if ($scope.savedAppLoaded == false) {
                    for (var i = 0; i < routingStates.length; i++) {
                        if (routingStates[i].viewName.toUpperCase() == $scope.savedAppScreenName.toUpperCase()) {
                            stateService.goToView(i);
                            $scope.CurrentViewName = stateService.getCurrentView();
                            $scope.savedAppLoaded = true;
                            break;
                        }
                    }
                }
            }

            $scope.CurrentViewName = stateService.getCurrentView();
        });

        //Check HMDA Screen we need to show or not during load
        $scope.checkIfHmdaApplicable = function () {
            var result = wmgCommonService.isHmdaApplicableForTheExistingProducts();
            if (result == true) {
                stateService.enableView(Enums.wmgViews.Hmda);
                $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            } else {
                stateService.disableView(Enums.wmgViews.Hmda);
                $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            }
        };
        $scope.checkIfHmdaApplicable();

        //Method to Move screen to next Debug Purpose location
        $scope.moveForceNext = function () {
            // wmgCommonService.getCursorTop();
            //  $scope.$broadcast(stateService.getCurrentView());
            // if (goAheadService.canProceed() == true) {
            stateService.moveNext();
            // }
            $scope.CurrentViewName = stateService.getCurrentView();
        };

        //Method to Move screen to next location
        $scope.moveNext = function () {
            wmgCommonService.getCursorTop();
            $scope.$broadcast(stateService.getCurrentView());
            if (goAheadService.canProceed() == true) {
                stateService.moveNext();
            }
            $scope.CurrentViewName = stateService.getCurrentView();
        };

        //Method to Move screen to previous location
        $scope.movePrevious = function () {
            pageError.errorSummary = [];
            wmgCommonService.getCursorTop();
            stateService.movePrevious();
            $scope.CurrentViewName = stateService.getCurrentView();

        };

        $scope.moveSummary = function () {
            if (wmgDataService.applicationError == false) {
                stateService.goToView(Enums.wmgViews.Product1);
            }
        };

        //Method to Save application
        $scope.saveApplication = function () {
            Root.applicationInfo.savedScreen = stateService.getCurrentView();

            APIService.SaveData(function (data) {
                if (data.Errors == null) {
                    var wealthData = { "wealthData": { "isAppSubmitted": "N", "isAppSaved": "Y" } };
                    try {
                        document.domain = "wellsfargo.com";
                        parent.ContinueOnWealth(sessionIdData, wealthData);
                    } catch (e) {
                        alert("Error in redirecting to SCS screen " + e);
                    }
                }
            });

           

        };

        $scope.verifyForm = function () {
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            AccordionGate.setStatus(1);
            $timeout(pageValidator, 500).then(function (result) {
                if (result == true) {
                    $scope.moveNext();
                }
            });
        };

       
    }]);







