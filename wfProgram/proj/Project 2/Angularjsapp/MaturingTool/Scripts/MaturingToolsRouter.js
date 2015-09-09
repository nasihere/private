
var MaturingTools = angular.module('MaturingTools', ['ngRoute']);

MaturingTools.value("apiUrl", apiUrl);

MaturingTools.config(function ($routeProvider) {

    $routeProvider.when("/MaturingTool", {
        templateUrl: "./views/MaturingTool.html"
    });
    $routeProvider.when("/NewAccount", {
        templateUrl: "./views/MaturingTool.html"
    });
    $routeProvider.when("/AccountDetails", {
        templateUrl: "./views/MaturingTool.html",
    });
    $routeProvider.when("/StatusScreen", {
        templateUrl: "./views/StatusScreen.html"
    });
    $routeProvider.otherwise({
        templateUrl: "./views/StatusScreen.html"
    });
});





