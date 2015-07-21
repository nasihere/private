//Custom state service for WMG Application required to navigate
//State service follows the below logic
wmg.factory("stateServiceHelper", ['$state', 'wmgDataService', 'cookieService', 'routingStates', 'Enums', 'wmgCommonService',
    function ($state, wmgDataService, cookieService, routingStates, Enums, wmgCommonService) {
        var current = 0;
        var productCode = "";

        var wmgViews = routingStates;

        if (Root != undefined && Root != "") {
            var products = Root.applicationInfo.WealthData.productList;
        }

        var LocalAutoStartupPage = function(id) {
            cookieService.setStateLocationIndicator(id);
        };

        //This method will tell the current view is, Product view or not
        var isProductsView = function (screenName) {
            var viewName = screenName.substr(0, screenName.length - 2);
            if ('products' == viewName.toLowerCase()) {
                return true;
            }
            return false;
        };

        //This method will tell the total number of products coming as part of the XML
        //Note: For this product by default we declare it as 4 products in routing configuration
        var getTotalProductCount = function () {
            if (!angular.isArray(products)) {
                return 1;
            } else {
                return Root.applicationInfo.WealthData.productList.length;
            }
        };

        //Local function to set the flag when the user is clicking the remove product
        var resetRemoveProduct = function (productCode) {
            angular.forEach(products, function (item) {
                if (item.productID == productCode) {
                    item.isProductRemoved = "";
                }
            });

        };

        //Local function to set the flag when the user is clicking the remove product
        var removeProduct = function (productCode) {
            angular.forEach(products, function (item) {
                if (item.productID == productCode) {
                    item.isProductRemoved = "Y";
                }
            });

        };

        var removeProductRoutingState = function (productCode) {
            //This below loop is to disable the parameter so that can be used in breadcrumb
            for (var j = 0; j < wmgViews.length; j++) {
                if (wmgViews[j].param.productID == productCode) {
                    wmgViews[j].param.removeProduct = true;
                    break;
                }
            }
            //The below loop is used to set the flag in Json. 
            angular.forEach(products, function (item) {
                if (item.productID == productCode) {
                    item.isProductRemoved = "Y";
                }
            });
        };

        var isProductExists = function (productCode) {

            for (var j = 0; j < wmgViews.length; j++) {
                if (isProductsView(wmgViews[j].viewName) == true) {
                    if (productCode == wmgViews[j].param.productID) {
                        return true;
                    }
                }
            }
            return false;
        };

        var helper = {};

        //Helper Function to get updated views after the flag set
        helper.getUpdatedRoutingState = function (existingRoutes) {
            if (existingRoutes == undefined) {
                return existingRoutes;
            }
            for (var i = 0; i < existingRoutes.length; i++) {
                for (var j = 0; j < wmgViews.length; j++) {
                    if (wmgViews[j].viewName == existingRoutes[i].viewName) {
                        if (wmgViews[j].param.removeProduct != undefined) {
                            existingRoutes[i].param.removeProduct = wmgViews[j].param.removeProduct;
                        }
                        existingRoutes[i].visible = wmgViews[j].visible;
                        break;
                    }
                }
            }
            return existingRoutes;
        };

        //This method which will loop through the product XML/JSon to get the product based on the location not using the Index.
        helper.getProductCode = function (viewName) {
            var productLoc = Number(viewName.substr(viewName.length - 1, 1)) - 1;

            if (isNaN(productLoc)) {
                return null;
            }

            if (angular.isArray(products) == false)
                return products.productID;

            return products[productLoc].productID;

            //Below logic is may required if we decided to not to show, the strikedout product.
            //var prodcount = 0;
            //for (var i = 0; i < products.length; i++) {
            //    prodcount++;
            //    if (prodcount == productLoc) {
            //        return products[i].productID;
            //    }
            //if (products[i].isProductRemoved != "Y") {
            //    prodcount++;
            //    if (prodcount == productLoc) {
            //        return products[i].productID;
            //    }
            //}
            //}
            //return null;
        };
        //This method which will loop through the product XML/JSon to get the product based on the location not using the Index.
        helper.getProductName = function (viewName) {
            var productLoc = Number(viewName.substr(viewName.length - 1, 1));

            if (isNaN(productLoc)) {
                return null;
            }

            if (angular.isArray(products) == false)
                return products.productName;

            var prodcount = 0;
            for (var i = 0; i < products.length; i++) {
                if (products[i].isProductRemoved != "Y") {
                    prodcount++;
                    if (prodcount == productLoc) {
                        return products[i].productName;
                    }
                }
            }
            return null;
        };

        helper.setVisibilityOfViewByProductCode = function (productCode, flag) {

            for (var j = 0; j < wmgViews.length; j++) {
                if (isProductsView(wmgViews[j].viewName) == true) {
                    if (productCode == wmgViews[j].param.productID) {
                        wmgViews[j].visible = flag;
                        wmgViews[j].param.removeProduct = !flag;

                    }
                }
            }
            //The below logic is to set the XML Flag (false = to hide/remove)
            if (flag == false) {
                removeProduct(productCode);
            } else {
                resetRemoveProduct(productCode);
            }
        };
        helper.checkAllProductRemoved = function () {
            var countProduct = 0;
            var countcancelProduct = 0;
            for (var j = 0; j < wmgViews.length; j++) {
                if (isProductsView(wmgViews[j].viewName) == true) {
                    if (wmgViews[j].visible == false) {
                        countcancelProduct++;
                    }
                    countProduct++;
                }
            }
            return (countProduct - countcancelProduct);
        };
        helper.getNextView = function () {
            var lastPage = cookieService.getStateLocationIndicator();

            for (var i = lastPage + 1; i < wmgViews.length; i++) {
                if (wmgViews[i].visible == true) {

                    //The below 'IF' condition added to show the strikeout
                    if (isProductsView(wmgViews[i].viewName) == true) {
                        if (wmgViews[i].param != undefined && wmgViews[i].param.removeProduct == false) {
                            cookieService.setStateLocationIndicator(i);
                            current = i;
                            break;
                        }
                    } else {
                        cookieService.setStateLocationIndicator(i);
                        current = i;
                        break;
                    }

                }
            }
            return wmgViews[current];
        };

        helper.getPreviousView = function () {
            var lastPage = cookieService.getStateLocationIndicator();

            for (var i = lastPage - 1; i >= 0 ; i--) {
                if (wmgViews[i].visible == true) {

                    //The below 'IF' condition added to show the strikeout 
                    if (isProductsView(wmgViews[i].viewName) == true) {
                        if (wmgViews[i].param != undefined && wmgViews[i].param.removeProduct == false) {
                            cookieService.setStateLocationIndicator(i);
                            current = i;
                            break;
                        }
                    } else {
                        cookieService.setStateLocationIndicator(i);
                        current = i;
                        break;
                    }
                }
            }
            return wmgViews[current];
        };

        helper.getCurrentViewName = function () {
            var c = cookieService.getStateLocationIndicator();
            return wmgViews[c].viewName;
        };

        helper.getCurrentState = function () {
            var c = cookieService.getStateLocationIndicator();
            return wmgViews[c];
        };

        helper.getCurrentLocation = function () {
            return cookieService.getStateLocationIndicator();
        };

        helper.skipProductView = function (productCode) {
            removeProduct(productCode); //Set the Flag in the XML
            wmgViews[current].visible = false;
        };

        helper.resetProductView = function (productCode) {
            resetRemoveProduct(productCode); //Set the Flag in the XML
            wmgViews[current].visible = true;
        };

        helper.skipProductRoutingView = function (productCode) {
            removeProductRoutingState(productCode);
            //wmgViews[current].visible = false;
        };

        helper.LocalAutoStartupPage = function (id) {
            LocalAutoStartupPage(id);
            //wmgViews[current].visible = false;
        };
        
        helper.hopToView = function (view) {
            cookieService.setStateLocationIndicator(view);
            return wmgViews[view];
        };

        helper.getAllViews = function (withProductsHeader) {
            if (withProductsHeader == undefined) {
                return wmgViews;
            } else {
                var views = angular.copy(wmgViews);
                for (var i = 0; i < views.length; i++) {
                    if (isProductsView(views[i].viewName)) {
                        views.splice(i, 0, { viewName: "Products", visible: true, groupTitle: "MainPage", breadCrumbTitle: "Products", param: {} });
                        break;
                    }
                }
                return views;
            }
        };

        helper.enableDisableView = function (stateLocation, status) {
            var loc = cookieService.getStateLocationIndicator();
            if (stateLocation < loc) {
                //console.log("Are you sure to enable this location, you already passed that state..!!.");
            }
            wmgViews[stateLocation].visible = status;
        };

        helper.disableFrom = function (from) {
            disableRightStates(from);
        };

        //TODO: we need to do change in this method to take parameter, aslo passing parameter to goTonNExt method need to make generic
        helper.disableFromAndMoveTo = function (from, to) {
            if (from == to) {
                console.log("From and To States should not be same location");
                return false;
            }

            var loc = cookieService.getStateLocationIndicator();
            if (loc == from) {
                console.log("Starting Location should not be the current state");
                return false;
            }

            if ((to == undefined) || (from < to)) {
                disableRightStates(from, to - 1);
                return true;
            } else {
                disableLeftStates(from, to + 1);
                return true;
            }
        };

        var disableLeftStates = function (from, to) {
            if ((to == undefined)) {
                to = 0;
            }
            for (var i = from; i >= to; i--) {
                wmgViews[i].visible = false;
            }
        };

        var disableRightStates = function (from, to) {
            if ((to == undefined)) {
                to = wmgViews.length;
            }
            for (var i = from; i <= to; i++) {
                wmgViews[i].visible = false;
            }
        };

        //This method is used to chop out list of declared states with actual products we received as part of the XML
        //If the Product is not part of this flow it will be set as 'state.data.status = false'
        var configureProductScreen = function () {
            if (wmgDataService.applicationError == true) {
                return false;
            }

            var productCounter = 1;
            var totalproducts = wmgCommonService.getTotalProductCount();
            for (var j = 0; j < wmgViews.length; j++) {

                if (isProductsView(wmgViews[j].viewName) == true) {

                    if (productCounter <= totalproducts) {

                        productCode = helper.getProductCode(wmgViews[j].viewName);
                        var product = wmgCommonService.getProductUsing(productCode);

                        if (product.isProductRemoved == 'Y') {
                            wmgViews[j].param.removeProduct = true;
                            wmgViews[j].visible = false;
                        } else {
                            wmgViews[j].param.removeProduct = false;
                            wmgViews[j].visible = true;
                        }
                        wmgViews[j].param.productID = productCode;
                        wmgViews[j].breadCrumbTitle = product.productName;
                        wmgViews[j].groupTitle = "Product ";


                        productCounter++;

                    } else {
                        wmgViews[j].visible = false;
                    }
                }
            }
        };
        configureProductScreen(); //IIFE

        return helper;
    }]);


