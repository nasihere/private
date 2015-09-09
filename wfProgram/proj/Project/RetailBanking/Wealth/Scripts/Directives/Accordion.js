/*
--------Signature-------
Created By: Nasir Sayed
Date: 18th Nov 2014
Reason: To avoid repeation of Accordion HTML in other pages. This directive will also help other developer to plug and play.
*/


// The accordion directive simply sets up the directive controller
// and adds an accordion CSS class to itself element.
wmg.directive('accordion', function () {
    return {
        transclude: true,
        scope: {
            heading: '@',
            open: "@",
            panelid: "@"

        },
        template: function (scope, ele, attr) {
            var _PanelStyle = " style='display:none' ";

            if (ele.open == "truea") {
                _PanelStyle = "";

            }
            return '<div class="tab-pane active" ><div class="tab-pane active"><div class=\"panel-group\" >' +
                  "<div class=\"panel panel-default\">\n" +
                  "  <div class=\"panel-heading\">\n" +
                  "    <h4 class=\"panel-title\">\n" +
                  "      <a href=\"javascript:void(0)\" ng-click=\"toggleOpen()\" ><span>{{heading}}</span></a>\n" +
                  "<i id=\"acd{{panelid}}\" class=\"pull-right pointer glyphiconwmg \" ng-click=\"toggleOpen()\" ng-class=\"(open == 'true' || open == true) ? 'glyphicon-chevron-downwmg' : 'glyphicon-chevron-rightwmg'\"></i>" +
                  "    </h4>\n" +
                  "  </div>\n" +
                  "  <div class=\"panel-collapse\" " + _PanelStyle + " id={{panelid}} >{{status}}\n" +
                  "	  <div class=\"panel-body\"   ng-transclude ></div>\n" +
                  "  </div>\n" +
                  "</div>\n" +
                  '</div></div></div>'
        },
        controller: function ($scope, AccordionGate) {
            $scope.api = AccordionGate;

            $scope.$watch('api.status', function (val) {
                 if (val == 1) {
                   
                    $("#" + $scope.panelid).show();
                    $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-rightwmg").addClass("glyphicon-chevron-downwmg");
                 
                } else if (val == 0) {
                    $("#" + $scope.panelid).hide();
                    $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-downwmg").addClass("glyphicon-chevron-rightwmg");
                   

                }
                setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");
            })

            $scope.toggleOpen = function () {

                $("#" + $scope.panelid).slideToggle('fast', function () {
                    var state = ($("#" + $scope.panelid).attr("style"));
                    setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");
                    
                    if (state != null) {
                        if (state.indexOf("block") != -1) {
                            $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-rightwmg").addClass("glyphicon-chevron-downwmg");
                        } else {
                            $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-downwmg").addClass("glyphicon-chevron-rightwmg");

                        }
                    } else {
                        $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-rightwmg").addClass("glyphicon-chevron-downwmg");
                    }
                    
                });



            }

            if ($scope.open == "true") {
                setTimeout(function () {

                    svpApp.init();
                    $("#" + $scope.panelid).slideDown('fast', function() {
                        setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");
                    });
                    $("#acd" + $scope.panelid).removeClass("glyphicon-chevron-rightwmg").addClass("glyphicon-chevron-downwmg");
                    
                    $scope.api.setStatus(null);
                }, 500)

            }
        }
    };
});


wmg.factory('AccordionGate', function () {
    return {
        status: null,
        
        setStatus: function (value) {
            this.status = value;
            AccordionGateStatus.status = value;
            
        },
        
    }
});


var AccordionGateStatus = {
    status: null        
}

wmg.controller('accordionController', function ($scope, AccordionGate) {
   // $scope.status = 0;
    $scope.expandAll = function () {
        $scope.status = 0;
        AccordionGate.setStatus(1);
        setTimeout(function () { setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost"); }, 1000);
    }
    $scope.collapseAll = function () {
        $scope.status = 1;
        AccordionGate.setStatus(0);
        setTimeout(function () { setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost"); }, 1000);
    }
    AccordionGate.setStatus(null);
});

wmg.directive("accordionSectionsLink", function () {
    return {
        scope: {
            heading: '@',
            status: '=status'
        },
        template: '<div ng-controller="accordionController">' +
            '<h2 class="underlined"><span>{{heading}}</span>' +
            '<i class="pull-right font11 pointer "  ng-class="{hide:(status == 0 || (status == \'0\'))}" id="expandall" ng-click="expandAll()">Expand All Sections</i>' +
            '<i class="pull-right font11 pointer " ng-class="{hide:(status == 1 || status == \'1\')}" ng-click="collapseAll()">Collapse All Sections</i>' +
            '</h2></div>'
        
    };
})