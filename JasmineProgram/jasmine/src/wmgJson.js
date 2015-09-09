function WmgJson() {
}
WmgJson.prototype.tobeYesNo = function (obj) {
    if (obj == undefined)
        throw new Error("reference object is undefined");
    else if (obj == "Y" || obj == "N")
        return true;
    else
        throw new Error("reference object does not have yes or no value");
};



WmgJson.prototype.wisconsinDiscloserContent = function (objFlag, objContent) {
    if (objFlag == "N")
        return true;
    else if (objFlag == "Y" && (objContent == "" || objContent == undefined))
        throw new Error("Winsconsin Discloser Content should have some value to displayed in modal popup menu for credit app screen");
   
    else
        return true;
};


WmgJson.prototype.checkApplicantInfomations = function (obj) {
    //console.log(obj)
    if (obj.length == 0)
        throw new Error("Applicant Information atleast have one array (json.applicationInfo.applicantInfoList.applicantInfo)");

    $.each(obj, function(i, j) {
        if (j.applicantName == "") {
            throw new Error("Applicant name should have some value");
        } else if (j.applicantType == undefined || j.applicantType == "") {
            throw new Error("Applicant type is mandatory required to show background questions list in compliance screen");
        } else if (j.applicantType != "P" && j.applicantType != "S") {
            throw new Error("Applicant type should be P-Primary or S-Secondary");
        } else if (j.backgroundInfoQuesList.length == 0) {
            throw new Error("Background Info questions not set");
        }
    });
        return true;
};


WmgJson.prototype.checkProducts = function (obj) {
    //console.log(obj)
    if (obj.length == 0)
        throw new Error("Atleast one product required to complete WMG application (json.applicationInfo.applicantInfoList.WealthData.productList)");

    $.each(obj, function(i, j) {
        if (j.productID == undefined || j.productID == "") {
            throw new Error("index " + i + "product id is undefined or empty. It's highly required to for navigation and mapping. Application can't processed without product key");
        } else if (j.productName == undefined || j.productName == "") {
            throw new Error("index " + i + "product name is  undefined or empty. UI need to show product name in the tab");
        } else if (j.collateralInfo == undefined) {
            throw new Error("index " + i + "collateralInfo is not defined in product list. It's required for cd saving and marketable collateral");
        } else if (j.collateralInfo.financialCollteralList == undefined) {
            throw new Error("index " + i + "financialCollteralList is not defined in product list. It's required for cd saving and marketable collateral");
        } else if (j.collateralInfo.financialCollteralList.cdSavingsCollateralList.CDSavingscollateral == undefined) {
            throw new Error("index " + i + "cdSavingsCollateralList.CDSavingscollateral is not defined in product list. It's required for cd saving and marketable collateral");
        } else if (j.collateralInfo.financialCollteralList.marketableCollateralList.marketablecollateral == undefined) {
            throw new Error("index " + i + "marketableCollateralList.marketablecollateral is not defined in product list. It's required for cd saving and marketable collateral");
        } else if (j.isChkApplicable == undefined || j.isChkApplicable == "") {
            throw new Error("index " + i + "isChkApplicable flag is required to show ACH form ");
        } else if (j.isNonRECollateralApplicable == undefined || j.isNonRECollateralApplicable == "") {
            throw new Error("index " + i + "isNonRECollateralApplicable  flag is required to show Non Re Collateral form");
        } else if (j.isODPapplicable == undefined || j.isODPapplicable == "") {
            throw new Error("index " + i + " isODPapplicable  flag is required to show ODP Form");
        } else if (j.productFeatures.ACHData == undefined) {
            throw new Error("index " + i + "productFeatures.ACHData is undefined");
        } else if (j.productFeatures.ODPData == undefined) {
            throw new Error("index " + i + "productFeatures.ODPData is undefined");
        } else if (j.productFeatures.Checks == undefined) {
            throw new Error("index " + i + "productFeatures.Checks is undefined");
        }
    });
    return true;
};

