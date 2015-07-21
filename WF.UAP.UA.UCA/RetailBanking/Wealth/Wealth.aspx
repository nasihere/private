<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wealth.aspx.cs" Inherits="WF.UAP.UA.UCA.RetailBanking.Wealth.Wealth" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <!--script  type="text/cjs">
        document.domain = "wellsfargo.com";
	</!--script!-->
    <!-- Meta Tag -->
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body class="q1" data-ng-app="wmg" data-ng-controller="baseController as base">

    <%--<p>UCARouter Before: <b class="bolded">{{ucaB}}</b> <br/>UCARouter After: <b class="bolded">{{ucaA}}</b><br/>Wealth.aspx loaded at: <b>{{wh}}</b>  AngularLoaded At: <b>{{dt}}</b></p>--%>
    <div class="container " style="width: 1200px;">
        <div ng-hide="_appErrorStatus == true">
            <ul class="wmgheader nav nav-tabs nav-tabs-top ng-scope">

                <li style="width: 3px"><a href="javascript:void(0)">&nbsp;</a></li>
                <li wmg-breadcrumb-directive ng-repeat="input in routingStates | filter:'MainPage'"></li>
            </ul>
        </div>
        <br />
        <!-- div ng-repeat="item in failurePreview.errorSummary | filter:{errorMsg: '!!'} "!-->
        <div ng-if="failurePreview.errorSummary.length > 0">
            <div class="alert alert-information ng-scope" wmgautoheight style="max-width: 100%; margin-right: 8px;">
                <p>Please provide the required information highlighted in red</p>
            </div>
        </div>
        <br />
        <div ng-show="_isCollateral.given == false">
            <div class="alert alert-information ng-scope" style="max-width: 100%; margin-right: 8px;">
                <p>Atleast one deposit or marketable collateral must be entered</p>
            </div>
        </div>
        <br />
        <div data-ui-view=""></div>
    </div>

    <footer class="clearfix" ng-hide="_appErrorStatus == true">
        <div class="subnavbar">
            <div class="subnav-inner">
                <div style="text-align: center">
                    <a class="btn btn-primary" ng-hide="CurrentViewName == 'CreditDetail'" ng-click="movePrevious()">Previous</a>
                    <a class="btn btn-primary" ng-click="saveApplication()">Save</a>
                    <a class="btn btn-primary" ng-click="verifyForm()">Continue</a>
                    <a href="javascript:;" onclick="$('#sideMenu').removeClass('ng-hide')">.</a>

                </div>
            </div>
        </div>
    </footer>
    <!--
    <div class="alignright">
        <a href="" ng-click="toggle = !toggle">_</a>
    </div>
    <div ng-show="toggle == true" style="vertical-align: central">
        <table class="table table-condensed" style="width: 500px;">
            <tr>
                <td class="font16 bolded">Service/Page Name</td>
                <td class="font16 bolded">Loaded at time</td>
                <td class="font16 bolded">Time taken</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3"><span class="font16 bolded">Token Service</span></td>
            </tr>
            <tr>
                <td>UCARouter Before:</td>
                <td>{{ucaB}}</td>
                <td></td>
            </tr>
            <tr>
                <td>UCARouter After:</td>
                <td>{{ucaA}}</td>
                <td>{{calculateDiff(ucaA, ucaB)}}</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3"><span class="font16 bolded">Json Data Service</span></td>
            </tr>
            <tr>
                <td>Wealth.aspx Before:</td>
                <td>{{wealthB}}</td>
                <td>{{calculateDiff(wealthB, ucaA)}}</td>
            </tr>
            <tr>
                <td>Wealth.aspx After:</td>
                <td>{{wealthA}}</td>
                <td>{{calculateDiff(wealthA, wealthB)}}</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3"><span class="font16 bolded">In Angular page</span></td>
            </tr>
            <tr>
                <td>Before Root Obj Set:</td>
                <td>{{angB}}</td>
                <td>{{calculateDiff(angB, wealthA)}}</td>
            </tr>
            <tr>
                <td>After Root Obj Set:</td>
                <td>{{angA}}</td>
                <td>{{calculateDiff(angA, angB)}}</td>
            </tr>
            <tr>
                <td>BaseController Loaded at:</td>
                <td>{{baseLoadedAt}}</td>
                <td>{{calculateDiff(baseLoadedAt, angA)}}</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2"><span class="font16 bolded">Overal all Time Taken</span></td>
                <td><span class="font16 bolded">{{calculateDiff(baseLoadedAt, ucaB)}}</span></td>
            </tr>

        </table>
    </div>
        -->
    <div ng-include src="'./Views/TestNDebug/JsonStatus.html'"></div>

    <!-- ========================================================================================================================= -->

    <!--Framework Libraries-->
    <script src="../../Global/Scripts/Lib/jQuery/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Global/Scripts/Lib/angularjs 1.2.24/angular.js"></script>
    <script src="../../Global/Scripts/Lib/angularjs 1.2.24/angular-ui-router.js"></script>
    <script src="../../Global/Scripts/Lib/angularjs 1.2.24/angular-cookies.js"></script>

    <script src="../../Global/Scripts/Lib/svp/bootstrap-datepicker.js" type="text/javascript"></script>
    <!--The below is required since UI-Router used in IE8-->
    <script src="../../Global/Scripts/Lib/svp/es5-shim-min.js"></script>
    <script src="../../Global/Scripts/Lib/svp/css3-mediaqueries.js"></script>

    <!--CSS StyleSheets-->
    <link href="../../Global/Content/Css/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Global/Content/Css/Bootstrap/ie.css" rel="stylesheet" />
    <link href="../../Global/Content/Css/Bootstrap/plugin.css" rel="stylesheet" />
    <link href="../../Global/Content/Css/Bootstrap/datepicker3.css" rel="stylesheet" />
    <link href="../../Global/Content/Css/Bootstrap/global.css" rel="stylesheet" />
    <link href="Content/Css/custom.css" rel="stylesheet" />


    <!--Resources-->
    <script src="Scripts/main.js"></script>
    <script src="Scripts/Controllers/BaseController/BaseXML.js"></script>
    <script src="Scripts/StaticData/StaticData.js"></script>

    <!--Services-->


    <!--Controllers-->
    <script src="Scripts/Directives/Validation.js"></script>
    <script src="Scripts/Controllers/Product/ProductController.js"></script>
    <script src="Scripts/Controllers/BaseController/BaseController.js"></script>
    <script src="Scripts/Controllers/Compliance/ComplianceController.js"></script>
    <script src="Scripts/Controllers/Product/ProductController.js"></script>
    <script src="Scripts/Controllers/PersonalFinance/PersonalFinanceController.js"></script>
    <script src="Scripts/Controllers/RECollateral/RECollateralController.js"></script>
    <script src="Scripts/Controllers/KYC/KYCController.js"></script>
    <script src="Scripts/Controllers/HMDA/HMDAController.js"></script>
    <script src="Scripts/Controllers/Summary/SummaryController.js"></script>
    <script src="Scripts/Controllers/ApplicationError/ApplicationErrorController.js"></script>
    <script src="Scripts/Controllers/SubmitApp/SubmitAppController.js"></script>

    <!--Custom Directives-->
    <script src="Scripts/Directives/Accordion.js"></script>
    <script src="Scripts/Controllers/BaseController/myDirective.js"></script>
    <script src="Scripts/Directives/Component.js"></script>


    <!--Services-->
    <script src="Scripts/Services/APIService.js"></script>
    <script src="Scripts/Services/XmlDataService.js"></script>
    <script src="Scripts/Services/Navigator.js"></script>

    <script src="Scripts/Services/StateService.js"></script>
    <script src="Scripts/Services/AppInitService.js"></script>
    <script src="Scripts/Services/CommonService.js"></script>
    <script src="Scripts/Services/DataService.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="../../Global/Scripts/Lib/SVP/html5shiv.js"></script>
	  <script src="../../Global/Scripts/Lib/SVP/respond.min.js"></script>

        <![endif]-->

    <!--[if lte IE 8]>
            <script>
                document.createElement('my-product');
                document.createElement('ng-include');
                document.createElement('data-ng-include');
            </script>
        <![endif]-->

    <!-- SVP Libraries -->

    <script src="../../Global/Scripts/Lib/svp/global.js" type="text/javascript"></script>
    <script src="../../Global/Scripts/Lib/svp/svp_app.js?v=2" type="text/javascript"></script>
    <script src="../../Global/Scripts/Lib/svp/bootstrap-datepicker.js" type="text/javascript"></script>




    <script src="../../Global/Scripts/Lib/SVP/bootstrap.min.js"></script>
