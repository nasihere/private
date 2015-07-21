<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <!-- Meta Tag -->
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
    <body class="q1" data-ng-app="wmg" data-ng-controller="baseController as base">

        
        <div class="container" style="width: 1200px;">
            <div>
                <ul class="nav nav-tabs nav-tabs-top ng-scope">
                    <li breadcrumb-directive ng-repeat="input in routingStates | filter:'MainPage'"></li>

                </ul>
            </div>
            <br/>
            <div data-ui-view=""></div>
        </div>

        <footer class="clearfix">
            <div class="subnavbar">
                <div class="subnav-inner">
                    <div style="text-align: center">
                        <a class="btn btn-primary" ng-hide="currentPage == 0" ng-click="movePrevious()">Previous</a>
                        <a class="btn btn-primary" href="javascript:void(0)">Save</a>
                        <a class="btn btn-primary" ng-click="moveNext()">Continue</a>
                    </div>
                </div>
            </div>
        </footer>
        <!--<img src="./RetailBanking/Wealth/Content/Images/SVP_Footer.PNG" />-->
        <div ng-include src="'/RetailBanking/Wealth/Views/TestNDebug/JsonStatus.html'"></div>


        <!-- ========================================================================================================================= -->

        <!--Framework Libraries-->
        <script src="/Global/Scripts/Lib/angularjs 1.2.24/angular.js"></script>
        <script src="/Global/Scripts/Lib/angularjs 1.2.24/angular-ui-router.js"></script>
        <script src="/Global/Scripts/Lib/angularjs 1.2.24/angular-cookies.js"></script>

        <!--The below is required since UI-Router used in IE8-->
        <script src="/Global/Scripts/Lib/svp/es5-shim-min.js"></script>

        <!--CSS StyleSheets-->
        <link href="/Global/Content/Css/Bootstrap/bootstrap.min.css" rel="stylesheet" />
        <link href="/Global/Content/Css/Bootstrap/ie.css" rel="stylesheet" />
        <link href="/Global/Content/Css/Bootstrap/plugin.css" rel="stylesheet" />


        <link href="https://docs.cci.wellsfargo.com/prototype/inreview/2015Stage1ARepo/_assets/css/global.css" rel="stylesheet" />


        <link href="/RetailBanking/Wealth/Content/Css/custom.css" rel="stylesheet" />

        <link href="/Global/Content/Css/Bootstrap/datepicker3.css" rel="stylesheet" />
        <!--Resources-->
        <script src="/RetailBanking/Wealth/Scripts/Main.js"></script>
        <!--Controllers-->
        <script src="/RetailBanking/Wealth/Scripts/Controllers/BaseController/BaseXML.js"></script>

        <script src="/RetailBanking/Wealth/Scripts/Services/wmgCommonService.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/StaticData/StaticData.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/Product/ProductController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/BaseController/BaseController.js"></script>

        <script src="/RetailBanking/Wealth/Scripts/Controllers/Compliance/ComplianceController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/Product/ProductController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/PersonalFinance/PersonalFinanceController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/RECollateral/RECollateralController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/KYC/KYCController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/HMDA/HMDAController.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Controllers/Summary/SummaryController.js"></script>

        <!--Custom Directives-->
        <script src="/RetailBanking/Wealth/Scripts/Controllers/BaseController/myDirective.js"></script>
        <!--Services-->
        <script src="/RetailBanking/Wealth/Scripts/Services/APIService.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Services/XmlDataService.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Services/Navigator.js"></script>
        <script src="/RetailBanking/Wealth/Scripts/Services/StateService.js"></script>

        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
            <script src="https://docs.cci.wellsfargo.com/prototype/inreview/SVP2Q2Repo/_assets/js/html5shiv.js"></script>
            <script src="https://docs.cci.wellsfargo.com/prototype/inreview/SVP2Q2Repo/_assets/js/respond.min.js"></script>
        <![endif]-->

        <!--[if lte IE 8]>
            <script>
                document.createElement('my-product');
                document.createElement('ng-include');
                document.createElement('data-ng-include');
            </script>
        <![endif]-->

        <!-- SVP Libraries -->
        <script src="/Global/Scripts/Lib/jQuery/jquery-1.9.0.js" type="text/javascript"></script>
        <script src="/Global/Scripts/Lib/svp/global.js" type="text/javascript"></script>
        <script src="/Global/Scripts/Lib/svp/svp_app.js" type="text/javascript"></script>
        <script src="/Global/Scripts/Lib/svp/bootstrap-datepicker.js" type="text/javascript"></script>


        <script src="/Global/Scripts/Lib/SVP/bootstrap.min.js"></script>

        <script>
            //setTimeout(function () {
            //    //     svpApp.init();
            //    $("#myModal").draggable({
            //        handle: ".modal-header"
            //    });
            //    //document.getElementById("xml").innerHTML = '<?xml version="1.0" encoding="UTF-8"?><root><AUM>150000</AUM><backgroundInfoQues1>N</backgroundInfoQues1><backgroundInfoQues2>N</backgroundInfoQues2><backgroundInfoQues3>N</backgroundInfoQues3><comment>need loan for studies</comment><isCoAppAvailable>Y</isCoAppAvailable><isPrivateBanker>Y</isPrivateBanker><jointApplicationApproval>Y</jointApplicationApproval><languagePref>E</languagePref><productList><product><element><BasicAmortization>9009</BasicAmortization><BasicAmountRequired>28000</BasicAmountRequired><BasicBalloonOption>15</BasicBalloonOption><BasicDesiredClosingDate>12/05/2014</BasicDesiredClosingDate><BasicMarginIndex>1</BasicMarginIndex><BasicPaymentFrequency>M</BasicPaymentFrequency><BasicPaymentType>1</BasicPaymentType><BasicPreferredPaymentDueDate>2</BasicPreferredPaymentDueDate><BasicPurposeDescription>Loan for studies</BasicPurposeDescription><BasicPurposeName>HIMP</BasicPurposeName><BasicStateLoanWillCloseIn>CA</BasicStateLoanWillCloseIn><BasicTerm>First Priority</BasicTerm><RECollateral /><isACHApplicable>Y</isACHApplicable><isChkApplicable>N</isChkApplicable><isNonRECollateralApplicable>N</isNonRECollateralApplicable><isODPApplicable>Y</isODPApplicable><isPerFinStmtApplicable>Y</isPerFinStmtApplicable><isRECollateralApplicable>N</isRECollateralApplicable><nonRECollateral /><productFeatures><ACHData><element /></ACHData><ODPData><element /></ODPData></productFeatures><productID>PB31I</productID><productName>Secured Loan Product</productName></element><element><BasicAmortization>9858</BasicAmortization><BasicAmountRequired>985841</BasicAmountRequired><BasicBalloonOption>30</BasicBalloonOption><BasicDesiredClosingDate>05/09/2015</BasicDesiredClosingDate><BasicMarginIndex>1</BasicMarginIndex><BasicPaymentFrequency>M</BasicPaymentFrequency><BasicPaymentType>1</BasicPaymentType><BasicPreferredPaymentDueDate>1</BasicPreferredPaymentDueDate><BasicPurposeDescription>Loan For Home</BasicPurposeDescription><BasicPurposeName>LOTL</BasicPurposeName><BasicStateLoanWillCloseIn>AZ</BasicStateLoanWillCloseIn><BasicTerm>Second Priority</BasicTerm><RECollateral /><isACHApplicable>N</isACHApplicable><isChkApplicable>N</isChkApplicable><isNonRECollateralApplicable>Y</isNonRECollateralApplicable><isODPApplicable>N</isODPApplicable><isPerFinStmtApplicable>N</isPerFinStmtApplicable><isRECollateralApplicable>N</isRECollateralApplicable><nonRECollateral /><productFeatures><ACHData><element /></ACHData><ODPData><element /></ODPData></productFeatures><productID>PB34Z</productID><productName>Unsecured Loan</productName></element><element><BasicAmortization>1584</BasicAmortization><BasicAmountRequired>154251</BasicAmountRequired><BasicBalloonOption>30</BasicBalloonOption><BasicDesiredClosingDate>04/03/2016</BasicDesiredClosingDate><BasicMarginIndex>1</BasicMarginIndex><BasicPaymentFrequency /><BasicPaymentType>1</BasicPaymentType><BasicPreferredPaymentDueDate>1</BasicPreferredPaymentDueDate><BasicPurposeDescription>Loan For Furniture</BasicPurposeDescription><BasicPurposeName>CTOP</BasicPurposeName><BasicStateLoanWillCloseIn>CO</BasicStateLoanWillCloseIn><BasicTerm>Third Priority</BasicTerm><RECollateral /><isACHApplicable>Y</isACHApplicable><isChkApplicable>Y</isChkApplicable><isNonRECollateralApplicable>Y</isNonRECollateralApplicable><isODPApplicable>Y</isODPApplicable><isPerFinStmtApplicable>N</isPerFinStmtApplicable><isRECollateralApplicable>Y</isRECollateralApplicable><nonRECollateral /><productFeatures><ACHData><element /></ACHData><ODPData><element /></ODPData></productFeatures><productID>F839S</productID><productName>PrimeLine</productName></element></product></productList></root>';
            //}, 1000);



            setParentIFrameSize = function () {
                setTimeout(function () {
                    var size = $("body").height();
                    parent.postMessage(size, "*");
                }, 900);
            };

            //setParentIFrameSize = function () {
            //    setTimeout(function () {
            //        $(window).ready(function () {
            //            parent.setFrameSize($("body").height() + 5 + "px");
            //        });
            //    }, 500);
            //};
            setParentIFrameSize();
        </script>
    </body>

</html>

