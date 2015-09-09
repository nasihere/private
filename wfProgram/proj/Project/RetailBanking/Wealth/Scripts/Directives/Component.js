wmg.directive("wmgRadio", function() {
   
   
    return {
        scope: {
            item: "="
        },
        template: '<div class="radio" ng-repeat="c in {{item}}">aaa</div>'
    };
    

});


wmg.directive("wmgShow", function() {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            scope.$watch(attrs.wmgShow, function(newval) {
                if (newval == undefined || newval == "") {
                    element.hide('fast');
                } else if (newval == false) {
                    element.hide('fast');
                } else {
                    element.show('fast');
                }
            });
        }
    };
});


