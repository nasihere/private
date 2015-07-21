wmg.controller("SummaryController", ['$scope', 'goAheadService', 'wmgCommonService', '$parse', function ($scope, goAheadService, wmgCommonService, $parse) {

    $scope.kyc = Root.applicationInfo.WealthData.KYC;
    $scope.applicationInfo = Root.applicationInfo;

    $scope.applicantInfoList = Root.applicationInfo.applicantInfoList.applicantInfo;
    $scope.additionalInfo = Root.applicationInfo.additionalContact;
    $scope.businessTypeInfo = Root.applicationInfo.businessTypeInfo;
    $scope.bankerInfo = Root.applicationInfo.bankerInfo;
    $scope.applicant = wmgCommonService.getApplicant("P");
    $scope.coapplicant = wmgCommonService.getApplicant("S");
    $scope.keyDescription = "";
    $scope.membersOfficers = "";
    $scope.membersOfficersHeading = [
        { position: 0, name: "Name" },
        { position: 1, name: "Role" },
        { position: 2, name: "Title/Position" }

    ];
	$scope.trm = function(str, spliter){
	    var result = "";
	    try {
	        var arr = str.split(spliter);
	        for (var i = 0; i < arr.length; i++) {
	            if (arr[i] != "") {
	                if (result != "") result += spliter;
	                result += arr[i];
	            }
	        }
	    } catch(e) {
	        return "";
	    }
	    return result;
	};
    $scope.c = function (obj, val) {
        try {

            var template = $parse(obj);
            var obj;
            if (val == undefined)
                obj = template($scope); // Hello Joe
            else
                obj = template(val);

            if (obj == null || obj == undefined || obj == "") {
                return "";
            }
            else {
               
                if (obj == "Y" || obj == "True") {
                    return "Yes"
                } else if (obj == "N" || obj == "false") {
                    return "No"
                }
                return obj;
            }
        } catch (e) {
            return "";
        }
    };
    $scope.get = function (list, key) {
        if (key == "") return "";
        if (list == null || list == undefined) return "";
        return wmgCommonService.findDescription(list, key);

    };

    $scope.product = null;
    $scope.getProduct = function (productID) {
        $.each(Root.applicationInfo.WealthData.productList, function (key, value) {
            if (value.productID == productID) {
                $scope.product = value;
                return value;
            }
        });
    };

    $scope.applicantInfoList = Root.applicationInfo.applicantInfoList;
    $scope.WealthData = Root.applicationInfo.WealthData;
    $scope.products = Root.applicationInfo.WealthData.productList;
    $scope.assetInfo = Root.applicationInfo.WealthData.assetInfo;
    $scope.assetsHeading = [
            { position: 0, name: "How held" },
            { position: 1, name: "Balance" },
            { position: 2, name: "Name" },

    ];
    $scope.getAssets = function () {
        var arrayTmp = [];
        var obj = {};
        $scope.assetsTable = angular.copy($scope.assetInfo.assets);
        $.each($scope.assetsTable, function (key, val) {
            if (val.howHeld != "") {
                val.howHeld = $scope.get(Common.SelectAppSpouseJoint, $scope.c('howHeld', val));
            }
            if (val.balance != "") {
                obj = {
                    0: val.howHeld,
                    1: val.balance,
                    2: val.name

                };

                arrayTmp.push(angular.copy(obj));
            }
        });
        return arrayTmp;
    };
    $scope.hmdaHeading = function () {
        try {
            if ($scope.HmdaAddress.propertyImproved == null || $scope.HmdaAddress.propertyImproved == undefined) {
                return "";
            }
            if ($scope.HmdaAddress.propertyImproved.addrLineList.addrLine1 != undefined && $scope.HmdaAddress.propertyImproved.addrLineList.addrLine1 != "") {
                return "HMDA";
            } else {
                return "";
            }
        } catch (e) {
            return "";
        }
    }
    $scope.hmdaName = function () {
        try {
            if ($scope.HmdaAddress.propertyImproved == null || $scope.HmdaAddress.propertyImproved == undefined) {
                return "";
            }
            if ($scope.HmdaAddress.propertyImproved.addrLineList.addrLine1 != undefined && $scope.HmdaAddress.propertyImproved.addrLineList.addrLine1 != "") {
                return ".";
            } else {
                return "";
            }
        } catch (e) {
            return "";
        }
    }
    $scope.assetInfo = Root.applicationInfo.WealthData.assetInfo;
    $scope.liabilitiesHeading = [
            { position: 0, name: "How held" },
            { position: 1, name: "Balance" },
            { position: 2, name: "Name" }



    ];
    $scope.getliabilities = function () {
        var arrayTmp = [];
        var obj = {};
        $scope.liabilitiesTable = angular.copy($scope.assetInfo.liabilities);
        $.each($scope.liabilitiesTable, function (key, val) {
            if (val.howHeld != "") {
                val.howHeld = $scope.get(Common.SelectAppSpouseJoint, $scope.c('howHeld', val));
            }
            if (val.balance != "") {
                obj = {
                    0: val.howHeld,
                    1: val.balance,
                    2: val.name

                };

                arrayTmp.push(angular.copy(obj));
            }
        });
        return arrayTmp;
    };

    ////////////////////////////////////////// Personal Finanace Note 2 Receivable /////////////////////////
    $scope.notesReceivableHeading = [
           { position: 0, name: "Name Of Debtor" },
            { position: 1, name: "Collateral Type" },
           { position: 2, name: "Maturity Date" },
           { position: 3, name: "Annual P&I" },
           { position: 4, name: "Unpad Balance" }

    ];
    $scope.getNoteReceivable = function () {
        var arrayTmp = [];
        var obj = {};
        $scope.notesReceivableTable = angular.copy($scope.assetInfo.notesReceivableList);
        $scope.notesReceivableTable = wmgCommonService.removeIfEmptyItemFound($scope.notesReceivableTable);
        $.each($scope.notesReceivableTable, function (key, val) {

            if (val != undefined) {
                if (val.unPaidBalance != undefined) {
                    obj = {
                        0: val.nameOfDebtor,
                        1: val.collateralType,
                        2: val.maturityDate,
                        3: val.annual,
                        4: val.unPaidBalance
                    };

                    arrayTmp.push(angular.copy(obj));
                }
            }
        });
        return arrayTmp;
    };
    ////////////////////////////////////////// Personal Finanace Note 2 Receivable /////////////////////////

    $scope.getCDSavingsCollateralsHeadingLabel = function () {
        try {
            if ($scope.getCDSavingsCollaterals().length != 0) {
                return ".";
            } else {
                return "";
            }

        } catch (e) {
            return "";
        }
    }
    $scope.getCDSavingsCollateralsHeadingLabel = function () {
        try {
            if ($scope.getCDSavingsCollaterals().length != 0) {
                return "CD Saving Collateral";
            } else {
                return "";
            }

        } catch (e) {
            return "";
        }
    }
    $scope.getMarketableCollateralsHeadingLabel = function ()
    {
        try {
            if ($scope.getMarketableCollaterals().length != 0) {
                return ".";
            } else {
                return "";
            }
            
        } catch (e) {
            return "";
        }
    }
    $scope.getMarketableCollateralsLabel = function () {
        try {
            if ($scope.getMarketableCollaterals().length != 0) {
                return "Marketable Collateral";
            } else {
                return "";
            }

        } catch (e) {
            return "";
        }
    }
    $scope.getMarketableCollateralsHeading = function () {

        var tmp = [
             { position: 0, name: "Type" },
             { position: 1, name: "Account" },
             { position: 2, name: "Intermediary" },
             { position: 3, name: "Name of Business" },
              { position: 4, name: "Name of Trust" },
            { position: 5, name: "Revocable/Irrevocable" },
            { position: 6, name: "Competent to Sign" }
        ];

        return tmp;

    };
    $scope.getMarketableCollaterals = function (productID) {
        try {
            $scope.getProduct(productID);
            $scope.PathMarketable = $scope.product.collateralInfo.financialCollteralList.marketableCollateralList;
            var arrayTmp = [];
            var obj = {};
            var iPos = 0;
            $.each($scope.PathMarketable, function (key, val) {
                if (val != undefined) {

                    if (val.securityInformation != undefined && val.securityInformation.accNumber != "") {
                        if (val.collAddress.line1 == undefined) val.collAddress.line1 = "";
                        if (val.collAddress.line2 == undefined) val.collAddress.line2 = "";
                        if (val.collAddress.city == undefined) val.collAddress.city = "";
                        if (val.collAddress.state == undefined) val.collAddress.state = "";
                        if (val.collAddress.zip == undefined) val.collAddress.zip = "";
                        obj = {
                            0: $scope.c('securityInformation.type', val),
                            1: $scope.c('securityInformation.accNumber', val),
                            2: $scope.c('securityInformation.intermediary', val),
                            3: $scope.c('business.colNameOfBusiness', val),
                            4: $scope.c('trust.colNameOfTheTrust', val),
                            5: $scope.c('trust.type', val),
                            6: $scope.c('trust.competentToSign', val)
                        };

                        arrayTmp.push(angular.copy(obj));
                    }
                }

            })
            return arrayTmp;
        } catch (ex) {
            return "";
        }

    };

    $scope.getCDSavingsCollateralsHeading = function () {

        var tmp = [
             { position: 0, name: "Type" },
             { position: 1, name: "Account" },
             { position: 2, name: "Amount" },
            { position: 3, name: "Name of Business" },
            { position: 4, name: "Name of Trust" },
            { position: 5, name: "Revocable/Irrevocable" },
            { position: 6, name: "Competent to Sign" }
            
        ];

        return tmp;

    };
    $scope.getCDSavingsCollaterals = function (productID) {

        $scope.getProduct(productID);
        $scope.PathCdSaving = $scope.product.collateralInfo.financialCollteralList.cdSavingsCollateralList;
        var arrayTmp = [];
        var obj = {};
        var iPos = 0;
        if ($scope.PathCdSaving.length != 0) {
            $.each($scope.PathCdSaving, function (key, val) {
                if (val != undefined) {
                    if (val.acctNumber != undefined && val.acctNumber != "") {
                        if (val.collAddress.line1 == undefined) val.collAddress.line1 = "";
                        if (val.collAddress.line2 == undefined) val.collAddress.line2 = "";
                        if (val.collAddress.city == undefined) val.collAddress.city = "";
                        if (val.collAddress.state == undefined) val.collAddress.state = "";
                        if (val.collAddress.zip == undefined) val.collAddress.zip = "";

                        obj = {
                            0: $scope.c('collType', val),
                            1: $scope.c('acctNumber', val),
                            2: $scope.c('amount', val),
                            3: $scope.c('business.colNameOfBusiness', val),
                            4: $scope.c('trust.colNameOfTrust', val),
                            5: $scope.c('trust.type', val),
                            6: $scope.c('trust.competentToSign', val)
                            
                       
                            
                        };
                        arrayTmp.push(angular.copy(obj));
                    }
                }
            })
        }
        return arrayTmp;
    };

    $scope.reCollateral = Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral;

    $scope.HmdaAddress = Root.applicationInfo.WealthData.HMDA;
    $scope.applicantRaceList = angular.copy(Common.Race);
    $scope.coApplicantRaceList = angular.copy(Common.Race);

    if (wmgCommonService.getApplicant("P") != undefined) {
        $scope.applicantHMDA = wmgCommonService.getApplicant("P").HMDA;
    }

    if (wmgCommonService.getApplicant("S") != undefined) {
        $scope.coapplicantHMDA = wmgCommonService.getApplicant("S").HMDA;
    }



    $scope.$on("Summary", $scope.validateBeforeContinue);
    $scope.$on("Summary", $scope.validateBeforeContinue = function () {
        goAheadService.setFlag(true);
    });

    $.each($scope.membersOfficers, function (key, val) {
        val.role = $scope.get(Common.ddMemberOfficerRole, $scope.c('role', val));
        val.title = $scope.get(Common.ddTitleOrPosition, $scope.c('title', val));

    });

    $scope.mortgageHeading = [
             { position: 0, name: "Account" },
             { position: 1, name: "Balance" },
             { position: 2, name: "Lender" },
             { position: 3, name: "Monthly Payment" },
             { position: 4, name: "Mortgage Type" }

    ];


    $scope.ProductRequest = [];
    var iPosProduct = 1;
    $.each($scope.products, function (key, val) {
        if (val != undefined && val.isProductRemoved != "Y") {
            $scope.ProductRequest.push(
                {
                    name: ".",
                    value: $scope.c('productName', val)

                },
                /*{
                    name: "Product Code",
                    value: $scope.c('productID', val)

                }, {
                    name: "Product Group",
                    value: $scope.c('productGroup', val)

                }, */
                {
                    name: "Total Amount Requested",
                    value: $scope.c('loanAmtRequested', val)

                }, {
                    name: "Money being used for Home Improvement",
                    value: $scope.c('HMDAQues1', val)

                },/* {
                    value: "Is any money being used for Purchase Money Second",
                    name: $scope.c('HMDAQues2', val)

                },*/ {
                    name: "Refinance or payoff a loan/line secured by?",
                    value: $scope.c('HMDAQues3', val)

                }, {
                    name: "Loan Purpose",
                    value: $scope.get(Common.DD_ProductPurposeItem, $scope.c('loanPurpose', val))

                }, {
                    name: "Purpose Description",
                    value: $scope.c('purposeDesc', val)

                }, {
                    name: "Desired Closing Date",
                    value: $scope.c('desiredClosingDate', val)

                }, {
                    name: "Term",
                    value: $scope.c('term', val)

                },{
                    name: "Amortization",
                    value: $scope.c('amortization', val)

                }, 
                {
                    name: "Preferred Payment Due Date",
                    value: $scope.get(Common.DD_ProductPreferredPaymentDueDate, $scope.c('preferredPaymentDueDate', val))

                },/* {
                    name: "State Loan Will Close In",
                    value: $scope.get(Common.DD_ProductStateLoanWillCloseIn, $scope.c('stateLoanClosedIn', val))

                },*/ {
                    name: "Payment Type",
                    value: $scope.get(Common.DD_ProductPaymentType, $scope.c('paymentType', val))

                }, {
                    name: "Payment Frequency",
                    value: $scope.get(Common.DD_ProductPaymentFrequency, $scope.c('paymentFrequency', val))

                }, {
                    name: "Margin Index",
                    value: $scope.get(Common.DD_ProductMarginIndex, $scope.c('marginIndex', val))

                },
                {
                    name: "Collateral accounts funded at Wells Fargo",
                    value: $scope.c('WFFundedInd', val)

                },
                
                 {
                     name: "Underwriting Type",
                     value: $scope.get(Common.ddUnderWritingType, $scope.c('underwritingType', val))

                 },
                 {
                     name: "For Increase or Renewal, enter Account Number",
                     value: $scope.c('incrAccNo', val)

                 },
                  {
                      name: "For Original Line Amount",
                      value: $scope.c('originalLineAmount', val)

                  },
              /*   {
                     name: $scope.getMarketableCollateralsHeadingLabel(),
                     value: $scope.getMarketableCollateralsLabel(val.productID)

                 }
                ,
                {
                    name: " ",
                    value: " "

                },*/ {
                    name: $scope.getMarketableCollateralsHeading(),
                    value: $scope.getMarketableCollaterals(val.productID)

                }
            /*
                 {
                     name: $scope.getCDSavingsCollateralsHeadingLabel(),
                     value: $scope.getCDSavingsCollateralsHeadingLabel(val.productID)

                 }*/
                , {
                    name: $scope.getCDSavingsCollateralsHeading(),
                    value: $scope.getCDSavingsCollaterals(val.productID)

                }, /*{
                    name: "ACH Payment Type",
                    value: $scope.get(Common.DefaultACHPaymentType, $scope.c('productFeatures.ACHData.ACHPaymentType', val))

                }, {
                    name: "Bank Name",
                    value: $scope.c('productFeatures.ACHData.bankName', val)

                }, {
                    name: "Pay from Account",
                    value: $scope.c('productFeatures.ACHData.accountType', val)

                }, {
                    name: "Account Number",
                    value: $scope.c('productFeatures.ACHData.accountNumber', val)

                }, {
                    name: "Other Wells Fargo Account Number",
                    value: $scope.c('productFeatures.ACHData.otherWFAccountNum', val)

                }, {
                    name: "Routing Transit Number(RTN)",
                    value: $scope.c('productFeatures.ACHData.routingNum', val)

                }, {
                    name: "OverDraft Protection Checking Account",
                    value: $scope.c('productFeatures.ODPData.checkingAccount', val)

                }, {
                    name: "Account Number",
                    value: $scope.c('productFeatures.ODPData.otherWFAccountNum', val)

                }, {
                    name: "Routing Transit Number(RTN)",
                    value: $scope.c('productFeatures.ODPData.routingNum', val)

                }, {
                    name: "Name on Account",
                    value: $scope.c('productFeatures.ODPData.accountOwner', val)

                }, {
                    name: "Check Delivery Address",
                    value: $scope.get(Common.ddCheckDeliveryAddress, $scope.c('productFeatures.Checks.deliveryAddress', val))

                }, {
                    name: "Address Line 1",
                    value: $scope.c('productFeatures.Checks.addressLine1', val)

                }, {
                    name: "Address Line 2",
                    value: $scope.c('productFeatures.Checks.addressLine2', val)

                }, {
                    name: "State",
                    value: $scope.c('productFeatures.Checks.state', val)

                },
                {
                    name: "City",
                    value: $scope.c('productFeatures.Checks.city', val)

                },
                {
                    name: "Zip",
                    value: $scope.c('productFeatures.Checks.zip', val)

                },
                */
                {
                    name: "Comment",
                    value: $scope.c("comments",val)
                },
                {
                    name: $scope.hmdaName(),
                    value: $scope.hmdaHeading()

                }, {
                    name: "Property Type",
                    value: $scope.get(Common.ddHmdaPropertyType, $scope.c('HMDA.propertyType', val))

                }, {
                    name: "Occupancy of the Collateral Property",
                    value: $scope.get(Common.ddCollateralPropertyOccupancy, $scope.c('HMDA.occupancy', val))

                }, {
                    name: "Address",
                    value: $scope.trm($scope.c('HMDA.propertyImproved.addrLineList.addrLine1', val) + ", " + $scope.c('HMDA.propertyImproved.addrLineList.addrLine2', val) + ", " + $scope.c('HMDA.propertyImproved.city', val) + ", " + $scope.c('HMDA.propertyImproved.state', val)  + ", " + $scope.c('HMDA.propertyImproved.zip', val),", ")

                }




             );
            iPosProduct++;
        }
    });
    $scope.reCollateralHeading = function () {
        try {
            if ($scope.reCollateral.estimatedValue == null || $scope.reCollateral.estimatedValue == undefined) {
                return "";
            }
            if ($scope.reCollateral.estimatedValue != undefined && $scope.reCollateral.estimatedValue != "") {
                return "Real Estate Collateral Property Details";
            } else {
                return "";
            }
        } catch (e) {
            return "";
        }
    }
    $scope.reCollateralName = function () {
        try {
            if ($scope.reCollateral.estimatedValue == null || $scope.reCollateral.estimatedValue == undefined) {
                return "";
            }
            if ($scope.reCollateral.estimatedValue != undefined && $scope.reCollateral.estimatedValue != "") {
                return ".";
            } else {
                return "";
            }
        } catch (e) {
            return "";
        }
    }
    
    $scope.titleAsset = function (type) {
        if ($scope.getAssets().length != 0) {
            if (type == "N")
                return ".";
            else
                return "Assets Information";
        } else {
            return "";
        }
    }
    $scope.titleLiabilities = function (type) {
        if ($scope.getliabilities().length != 0) {
            if (type == "N")
                return ".";
            else
                return "Liabilities Information";
        } else {
            return "";
        }
    }
    $scope.titleNoteReceivable = function (type) {
        if ($scope.getNoteReceivable().length != 0) {
            if (type == "N")
                return ".";
            else
                return "Note Receivable";
        } else {
            return "";
        }
    };

    for (var i = 0; i < Root.applicationInfo.DynamicData.addressList.length; i++) {
        if (Root.applicationInfo.DynamicData.addressList[i].addressType == 'CR') {
            $scope.address = Root.applicationInfo.DynamicData.addressList[i];
        }
    }
    
    $scope.SummaryMap = {
        "Summary": [
            {
                name: ".",
                value: "Basic Information"

            },
            {
                name: "Application Date",
                value: (new Date().toLocaleDateString())

            },
            {
                name: "Applicant Name",
                value: $scope.c('applicant.applicantName')

            }, {
                name: "Applicant Address",
                value: $scope.trm($scope.c('address.addressLine') + ", " + $scope.c('address.city') + ", " + $scope.c('address.state') + ", " + $scope.c('address.zip'),", ")

            },
            {
                name: "Employer",
                value: $scope.c('applicant.employer')

            },

            {
                name: "Job Title",
                value: $scope.c('applicant.jobTitle')

            },

            {
                name: "Filed for Bankruptcy?",
                value: $scope.c('applicant.backgroundInfoQuesList.backgroundInfoQues.bankruptcyInd')

            }, {
                name: "Convicted of Felony?",
                value: $scope.c('applicant.backgroundInfoQuesList.backgroundInfoQues.felonyInd')

            }, {
                name: "Party to claims or lawsuit?",
                value: $scope.c('applicant.backgroundInfoQuesList.backgroundInfoQues.lawsuitInd')

            }, {
                name: "Co Applicant Name",
                value: $scope.c('coapplicant.applicantName')

            }, {
                name: "Filed for Bankruptcy?",
                value: $scope.c('coapplicant.backgroundInfoQuesList.backgroundInfoQues.bankruptcyInd')

            }, {
                name: "Convicted of Felony?",
                value: $scope.c('coapplicant.backgroundInfoQuesList.backgroundInfoQues.felonyInd')

            }, {
                name: "Party to claims or lawsuit?",
                value: $scope.c('coapplicant.backgroundInfoQuesList.backgroundInfoQues.lawsuitInd')

            },
            {
                name: "Comments input into Streamline",
                value: $scope.c('WealthData.comments')

            },
            

            {
                name: ".",
                value: "Credit Details"

            },
           /* {
                name: "Language Preference",
                value: $scope.get(Common.Languages, $scope.c('applicationInfo.languagePref'))

            },*/ {
                name: "AUM",
                value: $scope.c('applicationInfo.AUM')

            }, {
                name: "Domestic Partnership or a Civil Union",
                value: $scope.c('applicant.domesticPartnerIndicator')

            }, {
                name: "Trust & Individual Credit",
                value: $scope.c('applicationInfo.businessTypeInfo.trustIndividualCredit')

            },
            {
                name: "Method of Application",
                value: $scope.get(Common.ddMethodFace, $scope.c('applicant.applicationMethod'))

            },
            {
                name: "Method of Co Application",
                value: $scope.get(Common.ddMethodFace, $scope.c('applicant.coapplicationMethod'))

            },/* {
                name: "Applicant Marital Status",
                value: $scope.get(Common.ddMaritalStatus, $scope.c('applicant.maritalStatus'))

            }, {
                name: "Co Applicant Marital Status",
                value: $scope.get(Common.ddMaritalStatus, $scope.c('coapplicant.maritalStatus'))

            },
                {
                    name: "Email",
                    value: $scope.c('applicant.email')

                },*/
                {
                    name: "Co Email",
                    value: $scope.c('coapplicant.email')

                },
                {
                    name: "Business Tax Id",
                    value: $scope.c('businessTypeInfo.businessTaxId')
                },

                {
                    name: "Business Name",
                    value: $scope.c('businessTypeInfo.businessName')
                },

                {
                    name: "Business Type",
                    value: $scope.get(Common.ddBusinessType, $scope.c('businessTypeInfo.businessType'))
                },
                {
                    name: ".",
                    value: "Banker Information"

                },
                //Need to work on membersOfficersList//.
                {
                    name: "Banker Name",
                    value: $scope.c('bankerInfo.bankerName')

                }, {
                    name: "Banker AU",
                    value: $scope.c('bankerInfo.au')

                }, {
                    name: "Banker Phone",
                    value: $scope.c('bankerInfo.bankerPhone')

                }, {
                    name: "Banker User ID",
                    value: $scope.c('bankerInfo.salesAgentUID')

                }, {
                    name: "Banker Officer ID",
                    value: $scope.c('bankerInfo.officerID')

                }, {
                    name: "Banker Location Code",
                    value: $scope.c('bankerInfo.location')

                }, 
                /*{
                    name: "Best time to call",
                    value: $scope.get(Common.SelectPreferredTimeToCall, $scope.c('applicationInfo.preferredTimeToCall'))

                }, */{
                    name: "Additional Banker or Associate Name",
                    value: $scope.c('additionalInfo.name')

                }, {
                    name: "Additional Banker or Associate Email",
                    value: $scope.c('additionalInfo.email')

                }, {
                    name: "Additional Banker or Associate Phone",
                    value: $scope.c('additionalInfo.phone')

                },
             {
                 name: ".",
                 value: "AML/KYC"

             },
              {
                  name: "AML/KYC Form Completed Online",
                  value: $scope.c('kyc.completeOnline')

              }, {
                  name: "Is Customer eligible per PB Program",
                  value: $scope.c('kyc.eligibleInd')

              }, {
                  name: "Parties identified and client link profiles created?",
                  value: $scope.c('kyc.partiesIdentifiedInd')

              }, {
                  name: "Required elements in client link complete and confirmed?",
                  value: $scope.c('kyc.borrowConfirmedInd')

              }, {
                  name: "All information confirmed and completed?",
                  value: $scope.c('kyc.grantorConfirmedInd')

              }, {
                  name: "All AML referral/reference requirements met?",
                  value: $scope.c('kyc.reqMetInd')

              }, {
                  name: "All AML/KYC background investigations ordered?",
                  value: $scope.c('kyc.level2CheckInd')

              }, {
                  name: "Have all country risks been considered?",
                  value: $scope.c('kyc.riskDesignationInd')

              }
             , {
                 name: "Were any negative news or red flags identified",
                 value: $scope.c('kyc.negNewsInd')

             }, {
                 name: "Mgmt reviewed/approved negative news and red flags?",
                 value: $scope.c('kyc.reviewedApprvInd')

             }, {
                 name: "Uploaded all negative news or red flag information?",
                 value: $scope.c('kyc.sharedCreditInd')

             }, {
                 name: "Completed for accurately?",
                 value: $scope.c('kyc.signatureInd')

             },


               // { name: $scope.titleAsset("N"), value: $scope.titleAsset("V"), href: "PersonalFin" },

             //   { name: $scope.assetsHeading, value: $scope.getAssets(), href: "PersonalFin" },
             //   { name: $scope.titleLiabilities("N"), value: $scope.titleLiabilities("V"), href: "PersonalFin" },
           //     { name: $scope.liabilitiesHeading, value: $scope.getliabilities(), href: "PersonalFin" },
            //    { name: $scope.titleNoteReceivable("N"), value: $scope.titleNoteReceivable("V"), href: "PersonalFin" },
             //   { name: $scope.notesReceivableHeading, value: $scope.getNoteReceivable(), href: "PersonalFin" },


                
                {
                    name: $scope.reCollateralName(),
                    value: $scope.reCollateralHeading()

                }, {
                    name: "Property Address",
                    value: $scope.trm($scope.c('reCollateral.propertyAddress.addressLine') + ", " + $scope.c('reCollateral.propertyAddress.addressLine2') + ", " + $scope.c('reCollateral.propertyAddress.city') + ", " + $scope.c('reCollateral.propertyAddress.state') + ", " + $scope.c('reCollateral.propertyAddress.zip'), ", ")

                },{
                    name: "Date Of purchase",
                    value: $scope.c('reCollateral.purchaseDate')

                }, {
                    name: "Purchase Price",
                    value: $scope.c('reCollateral.purchasePrice')

                }, {
                    name: "Estimated value",
                    value: $scope.c('reCollateral.estimatedValue')

                }, {
                    name: "Occupancy Type",
                    value: $scope.get(Common.ddOccupancyType, $scope.c('reCollateral.occupancyType'))

                }, {
                    name: "Property Type",
                    value: $scope.get(Common.ddPropertyType, $scope.c('reCollateral.propertyType'))

                }, {
                    name: "Acres",
                    value: $scope.c('reCollateral.area')

                },
                 {
                     name: "Vested in Trust",
                     value: $scope.c('reCollateral.trustVestedFlag')

                 }, {
                     name: "Property Under Construction",
                     value: $scope.c('reCollateral.propertyUnderConstruction')

                 },
                {
                    name: "Owned Free and Clear",
                    value: $scope.c('reCollateral.propertyFreeClearFlag')
                
                },

                {
                    name: "Property for Sale",
                    value:  $scope.c('reCollateral.propertyForSaleFlag')

                },
                // { name: $scope.mortgageHeading, value:  $scope.c('reCollateral.mortgageInformation.mortgageList,  },
                {
                    name: "Insurance Carrier",
                    value: $scope.c('reCollateral.hoiInformation.carrierName')

                }, /*{
                    name: "Agent Name",
                    value: $scope.c('reCollateral.hoiInformation.agentInformation.firstName')

                }, {
                    name: "Agent Phone Number",
                    value: $scope.c('reCollateral.hoiInformation.agentInformation.phoneNumber')

                }, */{
                    name: "Refinanced within the last 12 Months",
                    value: $scope.c('reCollateral.mortgageInformation.refinancedFlag')

                }, /*{
                    name: "HOA Dues/Co-op Fees Payment",
                    value: $scope.c('reCollateral.housingExpenses.hoaFees')

                }, {
                    name: "HOA Dues/Co-op Fees Payment",
                    value: $scope.c('reCollateral.housingExpenses.propertyTaxesPayment')

                }, {
                    name: "Monthly Mortgage Payment Include Taxes",
                    value:  $scope.c('reCollateral.housingExpenses.monthlyMortgageHasTax')

                }, {
                    name: "Homeowners Insurance Payment",
                    value: $scope.c('reCollateral.housingExpenses.hoiPayment')

                }, {
                    name: "Monthly Mortgage Payment",
                    value: $scope.c('reCollateral.housingExpenses.monthlyMortgagePayment')

                }, {
                    name: "Other Insurance Payments",
                    value: $scope.c('reCollateral.housingExpenses.otherInsurancePayment')

                }, {
                    name: "Other Housing Obligations Payments",
                    value: $scope.c('reCollateral.housingExpenses.otherHoPayment')

                },*/
                {
                    name: "Comments",
                    value: $scope.c('reCollateral.comments')

                },
            

             {
                 name: "Applicant Based upon visual observation",
                 value: $scope.get(Common.SelectYesNo, $scope.c('applicant.HMDA.Observation'))

             }, /*{
                 name: "Applicant Gender",
                 value: $scope.get(Common.ddGender, $scope.c('applicant.HMDA.gender'))

             }, {
                 name: "Applicant Ethnicity",
                 value: $scope.get(Common.ddEthnicity, $scope.c('applicant.HMDA.ethnicity'))

             },/* {
                 name: "Applicant Race",
                 value: $scope.get(Common.Race, $scope.c('applicant.HMDA.race1'))  + $scope.get(Common.Race, $scope.c('applicant.HMDA.race2'))  + $scope.get(Common.Race, $scope.c('applicant.HMDA.race3'))  + $scope.get(Common.Race, $scope.c('applicant.HMDA.race4'))  + $scope.get(Common.Race, $scope.c('applicant.HMDA.race5'))

             },*/
            {
                name: "Co Applicant Based upon visual observation",
                value: $scope.get(Common.SelectYesNo, $scope.c('coapplicant.HMDA.Observation'))

            },/* {
                name: "Co Applicant Gender",
                value: $scope.get(Common.ddGender, $scope.c('coapplicant.HMDA.gender'))

            }, {
                name: "Co Applicant Ethnicity",
                value: $scope.get(Common.ddEthnicity, $scope.c('coapplicant.HMDA.ethnicity'))

            },/* {
                name: "Co Applicant Race",
                value: $scope.get(Common.Race, $scope.c('coapplicant.HMDA.race1'))  + $scope.get(Common.Race, $scope.c('coapplicant.HMDA.race2'))  + $scope.get(Common.Race, $scope.c('coapplicant.HMDA.race3'))  + $scope.get(Common.Race, $scope.c('coapplicant.HMDA.race4'))  + $scope.get(Common.Race, $scope.c('coapplicant.HMDA.race5'))

            }*/
             
            
           



        ],

        "Product Request": []
    };
    $scope.SummaryMap["Product Request"] = angular.copy($scope.ProductRequest);

    $scope.Summary = [
       { position: 0, name: "Summary" },
       { position: 1, name: "Product Request" }



    ];
}]);

