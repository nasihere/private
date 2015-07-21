wmg.service('APIService', ['$http', function ($http) {

    this.SaveData = function (calback) {
        var rootJsonString = JSON.stringify(AppRoot);
        $.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken }, function (data) {
            calback({ data: data });
        });
    };

    this.SubmitData = function (calback) {
        /* var rootJsonString = JSON.stringify(AppRoot);
         var param = { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken };
         jQuery.support.cors = true;
 
         $.ajax({
             url: WebApiURL + 'SetData',
             type: 'POST',
             data: param,
             contentType: "application/json;charset=utf-8",
             success: function (data) {
                 calback({ data: data });
 
             },
             error: function (x, y, z) {
                 alert(x + '\n' + y + '\n' + z);
             }
         });
         
         /*24-02-2014 - Removing aspx page to submit app.. tyring to do it by jquery*/
        var rootJsonString = JSON.stringify(AppRoot);
        var param = { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken };

        $.ajax({
            url: './WealthSubmit.aspx',
            type: 'POST',
            data: param,
            async: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            success: function (data) {
                calback({ data: data });
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z + " url: " + "submit App");

            }
        });

        /*$.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken }, function (data) {
            calback({ data: data });
        });*/



    };

    //this.SaveData = function () {
    //    var rootJsonString = JSON.stringify(AppRoot);
    //    $.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken });
    //};

    //this.SubmitData = function () {
    //    var rootJsonString = JSON.stringify(AppRoot);
    //    $.post("./WealthSubmit.aspx", { SessionID: sessionIdData, rootJSON: rootJsonString, access_token: accessToken });
    //};

}]);