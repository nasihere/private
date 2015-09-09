MaturingTools.constant('constants', (function () {
    return {
        reNumber: new RegExp(/^\-?\d+$/),
        //reMoneyFormat: new RegExp(/^\d{0,8}[0-9](|.\d{0,1}[0-9]|,\d*[0-9])?$/),
        reBalanceFormat: new RegExp(/^(\$)?((\d{1,9})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/),
        reAmountFormat: new RegExp(/^(\$)?((\d{1,14})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/),
        rePaymentAmountFormat: new RegExp(/^(\$)?((\d{1,10})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/),
        reRateFormat: new RegExp(/^((\d{1,2}))(\.\d{1,3})?$/)
    }
})());