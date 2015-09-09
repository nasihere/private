var app = angular.module("app",['ui.router']);

app.config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');
 
    $stateProvider
        .state('Login', {
            url:'/',
            templateUrl: 'ang/Views/Login/Login.html'
        })
        .state('Dashboard', {
            url:'/Dashboard',
            templateUrl: 'ang/Views/Misc/Dashboard.html'
        })
        .state('CreateEmployee', {
            url:'/CreateEmployee',
            templateUrl: 'ang/Views/Misc/CreateEmployee.html'
        })
        .state('AccountEmployee', {
            url:'/AccountEmployeee',
            templateUrl: 'ang/Views/Misc/AccountEmployee.html'
        })
        .state('Reports', {
            url:'/Reports',
            templateUrl: 'ang/Views/Misc/Reports.html'
        })
        .state('CallCenter', {
            url:'/CallCenter',
            templateUrl: 'ang/Views/Misc/CallCenter.html'
        })
		
		
		
 
}]);