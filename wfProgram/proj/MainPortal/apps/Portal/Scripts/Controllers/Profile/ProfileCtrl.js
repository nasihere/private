app.controller('ProfileCtrl', function ($scope) {
    $scope.getRandomSpan = function () {
        return Math.floor((Math.random() * 3) + 1);
    }

    UpdateDom();

});