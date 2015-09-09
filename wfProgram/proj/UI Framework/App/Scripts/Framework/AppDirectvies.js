app.directive('appInput', function() {
    var directive = {};

    directive.restrict = 'E';

    directive.template = [
        '<single-calandar ng-if="option.type == \'calendar\'" prop="option"></single-calandar>' +
        '<single-select ng-if="option.type == \'dropdown\'" mapmodel="option.model" prop="option"></single-select>' +
        '<single-checkbox ng-if="option.type == \'chk\'" prop ="option"></single-checkbox>' +
        '<single-radio ng-if="option.type == \'rdo\'" prop ="option"></single-radio>' +
        '<single-button ng-if="option.type == \'button\'"  prop="option" ></single-button>' +
        '<single-text ng-if="option.type == \'text\'" mapmodel="option.model" prop="option"></single-text>'
        
    ].join('');

    directive.scope = {
        option: '='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});
app.directive('singleSelect', function () {
    var directive = {};

    directive.restrict = 'E';
   
    directive.template = [
        '<select ng-model="mapmodel" valid="{{prop.valid}}" class="form-control m-b {{prop.style}}">',
        '<option value="{{value.value}}" ng-repeat="value in prop.values">{{value.label}}</option>',
        '</select>'
    ].join('');

    directive.scope = {
        mapmodel: '=',
        prop: '='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});
app.directive('singleText', function () {
    var directive = {};

    directive.restrict = 'E';
    
    directive.template = [
        '<input type="text" ng-model="mapmodel" valid={{prop.valid}}  class="form-control {{prop.style}}" value="{{prop.text}}" ></input>'
    ].join('');

    directive.scope = {
        mapmodel: '=',
        prop:'='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});
app.directive('singleCalandar', function () {
    var directive = {};

    directive.restrict = 'E';

    directive.template = [
        '<input type="text"  valid="{{prop.valid}}"  class="form-control datecomp {{prop.style}}" value="{{prop.text}}" ></input>'
    ].join('');

    directive.scope = {
        mapmodel: '=',
        prop: '='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});

app.directive('singleRadio', function () {
    var directive = {};

    directive.restrict = 'E';

    directive.template = [
        ' <div class="radio i-checks"><label> <input type="radio"  checked="{{prop.optcheck}}" value="{{prop.optvalue}}" name="a"> <i></i> {{prop.complabel}} </label></div>'
    ].join('');

    directive.scope = {
       prop: '='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});
app.directive('singleCheckbox', function () {
    var directive = {};

    directive.restrict = 'E';

    directive.template = [
        '<div class="checkbox i-checks"><label> <input type="checkbox" value=""  checked="{{prop.chkvalue}}"> <i></i> {{prop.complabel}} </label></div>'
    ].join('');

    directive.scope = {
        prop:'='
    };

    directive.link = function (scope, element, attrs) {
    };

    return directive;
});

app.directive('singleButton', function ($compile) {
    var obj = '<a  class="btn btn-sm btn-primary {{prop.cls}}"><strong>{{prop.complabel}}</strong></a>';
  var directiveObj =  {
        restrict: 'E',
        replace: true,
        scope: {
            prop: '=',
            value: '=',
            cls: '='
          
            
        },
        template: obj
    };
    return directiveObj;

});
app.directive('jform', function () {
    return {
         scope: {
            form: "=",
            space: "@",
            template: "@"

        },
         template: '<ng-include src="getTemplateUrl()"/>',
         controller: function ($scope) {
             //function used on the ng-include to resolve the template
             $scope.getTemplateUrl = function() {
                 //basic handling
                 if ($scope.template == "Form")
                     return "layout/Form.html";
                 else
                     return "layout/" + $scope.template + ".html";
                 /*else if ($scope.template == "UAPStyleForm")
                     return "Form.html";
                     */

             };
         }
    };
});
app.directive('jmultiform', function () {
    return {
        scope: {
            attr: "=",
            template: "@",
            jform: "="

        },
        template: '<ng-include src="getTemplateUrl()"/>',
        controller: function ($scope) {
            //function used on the ng-include to resolve the template
            $scope.getTemplateUrl = function () {
                //basic handling
              
               
               if ($scope.template == "Form")
                   return "layout/Form.html";
               else
                   return "layout/" + $scope.template + ".html";
                /*else if ($scope.template == "UAPStyleForm")
                     return "./Styles/UAPAppStyle/Form.html";
                     */

            };
        }
    };
});


/* will remove this directive and call it on StateProvider service */
app.directive('componentbind', function () {
    return {
        
       
        controller: function ($scope) {
             setTimeout(function() { init(); }, 500);
        }
    };
});


/* Exporting Html element into HTML file */
var CleanUp = function(html) {
    html = html.replace(/<!--[^>]*-->/g, "");
    html = html.replace(/<single-text ng-if="option.type == 'text'" mapmodel="option.model" prop="option" class="ng-scope ng-isolate-scope">/g, "");
    html = html.replace(/<app-input option="option" class="ng-isolate-scope">/g, "");
    html = html.replace(/<app-input class="ng-isolate-scope" option="option">/g, "");
    html = html.replace(/<single-select ng-if="option.type == 'dropdown'" mapmodel="option.model" prop="option" class="ng-scope ng-isolate-scope">/g, "");
    html = html.replace(/<single-checkbox ng-if="option.type == 'chk'" prop="option" class="ng-scope ng-isolate-scope">/g, "");
    html = html.replace(/<single-radio ng-if="option.type == 'rdo'" prop="option" class="ng-scope ng-isolate-scope">/g, "");
    html = html.replace(/<single-calandar ng-if="option.type == 'calendar'" prop="option" class="ng-scope ng-isolate-scope">/g, "");
    html = html.replace(/<ng-include src="getTemplateUrl\(\)" class="ng-scope">/g, "");
     html = html.replace(/ ng-if="option.label != null"/g, "");
    html = html.replace(/ ng-if="option.type == 'button'"/g, "");
    html = html.replace(/<single-text class=" ng-isolate-scope" ng-if="option.type == 'text'" mapmodel="option.model" prop="option">/g, "");
    html = html.replace(/<single-select class=" ng-isolate-scope" ng-if="option.type == 'dropdown'" mapmodel="option.model" prop="option">/g, "");
    

    html = html.replace(/ ng-if="option.label != null"/g, "");
    html = html.replace(/ ng-repeat="option in form.fields"/g, "");
    html = html.replace(/<\/single-text>/g, "");
    html = html.replace(/<\/app-input>/g, "");
    html = html.replace(/<\/single-checkbox>/g, "");
    html = html.replace(/<\/single-select>/g, "");
    
    html = html.replace(/<\/single-radio>/g, "");
    html = html.replace(/<\/single-calandar>/g, "");
    html = html.replace(/<\/ng-include>/g, "");

    
    html = html.replace(/<single-text class="  ng-isolate-scope"/g, "");
    html = html.replace(/ mapmodel="option.model"/g, "");
    html = html.replace(/ng-if="option.type == 'text'"/g, "");

    html = html.replace(/ng-repeat="row in form.fields"/g, "");
    html = html.replace(/ng-if="row.length"/g, "");
    html = html.replace(/class="ng-isolate-scope"/g, "");
    html = html.replace(/ng-repeat="option in row"/g, "");
    html = html.replace(/contenteditable = "true"/g, "");
    html = html.replace(/contenteditable="true"/g, "");
    html = html.replace(/ng-if="option.label != ''"/g, "");
    html = html.replace(/ng-if="item.length"/g, "");
    html = html.replace(/ng-scope/g, "");
    html = html.replace(/<app-input/g, "");
    
    html = html.replace(/option="option">/g, "");
    html = html.replace(/ng-repeat="item in row"/g, "");
    html = html.replace(/ng-repeat="option in item"/g, "");
    html = html.replace(/ng-model="mapmodel"/g, "");
    html = html.replace(/valid = ""/g, "");
    html = html.replace(/value = ""/g, "");
    html = html.replace(/ng-class="{'control-label':option.align}"/g, "");
    
    return html;
}