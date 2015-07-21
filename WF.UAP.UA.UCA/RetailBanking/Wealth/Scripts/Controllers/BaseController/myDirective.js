/// <reference path="../../../Views/RECollateral/ExistingMortgageCollateralProperty/MortgageForm.html" />
/// <reference path="../../../Views/RECollateral/ExistingMortgageCollateralProperty/MortgageForm.html" />
/*
--------Signature-------
Created By: Nasir Sayed
Date: 27th Sep 2014
*/


wmg.directive('svpappinit', function () {

    return function svpappinit(scope, element, attr) {
        //setTimeout(function () {  }, 500);

    };
});

/*
wmg.directive("wmgOtherEnabled", function () {
    return function (scope, element, attrs) {
        scope.$watch(attrs.otherEnabled, function (val) {
            if (val == "OTHR") {
                element[0].value = "";
                element.removeAttr("disabled").removeClass("dis");
            }
            else if (val == "OD" || val == "OS")
                element.removeAttr("disabled").removeClass("dis");
            else if (val == "C")
                element.removeAttr("disabled").removeClass("dis");
            else if (val == "" || val != "O") {
                element[0].value = "";
                element.attr("disabled", "disabled").addClass("dis");
            }
            else
                element.removeAttr("disabled").removeClass("dis");
        });
    }
});
*/
wmg.directive('wmgProductMemberDirective', function ($compile) {
    return {
        template: '<div class="containerZ"> <div class="leftzc greyrightborder col-lg-12"> <select ng-model="input.Role" class="select" >' +
                '<option selected="selected" value= "">Please Select</option>' +
                '<option value="AUTHORIZER">Authorizer</option>' +
                '<option value="PARTNER/MEMBER">Partner/Member</option>' +
                '<option value="OFFICER1">Officer 1</option>' +
                '<option value="OFFICER2">Officer 2</option>' +
                

            '</select>' +
            '<span class="select">Select</span></div>' +


            ' <div class="centerzc greyrightborder col-lg-12">' +
            '<select  ng-model="input.TitlePosition" class="select" >' +
               ' <option selected="selected" value="">Please Select</option>' +
               ' <option value="Chief Executive Officer">Chief Executive Officer</option>' +
               ' <option value="Chief Operation Officer">Chief Operation Officer</option>' +
              '  <option value="Chief Financial Officer">Chief Financial Officer</option>' +
             '   <option value="Chairman of the Board">Chairman of the Board</option>' +
    '  <option value="SECRETARY">Secretary</option>' +
             '   <option value="TREASURER">Treasurer</option>' +
    '   <option value="President">President</option>' +
            '    <option value="Vice President">Vice President</option>' +
            '    <option value="Assistance Vice President">Assistance Vice President</option>' +
            '    <option value="Director">Director</option>' +
             '   <option value="Member">Member</option>' +
             '   <option value="Manager">Manager</option>' +
             '   <option value="General Partner">General Partner</option>' +
             '   <option value="Limited Partner">Limited Partner</option>' +

         '   </select>' +
         '   <span class="select">Select</span>' +
       ' </div>' +


        '<div class="rightzc">' +
         '   <input ng-class="{error:input.error, error: (!input.Name && (input.Role && input.TitlePosition)) }"  ng-model="input.Name" type="text" maxlength="30" >' +
    '        <a  class="plustoggle expand-toggle {{input.anchorclass}}" ng-click="BusinessPartnershipAddNewRow(input,$index)" href="javascript:void(0)"></a>' +


      '  </div>' +
            '' +
            '</div>',
        replace: true,
        link: function (scope, element) {
            var el = angular.element('<span/>');
            switch (scope.input.inputType) {
                case 'checkbox':
                    //el.append('<div  class="centerzc"><input type="checkbox" ng-model="input.checked"/><button ng-if="input.checked" ng-click="input.checked=false; doSomething()">X</button></div>');
                    break;
                case 'text':
                    //el.append('<div  class="rightzc"><input type="text" ng-model="input.value"/><button ng-if="input.value" ng-click="input.value=\'\'; doSomething()">X</button></div>');
                    break;
            }
            $compile(el)(scope);
            element.append(el);
            svpApp.util.buildSelects();
        }
    }
    //http://plnkr.co/edit/pzFjgtf9Q4kTI7XGAUCF?p=preview
});



