/*

--------Signature-------
Created By: Nasir Sayed
Date: 06 Dec 2014
Reason: 
To void angular directive in html i made this class to handle validation in one place and update all other elements. 
Example" Alert box, Continue or submit button and red border to each element with description.
*/

//Valid directive has four attributes
/*
    1) Valid = "number", "text", checkbox
    2) vmin = to check minimul length
    3) vmax - to check maxlenght
    4) vid to update errormsg and set auto focus 
    5) vmsg = to show in alert box
    6) vminval = minimum data example: 1M value shoule be entered or seleect box minimum value should be select

*/

wmg.directive('valid',['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            element.on('change', function (e) {
                
                validation(element, attrs, scope,$parse);

                //scope.$digest();
                //scope.$apply();

            });


        }
    };
}]);
wmg.directive('reset', [function ($parse) {
    return {
        restrict: 'A',
        require:'ngModel',
        link: function (scope, element, attrs, ngModel) {

            element.on('change', function () {
                if ($(element).is(":visible") == false || $(element).is(":disabled") == true) {
                   
                    ngModel.$setViewValue("", "");
                    scope.$apply();
                }
            });
            
           

        }
    };
}]);

var validation = function (element, attrs, scope, $parse) {
    var currentValue = element.val();
    var type = attrs.valid;
    var minlength = attrs.vmin;
    var maxlength = attrs.vmax;
    var errormsg = attrs.vmsg;
    var minvalue = attrs.vminval;
    var vif = attrs.vif;
    var errorid = $(element).attr("vid");//attrs.vid;
    var errorFlag = true;
    var errorInfo = true;
    if (errorid == undefined || errorid == "") {
        errorid = Math.random();
        $(element).attr("vid", errorid);


    }
    
    if (minvalue == undefined) {
        minvalue = 0;
    }
    /*     console.log(currentValue);
         console.log(minlength);
         console.log(maxlength);
         console.log(minvalue)
      */
    if (errorFlag == true && vif != undefined) {
        var template = $parse(vif);
        var obj;
        obj = template(scope); // Hello Joe
    
        if(obj == true){
            errorFlag = true;
            
        } else {
            removeError(errorid);
            element.removeClass("ng-invalid");
            removeErrorInfo(element);
            return;
        }
    }
    if (errorFlag == true && maxlength != undefined && currentValue.length > maxlength) {
        if (errormsg == undefined) errormsg = "";
        //errormsg += " value entered exceeds the maximum length";
        errorInfo = false;
        errorFlag = false;
    }
    if (errorFlag == true && minlength != undefined && currentValue.length < minlength) {
        if (errormsg == undefined) errormsg = "";
        //errormsg += " minimum " + minlength + " characters ";
        errorInfo = false;
        errorFlag = false;
    }
    if (errorFlag == true && type == "text" && !reText.test(currentValue)) {
        //  failure.error[errorid] = errormsg;
        errorFlag = false;
        errorInfo = false;
    }
    if (errorFlag == true && type == "number" && !INTEGER_REGEXP.test(currentValue)) {
        //  failure.error[errorid] = errormsg;
        errorFlag = false;
        errorInfo = false;
    }
    if (errorFlag == true && type == "email" && !reEmail.test(currentValue)) {
        //  failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "zip" && !rePostalZip.test(currentValue)) {
        //    failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "radio" && ($(element).is(':checked') == false)) {
        //    failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "check" && ($(element).is(':checked') == false)) {
        //    failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "date" && !reDate.test(currentValue)) {
        //   failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "fill" && currentValue.trim() == "") {
        //  failure.error[errorid] = errormsg;
        errorFlag = false;
        errorInfo = false;
    }
    if (errorFlag == true && type == "if") {
        errorFlag = false;
        errorInfo = false;
    }
    if (errorFlag == true && type == "daterange") {
        var daterecd = Date.parse(currentValue);
        var now = new Date();
        var yesterdayMs = now.getTime() - 1000 * 60 * 60 * 24 * minvalue; // Offset by one day;
        now.setTime(yesterdayMs);
        var datemmin = Date.parse(now);
        
        if (daterecd > datemmin) {
            errorInfo = false;
            errorFlag = false;
        } else {
            removeError(errorid);
            element.removeClass("ng-invalid");
            removeErrorInfo(element);
            return;
        }
        
    }
    if (errorFlag == true && type == "name" && !reName.test(currentValue)) {
        //   failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "addr" && !reAddress.test(currentValue)) {
        //   failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "phone" && !reNumber.test(currentValue)) {
        //   failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && type == "select" && minvalue == currentValue) {
        //   failure.error[errorid] = errormsg;
        errorFlag = false;
    }
    if (errorFlag == true && parseInt(minvalue) > parseInt(currentValue)) {
        if (errormsg == undefined) errormsg = "";
        //errormsg += " minimum " + minvalue + " value expecting";
        errorInfo = false;
        errorFlag = false;
    }
    if (errorFlag == true && type.indexOf("[") != -1 && currentValue != "") {
        var testExp = new RegExp(type);
        if (testExp.test(currentValue) == false) {
            errorFlag = false;
        }
    }
    if (errorFlag == true && type.indexOf("[") == -1 && currentValue == "") {
        //    failure.error[errorid] = errormsg;
        errorFlag = false;

    }



    try {
        if (errorFlag == false) {
            addOrUpdateError(errorid, errormsg);
            //ngModel.$setValidity('invalid', false);
            element.removeClass("ng-invalid");
          //  element.addClass("ng-invalid", 1000);
            setTimeout(function () {
                     element.addClass('ng-invalid');
            
            },100);
            if (errorInfo == false) {
                addErrorInfo(element, errormsg);
            }


            //This condition only for SVP Template...
            /*   if (type == "select") {
               svpApp.util.buildSelects();
            }*/
            //This condition only for SVP Template...

        } else {
            removeError(errorid);
            element.removeClass("ng-invalid");

            removeErrorInfo(element);

        }

    } catch (e) {
        alert("error " + e)
    }
}
function setParentTransition(id, prop, delay, style, callback) {
    $(id).css({ '-webkit-transition': prop + ' ' + delay + ' ' + style });
    $(id).css({ '-moz-transition': prop + ' ' + delay + ' ' + style });
    $(id).css({ '-o-transition': prop + ' ' + delay + ' ' + style });
    $(id).css({ 'transition': prop + ' ' + delay + ' ' + style });
    callback();
}

