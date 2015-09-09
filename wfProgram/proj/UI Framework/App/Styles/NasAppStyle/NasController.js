
var JFormArray = {
    data: [
        { label: 'Activity Code', type: "text", model:"act_code", valid: 'valid=number#_#vmsg=Activity code important', hint_: 'Example: Numeric fields required' },
        { label: 'Activity Date', dtvalue: "03/09/2015", type: "calendar", model: "act_date" },
        { label: 'Activity Time', type: "text", model: "act_time" },
        { label: 'General Comments', type: "text", model: "gen_comment" },
        { label: 'CCH Misc', type: "text", model: "cch_misc" },
        { label: 'Route To', type: "text", model: "route_to" },
        { label: 'User ID', type: "text", model: "user_id" },
         { label: 'For Check Box', type: "chk", chkvalue: "checked", complabel: "Check box Component" },
        { label: 'For radio ', type: "rdo", optvalue: "checked", optcheck: "a1", complabel: "Radio Component" },
        { label: 'For radio ', type: "rdo", optvalue: "unchecked", optcheck: "a1", complabel: "Radio Component" },
        { label: 'Workstate', type: "dropdown", values: tempRepeat, model: "work_state" },
        {
            label: '',
            type: "button",
            complabel:"Continue",
            click: function (param) {
                
                alert("click even triggered " + param);
            },
            clickparam: {
                "p1": "nasir",
                "p2":"zakir"
            },
            blur: function(param) {
                alert("blur even triggered " + param);
            },
            blurparam: {
                "b1": "Mark",
                "b2": "Jolly"
            },
            cls: 'pull-right'
        }
    ]

};

//Preview Form
var jPreviewForm = {
    jFormPreview: { heading: "Preview", layout: "col-lg-6", template: "Form", fields: JFormArray.data, space: "col-lg-12", clscomp: 'col-lg-8', classlabel: 'col-lg-4' }

};

var JModel = {};

app.controller("JFormController", ['$scope', function ($scope) {
    $scope.jform = JFormArray;
    $scope.jPreviewForm = jPreviewForm;
    $scope.addfields = function () {
        JFormArray.data.push({ label: 'NEW lABEL', type: "", model: "" });
    };
    $scope.removefields = function (index) {
        JFormArray.data.splice(index, 1)
    };
    $scope.change = function(item, valid) {
        if (valid.type == "")
            item.valid = "";
        else
            item.valid = "valid=" + valid.type + "#_#" + "vmsg=" + valid.vmsg;
    };
    $scope.setList = function (item, valuesName) {
        if (valuesName == "")
            item.values = "";
        else
            item.values = valuesName;
    };
    $scope.jColSize = jColSize;
    $scope.jLayoutSize = jLayoutSize;
    $scope.exportData = function (filename) {

        var html = document.getElementById('ExportPreviewHtml').innerHTML;
        html = CleanUp(html);
        var blob = new Blob([html], {
            type: "text/html"
        });
        saveAs(blob, filename + ".html");

    };
}]);