</body>
<script type="text/javascript">
    try {
        document.domain = "wellsfargo.com";
        parent.hideWaitMsg();

    } catch (e) {
    }
</script>
<script type="text/javascript">
    function getInitialData() {
        var initData = '<%=isWmgInitialData%>';
        if (initData == 'Y') {
            var jsonInitData = parent.document.getElementById("ScsNavigator_wealthCreditDetailCtl_WealthAppData").value;
            //var jsonInitData = {
            //    "applicationInfo": {
            //        "isCoAppAvailable": "Y",
            //        "isTPBInd": "N",
            //        "isSavedApp": "N",
            //        "savedScreen": "",
            //        "AUM": "1000000",
            //        "jointApplicationApprovalInd": "N",
            //        "isNonSpouseAvailable": "",
            //        "isWIDisclosureApplicable": "Y",
            //        "isWIDisclosureConfirmed": "",
            //        "isAMLKYCApplicable": "Y",
            //        "languagePref": "ENG",
            //        "applicantInfoList": [
            //            {
            //                "applicantType": "P",
            //                "applicantName": "CESAR DAVIS",
            //                "applicationMethod": "F",
            //                "inPersonInd": "Y",
            //                "backgroundInfoQuesList": {
            //                    "backgroundInfoQues": {
            //                        "bankruptcyInd": "N",
            //                        "felonyInd": "N",
            //                        "lawsuitInd": "N"
            //                    }
            //                },
            //                "HMDA": {
            //                    "Observation": "",
            //                    "ethnicity": "",
            //                    "gender": "",
            //                    "race1": "",
            //                    "race2": "",
            //                    "race3": "",
            //                    "race4": "",
            //                    "race5": "",
            //                    "race6": ""
            //                },
            //                "email": "lory.paul-jones@wellsfargo.com",
            //                "maritalStatus": "M",
            //                "domesticPartnerIndicator": ""
            //            },
            //            {
            //                "applicantType": "S",
            //                "applicantName": "KAMESH",
            //                "applicationMethod": "P",
            //                "inPersonInd": "N",
            //                "backgroundInfoQuesList": {
            //                    "backgroundInfoQues": {
            //                        "bankruptcyInd": "N",
            //                        "felonyInd": "N",
            //                        "lawsuitInd": "N"
            //                    }
            //                },
            //                "HMDA": {
            //                    "Observation": "",
            //                    "ethnicity": "",
            //                    "gender": "",
            //                    "race1": "",
            //                    "race2": "",
            //                    "race3": "",
            //                    "race4": "",
            //                    "race5": "",
            //                    "race6": ""
            //                },
            //                "email": "lory.paul-jones@wellsfargo.com",
            //                "maritalStatus": "M",
            //                "domesticPartnerIndicator": ""
            //            }
            //        ],
            //        "additionalContact": {
            //            "name": "",
            //            "phone": "",
            //            "email": ""
            //        },
            //        "preferredTimeToCall": "",
            //        "applicantPhone": "",
            //        "businessTypeInfo": {
            //            "trustIndividualCredit": "Y",
            //            "businessTaxId": "",
            //            "businessName": "",
            //            "businessType": "",
            //            "membersOrOfficersAll": "",
            //            "guarantor": "",
            //            "membersOrOfficersCorp": "",
            //            "secretary": "",
            //            "membersOfficersList": []
            //        },
            //        "bankerInfo": {
            //            "salesAgentUID": "gtst6068",
            //            "bankerName": "CHRIS WRIGHT",
            //            "officerID": "A0248",
            //            "location": "00014",
            //            "au": "4187",
            //            "bankerPhone": "7012322082",
            //            "comment": ""
            //        },
            //        "WealthData": {
            //            "productList": [
            //                {
            //                    "HMDA": {
            //                        "proceedsHomeImpAmount": "",
            //                        "propertyImproved": {
            //                            "addrLineList": {
            //                                "addrLine1": "",
            //                                "addrLine2": ""
            //                            },
            //                            "city": "",
            //                            "state": "",
            //                            "zip": ""
            //                        },
            //                        "propertyType": "",
            //                        "occupancy": ""
            //                    },
            //                    "productID": "PB34Z",
            //                    "productName": "Sec Primeline Plus (Deed/Liq.)",
            //                    "productType": "Line",
            //                    "LOB": "HEQ",
            //                    "productGroup": "RE",
            //                    "isHMDAApplicable": "",
            //                    "isODPApplicable": "Y",
            //                    "isChkApplicable": "Y",
            //                    "isProductRemoved": "",
            //                    "isNonRECollateralApplicable": "Y",
            //                    "isRECollateralApplicable": "Y",
            //                    "isPerFinStmtApplicable": "Y",
            //                    "loanAmtRequested": "",
            //                    "HMDAQues1": "",
            //                    "HMDAQues2": "",
            //                    "HMDAQues3": "",
            //                    "loanPurpose": "",
            //                    "purposeDesc": "",
            //                    "stateLoanClosedIn": "",
            //                    "desiredClosingDate": "",
            //                    "paymentType": "",
            //                    "paymentFrequency": "",
            //                    "marginIndex": "",
            //                    "preferredPaymentDueDate": "",
            //                    "term": "",
            //                    "amortization": "",
            //                    "WFFundedInd": "",
            //                    "payoffRefiLiquidCollateralInd": "",
            //                    "underwritingType": "",
            //                    "comments": "",
            //                    "commentsCDSavings": "",
            //                    "commentsMC": "",
            //                    "incrAccNo": "",
            //                    "originalLineAmount": "",
            //                    "productFeatures": {
            //                        "ACHData": {
            //                            "achInd": "",
            //                            "ACHPaymentType": "",
            //                            "additionalPrincipal": "",
            //                            "preferredFixAmount": "",
            //                            "bankName": "",
            //                            "accountType": "",
            //                            "accountNumber": "",
            //                            "otherWFAccountNum": "",
            //                            "routingNum": "",
            //                            "AcctValidated": ""
            //                        },
            //                        "ODPData": {
            //                            "odpInd": "",
            //                            "checkingAccount": "",
            //                            "otherWFAccountNum": "",
            //                            "routingNum": "",
            //                            "accountOwner": "",
            //                            "bankName": ""
            //                        },
            //                        "Checks": {
            //                            "checksInd": "",
            //                            "deliveryAddress": "",
            //                            "addressLine1": "",
            //                            "addressLine2": "",
            //                            "city": "",
            //                            "state": "",
            //                            "zip": ""
            //                        }
            //                    },
            //                    "collateralInfo": {
            //                        "financialCollteralList": {
            //                            "cdSavingsCollateralList": [
            //                                {
            //                                    "collOwnerInd": "",
            //                                    "collOwner": {
            //                                        "owner1": "",
            //                                        "owner2": "",
            //                                        "owner3": "",
            //                                        "owner4": ""
            //                                    },
            //                                    "collAddress": {
            //                                        "line1": "",
            //                                        "line2": "",
            //                                        "city": "",
            //                                        "state": "",
            //                                        "zip": ""
            //                                    },
            //                                    "trust": {
            //                                        "colNameOfTrust": "",
            //                                        "type": "",
            //                                        "borrowerFlag": "",
            //                                        "competentToSign": "",
            //                                        "taxId": "",
            //                                        "name": ""
            //                                    },
            //                                    "business": {
            //                                        "colNameOfBusiness": "",
            //                                        "taxId": "",
            //                                        "name": "",
            //                                        "type": "",
            //                                        "membersOrOfficersAll": "",
            //                                        "guarantor": "",
            //                                        "membersOrOfficersCorp": "",
            //                                        "secretary": ""
            //                                    },
            //                                    "collType": "",
            //                                    "acctNumber": "",
            //                                    "amount": "",
            //                                    "maturityDate": ""
            //                                }
            //                            ],
            //                            "marketableCollateralList": [
            //                                {
            //                                    "collOwnerInd": "",
            //                                    "collOwner": {
            //                                        "owner1": "",
            //                                        "owner2": "",
            //                                        "owner3": "",
            //                                        "owner4": ""
            //                                    },
            //                                    "collAddress": {
            //                                        "line1": "",
            //                                        "line2": "",
            //                                        "city": "",
            //                                        "state": "",
            //                                        "zip": ""
            //                                    },
            //                                    "trust": {
            //                                        "colNameOfTheTrust": "",
            //                                        "type": "",
            //                                        "borrowerFlag": "",
            //                                        "competentToSign": "",
            //                                        "taxId": "",
            //                                        "name": ""
            //                                    },
            //                                    "business": {
            //                                        "colNameOfBusiness": "",
            //                                        "taxId": "",
            //                                        "name": "",
            //                                        "type": "",
            //                                        "membersOrOfficersAll": "",
            //                                        "guarantor": "",
            //                                        "membersOrOfficersCorp": "",
            //                                        "secretary": ""
            //                                    },
            //                                    "securityInformation": {
            //                                        "accNumber": "",
            //                                        "type": "",
            //                                        "wfbTrustee": "",
            //                                        "intermediary": "",
            //                                        "fccsBrokerageName": "",
            //                                        "investmentManager": ""
            //                                    }
            //                                }
            //                            ]
            //                        }
            //                    }
            //                },
            //                {
            //                    "HMDA": {
            //                        "proceedsHomeImpAmount": "",
            //                        "propertyImproved": {
            //                            "addrLineList": {
            //                                "addrLine1": "",
            //                                "addrLine2": ""
            //                            },
            //                            "city": "",
            //                            "state": "",
            //                            "zip": ""
            //                        },
            //                        "propertyType": "",
            //                        "occupancy": ""
            //                    },
            //                    "productID": "PB22X",
            //                    "productName": "Uns Business Loan (Fixed) Std",
            //                    "productType": "Loan",
            //                    "LOB": "PCM",
            //                    "productGroup": "UN",
            //                    "isHMDAApplicable": "",
            //                    "isODPApplicable": "N",
            //                    "isChkApplicable": "N",
            //                    "isProductRemoved": "",
            //                    "isNonRECollateralApplicable": "N",
            //                    "isRECollateralApplicable": "N",
            //                    "isPerFinStmtApplicable": "N",
            //                    "loanAmtRequested": "",
            //                    "HMDAQues1": "",
            //                    "HMDAQues2": "",
            //                    "HMDAQues3": "",
            //                    "loanPurpose": "",
            //                    "purposeDesc": "",
            //                    "stateLoanClosedIn": "",
            //                    "desiredClosingDate": "",
            //                    "paymentType": "",
            //                    "paymentFrequency": "",
            //                    "marginIndex": "",
            //                    "preferredPaymentDueDate": "",
            //                    "term": "",
            //                    "amortization": "",
            //                    "WFFundedInd": "",
            //                    "payoffRefiLiquidCollateralInd": "",
            //                    "underwritingType": "",
            //                    "comments": "",
            //                    "commentsCDSavings": "",
            //                    "commentsMC": "",
            //                    "incrAccNo": "",
            //                    "originalLineAmount": "",
            //                    "productFeatures": {
            //                        "ACHData": {
            //                            "achInd": "",
            //                            "ACHPaymentType": "",
            //                            "additionalPrincipal": "",
            //                            "preferredFixAmount": "",
            //                            "bankName": "",
            //                            "accountType": "",
            //                            "accountNumber": "",
            //                            "otherWFAccountNum": "",
            //                            "routingNum": "",
            //                            "AcctValidated": ""
            //                        },
            //                        "ODPData": {
            //                            "odpInd": "",
            //                            "checkingAccount": "",
            //                            "otherWFAccountNum": "",
            //                            "routingNum": "",
            //                            "accountOwner": "",
            //                            "bankName": ""
            //                        },
            //                        "Checks": {
            //                            "checksInd": "",
            //                            "deliveryAddress": "",
            //                            "addressLine1": "",
            //                            "addressLine2": "",
            //                            "city": "",
            //                            "state": "",
            //                            "zip": ""
            //                        }
            //                    },
            //                    "collateralInfo": {
            //                        "financialCollteralList": {
            //                            "cdSavingsCollateralList": [
            //                                {
            //                                    "collOwnerInd": "",
            //                                    "collOwner": {
            //                                        "owner1": "",
            //                                        "owner2": "",
            //                                        "owner3": "",
            //                                        "owner4": ""
            //                                    },
            //                                    "collAddress": {
            //                                        "line1": "",
            //                                        "line2": "",
            //                                        "city": "",
            //                                        "state": "",
            //                                        "zip": ""
            //                                    },
            //                                    "trust": {
            //                                        "colNameOfTrust": "",
            //                                        "type": "",
            //                                        "borrowerFlag": "",
            //                                        "competentToSign": "",
            //                                        "taxId": "",
            //                                        "name": ""
            //                                    },
            //                                    "business": {
            //                                        "colNameOfBusiness": "",
            //                                        "taxId": "",
            //                                        "name": "",
            //                                        "type": "",
            //                                        "membersOrOfficersAll": "",
            //                                        "guarantor": "",
            //                                        "membersOrOfficersCorp": "",
            //                                        "secretary": ""
            //                                    },
            //                                    "collType": "",
            //                                    "acctNumber": "",
            //                                    "amount": "",
            //                                    "maturityDate": ""
            //                                }
            //                            ],
            //                            "marketableCollateralList": [
            //                                {
            //                                    "collOwnerInd": "",
            //                                    "collOwner": {
            //                                        "owner1": "",
            //                                        "owner2": "",
            //                                        "owner3": "",
            //                                        "owner4": ""
            //                                    },
            //                                    "collAddress": {
            //                                        "line1": "",
            //                                        "line2": "",
            //                                        "city": "",
            //                                        "state": "",
            //                                        "zip": ""
            //                                    },
            //                                    "trust": {
            //                                        "colNameOfTheTrust": "",
            //                                        "type": "",
            //                                        "borrowerFlag": "",
            //                                        "competentToSign": "",
            //                                        "taxId": "",
            //                                        "name": ""
            //                                    },
            //                                    "business": {
            //                                        "colNameOfBusiness": "",
            //                                        "taxId": "",
            //                                        "name": "",
            //                                        "type": "",
            //                                        "membersOrOfficersAll": "",
            //                                        "guarantor": "",
            //                                        "membersOrOfficersCorp": "",
            //                                        "secretary": ""
            //                                    },
            //                                    "securityInformation": {
            //                                        "accNumber": "",
            //                                        "type": "",
            //                                        "wfbTrustee": "",
            //                                        "intermediary": "",
            //                                        "fccsBrokerageName": "",
            //                                        "investmentManager": ""
            //                                    }
            //                                }
            //                            ]
            //                        }
            //                    }
            //                }
            //            ],
            //            "realEstateCollateralList": {
            //                "realEstateCollateral": {
            //                    "propertyAddress": {
            //                        "addressLine": "",
            //                        "city": "",
            //                        "state": "",
            //                        "zip": ""
            //                    },
            //                    "purchaseDate": "",
            //                    "purchasePrice": "",
            //                    "estimatedValue": "",
            //                    "occupancyType": "",
            //                    "appCollateralFlag": "",
            //                    "coAppCollateralFlag": "",
            //                    "propertyType": "",
            //                    "area": "",
            //                    "propertyUnderConstruction": "",
            //                    "trustVestedFlag": "",
            //                    "propertyFreeClearFlag": "",
            //                    "propertyForSaleFlag": "",
            //                    "agricultureFlag": "",
            //                    "explaination": "",
            //                    "refinanceCompany": "",
            //                    "mortgageInformation": {
            //                        "mortgageList": [
            //                            {
            //                                "type": "",
            //                                "mortgageType": "",
            //                                "lender": "",
            //                                "balance": "",
            //                                "account": "",
            //                                "monthlyPayment": ""
            //                            }
            //                        ],
            //                        "anotherMortgageFlag": "",
            //                        "refinancedFlag": "",
            //                        "existingMortgageFlag": "",
            //                        "payOffFlag": "",
            //                        "propertyTaxFlag": "",
            //                        "hoiFlag": ""
            //                    },
            //                    "hoiInformation": {
            //                        "carrierName": "",
            //                        "agentInformation": {
            //                            "firstName": "",
            //                            "lastName": "",
            //                            "phoneNumber": ""
            //                        }
            //                    },
            //                    "housingExpenses": {
            //                        "hoaFees": "",
            //                        "hoaFeesFreq": "",
            //                        "propertyTaxesPayment": "",
            //                        "propertyTaxesPaymentFreq": "",
            //                        "monthlyMortgageHasTax": "",
            //                        "hoiPayment": "",
            //                        "hoiPaymentFreq": "",
            //                        "monthlyMortgagePayment": "",
            //                        "otherInsurancePayment": "",
            //                        "otherInsurancePaymentFreq": "",
            //                        "otherHoPayment": "",
            //                        "otherHoPaymentFreq": ""
            //                    },
            //                    "comments": ""
            //                }
            //            },
            //            "assetInfo": {
            //                "propertTypeInd": "",
            //                "selfPrepPFSInd": "",
            //                "ownPropertyInd": "",
            //                "totalAssets": "",
            //                "totalLiabilities": "",
            //                "totalNotesReceivable": "",
            //                "totalREO": "",
            //                "netWorth": "",
            //                "assets": [
            //                    {
            //                        "name": "Cash in Wells Fargo",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Cash in other institutions",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Other Securities Owned**",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "IRA/Keogh/Pension",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Notes Receivable (Sch 1)",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Real Estate Value (Sch 2)",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "text"
            //                    },
            //                    {
            //                        "name": "",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "text"
            //                    },
            //                    {
            //                        "name": "Automobiles",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Personal Property",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Other Assets",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    }
            //                ],
            //                "liabilities": [
            //                    {
            //                        "name": "Income Taxes",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Other Taxes",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Revolving Credit",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "text"
            //                    },
            //                    {
            //                        "name": "",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "text"
            //                    },
            //                    {
            //                        "name": "Mortgages Payable (Sch. 2)",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Other Liability",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    },
            //                    {
            //                        "name": "Other Liability",
            //                        "howHeld": "",
            //                        "balance": "",
            //                        "type": "label"
            //                    }
            //                ],
            //                "notesReceivableList": [
            //                    {
            //                        "nameOfDebtor": "",
            //                        "collateralType": "",
            //                        "maturityDate": "",
            //                        "annual": "",
            //                        "unPaidBalance": "",
            //                        "anchorclass": "",
            //                        "error": "",
            //                        "errorNo": ""
            //                    }
            //                ],
            //                "realEstateOwnedList": [
            //                    {
            //                        "reoType": "",
            //                        "reoAddrStreet": "",
            //                        "reoPercentageOfOwnership": "",
            //                        "reofreeClearInd": "",
            //                        "reoMTGLienHolder": "",
            //                        "reoMaturityDate": "",
            //                        "reoLienAmount": "",
            //                        "reoMonthlyPayment": "",
            //                        "reoPresentMktValue": "",
            //                        "reoGrossRentalIncome": "",
            //                        "reoHOInsurance": "",
            //                        "reoInsuranceEscrowInd": "",
            //                        "reoMonthlyTaxes": "",
            //                        "reoTaxEscrowInd": "",
            //                        "reoMonthlyFloodIns": "",
            //                        "reoFloodInsPayment": "",
            //                        "reoOtherHousingOblig": "",
            //                        "reoHOADues": "",
            //                        "reoOtherMonthlyInsurance": "",
            //                        "reoMortgagePayment": ""
            //                    }
            //                ]
            //            },
            //            "KYC": {
            //                "completeOnline": "",
            //                "eligibleInd": "",
            //                "dueDiligenceInd": "",
            //                "partiesIdentifiedInd": "",
            //                "borrowConfirmedInd": "",
            //                "grantorConfirmedInd": "",
            //                "reqMetInd": "",
            //                "level2CheckInd": "",
            //                "riskDesignationInd": "",
            //                "negNewsInd": "",
            //                "reviewedApprvInd": "",
            //                "sharedCreditInd": "",
            //                "signatureInd": ""
            //            },
            //            "comments": ""
            //        },
            //        "DynamicData": {
            //            "LOBList": [
            //                {
            //                    "LOBType": "HEQ",
            //                    "ACHAcctList": [
            //                        {
            //                            "acctNum": "Select",
            //                            "acctVal": ""
            //                        },
            //                        {
            //                            "acctNum": "Other",
            //                            "acctVal": "O"
            //                        }
            //                    ],
            //                    "ODPAcctList": [
            //                        {
            //                            "acctNum": "Select",
            //                            "acctVal": ""
            //                        },
            //                        {
            //                            "acctNum": "Other",
            //                            "acctVal": "O"
            //                        }
            //                    ]
            //                },
            //                {
            //                    "LOBType": "PLL",
            //                    "ACHAcctList": [
            //                        {
            //                            "acctNum": "Select",
            //                            "acctVal": ""
            //                        },
            //                        {
            //                            "acctNum": "Other",
            //                            "acctVal": "O"
            //                        }
            //                    ],
            //                    "ODPAcctList": [
            //                        {
            //                            "acctNum": "Select",
            //                            "acctVal": ""
            //                        },
            //                        {
            //                            "acctNum": "Other",
            //                            "acctVal": "O"
            //                        }
            //                    ]
            //                }
            //            ],
            //            "addressList": [
            //                {
            //                    "addressType": "CR",
            //                    "addressLine": "1330 W 90TH AVE N",
            //                    "city": "CONWAY SPRINGS",
            //                    "state": "KS",
            //                    "zip": "670318233"
            //                },
            //                {
            //                    "addressType": "MA",
            //                    "addressLine": "1330 W 90TH AVE N",
            //                    "city": "CONWAY SPRINGS",
            //                    "state": "KS",
            //                    "zip": "670318233"
            //                }
            //            ]
            //        }
            //    }
            //};
            var root = {
                ViewModel: {
                    ucaViewModel: {},
                    ErrorMessages: []
                },
                DataModel: null,
                Errors: null
            };

            root.ViewModel.ucaViewModel = angular.fromJson(jsonInitData);
            return root;
        }
    }
