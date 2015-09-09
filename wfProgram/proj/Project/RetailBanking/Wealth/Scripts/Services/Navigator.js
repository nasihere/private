
wmg.factory("navigator", function (scsXml) {

    var navigator = {};

    var applicationData = scsXml.applicationData;

    var index = 0;
    var currentPage = 1;
    var productIndex = 0;
    var totalScreens = 0;

    //The below need to get converted as common method/service
    var pages = [
        { screen: 1, index: 0 },
        { screen: 2, index: 0 },
        { screen: 3, index: 0 },
        { screen: 4, index: 0 },
        { screen: 5, index: 0 }
    ];

    totalScreens = pages.length;

    navigator.update = function () {
        applicationData.languagePref = this.language;
    };

    // Logic to decide the Product(s) navigation
    navigator.appendProductScreens = function () {
        var p = applicationData.productsData.product.length;
        for (var i = 1; i < p; i++) {
            pages.push({ screen: 2, index: i });
        }

        pages.sort(function (x, y) {
            return x.screen < y.screen ? -1 : x.screen > y.screen ? 1 : 0;
        });

        totalScreens = pages.length;
    };

    //Navigation to next and previous pages
    navigator.next = function () {
        if ((index + 1) <= totalScreens)
            index++;
        productIndex = pages[index].index;
        currentPage = pages[index].screen;
        return currentPage;
    };

    navigator.previous = function () {
        if ((index) > 0)
            index--;
        currentPage = pages[index].screen;
        productIndex = pages[index].index;
        return currentPage;
    };

    //Slicing down the Root Object to different childs 
    navigator.getProduct = function () {
        return applicationData.productsData.product[productIndex];
    };

    navigator.getOdp = function () {
        return applicationData.productsData.product[productIndex].ODPData;
    };

    navigator.getAch = function () {
        return applicationData.productsData.product[productIndex].ACHData;
    };

    navigator.getRoot = function () {
        return applicationData;
    };

    navigator.isOdpEmpty = function () {
        var isOdpEmpty = jQuery.isEmptyObject(applicationData.productsData.product[productIndex].ODPData);
        return isOdpEmpty;
    };

    navigator.isAchEmpty = function () {
        //var achApplicable = (applicationData.productsData.product[productIndex].isACHApplicable == 'Y') ? true : false;
        var isAchEmpty = jQuery.isEmptyObject(applicationData.productsData.product[productIndex].ACHData);
        return isAchEmpty;
    };

    return navigator;
});
