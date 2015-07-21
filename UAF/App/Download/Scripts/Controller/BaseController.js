wmg.controller('baseController', ['$scope', 'stateService', 'Enums', 'goAheadService', 'appInitService', 'routingStates', 'APIService', '$http', 'wmgDataService', 'AccordionGate', '$timeout',
    function ($scope, stateService, Enums, goAheadService, appInitService, routingStates, APIService, $http, wmgDataService, AccordionGate, $timeout) {


        // - Nasir : Purpose is to see the debug message on Top.. Becasue many time IE does not support Console.log so we created own console.log for run time debugging purpose...
        $scope.NC = nW;
        if (WebApiURL.indexOf("localhost") != -1) {
            $scope.Local = true;
        } else {
            $scope.Local = false;
        }



        //Method to Move screen to next location
        $scope.moveNext = function () {
            
        };

        //Method to Move screen to previous location
        $scope.movePrevious = function () {
          
        };


        //Method to Save application
        $scope.saveApplication = function () {
           


        };

        $scope.verifyForm = function () {
            $scope.routingStates = stateService.getUpdatedRoutingState($scope.routingStates);
            AccordionGate.setStatus(1);
            $timeout(pageValidator, 500).then(function (result) {
                if (result == true) {
                    $scope.moveNext();
                }
            });
        };

    }]);