</script>
<script>
    var accessToken = '<%=accessToken%>';
    var wmgJsonData = '<%=JsonData%>';
    var wmgErrorData = '<%=ErrorData%>';
    var sessionIdData = "<%=SessionIDData%>";
    var callIsFromSCSPrevious = '<%=CallIsFromSCSPrevious%>';
    var wealthPageB = '<%=timeB%>';
    var wealthPageA = '<%=timeA%>';
    var ucaRouterLoadTimeB = '<%=ucaRouterLoadTimeB%>';
    var ucaRouterLoadTimeA = '<%=ucaRouterLoadTimeA%>';

    var isWmgInitialJsonData = '<%=isWmgInitialData%>';
    var wmgInitialJsonData = getInitialData();

</script>
<script type="text/javascript">
    function getDocHeight(doc) {
        try {
            doc = doc || parent.document;
            var body = doc.body, html = doc.documentElement;
            var height = Math.max(body.scrollHeight, body.offsetHeight);
            height = height + 50;
            return height;
        } catch (e) {
        }
    }

    var iFrameheight = 0;
    function setIframeHeight(id) {
        try {
            var ifrm = parent.document.getElementById(id);
            var doc = ifrm.contentDocument ? ifrm.contentDocument :
                ifrm.contentWindow.document;

            if (iFrameheight != getDocHeight(doc)) {
                iFrameheight = getDocHeight(doc);
                ifrm.style.height = iFrameheight + "px";
            }


        } catch (e) {
        }
    }

    setInterval(function () {
        setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");

    }, 2000);

</script>


</html>