wmg.directive("wmgSummary", function () {
    return {
        transclude: true,
        scope: {
            name: '=name',
            value: "=value",
            href: "@"

        },
        template: function (scope, ele, attr) {
            return '<div class="containerz summaryRow" ng-if="name[0].position == 0 && value.length != 0" >' +
                        ' <table class="table table-responsive"> <thead> ' +
                            '<tr ng-init="cols=0;" style="background-color: #efefef!important;" >' +
                               '<th ng-repeat="head in name  | orderBy: \'position\'">{{head.name}}</th>' +
                            '</tr></thead><tbody>' +
                                '<tr class="leftnormallabel" ng-repeat="table in value">' +
                                    '<td ng-repeat="data in table  | orderBy: \'position\'">{{data}}</td>' +

                                '</tr>' +
                            '</tbody></table>' +

                '</div>' +

                '<div class="containerz "  ng-if="name != \'\' && value != \'\' && value != \'Select\' && name[0].position != 0 && name[0].position != -1" >' +
                    '<div class="leftz summaryRow" style="width:30%" ng-class="{linkHeaderName: name == \'.\'}">{{name}}</div>' +
                    '<div class="rightz summaryRow"  style="width:70%" ng-class="{linkHeader: name == \'.\'}">{{value}}</div>' +
                '</div>' +


            '<div class="containerz "  ng-if="name != \'\' && value != \'\' && value != \'Select\' && name[0].position != 0 && name[0].position != -1" >' +
                '<div class="leftz summaryRow"  style="width:30%; border-bottom:1px dotted gray" ng-class="{linkHeaderName: name == \'.\'}"></div>' +
                '<div class="rightz summaryRow"  style="width:70%; border-bottom:1px dotted gray" ng-class="{linkHeader: name == \'.\'}"></div>' +
            '</div>'
            ;


        }
    };
});

