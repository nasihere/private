//This appInitService is required to do the basic evaluation of the application before it starts, 
//One usecase is that we need to decide when the application loads whether we need to show the KYC screen or not.
wmg.factory("appInitService", ['stateService', 'Enums', 'wmgCommonService', function (stateService, Enums, wmgCommonService) {

    if (Root == undefined || Root == "") {
        return false;
    }
    var allProducts = Root.applicationInfo.WealthData.productList;

    //var isProductExistsIn = function (productId, products) {

    //    if (productId == undefined || products == undefined || products.length == 0) {
    //        return false;
    //    }
    //    var givenProducts = products.split(",");
    //    for (var i = 0; i < givenProducts.length; i++) {
    //        if (productId.trim().toUpperCase() == givenProducts[i].trim().toUpperCase()) {
    //            return true;
    //        }
    //    }
    //    return false;
    //};

    return {
        checkIfKycApplicable: function () {
            if (Root == undefined || Root == "") {
                return false;
            }

            var status = Root.applicationInfo.isAMLKYCApplicable;
            if (status == 'N') {
                stateService.disableView(Enums.wmgViews.Kyc);
            }
        },
        //FSD-FR.146
        isChecksApplicable: function (productId) {
            var applicableChecksProducts = "PB31I,PB34I,PB34Z,PB37I";

            if (productId == undefined || productId.length == 0) {
                for (var i = 0; i < allProducts.length; i++) {
                    if (wmgCommonService.isProductExistsIn(allProducts[i].productID, applicableChecksProducts) == true) {
                        return true;
                    }
                }
            } else {
                if (wmgCommonService.isProductExistsIn(productId, applicableChecksProducts) == true) {
                    return true;
                }
            }
            return false;
        },
        //FSD-Section-5.1.5
        isProductFeaturesApplicable: function (productId) {
            var applicableProductsOfProductFeatures = "PB30Y,PB34Z,PB36K,PB22X,PB22Y,PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y,PB31I,PB31K,PB34I,PB34K,PB37I,PB37K";

            if (productId == undefined || productId.length == 0) {
                for (var i = 0; i < allProducts.length; i++) {
                    if (wmgCommonService.isProductExistsIn(allProducts[i].productID, applicableProductsOfProductFeatures) == true) {
                        return true;
                    }
                }
            } else {
                if (wmgCommonService.isProductExistsIn(productId, applicableProductsOfProductFeatures) == true) {
                    return true;
                }
            }
            return false;
        },
        //FSD-Section-5.1.7
        isRECollateralApplicable: function (productId) {
            var applicableCollateralProducts = "PB30Y,PB34Z,PB36K";
            if (productId == undefined || productId.length == 0) {
                for (var i = 0; i < allProducts.length; i++) {
                    if (wmgCommonService.isProductExistsIn(allProducts[i].productID, applicableCollateralProducts) == true) {
                        return true;
                    }
                }
            } else {
                if (wmgCommonService.isProductExistsIn(productId, applicableCollateralProducts) == true) {
                    return true;
                }
            }
            return false;
        },
        //FSD-FR.182
        isPersonalFinanceApplicable: function (productId) {
            var applicablePersonalFinanceProducts = "PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y,PB31I,PB34I,PB34K,PB34Z,PB37I";
            if (productId == undefined || productId.length == 0) {
                for (var i = 0; i < allProducts.length; i++) {
                    if (wmgCommonService.isProductExistsIn(allProducts[i].productID, applicablePersonalFinanceProducts) == true) {
                        return true;
                    }
                }
            } else {
                if (wmgCommonService.isProductExistsIn(productId, applicablePersonalFinanceProducts) == true) {
                    return true;
                }
            }
            return false;
        },
        isActiveProductsExists: function() {
            
        },
        appInitialize: function () {
            if (this.isChecksApplicable() == false) {
                //should be called outside
            }
            if (this.isProductFeaturesApplicable() == false) {
                //should be called outside
            }
            if (this.isRECollateralApplicable() == false) {
                stateService.disableView(Enums.wmgViews.RECollateral);
            }
            if (this.isPersonalFinanceApplicable() == false) {
                stateService.disableView(Enums.wmgViews.PersonalFinance);
            }
        }
    };
}]);

