
var wmg = angular.module("wmg", ['ui.router', 'ngCookies']);
//
//If you are adding new states/views follow the below Step
//1. Add the new view in the $stateProvider config service
//2. Add the Enum for the new view in the below service (Enums)
//3. Add the new view name as it is givien in the $stateRouteProvider in 'routingStates' service.
//Note: All these services are available in tihs main.js file. We need to add this without fail. Missing this will cause error in State Routing


wmg.config(["$stateProvider", "$urlRouterProvider", "$httpProvider",
            function ($stateProvider, $urlRouterProvider, $httpProvider) {

                $urlRouterProvider.when("", "CreditDetail");

                $httpProvider.defaults.useXDomain = true;
                //$httpProvider.defaults.withCredentials = true;
                delete $httpProvider.defaults.headers.common['X-Requested-With'];
                $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';



                $stateProvider
                    .state("CreditDetail", {
                        url: "/CreditDetail",
                        views: {
                            "": { templateUrl: "./XXXXXX.html" },

                        },
                        controller: "complianceController"
                    })
                    
                    .state("RECollateral", {
                        url: "/RECollateral",
                        views: {
                            "": { templateUrl: ".XYZ.html" },
                           
                        },

                        controller: "RECollateralController"
                    })
                    .state("Summary", {
                        url: "/Summary",
                        templateUrl: ".XXXXXXXXXXXXX.html",
                        controller: "SummaryController"
                    })
                    .state("SubmitApp", {
                        url: "/SubmitApp",
                        templateUrl: ".XXXXXXXXXXXXX.html",
                        controller: ""
                    })
                    .state("Error", {
                        url: "/Error",
                        templateUrl: ".XXXXXXXXXXXXXr.html",
                        controller: "applicationErrorController"
                    });
            }]
    );

//This is the startup method will initialize the data
wmg.run(function (wmgDataService) {
   
   
});



//This is the actual routing for the navigation, if we add states/modify states the traversal path of the application will change
//This should match with the Eum as well as the state we need to transition
wmg.factory("routingStates", function () {
    return views = [
        { viewName: "CreditDetail", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Credit Detail", param: {} },
        { viewName: "Products.1", visible: true, groupTitle: "", breadCrumbTitle: "Product 1", param: {} },
        { viewName: "Products.2", visible: true, groupTitle: "", breadCrumbTitle: "Product 2", param: {} },
        { viewName: "Products.3", visible: true, groupTitle: "", breadCrumbTitle: "Product 3", param: {} },
        { viewName: "Products.4", visible: true, groupTitle: "", breadCrumbTitle: "Product 4", param: {} },
        { viewName: "RECollateral", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Real Estate Collateral", param: {} },
        { viewName: "PersonalFin", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Personal Finance", param: {} },
        { viewName: "Hmda", visible: false, groupTitle: "MainPage", breadCrumbTitle: "HMDA", param: {} },
        { viewName: "Kyc", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Know your customer", param: {} },
        { viewName: "Summary", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Applicant Summary", param: {} },
        { viewName: "SubmitApp", visible: true, groupTitle: "", breadCrumbTitle: "Submit Application", param: {} },
        { viewName: "Error", visible: true, groupTitle: "", breadCrumbTitle: "Application Error", param: {} }
    ];
});




