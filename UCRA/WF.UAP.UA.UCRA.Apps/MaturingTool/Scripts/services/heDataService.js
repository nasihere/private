MaturingTools.factory("heDataService", ['$http', function ($http) {
    return {
        getheData: function (url, accountNum, userId, userType, success, error) {
            $http({
                method: 'GET',
                url: url,
                params: { accountNum: accountNum, userId: userId, userType: userType }
            }).success(function (data, status, header, config) {
                success(data);
            }).error(function (data) {
                error(data);
            });
        },

        getDataList: function (url, success, error) {
            $http({
                method: 'GET',
                url: url
            }).success(function (data, status, header, config) {
                success(data);
            }).error(function (data) {
                error(data);
            });
        },

        getData: function (url, data, success, error) {

            var config = {
                headers: {
                    "content-type": "application/json"
                }
            }

            $http.post(url, data, config).success(function (response) {
                success(response);
            }).error(function (error) {
                error(error);
            });
        },

        saveData: function (url, jsonData, calback, error) {
            var rootJsonString = JSON.stringify(angular.fromJson(jsonData));
            $.post(url, { rootJSON: rootJsonString }, function (data) {
                calback(data);
            });
        },
    };

}]);