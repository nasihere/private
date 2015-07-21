'use strict';

var serialize = function (obj) {
    var str = [];
    for (var p in obj)
        if (obj.hasOwnProperty(p)) {
            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
        }
    return str.join("&");
}

app.factory('UserSvc', function ($http,$window, ufepConfig) {

    var currentUser = { userName: '', alias: '', role: '', isAuthenticated: false };

    return {
        login: function (user, success, error) {
            $http.post("http://localhost:41350/Token", serialize(user), { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (data, status, headers, config) {
                    $window.sessionStorage.setItem('token', data.access_token);
                    success(data);
                }).error(function(err) {
                    alert(err);
                });
        },

        logout: function (success, error) {
            $http.post('/logout').success(function () {
                isAuthenticated = false;
                success();
            }).error(error);
        },
        isAuthenticated: false,
        user: currentUser
    };
});