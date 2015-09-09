MaturingTools.directive('numberFormat', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$formatters.unshift(function (modelValue) {
                if (modelValue != null && modelValue != "")
                    return parseFloat(modelValue).toFixed(2);
                else {
                    return modelValue;
                }
            });
        }
    }
});


MaturingTools.directive('amountFormat', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,14})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    modelValue = parseFloat(modelValue).toFixed(2);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });

            ngModelCtrl.$parsers.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,14})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });
        }
    }
});

MaturingTools.directive('paymentFormat', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,10})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    modelValue = parseFloat(modelValue).toFixed(2);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });

            ngModelCtrl.$parsers.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,10})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });
        }
    }
});

MaturingTools.directive('balanceFormat', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,9})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    modelValue = parseFloat(modelValue).toFixed(2);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });

            ngModelCtrl.$parsers.push(function (modelValue) {
                if (modelValue != null && modelValue != "") {
                    var regex = new RegExp(/^(\$)?((\d{1,9})|(\d{1,3})(\,\d{3})*)(\.\d\d)?$/);
                    ngModelCtrl.$setValidity('pattern', regex.test(modelValue));
                    return modelValue;
                } else {
                    return modelValue;
                }
            });
        }
    }
});