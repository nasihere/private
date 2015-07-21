wmg.service('APIService', ['$http', function ($http) {

    this.SaveData = function (calback) {
        var rootJsonString = JSON.stringify(AppRoot);
        $.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken }, function (data) {
            calback({ data: data });
        });
    };

    this.SubmitData = function (calback) {
       
        var rootJsonString = JSON.stringify(AppRoot);
        $.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken }, function (data) {
            calback({ data: data });
        });
    };

    
}]);