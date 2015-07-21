var SubmitDataResponse = null;
var Root = "";
var AppRoot = "";
var AppError = "";
var angB;
var angA;
var applicationMethod;
var coApplicationMethod;
var nW = {
    l: "",
    e: "",
    w: "",
    s: ""
}

var fotterButtons = {
    "continue": "Continue"
};
var notesReceivableList = {
    "notesReceivable": [
     {
         "nameOfDebtor": "",
         "collateralType": "",
         "maturityDate": "",
         "annual": "",
         "unPaidBalance": "",
         "anchorclass": "",
         "error": "",
         "errorNo": ""
     }
    ],
    "total": ""
};
var assets = [
    { "name": "Cash in Wells Fargo", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Cash in other institutions", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Other Securities Owned**", "howHeld": "", "balance": "", "type": "label" },
    { "name": "IRA/Keogh/Pension", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Notes Receivable (Sch 1)", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Real Estate Value (Sch 2)", "howHeld": "", "balance": "", "type": "label" },
    { "name": "", "howHeld": "", "balance": "", "type": "text" },
    { "name": "", "howHeld": "", "balance": "", "type": "text" },
    { "name": "Automobiles", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Personal Property", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Other Assets", "howHeld": "", "balance": "", "type": "label" }
];

var liabilities = [
    { "name": "Income Taxes", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Other Taxes", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Revolving Credit", "howHeld": "", "balance": "", "type": "label" },
    { "name": "", "howHeld": "", "balance": "", "type": "text" },
    { "name": "", "howHeld": "", "balance": "", "type": "text" },
    { "name": "Mortgages Payable (Sch. 2)", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Other Liability", "howHeld": "", "balance": "", "type": "label" },
    { "name": "Other Liability", "howHeld": "", "balance": "", "type": "label" }
];

var isLocalDebug = function() {
    
    if (WebApiURL.indexOf("localhost") != -1 || WebApiURL.indexOf("sbtester") != -1) {
        if (local_test_pid != undefined) {
            wmgJsonData.ViewModel.ucaViewModel.applicationInfo.WealthData.productList[0].productID = local_test_pid;
            wmgJsonData.ViewModel.ucaViewModel.applicationInfo.WealthData.productList[0].productName = "modified";
         
        }
        return true;
    } else {
        return false;
    }
}