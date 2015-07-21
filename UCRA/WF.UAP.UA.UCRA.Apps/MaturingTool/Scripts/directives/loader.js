MaturingTools.directive('loader', function() {
    return {
        restrict: 'EA',
        scope: { showloader: "=loader" },
        template: "<div class='overlay' id='loader' ng-hide='!showloader'><div class='loader'><b>Loading....</b><span class='spinner'></span></div></div>"
    };
});