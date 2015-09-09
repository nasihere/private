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
              //  $urlRouterProvider.when("", "SubmitApp");
                
                
                $httpProvider.defaults.useXDomain = true;
                //$httpProvider.defaults.withCredentials = true;
                delete $httpProvider.defaults.headers.common['X-Requested-With'];
                $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                

                //$httpProvider.defaults.headers.common['Cache-Control'] = 'no-cache';
                //$httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

                //if (!$httpProvider.defaults.headers.get) {
                //    $httpProvider.defaults.headers.get = {};
                //}
                //console.log($httpProvider.defaults.headers.get['If-Modified-Since']);
                ////disable IE ajax request caching
                //$httpProvider.defaults.headers.get['If-Modified-Since'] = '1';

                $stateProvider
                    .state("CreditDetail", {
                        url: "/CreditDetail",
                        views: {
                            "": { templateUrl: "./Views/CreditDetail/CreditDetails.html" },

                            "BasicDetails@CreditDetail": { templateUrl: "./Views/CreditDetail/Compliance/Form.html" },
                            "BusinessType@CreditDetail": { templateUrl: "./Views/CreditDetail/BusinessTypeInformation/Form.html" },
                            "MembersOfficers@CreditDetail": { templateUrl: "./Views/CreditDetail/MembersOfOfficers/Form.html" },
                            "CustomerContacts@CreditDetail": { templateUrl: "./Views/CreditDetail/CustomerBankerContact/Form.html" },
                            "JointAuthority@CreditDetail": { templateUrl: "./Views/CreditDetail/JointAuthority/Form.html" },
                            "Comments@CreditDetail": { templateUrl: "./Views/CreditDetail/Comment/Form.html" }
                        },
                        controller: "complianceController"
                    })
                    .state("Products", {
                        abstract: true,
                        url: "/Products",
                        template: "<div ui-view=''></div>"
                    })
                    .state("Products.1", {
                        url: "/:productId",
                        views: {
                            "": { templateUrl: "./Views/ProductDetail/Product.html" },
                            "BasicInfo@Products.1": { templateUrl: "./Views/ProductDetail/BasicInfo.html" },

                            "Collaterals@Products.1": { templateUrl: "./Views/Collateral/Collateral.html" },
                            "MarketableSummary@Collaterals": { templateUrl: "./Views/Collateral/Marketable/SummaryTable.html" },
                            "MarketableForm@Collaterals": { templateUrl: "./Views/Collateral/Marketable/Form.html" },
                            "CDSavingsSummary@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/SummaryTable.html" },
                            "CDSavingsForm@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/Form.html" },

                            "ProductFeatures@Products.1": { templateUrl: "./Views/ProductFeatures/ProductFeatures.html" },
                            "ACH@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ACH/Form.html" },
                            "ODP@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ODP/Form.html" },
                            "Checks@ProductFeatures": { templateUrl: "./Views/ProductFeatures/Checks/Form.html" },

                            "BusinessPartnership@Products.1": { templateUrl: "./Views/BusinessPartnership/BusinessPartnership.html" },
                            "BusinessPartnershipForm@BusinessPartnership": { templateUrl: "./Views/BusinessPartnership/Form.html" },

                            "Comment@Products.1": { templateUrl: "./Views/ProductDetail/Comment/Form.html" },

                        },
                        controller: "ProductController"
                    })
                    .state("Products.2", {
                        url: "/:productId",
                        views: {
                            "": { templateUrl: "./Views/ProductDetail/Product.html" },
                            "BasicInfo@Products.2": { templateUrl: "./Views/ProductDetail/BasicInfo.html" },
                            "Collaterals@Products.2": { templateUrl: "./Views/Collateral/Collateral.html" },
                            "MarketableSummary@Collaterals": { templateUrl: "./Views/Collateral/Marketable/SummaryTable.html" },
                            "MarketableForm@Collaterals": { templateUrl: "./Views/Collateral/Marketable/Form.html" },
                            "CDSavingsSummary@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/SummaryTable.html" },
                            "CDSavingsForm@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/Form.html" },

                            "ProductFeatures@Products.2": { templateUrl: "./Views/ProductFeatures/ProductFeatures.html" },
                            "ACH@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ACH/Form.html" },
                            "ODP@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ODP/Form.html" },
                            "Checks@ProductFeatures": { templateUrl: "./Views/ProductFeatures/Checks/Form.html" },

                            "BusinessPartnership@Products.2": { templateUrl: "./Views/BusinessPartnership/BusinessPartnership.html" },
                            "BusinessPartnershipForm@Products.2": { templateUrl: "./Views/BusinessPartnership/Form.html" },

                            "Comment@Products.2": { templateUrl: "./Views/ProductDetail/Comment/Form.html" },
                        },
                        controller: "ProductController"
                    })
                    .state("Products.3", {
                        url: "/:productId",
                        views: {
                            "": { templateUrl: "./Views/ProductDetail/Product.html" },
                            "BasicInfo@Products.3": { templateUrl: "./Views/ProductDetail/BasicInfo.html" },
                            "Collaterals@Products.3": { templateUrl: "./Views/Collateral/Collateral.html" },
                            "MarketableSummary@Collaterals": { templateUrl: "./Views/Collateral/Marketable/SummaryTable.html" },
                            "MarketableForm@Collaterals": { templateUrl: "./Views/Collateral/Marketable/Form.html" },
                            "CDSavingsSummary@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/SummaryTable.html" },
                            "CDSavingsForm@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/Form.html" },

                            "ProductFeatures@Products.3": { templateUrl: "./Views/ProductFeatures/ProductFeatures.html" },
                            "ACH@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ACH/Form.html" },
                            "ODP@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ODP/Form.html" },
                            "Checks@ProductFeatures": { templateUrl: "./Views/ProductFeatures/Checks/Form.html" },

                            "BusinessPartnership@Products.3": { templateUrl: "./Views/BusinessPartnership/BusinessPartnership.html" },
                            "BusinessPartnershipForm@Products.3": { templateUrl: "./Views/BusinessPartnership/Form.html" },

                            "Comment@Products.3": { templateUrl: "./Views/ProductDetail/Comment/Form.html" },
                        },
                        controller: "ProductController"
                    })
                    .state("Products.4", {
                        url: "/:productId",
                        views: {
                            "": { templateUrl: "./Views/ProductDetail/Product.html" },
                            "BasicInfo@Products.4": { templateUrl: "./Views/ProductDetail/BasicInfo.html" },
                            "Collaterals@Products.4": { templateUrl: "./Views/Collateral/Collateral.html" },
                            "MarketableSummary@Collaterals": { templateUrl: "./Views/Collateral/Marketable/SummaryTable.html" },
                            "MarketableForm@Collaterals": { templateUrl: "./Views/Collateral/Marketable/Form.html" },
                            "CDSavingsSummary@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/SummaryTable.html" },
                            "CDSavingsForm@Collaterals": { templateUrl: "./Views/Collateral/CDSaving/Form.html" },

                            "ProductFeatures@Products.4": { templateUrl: "./Views/ProductFeatures/ProductFeatures.html" },
                            "ACH@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ACH/Form.html" },
                            "ODP@ProductFeatures": { templateUrl: "./Views/ProductFeatures/ODP/Form.html" },
                            "Checks@ProductFeatures": { templateUrl: "./Views/ProductFeatures/Checks/Form.html" },

                            "BusinessPartnership@Products.4": { templateUrl: "./Views/BusinessPartnership/BusinessPartnership.html" },
                            "BusinessPartnershipForm@BusinessPartnership": { templateUrl: "./Views/BusinessPartnership/Form.html" },

                            "Comment@Products.4": { templateUrl: "./Views/ProductDetail/Comment/Form.html" },
                        },
                        controller: "ProductController"
                    })
                    .state("RECollateral", {
                        url: "/RECollateral",
                        views: {
                            "": { templateUrl: "./Views/RECollateral/RECollateral.html" },
                            "CollateralProperty@RECollateral": { templateUrl: "./Views/RECollateral/CollateralProperty/Form.html" },
                            "MortgageCollateral@RECollateral": { templateUrl: "./Views/RECollateral/ExistingMortgageCollateralProperty/Form.html" },
                            "HomeownerInsurance@RECollateral": { templateUrl: "./Views/RECollateral/HomeownerInsurance/Form.html" },
                            "HousingExpenses@RECollateral": { templateUrl: "./Views/RECollateral/HousingExpenses/Form.html" },
                            "Comment@RECollateral": { templateUrl: "./Views/RECollateral/Comment/Form.html" },
                        },

                        controller: "RECollateralController"
                    })
                    .state("PersonalFin", {
                        url: "/PersonalFin",
                        views: {
                            "": { templateUrl: "./Views/PersonalFinance/PersonalFinance.html" },
                            "BasicDetails@PersonalFin": { templateUrl: "./Views/PersonalFinance/BasicDetails/Form.html" },
                            "AssetLiabilities@PersonalFin": { templateUrl: "./Views/PersonalFinance/AssetLiabilities/Form.html" },
                            "Schedule2Form@PersonalFin": { templateUrl: "./Views/PersonalFinance/Schedule2/Form.html" },
                            "Schedule3MainForm@PersonalFin": { templateUrl: "./Views/PersonalFinance/Schedule3/MainForm.html" },
                            "Schedule3Summary@PersonalFin": { templateUrl: "./Views/PersonalFinance/Schedule3/SummaryTable.html" },
                            "Schedule3Form@PersonalFin": { templateUrl: "./Views/PersonalFinance/Schedule3/Form.html" }
                        },
                        controller: "PersonalFinanceController"
                    })
                    .state("Hmda", {
                        url: "/Hmda",
                        views: {
                            "": { templateUrl: "./Views/Hmda/Hmda.html" },
                            "WealthHmdaInfo@Hmda": { templateUrl: "./Views/HMDA/WealthHmdaInfo/Form.html" },
                            "DisclosureReadCust@Hmda": { templateUrl: "./Views/HMDA/DisclosureReadCust/Form.html" },
                            "InformationBanker@Hmda": { templateUrl: "./Views/HMDA/InformationBanker/Form.html" },
                            "ApplicantInformation@Hmda": { templateUrl: "./Views/HMDA/ApplicantInformation/Form.html" },
                        },
                        controller: "HMDAController"
                    })
                    .state("Kyc", {
                        url: "/Kyc",
                        views: {
                            "": { templateUrl: "./Views/Kyc/Kyc.html" },
                            "KycInformation@Kyc": { templateUrl: "./Views/KYC/KYCInformation/Form.html" }
                        },
                        controller: "KYCController"
                    })
                    .state("Summary", {
                        url: "/Summary",
                        templateUrl: "./Views/Summary/SummaryJson.html",
                        controller: "SummaryController"
                    })
                    .state("SubmitApp", {
                        url: "/SubmitApp",
                        templateUrl: "./Views/SubmitApp/SubmitApp.html",
                        controller: ""
                    })
                 .state("ProcessApp", {
                     url: "/ProcessApp",
                     templateUrl: "./Views/ProcessApp/ProcessApp.html",
                     controller: ""
                 })
                    .state("Error", {
                        url: "/Error",
                        templateUrl: "./Views/ApplicationError/Error.html",
                        controller: "applicationErrorController"
                    });
            }]
    );

//This is the startup method will initialize the data
wmg.run(function (wmgDataService) {
    angB = new Date();
    //console.log("Before service Call");
    //console.log(date);
    wmgDataService.setData();
    angA = new Date();
    //console.log(date);

    //CommonNew = wmgDataService.readStaticDataFile();
    //console.log(CommonNew);
});

//If any views are added above the names of that aadded view should be added here also in this Enum Service.
//Also follow the sequence in the order you want to show the views by providing the number sequence.
wmg.factory("Enums", function () {
    return {
        wmgViews: {
            CreditDetail: 0,
            Product1: 1,
            Product2: 2,
            Product3: 3,
            Product4: 4,
            RECollateral: 5,
            PersonalFinance: 6,
            Hmda: 7,
            Kyc: 8,
            Summary: 9,
            SubmitApp: 11,
            ProcessApp: 10,
            Error: 12
        }
    };
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
        { viewName: "SubmitApp", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Submit Application", param: {} },
          { viewName: "ProcessApp", visible: true, groupTitle: "", breadCrumbTitle: "Decision", param: {} },
        { viewName: "Error", visible: true, groupTitle: "", breadCrumbTitle: "Application Error", param: {} }
    ];
});