wmg.factory("stateService", ['$state', 'wmgDataService', 'stateServiceHelper', 'cookieService',
    function ($state, wmgDataService, stateServiceHelper, cookieService) {

        var routeService = {};



        routeService.moveNext = function () {
            var view = stateServiceHelper.getNextView();
            $state.go(view.viewName, { productId: view.param.productID });

            return 0;
        };

        routeService.movePrevious = function () {
            var view = stateServiceHelper.getPreviousView();
            $state.go(view.viewName, { productId: view.param.productID });
            return 0;
        };

        //This returns the View Name
        routeService.getCurrentView = function () {
            return stateServiceHelper.getCurrentViewName();
        };

        routeService.goToView = function (viewName) {
            var state = stateServiceHelper.hopToView(viewName);
            $state.go(state.viewName, { productId: state.param.productID });
        };

        routeService.skipProduct = function (productCode) {
            stateServiceHelper.skipProductView(productCode);
        };
        routeService.resetProductView = function (productCode) {
            stateServiceHelper.resetProductView(productCode);
        };

        routeService.skipProductRouting = function (productCode) {
            stateServiceHelper.skipProductRoutingView(productCode);
            return stateServiceHelper.checkAllProductRemoved();
        };

        routeService.getAllViews = function (withProductsHeader) {
            return stateServiceHelper.getAllViews(withProductsHeader);
        };

        routeService.disableFromAndMoveTo = function (from, to) {
            var status = stateServiceHelper.disableFromAndMoveTo(from, to);
            if (status == true) {
                routeService.goToView(to);
            }
        };
        routeService.disableFrom = function (from) {
            return stateServiceHelper.disableFrom(from);
        };
        routeService.LocalAutoStartupPage = function (id) {
            return stateServiceHelper.LocalAutoStartupPage(id);
        };

        routeService.enableView = function (view) {
            stateServiceHelper.enableDisableView(view, true);
        };

        routeService.disableView = function (view) {
            stateServiceHelper.enableDisableView(view, false);
        };

        routeService.currentLocation = function () {
            return stateServiceHelper.getCurrentLocation();
        };

        routeService.disableProductByCode = function (productCode, visibility) {
            return stateServiceHelper.setVisibilityOfViewByProductCode(productCode, visibility);
        };

        routeService.getUpdatedRoutingState = function (existingRoutes) {
            return stateServiceHelper.getUpdatedRoutingState(existingRoutes);
        };

        return routeService;
    }]);

//Cookie Store service which stores the values of the Current Page so that
//during the refresh of the page it will not get routed back to the first page
wmg.factory("cookieService", ["$cookieStore", function ($cookieStore) {
    var service = {};

    service.val = 0;
    service.getStateLocationIndicator = function () {
        return this.val;
        //try {
        //    this.val = $cookieStore.get("counterKey");
        //    if (this.val == undefined) {
        //        //$cookieStore.put("counterKey", 0);
        //        return 0;
        //    }
        //    return this.val;
        //} catch (e) {
        //    //$cookieStore.put("counterKey", 0);
        //    return 0;
        //}
    };

    service.setStateLocationIndicator = function (counter) {
        this.val = counter;
        //$cookieStore.put("counterKey", counter);
        //pageCounter.set(counter);
    };
    return service;
}]);

//Note: This service one of the important service which is used to give signal to the controller to move to the next screen
//Or not, based on the flag only the stateService will get triggered in the baseController
wmg.factory("goAheadService", [function () {

    return {
        flag: true,
        canProceed: function () {
            return this.flag;
        },
        setFlag: function (status) {
            this.flag = status;
        }
    };
}]);

