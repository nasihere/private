//Sample Fields

var LastRow = { label: 'Activity Code', type: "text", model: "act_code", cls: 'col-lg-4' }

var JFormArray = {
    data: {
        row: [], row1: [], row2: [], row3: [], row4: [], row5: [], row6: [], row7: [], row8: [], row9: [], row10: [], row11: [], row12: [], row13: []

    }

};

JFormArray.data.row.push(angular.copy(LastRow));
var oldForm = JSON.parse(localStorage.getItem("recent"));

if (oldForm) {
    JFormArray = angular.copy(oldForm);
} else if (localStorage.getItem("clear") == null) {
    JFormArray = angular.copy(tempjson);
}
//Preview Form
var jPreviewForm = {
    jFormPreview: {
        heading: "Form Title",
        template: "Form",
        fields: JFormArray.data,
        space: "col-12",
        clscomp: 'col-8'
    }

};

app.controller("JFormController", ['$scope', function ($scope) {
    $scope.jform = JFormArray;
    $scope.tempjson = tempjson;

    $scope.classes = classes;
    $scope.jPreviewForm = jPreviewForm;
   
    $scope.removefields = function (index) {
        JFormArray.data.splice(index, 1)
    };
    $scope.rowCount = 0;
    $scope.addRow = function () {
        $scope.rowCount++;
        var index = $scope.rowCount;

        if (index == 1)
            JFormArray.data.row1.push(LastRow);
        else if (index == 2)
            JFormArray.data.row2.push(LastRow);
        else if (index == 3)
            JFormArray.data.row3.push(LastRow);
        else if (index == 4)
            JFormArray.data.row4.push(LastRow);
        else if (index == 5)
            JFormArray.data.row5.push(LastRow);
        else if (index == 6)
            JFormArray.data.row6.push(LastRow);
        else if (index == 7)
            JFormArray.data.row7.push(LastRow);
        else if (index == 8)
            JFormArray.data.row8.push(LastRow);
        else if (index == 9)
            JFormArray.data.row9.push(LastRow);
        else if (index == 10)
            JFormArray.data.row10.push(LastRow);
        else if (index == 11)
            JFormArray.data.row11.push(LastRow);
        else if (index == 12)
            JFormArray.data.row12.push(LastRow);
        else if (index == 13)
            JFormArray.data.row13.push(LastRow);

    }
    $scope.addfields = function (row) {
        LastRow = angular.copy(row[row.length - 1]);
        row.push(LastRow);
        
        setTimeout(function() {
            $("[name='lblmodel0']").last().focus().select();
            $scope.SaveForm();
        },200);
        

        //var lastobj = angular.copy(JFormArray.data[JFormArray.data.length - 1]);

        //JFormArray.data.push(lastobj);
    };
    $scope.removefields = function (row, index) {
        row.splice(index, 1);
    };
    $scope.change = function (item, valid) {
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
    $scope.keyPress = function (keyCode, row) {
        if (keyCode == 113) //F2 so create new field
        {
            $scope.addfields(row);
        }
        else if (keyCode == 121) { // F10 then new row
            $scope.addRow();
        }
        
    }
    $scope.SaveForm = function () {
        localStorage.setItem("recent", JSON.stringify(JFormArray));
    }
    $scope.SaveFormAsFile = function () {
        localStorage.setItem("recent", JSON.stringify(JFormArray));
        var json = JSON.stringify(JFormArray);
        var filename = "myJson";
        var blob = new Blob([json], {
            type: "text/json"
        });
        saveAs(blob, filename + ".json");
    }
    $scope.OpenMyJsonDialog = function () {
        document.getElementById("myJsonDialog").showModal();
    }
   
    $scope.openDialog = function (index) {
        document.getElementById("openEdit" + index).show();
    }
    $scope.hideDialog = function (index) {
        document.getElementById("openEdit" + index).close();
    }
    $scope.UpdateMyJson= function () {
        var oldForm = JSON.parse($("#MyJson").html());
        if (oldForm) {
            localStorage.setItem("recent", JSON.stringify(oldForm));
             location.reload();
        }
    }
    $scope.ClearForm = function () {
        localStorage.removeItem("recent");
        localStorage.setItem("clear","true");
        location.reload();
    }
    $scope.lblcounter = 0;

}]);