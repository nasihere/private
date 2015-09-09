angular.module('Console.BaseCtrl', [])

.controller('BaseCtrl', ['$scope', '$location', '$rootScope', 'USER_ROLES', 'AuthService', '$localStorage', 'AUTH_EVENTS', 'Session', 'URLWrapper','$state',
    function ($scope, $location, $rootScope, USER_ROLES, AuthService, $localStorage, AUTH_EVENTS, Session, URLWrapper, $state) {
        $rootScope.breadName = $location.path();
        webstate.referrerurl = $rootScope.breadName.substring(1);
        $scope.webstate = webstate;
        $scope.currentUser = null;
        $scope.userRoles = USER_ROLES;
        $scope.isAuthorized = AuthService.isAuthorized;

        $scope.setCurrentUser = function(user) {
            $scope.currentUser = user;
        };
        $scope.$storage = $localStorage.$default();
        $scope.URLWrap = URLWrapper;
        $scope.LogOff = function () {
            $scope.$storage.$reset();
            Session.destroy();
            $rootScope.$broadcast(AUTH_EVENTS.sessionTimeout);
            
        }
        $scope.StateURL = $state.get();
        $scope.MenuNavigate = function (submenu) {
            $scope.$storage.user.navigate = angular.copy(submenu);
            $state.go("Dashboard");
        };


     /*   $scope.TestScript = function() {
            setInterval(function() {
                var myArray = ["Debug-User-Registration", "BannerApp", "Debug-User-URL", "Debug-Route-View", "Debug-Publish-App", "MenuLink", "Dashboard", "Compass-Approver"];
                var rand = myArray[Math.floor(Math.random() * myArray.length)];
                $state.go(rand);
            }, 190000);
        }
        $scope.TestScript();
        */
    }])


.service('URLWrapper', function ($state, $sce) {
    this.iframeSrc = "";
    
    this.OpenIFrame = function (url,target) {
        
        if (target == "_self") {
            winTab.push(tab);
            winTab[winTab.length - 1] = window.open(url, '_blank');
            return;
        }
        else if (target == "_iframe") {
            this.iframeSrc = url;
            $state.go("MyWindow");
        }
    };
    this.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    }
    this.getUrl = function() {
        return this.trustSrc(this.iframeSrc)
    };
   
    this.CloseIFrame = function () {
        this.iframeSrc = "";
    };
})
