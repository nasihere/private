wmg.factory("wmgDataService", ["$http", function ($http) {

    var service = {};
    var isJson = function (str) {
        try {
            JSON.parse(str);
        } catch (e) {
            return false;
        }
        return true;
    };

    service.getData = function (token) {
        $http({
            method: "POST",
            url: "http://eaicncadev100/UCAWebApi/GetData",
            data: "opurayyrgtqfcrb1puqvt0ze",
            //data: { wmgData: Root },
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
                //'eOf0DqvocRH111r7x9ScyAz9ts8VJ6J61ccJMdPbhnX-P9gGDoTH3RlUifsf6Be8jCNKFxkO3jKFr0VK7WpX4L1zJRgRZcYX2LM9I8uT1i06GiVZ-W2w3iVMW88EH16FwuAlDl4-LwlE-jYFiZkksYsvWz4camuZICU3dxAP7Y4VzMiZWf9wWXFUZpz93svz9bw2qmMe6eC8_gMUVx8mTFdppsKYKouTv-FkU04ICx2KwfEa2bpgSyfYyYQnBDqVEvx5S6YCikrsWUssneIXc_ZcKRIKN6duPIXD1bqLbPZdtEbrve9T9yE_ZP4JWXaT'
            }
        }).then(function (success) {
            alert("Service GETDATA is success");
            //console.log("Config:");
            //console.log(success.config);
            //console.log("Data:");
            //console.log(success.data);
            //console.log("Headers:");
            //console.log(success.config.headers);
            //console.log("Status:");
            //console.log(success.status);
        }, function (failure) {
            alert("Service GETDATA failed");
            //console.log(failure);
        });
    };

    service.setData = function () {
        var error;
        var jsonData;

        error = wmgErrorData;
        if (isWmgInitialJsonData == "Y") {
            jsonData = wmgInitialJsonData;
        } else {
            jsonData = wmgJsonData;
        }

        //jsonData = wmgJsonData;

    /*    if (error != undefined && error != "") {
            if (isJson(error) == true) {
                this.errorInfo = angular.fromJson(error);
            } else {
                service.errorInfo = [{ Code: "Not Json Formattable Error Message", Message: error }];
            }
            this.applicationError = true;
            return false;
        }
       
        if ((error == "" && jsonData == "") || jsonData == undefined ) {
            service.errorInfo = [{ Code: "Not Json Formattable Error Message", Message: error }];
            this.applicationError = true;
            return false;
        }
        */
        if (jsonData != undefined && jsonData != "") {
            //AppRoot = angular.fromJson(jsonData);
            AppRoot = jsonData;
            Root = jsonData.ViewModel.ucaViewModel;
            if (Root == null) {
                var ErrorMsg = jsonData.ViewModel.ErrorMessages[0].Message;
                var ErrorCode = jsonData.ViewModel.ErrorMessages[0].Code;
                service.errorInfo = [{ Code: ErrorCode, Message: ErrorMsg }];
                this.applicationError = true;
                return false;
            }
            //Added this timebeing... till we get this model from JSON
            if (Root.applicationInfo.WealthData.assetInfo.assets == null || Root.applicationInfo.WealthData.assetInfo.assets == undefined || Root.applicationInfo.WealthData.assetInfo.assets.length == 0) {
                Root.applicationInfo.WealthData.assetInfo.assets = assets;
            }
            if (Root.applicationInfo.WealthData.assetInfo.liabilities == null || Root.applicationInfo.WealthData.assetInfo.liabilities == undefined || Root.applicationInfo.WealthData.assetInfo.liabilities.length == 0) {
                Root.applicationInfo.WealthData.assetInfo.liabilities = liabilities;
            }
            if (Root.applicationInfo.WealthData.assetInfo.notesReceivableList == null || Root.applicationInfo.WealthData.assetInfo.notesReceivableList == undefined || Root.applicationInfo.WealthData.assetInfo.notesReceivableList.length == 0) {
                Root.applicationInfo.WealthData.assetInfo.notesReceivableList = notesReceivableList;

            }
            return true;
        }
    };

    var buildStaticDataList = function (data) {
        if (data == undefined) {
            return undefined;
        }
        console.log(data.staticData);
        var updatedStaticDataList = {};

        for (var key in data.staticData) {
            if (data.staticData.hasOwnProperty(key)) {
                if (angular.isObject(data.staticData[key]) == true) {
                    var innerList = [];
                    if (data.staticData[key].element == undefined) {
                        continue;
                    }
                    var elements = data.staticData[key].element;
                    for (var j = 0; j < elements.length; j++) {
                        innerList.push(elements[j]);
                    }
                    //data.staticData[key].splice(index, 1);

                    data.staticData[key] = [{}];
                    for (var i = 0; i < innerList.length; i++) {
                        data.staticData[key].push(innerList[i]);
                    }
                }
            }
        }
        updatedStaticDataList = data.staticData;
        return updatedStaticDataList;
    };

    service.readStaticDataFile = function () {
        var response = {};
        try {
            var promise = $http.get('/UCA/RetailBanking/Wealth/Scripts/StaticData/StaticData_tmp.json');

            promise.then(function (data) {
                response = buildStaticDataList(data.data);
            });

            promise.error(function (error) {
                alert("error in loading static data:" + error);
            });

            return response;
        } catch (e) {
            console.log("Error in Data Service Function ReadStaticData" + e.message);
        }

    };

    service.applicationError = false;
    service.errorInfo = null;

    return service;

}]);