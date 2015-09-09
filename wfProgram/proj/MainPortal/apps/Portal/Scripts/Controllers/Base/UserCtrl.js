//this is used to parse the profile
function url_base64_decode(str) {
    var output = str.replace('-', '+').replace('_', '/');
    switch (output.length % 4) {
        case 0:
            break;
        case 2:
            output += '==';
            break;
        case 3:
            output += '=';
            break;
        default:
            throw 'Illegal base64url string!';
    }
    return window.atob(output);
}

//User Controller For Autenthication and Authorization
app.controller('UserCtrl', ['$scope', '$window', '$location', 'UserSvc', function ($scope, $window, $location, UserSvc) {
    $scope.user = { username: 'test1', password: '1', grant_type: 'password' };
    $scope.isAuthenticated = false;
    $scope.isInvestigatorSup = false;
    $scope.role = '';
    $scope.alias = '';
    $scope.templateUrl = '';

    $scope.message = '';


    $scope.submit = function () {
        UserSvc.login($scope.user,
        function (res) {
            $scope.isAuthenticated = true;

            $scope.role = res.roles;
            $scope.alias = res.alias;
            //TODO Teddy Create Constant
        
            $location.path("Dashboard");
      
        },
        function (err) {

            $scope.isAuthenticated = false;
            $scope.message = "Failed to login";
        });


    };

    $scope.logout = function () {
        $scope.isAuthenticated = false;
        delete $window.sessionStorage.token;
    };



}]);

function GotoHomePage(role) {
    var path = '';
    switch (role) {
        
        case 'Admin':
            path = '/UI001';
            break;
        case 'Collector':
            path = '/UI002';
           break;
        default:
            path = '/UI003';
    }
    return path;
} 