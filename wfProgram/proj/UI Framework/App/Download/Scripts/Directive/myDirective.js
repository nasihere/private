
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