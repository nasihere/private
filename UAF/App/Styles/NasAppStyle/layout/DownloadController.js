

app.controller("DownloadController", ['$scope', function ($scope) {
    $scope.FileTable = {
        Scripts: [{
                name: "Controller",
                files: ["BaseController.js", "FormController.js"]
            }, {
                name: "StaticData",
                files: ["BaseXML.js","StaticData.js"]
            }, {
                name: "Services",
                files: ["APIService.js"]
            }, {
                name: "Directive",
                files: ["Validation.js", "myDirective.js", "Accordion.js"]
            }, ],
        Views: [{
            name: "MainPage",
            files: ["Footerbar.html", "TabBar.html"]
        }, {
            name: "TestNDebug",
            files: ["DebugingTool.zip"]
        }, {
            name: "SampleForm",
            files: ["Form.html"]
        }, ]
    };
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
        var blob = new Blob([document.getElementById('ExportPreviewHtml').innerHTML], {
            type: "text/html"
        });
        saveAs(blob, filename+ ".html");
    };
}]);