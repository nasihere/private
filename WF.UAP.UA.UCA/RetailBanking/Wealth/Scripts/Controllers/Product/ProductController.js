var EditIndex = null;

wmg.controller("ProductController", [
    '$scope', '$stateParams', 'stateService', 'goAheadService', 'wmgCommonService', 'Enums', '$timeout', 'appInitService',
function ($scope, $stateParams, stateService, goAheadService, wmgCommonService, Enums, $timeout, appInitService) {

    $scope.dd_PurposeItems = angular.copy(Common.DD_ProductPurposeItem);
    //Start - Helper Functions
    $scope.product = null;
    $scope.getProduct = function () {
        $.each(Root.applicationInfo.WealthData.productList, function (key, value) {
            if (value.productID == $stateParams.productId) {
                $scope.product = value;
                return value;
            }
        });
    };
    $scope.getProduct();

    $scope.getDescription = function (list, key) {
        return wmgCommonService.findDescription(list, key);
    };

    var refreshDropDown = function () {
        setTimeout(function () {
            svpApp.util.buildSelects();
        }, 50);
    };

    //End - Helper Functions

    //Start - Basic Initialization for Product Controller
    $scope.DynamicData = Root.applicationInfo.DynamicData.LOBList[0];
    $scope.DynamicAddress = Root.applicationInfo.DynamicData.addressList.address;
    $scope.Comment = $scope.product.collateralInfo;

    $scope.PathCdSaving = $scope.product.collateralInfo.financialCollteralList.cdSavingsCollateralList;
    $scope.PathMarketable = $scope.product.collateralInfo.financialCollteralList.marketableCollateralList;

    /*if (typeof $scope.PathCdSaving == "object") {
          $scope.PathCdSaving = [];
      }
      if (typeof $scope.PathMarketable == "object") {
          $scope.PathMarketable = [];
      }*/
    $scope.PathCdSaving = wmgCommonService.removeIfEmptyItemFound($scope.PathCdSaving);
    $scope.PathMarketable = wmgCommonService.removeIfEmptyItemFound($scope.PathMarketable);

    if (wmgCommonService.getApplicant("P") != undefined) {
        $scope.applicant = wmgCommonService.getApplicant("P");
    }
    if (wmgCommonService.getApplicant("S") != undefined) {
        $scope.coapplicant = wmgCommonService.getApplicant("S");
    }

    $scope.trustIndividualCreditFlag = Root.applicationInfo.businessTypeInfo.trustIndividualCredit;

    $scope.title = $stateParams.productId;

    $scope.ach = $scope.product.productFeatures.ACHData;
    $scope.odp = $scope.product.productFeatures.ODPData;
    $scope.Check = $scope.product.productFeatures.Checks;

    $scope.ach.achInd = wmgCommonService.setValueEmpty($scope.ach.achInd);
    $scope.odp.odpInd = wmgCommonService.setValueEmpty($scope.odp.odpInd);
    $scope.Check.checksInd = wmgCommonService.setValueEmpty($scope.Check.checksInd);

    $scope.removeProductTriggered = false;
    //End - Basic Initialization for Product Controller


    //Start - Basic Info UI Validation
    //FSD-FR.51.1 FR.51.2
    $scope.disablePurposeDesc = false;
    $scope.isForHomeImprovement = function () {
        var products = "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PG28Y,PB28Y";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    };
    $scope.checkHMDAQues1 = function () {
        if ($scope.product.HMDAQues1 == "Y") {
            $scope.product.loanPurpose = "HIMP";
            refreshDropDown();

            $scope.disablePurposeDesc = true;
        } else {
            $scope.product.loanPurpose = "";
            refreshDropDown();

            $scope.disablePurposeDesc = false;
        }
        $scope.changePurposeItem(); // to disabled or hide purpose description
    };

    //FSD-FR.51.3.B
    $scope.isPurchaseMoneySecond = function () {
        var products = "PB30Y";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    };
    $scope.checkHMDAQues2 = function () {
        if ($scope.product.HMDAQues2 == "Y") {
            $scope.product.loanPurpose = "PMSD";
            refreshDropDown();
            $scope.disablePurposeDesc = true;
        } else {
            $scope.product.loanPurpose = "";
            refreshDropDown();
            $scope.disablePurposeDesc = false;
        }
        $scope.changePurposeItem(); // to disabled or hide purpose description
    };

    //FSD-FR.51.3.B
    $scope.isMoneyUsedForRefinance = function () {
        var products = "PB30Y,PB34Z,PB36K";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    };
    $scope.checkHMDAQues3 = function () {
        if ($scope.product.HMDAQues3 == "Y") {
            $scope.product.loanPurpose = "REFN";
            refreshDropDown();
            $scope.disablePurposeDesc = true;
        } else {
            $scope.product.loanPurpose = "";
            refreshDropDown();
            $scope.disablePurposeDesc = false;
        }
        $scope.changePurposeItem(); // to disabled or hide purpose description
    };

    //FSD-FR.15.1.C
    $scope.removePurchaseMoney2nd = function () {
        if ($stateParams.productId == "PB30Y") {
            return;
        } else {
            var keyToRemove = "PMSD";
            for (var i = 0; i < $scope.dd_PurposeItems.length; i++) {
                if ($scope.dd_PurposeItems[i].key == keyToRemove) {
                    $scope.dd_PurposeItems.splice(i, 1);
                    break;
                }
            }
        }
    };
    $scope.removePurchaseMoney2nd();

    //FSD-FR.15.1.C
    $scope.removePurchaseDwelling = function () {
        if ($stateParams.productId == "PB30Y" ||
            $stateParams.productId == "PB34Z" ||
            $stateParams.productId == "PB36K" ||
            $stateParams.productId == "PB28G" ||
            $stateParams.productId == "PB28Y" || 
            $stateParams.productId == "PB281" ||
            $stateParams.productId == "PB282" ||
            $stateParams.productId == "PB25G" ||
            $stateParams.productId == "PB25H" ||
            $stateParams.productId == "PB251" ||
            $stateParams.productId == "PB252") {
            var keyToRemove = "PRDW";
            for (var i = 0; i < $scope.dd_PurposeItems.length; i++) {
                if ($scope.dd_PurposeItems[i].key == keyToRemove) {
                    $scope.dd_PurposeItems.splice(i, 1);
                    break;
                }
            }
        }
    };
    $scope.removePurchaseDwelling();

    //FSD-FR.135
    $scope.changePurposeItem = function () {
        $scope.NC.l = $scope.product.loanPurpose; // Console.log In UI
        if ($scope.product.loanPurpose == "OTHR" || $scope.product.loanPurpose == "PERS"
            || $scope.product.loanPurpose == "CARE") {
            $scope.isPurposeDescriptionApplication = true;

        } else {
            $scope.isPurposeDescriptionApplication = false;

        }
        $scope.product.purposeDesc = "";

        if ($scope.product.loanPurpose == "HIMP") {
            $scope.product.HMDAQues1 = "Y";
            setTimeout(function () {
                svpApp.util.buildSelects();
            }, 500);
            $scope.NC.s = $scope.product.HMDAQues1e; // Console.log In UI
        }
        else if ($scope.product.loanPurpose == "REFN") {
            $scope.product.HMDAQues3 = "Y";
            $scope.product.HMDAQues2 = "N";
            setTimeout(function () {
                svpApp.util.buildSelects();
            }, 500);
            $scope.NC.s = $scope.product.HMDAQues3; // Console.log In UI
        }
        else if ($scope.product.loanPurpose == "PMSD") {
            $scope.product.HMDAQues2 = "Y";
            $scope.product.HMDAQues3 = "N";
            setTimeout(function () {
                svpApp.util.buildSelects();
            }, 500);
            $scope.NC.s = $scope.product.HMDAQues2; // Console.log In UI
        }
        else {
            if ($scope.product.loanPurpose != "HIMP") {
                $scope.product.HMDAQues1 = "N";
                setTimeout(function () {
                    svpApp.util.buildSelects();
                }, 500);
                $scope.NC.s = $scope.product.HMDAQues1; // Console.log In UI
            }
            else if ($scope.product.loanPurpose != "PMSD" && $scope.product.loanPurpose != "REFN") {
                $scope.product.HMDAQues3 = "N";
                $scope.product.HMDAQues2 = "N";
                setTimeout(function () {
                    svpApp.util.buildSelects();
                }, 500);
                $scope.NC.s = $scope.product.HMDAQues2 + $scope.product.HMDAQues3; // Console.log In UI
            }
        }
    };
    //FSD-FR.136 -As per this requirement added the value 'Purchase or Carry Margin Stock' in static data

    /* - Nasir
    CR-14671, FR.193.1)  If product = PB251, PB252, PB25G, PB25H, or PB31I, PB31K, , PB34Z, PB36K, then display:
    Label:  Is this request to refinance or payoff a loan or line which is secured by Liquid Collateral?
    */
    $scope.isRequestRefinancePayOffApplicable = function () {
        var products = "PB251,PB252,PB25G,PB25H,PB31I,PB31K,PB34Z,PB36K";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    }
    /*---------------*/

    /* - Nasir
        FR.193.1)  If product = PB251, PB252, PB25G, PB25H, or PB31I, PB31K, PB34Z, PB36K, then display:
        Label:  Are all of the collateral accounts funded at Wells Fargo?

   */
    $scope.isCollateralFundedAtWFApplicable = function () {
        var products = "PB251,PB252,PB25G,PB25H,PB31I,PB31K,PB34Z,PB36K";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    }
    /*-----------------------*/


    //FSD-FR.109, FR.115, FR.121, FR.127
    $scope.isPaymentTypeApplicable = function () {
        var products = "PB31I,PB34I,PB34Z,PB37I";
        return wmgCommonService.isProductExistsIn($stateParams.productId, products);
    };

    //FSD-FR.263
    $scope.isMarginIndexApplicable = function () {
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB31I,PB34I,PB34Z,PB37I");
    };

    //FSD-Page-27
    $scope.isTermApplicable = function () {
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB22X,PB22Y,PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
    };

    //FSD-Page-
    //(Note to developer: Automatic Payment (ACH) is not available to line increase products.)
    $scope.flagACHApplicable = wmgCommonService.isProductExistsIn($stateParams.productId, "PB30Y,PB31K,PB34K,PB34R,PB36K,PB37K");


    //FSD-Page-27
    $scope.flagAmortizationApplicable = wmgCommonService.isProductExistsIn($stateParams.productId, "PB22X,PB252,PB25G,PB282,PB28G");

    // $scope.InvisibleAmortizationApplicable = wmgCommonService.isProductExistsIn($stateParams.productId, "PB22Y,PB251,PB25H,PB281,PB28Y");

    $scope.isAmortizationApplicable = function () {
        //return wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB22X,PB22Y,PB281,PB282,PB28G,PB28Y");
        //Highlighted are the Product codes for I/O products. You should not show amortization for these products. - Sahana Requested
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB22X,PB252,PB25G,PB282,PB28G");
    };

    $scope.prefillAmortization = function () {
        if ($scope.isAmortizationApplicable() == false) {
            return;
        }
        $scope.product.amortization = $scope.product.term;
    };

    $scope.isUnderWritingApplicable = function () {
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB31I,PB31K");
    };

    $scope.isIncreaseOrRenewalApplicable = function () {
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB30Y,PB36K,PB31K,PB34K,PB34R,PB37K");
    };
    //End - Basic Info UI Validation


    //Start - General Collaterals Section Validation
    $scope.checkCollateralGiven = function () {
        var productExists = wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB31I,PB31K,PB34Z,PB36K");

        if (productExists == true) {
            var cdSavingsCount = wmgCommonService.getCDSavingsCountForProduct($stateParams.productId);
            var marketableCount = wmgCommonService.getMarketablCountForProduct($stateParams.productId);

            if (cdSavingsCount == 0 && marketableCount == 0) {
                return false;
            } else {
                //Note: there is not requirement mentioned on the Else part of this logic, still assuming to proceed
                return true;
            }
        } else {
            return true;
        }
    };
    if (($scope.PathMarketable.length > 0) || ($scope.PathCdSaving.length > 0)) {
        $scope._isCollateral.given = true;
    }
    //End - General Collaterals Section Validation


    //Start - Collaterals Section - CRUD Operation
    $scope.CDSaving = [];
    $scope.market = [];
    $scope.SaveCollateral = function (formobject, type) {
        // try {
        if (pageValidator("modal") == true) {

            if (type == "CDSaving") {
                if (EditIndex != null) {
                    $scope.PathCdSaving[EditIndex] = angular.copy($scope.CDSaving.Form);
                    //  $scope.product.nonRECollateral.CDSavingsList.splice($scope.CDSaving.Form.index, 1);
                } else {

                    $scope.PathCdSaving.push(angular.copy($scope.CDSaving.Form));
                }
                $scope.modalPopupCdSavingClose();


            } else if (type == "Marketable") {
                if (EditIndex != null) {
                    //$scope.product.nonRECollateral.marketableCollateralList.splice($scope.market.Form.index, 1);
                    $scope.PathMarketable[EditIndex] = angular.copy($scope.market.Form);
                } else {
                    $scope.PathMarketable.push(angular.copy($scope.market.Form));
                }
                $scope.modalPopupMarketableClose();

            }

            // $scope.CDSaving = [];
            // $scope.market = [];
        }
        wmgCommonService.getAutoheightIFrame();
        // } catch (err) {
        //    alert("warning " + err)
        // }

    };

    $scope.removeCollateral = function (index, type) {
        if (type == "CDSaving") {
            $scope.PathCdSaving.splice(index, 1);
        } else if (type == "Marketable") {
            $scope.PathMarketable.splice(index, 1);
        }

    };

    $scope.editCollateral = function (index, type, href, datatitle, cancelbtn, buttontext) {
        if (type == "CDSaving") {
            $scope.CDSaving.Form = angular.copy($scope.PathCdSaving[index]);
            $scope.CDSaving.Form.collOwnerInd = "Y";
            $scope.CDSaving.Form.index = index;
            $scope.modalPopupCdSaving(index);

        } else if (type == "Marketable") {
            $scope.market.Form = angular.copy($scope.PathMarketable[index]);
            $scope.market.Form.index = index;
            $scope.modalPopupMarketable(index);
        }

    };
    //End - Collaterals Section  CRUD Operation


    // Start - Marketable modal popup open
    $scope.modalPopupMarketable = function (index) {

        if (index == undefined) {
            if ($scope.market.Form != undefined) {
                $scope.market.Form = wmgCommonService.emptyGivenObject($scope.market.Form);

            }
            index = null;
        }

        EditIndex = index;
        $("#dvMarketableModal").show();
        wmgCommonService.getCursorTop();
        var widthContainer = 1250;
        var bodyContainer = $('body').width();

        $("#dvMarketableModal .modalcontainer").css({ width: widthContainer + "px" });
        var LeftSpace = (bodyContainer - widthContainer) / 2;
        $("#dvMarketableModal .modalcontainer").css({ left: LeftSpace + "px" });

        setTimeout(function () {
            svpApp.util.buildSelects();
        }, 500);
    };
    $scope.modalPopupMarketableClose = function () {
        EditIndex = null;
        $("#dvMarketableModal").hide();
    };
    //End - Marketable modal popup open


    // Start - CD Saving Modal Popup Open
    $scope.modalPopupCdSaving = function (index) {

        if (index == undefined) {
            if ($scope.CDSaving.Form != undefined) {

                $scope.CDSaving.Form = wmgCommonService.emptyGivenObject($scope.CDSaving.Form);

            }
            index = null;
        }

        EditIndex = index;
        $("#dvCDSavingModal").show();
        wmgCommonService.getCursorTop();
        var widthContainer = 1105;
        var bodyContainer = $('body').width();

        $("#dvCDSavingModal .modalcontainer").css({ width: widthContainer + "px" });
        var LeftSpace = (bodyContainer - widthContainer) / 2;
        $("#dvCDSavingModal .modalcontainer").css({ left: LeftSpace + "px" });

        setTimeout(function () {
            svpApp.util.buildSelects();
        }, 500);
    };
    // End
    $scope.modalPopupCdSavingClose = function () {
        EditIndex = null;
        $("#dvCDSavingModal").hide();
    };


    // Start - CD Saving Modal Form Logic
    //FSD-FR.196, FR.204
    $scope.autoFilledOwner1 = false;
    $scope.autoFilledOwner2 = false;

    $scope.autoFillCollateralForm = function (collateralForm) {
        if (collateralForm.collOwnerInd == "Y") {
            //FR.204
            if ($scope.applicant.applicantName != undefined) {
                collateralForm.collOwner.owner1 = $scope.applicant.applicantName;
                collateralForm.collOwner.owner3 = "";
                collateralForm.collOwner.owner4 = "";
                if (collateralForm.collOwner.owner1.length > 0) $scope.autoFilledOwner1 = true;
            }
            if ($scope.coapplicant != null && $scope.coapplicant.applicantName != undefined) {
                collateralForm.collOwner.owner2 = $scope.coapplicant.applicantName;
                if (collateralForm.collOwner.owner2.length > 0) $scope.autoFilledOwner2 = true;
            } else {
                collateralForm.collOwner.owner2 = "";
            }

            //FR.198
            $scope.DynamicAddress = wmgCommonService.getCustomerAddress("CR");
            collateralForm.collAddress.line1 = $scope.DynamicAddress.addressLine;
            collateralForm.collAddress.line2 = "";
            collateralForm.collAddress.state = $scope.DynamicAddress.state;
            collateralForm.collAddress.city = $scope.DynamicAddress.city;
            collateralForm.collAddress.zip = $scope.DynamicAddress.zip;

            if (collateralForm != undefined) {
                collateralForm.acctNumber = "";
                collateralForm.amount = "";
                collateralForm.collType = "";
                collateralForm.maturityDate = "";
            }

        } else {
            collateralForm.collOwner = wmgCommonService.emptyGivenObject(collateralForm.collOwner);
            collateralForm.collAddress = wmgCommonService.emptyGivenObject(collateralForm.collAddress);
            collateralForm.securityInformation = wmgCommonService.emptyGivenObject(collateralForm.securityInformation);

            if (collateralForm != undefined) {
                collateralForm.acctNumber = "";
                collateralForm.amount = "";
                collateralForm.collType = "";
                collateralForm.maturityDate = "";
            }
        }
        setTimeout(function () {
            svpApp.util.buildSelects();
        }, 500);
    };

    //NOTE:  FR.204 is implemented in UI.

    //FSD-FR.83 BRD.V6.0 (Can be used once Nasir completes the code changes)
    $scope.isTrustInfoApplicable = function () {
        return wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB31I,PB31K,PB34Z,PB36K");
    };



    // End - CD Saving Modal Form Logic


    // Start - Remove product functionality
    //FSD-Section-5.1.2
    $scope.removeProduct = function () {
        $scope.removeProductTriggered = true;

        //This is to validate RECollateral Page applicable or not
        var status = wmgCommonService.isProductExistsIn($scope.product.productID, "PB30Y, PB34Z, PB36K");
        if (status == true && $scope._isRECollateralApplicable.status == false) stateService.disableView(Enums.wmgViews.RECollateral);

        //Remove product and Update the routing states
        var existingProductCount = stateService.skipProductRouting($scope.product.productID);
        $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);

        //This is to hide the error message
        $scope._isCollateral.given = true;

        //This is required to set HMDA Screen 
        $scope._isHmdaApplicable.status = false;

        if (existingProductCount == 0) {
            stateService.disableFromAndMoveTo(stateService.currentLocation() + 1, Enums.wmgViews.SubmitApp);
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            //work around to hide the Products menu
            $scope.routingStates[1].visible = false;
        } else {
            $scope.moveNext();
        }
    };

    // End - Remove product functionality

    $scope.resetTrustTypeNo = function (val, jsonReference) {
        if (val == "N") {
            jsonReference.type = "";
            jsonReference.competentToSign = "";
            jsonReference.name = "";
            jsonReference.taxId = "";
            refreshDropDown();
        }

    };


    //Start - ACH UI Validation
    //FSD-FR.174
    $scope.changeACHPaymentType = function (val) {
        if (val == "3") {
            $scope.isAdditionalPrincipalApplication = true;

        } else {
            $scope.isAdditionalPrincipalApplication = false;

        }
        $scope.ach.additionapPrincipal = "";
        if (val == "5") {
            $scope.isPreferredFixAmountApplication = true;
        } else {
            $scope.isPreferredFixAmountApplication = false;
        }
        $scope.ach.preferredFixAmount = "";
    };
    //End - ACH UI Validation


    //Start - Checks UI Validation

    if ($scope.Check.deliveryAddress == null || $scope.Check.deliveryAddress == undefined) $scope.Check.deliveryAddress = "";
    $scope.isChecksApplicable = function () {
        return appInitService.isChecksApplicable($stateParams.productId);
    };
    //End - Checks UI Validation


    //START - Validate is HMDA Screen is applicable
    //This is as per the requirement we need to decide to show / hide the HMDA Screen
    //based on the values entered in Products screen
    //FSD-Section-5.1.9
    $scope.isHMDAScreenApplicable = function () {
        //FSD-FR.51.1, FR.51.2, FR.51.1.A //FSD-FR.51.3.A, FR.51.1.B

        //NOTE: This IF condition is required, becz just in case the 1st product HMDA Applicable or for 2nd Product it is not, in that case we always get HMDA is not applicable
        $scope.products = wmgCommonService.getAllProductrs();
        for (var i = 0; i < $scope.products.length; i++) {
            if ($scope.products[i].isProductRemoved == 'Y') {
                continue;
            }
            if (wmgCommonService.isProductExistsIn($scope.products[i].productID, "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y")) {
                if ($scope.products[i].loanPurpose != undefined && $scope.products[i].loanPurpose == "HIMP") {
                    $scope._isHmdaApplicable.status = true;
                    $scope._showAppRaceEthnicity.status = true;
                    if ($scope.coapplicant != null) {
                        $scope._showCoAppRaceEthnicity.status = true;
                    }

                    return true;
                }
            }
        }

        //var result = wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
        //if (result == true) {
        //    if ($scope.product.loanPurpose == "HIMP") {
        //        $scope._isHmdaApplicable.status = true;

        //        $scope._showAppRaceEthnicity.status = true;
        //        if ($scope.coapplicant != null) {
        //            $scope._showCoAppRaceEthnicity.status = true;
        //        }

        //        return true;
        //    }
        //}
        $scope._isHmdaApplicable.status = false;
        return false;
    };

    $scope.validateHMDAApplicable = function () {
        if ($scope.isHMDAScreenApplicable() == false) {
            stateService.disableView(Enums.wmgViews.Hmda);
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
        } else {
            stateService.enableView(Enums.wmgViews.Hmda);
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
        }
    };

    //END - Validate is HMDA Screen is applicable


    $scope.isRECollateralScreenApplicable = function () {

        if (($stateParams.productId == "PB30Y")
            && (($scope.product.loanPurpose == "PMSD") || ($scope.product.loanPurpose == "REFN"))) {
            $scope._isRECollateralApplicable.status = true;
        }
        if ((($stateParams.productId == "PB34Z") || ($stateParams.productId == "PB36K"))
            && (($scope.product.loanPurpose == "REFN"))) {
            $scope._isRECollateralApplicable.status = true;
        }
    };

    $scope.$on("Products.1", $scope.validateBeforeContinue);
    $scope.$on("Products.2", $scope.validateBeforeContinue);
    $scope.$on("Products.3", $scope.validateBeforeContinue);
    $scope.$on("Products.4", $scope.validateBeforeContinue);

    $scope.$on("Products.1", $scope.validateBeforeContinue = function (event, data) {
        productScreenValidation(data);
    });
    $scope.$on("Products.2", $scope.validateBeforeContinue = function (event, data) {
        productScreenValidation(data);
    });
    $scope.$on("Products.3", $scope.validateBeforeContinue = function (event, data) {
        productScreenValidation(data);
    });
    $scope.$on("Products.4", $scope.validateBeforeContinue = function (event, data) {
        productScreenValidation(data);
    });

    var productScreenValidation = function (data) {
        //If the user is clicking REMOVE then there is no need to do the validation
        if ($scope.removeProductTriggered == true) {
            goAheadService.setFlag(true);
            return;
        }

        //Check if HMDA is applicable or not based on the Product informatio provided
        $scope.validateHMDAApplicable();
        //$scope._isCollateralGiven = true; //This variable is used in UI
        if (wmgCommonService.isProductExistsIn($stateParams.productId, "PB251,PB252,PB25G,PB25H,PB31I,PB31K,PB34Z,PB36K") == true) {
            $scope._isCollateral.given = $scope.checkCollateralGiven();
        }

        //Added this to make sure that the RE Collateral screen should be visible for the applicable products
        $scope.isRECollateralScreenApplicable();

        if ($scope._isCollateral.given == true) {
            goAheadService.setFlag(true);
        } else {
            goAheadService.setFlag(false); // need to chck .. we disconnect validation for now
        }
    };

    $scope.UpdateAmountRequested = function () {
        if (wmgCommonService.isProductExistsIn($stateParams.productId, "PB34R") == true) {
            if ($scope.product.originalLineAmount == undefined || $scope.product.originalLineAmount == 0) {
                $("#id_loanAmtRequested").attr('disabled', false);
            } else {
                $scope.product.loanAmtRequested = $scope.product.originalLineAmount;
                $("#id_loanAmtRequested").attr('disabled', true);
            }
        }
    };
}
]);


