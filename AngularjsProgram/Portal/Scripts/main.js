var ConsoleConfig = {
    tokenId: "",
    userId: "",
    userRole:"",
    baseurl: "/ConsoleWebApi/",
    TOKEN: "Token",
    AccountInfo: "Account/GetAccountModules",
    REGISTER: "Account/Register",
    GETROLES: "Account/GetRoles",
    UserList: "Account/UserList",
    SaveAccount: "Account/SaveAccount",
    SaveAccountRoles: "Account/SaveAccountRoles",
}



app.config(function ($httpProvider) {
    $httpProvider.interceptors.push([
        '$injector',
        function ($injector) {
            return $injector.get('AuthInterceptor');
        }
    ]);
});
app.factory('AuthInterceptor', function ($rootScope, $q,
    AUTH_EVENTS) {
    return {
        responseError: function (response) {
            $rootScope.$broadcast({
                401: AUTH_EVENTS.notAuthenticated,
                403: AUTH_EVENTS.notAuthorized,
                419: AUTH_EVENTS.sessionTimeout,
                440: AUTH_EVENTS.sessionTimeout
            }[response.status], response);
            return $q.reject(response);
        }
    };
});


app.run(function ($rootScope, AUTH_EVENTS, AuthService, $state) {
    $rootScope.$on('$stateChangeStart', function (event, next) {
        var authorizedRoles = next.data.authorizedRoles;
        if (!AuthService.isAuthorized(authorizedRoles)) {
            event.preventDefault();
            if (AuthService.isAuthenticated()) {
                // user is not allowed
                $rootScope.$broadcast(AUTH_EVENTS.notAuthorized);
            } else {
                // user is not logged in
                $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);

            }
        }
    });
});