var addErrorInfo = function (element, errormsg) {
    if (errormsg != undefined) {
        var obj = element.parent();
        if (obj != undefined) {
            if (obj.find(".infoicon").length == 0) {

                //obj.append('<a href="javascript:void(0)"  class="infoicon infoicon-error" data-toggle="tooltip" data-placement="right" title="' + errormsg + '">&nbsp;</a>');
                obj.append('<div class="alert alert-information col-sm-12 nomargin  infoicon">' + errormsg + '</div>');

                //$('[data-toggle="tooltip"]').tooltip();
            }
        }
    }
}
var removeErrorInfo = function (element) {
    var obj = element.parent();
    if (obj != undefined) {
        if (obj.find(".infoicon").length > 0) {
            var removeObj = obj.find(".infoicon");
            removeObj.remove();
        }
    }
}
var addOrUpdateError = function (errorid, errormsg) {
    //Add / Update Error Summary
    if (pageError.errorSummary.length != 0) {
        var alreadyErrorIdExists = false;
        for (var j = 0; j < pageError.errorSummary.length; j++) {
            if (pageError.errorSummary[j] != null && pageError.errorSummary[j].errorId == errorid) {
                alreadyErrorIdExists = true;
                pageError.errorSummary[j].errorId = errorid;
                pageError.errorSummary[j].errorMsg = errormsg;

            }
        }
        if (alreadyErrorIdExists == false) {
            pageError.errorSummary.push({ errorId: errorid, errorMsg: errormsg });
        }
    } else {
        pageError.errorSummary.push({ errorId: errorid, errorMsg: errormsg });
    }
    setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");
};

var removeError = function (errorid) {
    //If the user updated correct value then remove that error

    for (var i = 0; i < pageError.errorSummary.length; i++) {
        if (pageError.errorSummary[i] != null) {
            if (pageError.errorSummary[i] != undefined && pageError.errorSummary[i].errorId == errorid) {
                //delete failure.errorSummary[i];
                pageError.errorSummary.splice(i, 1);
                break;
            }
        }
    }
    //setIframeHeight("ScsNavigator_wealthCreditDetailCtl_wmgHost");
};



var pageError = {
    submit: false,
    error: {},
    errorSummary: []
};
var INTEGER_REGEXP = /^\-?\d+$/;

var pageValidator = function (Parent) {


/*
    $.each($("[reset]"), function (i, j) {
        if ($(j).is(":visible") == false || $(j).is(":disabled") == true) {
        //    $(j).trigger("change");
           
        }
    });*/
    setTimeout(function () {
        svpApp.util.buildSelects();
    }, 100);
    
    var searchObjStart = null;
    if (Parent == "modal") {
        searchObjStart = $(".modalcontainer").find("[valid]");
    }
    if (searchObjStart == null) {
        searchObjStart = $("[valid]");
    }
    $.each($(searchObjStart), function (i, j) {
        if ($(j).is(":visible") == false || $(j).is(":disabled") == true) {
            $(j).removeClass("ng-invalid");
            
            removeError($(j).attr("vid"));

        } else {
            $(j).trigger('change');
        }


    });
    $(".panel-default-error").css("border", "");
    //console.log(failure);
    var submitFlag = true;
    var scrollToId = "";
    angular.forEach(pageError.errorSummary, function (object, index) {
        if (object != undefined && object.errorMsg != "") {
            submitFlag = false;
            $("[vid='" + object.errorId + "']").removeClass("ng-invalid");
            $("[vid='" + object.errorId + "']").closest(".panel-default-error").css("border", "1px solid red");
            $("[vid='" + object.errorId + "']").closest(".panel-default").css("border", "1px solid red");
            setTimeout(function() {
                $("[vid='" + object.errorId + "']").addClass('ng-invalid');
                $("[vid='" + object.errorId + "']").closest(".panel-default").css("border", "");


            }, 3000);

            // $("[vid='" + object.errorId + "']").addClass("ng-invalid");
            if (scrollToId == "")
                scrollToId = "[vid = '" + object.errorId + "']";
        } 
    });
    pageError.submit = submitFlag;
    try {
        if (scrollToId != "") {
            $('html, body').scrollTop(0);
            $(parent.window).scrollTop(0);
        }
    } catch (e) {
    }
    return pageError.submit;
}
