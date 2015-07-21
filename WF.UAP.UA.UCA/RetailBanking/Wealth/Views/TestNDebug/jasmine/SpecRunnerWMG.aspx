<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecRunnerWMG.aspx.cs" Inherits="WF.EAI.Web.UCA.RetailBanking.Wealth.Views.TestNDebug.jasmine.SpecRunnerWMG" %>

<!DOCTYPE HTML>
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <title>Spec Runner v2.0.2</title>
    <script>
        var Root = <% = ViewState["json"] %>;
    </script>
  <link rel="stylesheet" type="text/css" href="../../../../../Global/Content/Css/jasmine-2.0.2/jasmine.css">
    
  <script type="text/javascript" src="../../../../../Global/Scripts/Lib/jQuery/jquery-1.9.0.min.js"></script>
  <script type="text/javascript" src="../../../../../Global/Scripts/Lib/jasmine-2.0.2/jasmine.js"></script>
  <script type="text/javascript" src="../../../../../Global/Scripts/Lib/jasmine-2.0.2/jasmine-html.js"></script>
  <script type="text/javascript" src="../../../../../Global/Scripts/Lib/jasmine-2.0.2/boot.js"></script>
    
    
  <!-- include source files here... -->
  <script type="text/javascript" src="src/wmgJson.js"></script>
  
  <!-- include spec files here... -->
  
  <script type="text/javascript" src="spec/wmgJSONTestService.js"></script>

</head>

    <body>
        <h4>Wealth Management JSON Test Case</h4>
    </body>
</html>