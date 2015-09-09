

angular.module('UAP.API', [])

.constant('API_CONSTANT', {
    BASE_URL: 'App/WMG/Debug/',
    METHOD: "POST",
    ASYNC: true

})

.service('api', function ($http, $rootScope, $state, API_CONSTANT) {
    return {
        get: function (ActionName, paramObj) {
            var url = UAPConfig.baseurl + ActionName;
            return $http({
                method: 'GET',
                url: url,
                headers: {
                    Authorization: 'Bearer ' + UAPConfig.tokenId,
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: paramObj
            }).success(function (res) {


                return res;
            }).error(function (er, status, code) {
                if (status == 500) {
                    alert("500: Internal Server error");
                }
                SmallBox("UAP - " + er.error, "<b>" + er.error_description + "</b>", "error");

                return null;
            });
        },
        post: function (ActionName,paramObj) {
            var url = UAPConfig.baseurl + ActionName;
            var DTO = JSON.stringify(paramObj);
            console.log(DTO);
            return $http({
                method: 'POST',
                url: url,
                headers: {
                    Authorization: 'Bearer ' + UAPConfig.tokenId,
                    "Content-Type": "application/json; charset=utf-8"
                },
                data: DTO
            }).success(function (res) {
              

                return res;
            }).error(function (er, status, code) {
                if (status == 500) {
                    alert("500: Internal Server error");
                }
                SmallBox("UAP - " + er.error, "<b>" + er.error_description + "</b>", "error");

                return null;
            });
        }
    };
})
    