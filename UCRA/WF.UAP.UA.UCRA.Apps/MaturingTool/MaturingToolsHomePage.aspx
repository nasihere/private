<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaturingToolsHomePage.aspx.cs" Inherits="WF.UAP.UA.UCRA.Apps.MaturingTool.MaturingToolsHomePage" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge; IE=11; IE=8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../Global/Content/Css/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="../Global/Content/Css/Bootstrap/ie.css" rel="stylesheet" />
    <link href="../Global/Content/Css/Bootstrap/plugin.css" rel="stylesheet" />
    <link href="../Global/Content/Css/Bootstrap/datepicker3.css" rel="stylesheet" />
    <link href="../Global/Content/Css/Bootstrap/global.css" rel="stylesheet" />
    <link href="Content/custom.css" rel="stylesheet" />
    <link href="../Global/Content/Css/loader.css" rel="stylesheet" />
    <script>
        var apiUrl = "<%=CuspAppsMaturingOptionsWebApiBaseUrl%>";
    </script>
    <script src="../Global/Scripts/Lib/SVP/es5-shim-min.js"></script>
    <script src="../Global/Scripts/Lib/jQuery/jquery-1.9.0.js"></script>
    <script src="../Global/Scripts/Lib/angularjs 1.2.24/angular.js"></script>
    <script src="../Global/Scripts/Lib/angularjs%201.2.24/angular-route.js"></script>
    <script src="../Global/Scripts/Lib/svp/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="Scripts/MaturingToolsRouter.js"></script>
    <script src="Scripts/controllers/statusScreenController.js"></script>
    <script src="Scripts/controllers/maturingController.js"></script>
    <script src="Constants/constants.js"></script>
    <script src="Scripts/services/heDataService.js"></script>
    <script src="Scripts/directives/DatePickerDirective.js"></script>
    <script src="Scripts/directives/decimalFormat.js"></script>
    <script src="Scripts/directives/loader.js"></script>
</head>  
<body data-ng-app="MaturingTools">
    <div ng-view></div>
    <input type="hidden" id="userId" value="<%=UserId%>"/>
    <input type="hidden" id="userType" value="<%=UserType%>"/>
    <input type="hidden" id="mloId" value="<%=MloId%>"/>
    <input type="hidden" id="agentName" value="<%=AgentName%>"/>
    <input type="hidden" id="endDate" value="<%=EndDate%>"/>
</body>
</html>