wmg.directive('wmgPersonalFinanceNoteDirective', function ($compile) {
    return {
        template: '<table class="table table-striped "><tr>' +
                    '<td style="width: 30%">' +
                   '     <input valid="^[a-zA-Z0-9\s]*$" vmsg="only alphanumeric allowed" ng-class="{error:input.errorNo == 1 }" type="text" ng-model="input.nameOfDebtor" class="width100" maxlength="25"></td>' +
                   ' <td style="width: 16%">' +
                   '     <input valid="^[a-zA-Z0-9\s]*$" vmsg="only alphanumeric allowed" ng-class="{error:input.errorNo == 2 }"  type="text" ng-model="input.collateralType" class="width100" maxlength="25"></td>' +
                   ' <td style="width: 16%">' +
                   '     <input wmg-datepicker ng-class="{error:input.errorNo == 3 }"   type="text"  ng-model="input.maturityDate" class="width100 " placeholder="MM/DD/YYYY" ></td>' +
                   ' <td style="width: 16%">' +
                   '     <input valid="^[0-9.]+$" vmsg="only numeric allowed" ng-class="{error:input.errorNo == 4 }"  type="text"  ng-model="input.annual" class="width100 " placeholder="$0.00" maxlength="25"></td>' +
                   ' <td style="width: 16%">' +
                   '     <input valid="^[0-9.]+$" vmsg="only numeric allowed" ng-class="{error:input.errorNo == 5 }"  type="text"  ng-model="input.unPaidBalance" class="width100 " placeholder="$0.00" maxlength="25"></td>' +
                   ' <td style="width: 5%">' +
                   '     <a  class="plustoggle expand-toggle {{input.anchorclass}}"  ng-click="Note2ReceivableAddNewRow(input,$index)" href="javascript:void(0)"></a>' +

                   '     </dd>' +

                '</tr></table>' +
            '',
        replace: true,
        link: function (scope, element) {
            var el = angular.element('<span/>');
            switch (scope.input.inputType) {
                case 'checkbox':
                    //el.append('<div  class="centerzc"><input type="checkbox" ng-model="input.checked"/><button ng-if="input.checked" ng-click="input.checked=false; doSomething()">X</button></div>');
                    break;
                case 'text':
                    //el.append('<div  class="rightzc"><input type="text" ng-model="input.value"/><button ng-if="input.value" ng-click="input.value=\'\'; doSomething()">X</button></div>');
                    break;
            }
            $compile(el)(scope);

        }
    }
    //http://plnkr.co/edit/pzFjgtf9Q4kTI7XGAUCF?p=preview
});

wmg.directive('wmgBreadcrumbDirective', function () {
    return {
        template: function (scope, ele, attr) {
            return '<li ng-class="{hide:(!input.visible) , active: (CurrentViewName.indexOf(input.viewName) != -1)}"><a __ui-sref__="{{input.viewName},}"  class="greytext ng-class:{\'linethrough\' : input.param.removeProduct};" style="cursor:default;border-left:none!important;border-right:none!important;">{{input.breadCrumbTitle}}</a>  </li>';
        },
        replace: true
    }
});


wmg.directive('wmgDatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datepicker({
                    dateFormat: 'dd/mm/yy',
                    onSelect: function (date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                });
            });
        }
    }
});

wmg.directive("wmgRecollateralMortgage", function() {
    return {
        restrict: "AE",
        templateUrl: "./Views/RECollateral/ExistingMortgageCollateralProperty/MortgageForm.html",
        scope: {
            title: '@',
            mortgage: '='
        }
    }
})
/*
wmg.directive("wmgHowHeld", function ($compile) {
    return {
        restrict: "AC",
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.on('change', function (e) {
                if (ngModelCtrl.$modelValue != undefined) {
                    if (parseInt(ngModelCtrl.$modelValue) > 0) {
                        var ngModelObj = attrs["ngModel"];

                        ngModelObj = ngModelObj.replace("balance", "howHeld");
                        var obj = angular.element($("[ng-model='" + ngModelObj + "']"));
                        $(obj).attr("valid", "select");
                        $(obj).attr("vminval", "0");

                        $compile(obj)(scope);
                        scope.$digest();
                        scope.$apply();
                        setTimeout(
                            function ()
                            {
                                $(obj).trigger('change');
                                svpApp.util.buildSelects();

                            }, 100);
                        

                    } else {
                        var ngModelObj = attrs["ngModel"];

                        ngModelObj = ngModelObj.replace("balance", "howHeld");
                        var obj = angular.element($("[ng-model='" + ngModelObj + "']"));
                        
                        $(obj).removeAttr("valid", "select");
                        $(obj).removeAttr("vminval", "0");
                        $(obj).removeClass("ng-invalid");
                        
                        $compile(obj)(scope);
                        scope.$digest();
                        scope.$apply();
                        setTimeout(function ()
                        {
                            svpApp.util.buildSelects();
                        }, 100);
                       

                    }
                }
            });
           
        }
    };
});
*/
wmg.directive("wmgautoheight", function () {
    return function (scope) {
        if (scope.$last || scope.$last == undefined) {
            setTimeout(function () { setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost"); }, 1000);
        }
    };
});

