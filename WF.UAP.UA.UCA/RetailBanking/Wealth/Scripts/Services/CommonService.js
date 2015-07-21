wmg.factory("wmgCommonService", ['wmgDataService', '$http', function (wmgDataService, $http) {

    //Helper functions and global variables for this service
    if ((Root == undefined) || (Root == "")) {
        return false;
    }

    var allProducts = Root.applicationInfo.WealthData.productList;
    var customerAddresses = Root.applicationInfo.DynamicData.addressList;
    //console.log(allProducts);
    //console.log(Root);
    var keyDescription = "";
    var checkIfProductExistsIn = function (productId) {
        for (var j = 0; j < allProducts.length; j++) {
            if (productId.trim().toUpperCase() == allProducts[j].productID.trim().toUpperCase()) {
                return true;
            }
        }
    };

    var buildStaticDataList = function (data) {
        if (data == undefined) {
            return undefined;
        }
        console.log(data.staticData);
        var updatedStaticDataList = {};

        for (var key in data.staticData) {
            if (data.staticData.hasOwnProperty(key)) {
                if (angular.isObject(data.staticData[key]) == true) {
                    var innerList = [];
                    if (data.staticData[key].element == undefined) {
                        continue;
                    }
                    var elements = data.staticData[key].element;
                    for (var j = 0; j < elements.length; j++) {
                        innerList.push(elements[j]);
                    }
                    //data.staticData[key].splice(index, 1);

                    data.staticData[key] = [{}];
                    for (var i = 0; i < innerList.length; i++) {
                        data.staticData[key].push(innerList[i]);
                    }
                }
            }
        }
        updatedStaticDataList = data.staticData;
        return updatedStaticDataList;
    };

    //Service Functions
    return {
        getApplicant: function (type) {
            if (!angular.isArray(Root.applicationInfo.applicantInfoList)) {
                if (Root.applicationInfo.applicantInfoList.applicantType == type) {
                    return Root.applicationInfo.applicantInfoList;
                }
            } else {
                for (var i = 0; i < Root.applicationInfo.applicantInfoList.length; i++) {
                    if (Root.applicationInfo.applicantInfoList[i].applicantType == type) {
                        return Root.applicationInfo.applicantInfoList[i];
                    }
                }
                return undefined;
            }

        },
        getAllProductrs: function () {
            return Root.applicationInfo.WealthData.productList;
        },
        getAllActiveProducts: function () {
            if (allProducts == undefined) {
                return;
            }
            var activeProducts = [];
            for (var i = 0; i < allProducts.length; i++) {
                if (allProducts[i].isProductRemoved != 'Y') {
                    activeProducts.push(allProducts[i]);
                }
            }
            return activeProducts;
        },
        getProductUsing: function (productId) {
            if (productId == undefined || productId == "") {
                return null;
            }
            for (var i = 0; i < Root.applicationInfo.WealthData.productList.length; i++) {
                if (Root.applicationInfo.WealthData.productList[i].productID.trim().toUpperCase() == productId.trim().toUpperCase()) {
                    return Root.applicationInfo.WealthData.productList[i];
                }
            }
        },
        //This method will check the given product exists in the given product list 
        isProductExistsIn: function (productId, products) {
            if (productId == undefined || products == undefined || products.length == 0 || wmgDataService.applicationError == true) {
                return false;
            }
            
            //Need to add this below to avoid the cross condition scenario
            var prod = this.getProductUsing(productId);
            if (prod.isProductRemoved == 'Y') {
                return false;
            }
            var givenProducts = products.split(",");
            for (var i = 0; i < givenProducts.length; i++) {
                if (productId.trim().toUpperCase() == givenProducts[i].trim().toUpperCase()) {
                    return true;
                }
            }
            return false;
        },
        //This method will check whether the given set of products exists/matches with the received 4 products (as part of Json)
        isProductFound: function (products) {
            if (products == undefined || products.length == 0 || wmgDataService.applicationError == true) {
                return false;
            }
            var givenProducts = products.split(",");

            for (var i = 0; i < givenProducts.length; i++) {
                if (checkIfProductExistsIn(givenProducts[i]) == true) {
                    return true;
                }
            }
            return false;
        },
        findDescription: function (fromList, usingKey) {
            if (fromList == undefined || fromList == "" || usingKey == "") {
                keyDescription = "";
                return "";
            }
            angular.forEach(fromList, function (object, index) {
                if (object.key == usingKey) {
                    keyDescription = object.value;
                    return true;
                }
            });

            return keyDescription;
        },
        getTotalProductCount: function () {
            if (!angular.isArray(Root.applicationInfo.WealthData.productList)) {
                return 1;
            } else {
                return Root.applicationInfo.WealthData.productList.length;
            }
        },
        getObjectUsingKey: function (givenKey, fromList) {
            if (fromList == "" || fromList == undefined || givenKey == "" || givenKey == undefined) {
                return undefined;
            }
            for (var i = 0; i < fromList.length; i++) {
                if (fromList[i].key == givenKey) {
                    return fromList[i];
                }
            }
        },
        getCDSavingsCountForProduct: function (productId) {
            if (productId == "" || productId == undefined) {
                return 0;
            }
            for (var i = 0; i < allProducts.length; i++) {
                if (allProducts[i].productID == productId) {
                    return allProducts[i].collateralInfo.financialCollteralList.cdSavingsCollateralList.length;
                }
            }
            return 0;
        },
        getMarketablCountForProduct: function (productId) {
            if (productId == "" || productId == undefined) {
                return 0;
            }

            for (var i = 0; i < allProducts.length; i++) {
                if (allProducts[i].productID == productId) {
                    return allProducts[i].collateralInfo.financialCollteralList.marketableCollateralList.length;
                }
            }
            return 0;
        },
        removeIfEmptyItemFound: function (fromList) {


            //Note this method will filter/remove if the 'fromList' has only one entry
            if (fromList.length > 1) {
                return fromList;
            }
            var listItem = fromList[0];
            var allFieldsEmpty = true;
            if (listItem == undefined) {
                return fromList;
            }


            for (var key in listItem) {
                if (listItem.hasOwnProperty(key)) {
                    //console.log(angular.isObject(listItem[key]));
                    if (angular.isObject(listItem[key]) == false) {
                        if (listItem[key] == null) {
                            allFieldsEmpty = false;
                            continue;
                        }
                        if (listItem[key].trim().length == 0) {
                            allFieldsEmpty = true;
                        } else {
                            allFieldsEmpty = false;
                            break;
                        }
                    } else {
                        allFieldsEmpty = this.checkContent(listItem[key]);
                    }
                }
            }


            if (allFieldsEmpty == true) {

                fromList.shift();
                return fromList;
            }
            return fromList;
        },
        //The below is temp function need to make it as local function not class function
        checkContent: function (object) {
            var allFieldsEmpty = true;
            for (var key in object) {
                if (object.hasOwnProperty(key)) {
                    if (object[key].trim().length == 0) {
                        allFieldsEmpty = true;
                    } else {
                        allFieldsEmpty = false;
                    }
                }
            }
            return allFieldsEmpty;
        },
        getCursorTop: function () {
            try {
                $('html, body').scrollTop(0);
                $(parent.window).scrollTop(0);

            } catch (e) {
            }
        },
        getAutoheightIFrame: function () {
            try {
                setTimeout(function () { setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost"); }, 1000);

            } catch (e) {
            }
        },
        getCustomerAddress: function (type) {
            try {
                var crAddress = {};

                var addressToSend = {
                    addressType: "",
                    addressLine: "",
                    city: "",
                    state: "",
                    zip: ""
                };

                for (var i = 0; i < customerAddresses.length; i++) {
                    if (customerAddresses[i].addressType == type) {
                        crAddress = customerAddresses[i];
                    }
                }
                if (crAddress.addressLine != null && crAddress.addressLine != undefined) {
                    addressToSend.addressLine = crAddress.addressLine;
                }
                if (crAddress.city != null && crAddress.city != undefined) {
                    addressToSend.city = crAddress.city;
                }
                if (crAddress.state != null && crAddress.state != undefined) {
                    addressToSend.state = crAddress.state;
                }
                if (crAddress.zip != null && crAddress.zip != undefined) {
                    addressToSend.zip = crAddress.zip;
                }

                return addressToSend;

            } catch (e) {
                //console.log(e.message);
            }
        },
        emptyGivenObject: function (objectToEmpty) {
            try {
                if (objectToEmpty == undefined) {
                    return objectToEmpty;
                }
                for (var key in objectToEmpty) {
                    var objectType = typeof objectToEmpty[key];
                    if (objectType == "object") {
                        var innerObj = objectToEmpty[key];
                        for (var innerKey in innerObj) {
                            innerObj[innerKey] = "";
                        }
                    }
                    else {
                        if (objectToEmpty.hasOwnProperty(key) && objectType != "object") {
                            //We can do Call Recursion - BUT NEED TO TEST THIS
                            //if (angular.isObject(objectToEmpty[key]) == true) {
                            //    return this.emptyGivenObject(objectToEmpty[key]);
                            //}
                            objectToEmpty[key] = "";
                        }
                    }
                }
                return objectToEmpty;

            } catch (e) {
            }
        },
        populateStaticData: function () {
            var response;

            try {
                wmgDataService.readStaticDataFile().then(function (data) {
                    response = buildStaticDataList(data.data);
                });

                return response;
            } catch (e) {

            }
        },
        setValueEmpty: function (obj) {
            if (obj == null || obj == undefined) {
                return "";
            }
            return obj;
        },
        isHmdaApplicableForTheExistingProducts: function () {

            //FSD-FR.51.1, FR.51.2, FR.51.1.A //FSD-FR.51.3.A, FR.51.1.B (NEED TO CHECK)
            var caseOneResult = this.isProductFound("PB251,PB252,PB25G,PB25H,PB281,PB282,PB28G,PB28Y");
            var caseTwoResult = this.isProductFound("PB30Y,PB34Z,PB36K");
            var products = this.getAllProductrs();
            for (var i = 0; i < products.length; i++) {
                if ((caseOneResult == true)) {
                    if ((products[i].loanPurpose == "REFN") || (products[i].loanPurpose == "HIMP") || (products[i].loanPurpose == "PMSD")) {
                        return true;
                    }
                }
                //FST-FR-51.3.A, FR.51.3.B
                if ((caseTwoResult == true)
                   && ((products[i].loanPurpose == "REFN") || (products[i].loanPurpose == "HIMP") || (products[i].loanPurpose == "PMSD"))
                   && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
                   && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.appCollateralFlag == 'Y')) {
                    return true;
                }
                //FSD-FR.51.3, FR.51.3.A, FR.51.3.B
                if ((caseTwoResult == true)
                   && ((products[i].loanPurpose == "REFN") || (products[i].loanPurpose == "HIMP") || (products[i].loanPurpose == "PMSD"))
                   && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.occupancyType == 'P')
                   && (Root.applicationInfo.WealthData.realEstateCollateralList.realEstateCollateral.coAppCollateralFlag == 'Y')
                   && (Root.isCoAppAvailable == 'Y')) {
                    return true;
                }
            }
            return false;
        },
        removeMarkedProduct: function () {
            var products = this.getAllProductrs();
            if (products == undefined) {
                return;
            }
            try {
                for (var i = 0; i < products.length; i++) {
                    if (products[i].isProductRemoved == 'Y') {
                        products.splice(i, 1);
                    }
                }
            } catch (e) {

            }
        },
        aumWarningPopupShown: false,
        isAumWarningApplicable: function (aumValue) {
            if (aumValue == undefined) {
                aumValue = 0;
            }
            var pcount = this.getTotalProductCount();
            var flag = Root.applicationInfo.isTPBInd;

            if (pcount == 0)
                $scope.aumWarning = false;

            var productFound = this.isProductFound('PB34Z');
            var product = this.getProductUsing('PB34Z'); // added recently on 12/29/2014
            if ((productFound == true) && ((flag == 'N') || flag == '') && (aumValue < 1000000) && (product.isProductRemoved != 'Y')) {
                return true;
            } else {
                return false;
            }
        },
        setAumWarningPopUpFlag: function (flag) {
            this.aumWarningPopupShown = flag;
        }

    };

}]);