// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InqResponse.cs" company="">
//   
// </copyright>
// <summary>
//   The inq response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json;

namespace WF.EAI.BLL.CUSP.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    using WF.EAI.Entities.domain.c2c.Common;
    using WF.EAI.Entities.domain.c2c.Core;
    using WF.EAI.Entities.domain.cusp.Core;
    using WF.EAI.Utils;
    using WF.EAI.Entities.constants;
    using WF.UAP.UASF.CrossCutting.Logging;

    using AcapsErrorMessage = WF.EAI.Entities.domain.cusp.Core.AcapsErrorMessage;

    /// <summary>
    ///     The inq response.
    /// </summary>
    public class InqResponse
    {
        #region Constants

        /// <summary>
        ///     The error_ node.
        /// </summary>
        private const string ErrorNode = "/ACAPS01/Body/Data/message/panel/error";

        /// <summary>
        ///     The error_ panel.
        /// </summary>
        private const string ErrorPanel = "/ACAPS01/Body/Data/message/panel";

        /// <summary>
        ///     The field_ xpath.
        /// </summary>
        private const string FieldXpath = "/ACAPS01/Body/Data/fields/field";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="resXmlStr">
        /// The res xml str.
        /// </param>
        /// <param name="appHeaderEx">
        /// The app header ex.
        /// </param>
        /// <returns>
        /// The <see cref="CUSPAppDataHeader"/>.
        /// </returns>
        public static CUSPAppDataHeader Parse(string resXmlStr, CUSPAppDataHeader appHeaderEx)
        {
            const string FIELD_XPATH = "/ACAPS01/Body/Data/fields/field";
            const string ERROR_PANEL = "/ACAPS01/Body/Data/message/panel";
            const string ERROR_NODE = "/ACAPS01/Body/Data/message/panel/error";
            var name = string.Empty;
            var theValue = string.Empty;
            var id = string.Empty;
            var cursor = string.Empty;
            var type = string.Empty;
            var errorCode = string.Empty;
            var currentXPath = string.Empty;
            List<AcapsErrorMessage> acapsMessages = null;
            var acapsFieldTemp = new Dictionary<string, FieldAttribute>();
            var acapsKey = string.Empty;
            var length = string.Empty;
            var protection = string.Empty;
            var pageNumber = string.Empty;
            FieldAttribute lp = null;
            try
            {
                using (var xmlReader = XmlReader.Create(new StringReader(resXmlStr)))
                {
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                currentXPath += "/" + xmlReader.Name.ToUpper();
                                if (string.Compare(currentXPath, FIELD_XPATH, true) == 0)
                                {
                                    name = xmlReader.GetAttribute("name").Trim();
                                    length = xmlReader.GetAttribute("length").Trim();
                                    protection = xmlReader.GetAttribute("protected");
                                    pageNumber = xmlReader.GetAttribute("page_number").Trim();
                                }
                                else if (string.Compare(currentXPath, ERROR_PANEL, true) == 0)
                                {
                                    id = xmlReader.GetAttribute("id").Trim();
                                    cursor = xmlReader.GetAttribute("cursor").Trim();
                                }
                                else if (string.Compare(currentXPath, ERROR_NODE, true) == 0)
                                {
                                    errorCode = xmlReader.GetAttribute("errorCode").TrimStart('0');
                                    type = xmlReader.GetAttribute("type").Trim();
                                }

                                if (xmlReader.IsEmptyElement)
                                {
                                    if (string.Compare(currentXPath, FIELD_XPATH, true) == 0)
                                    {
                                        lp = new FieldAttribute(name, theValue.Trim(), length, protection, pageNumber);
                                        acapsKey = string.Concat(name, "_PG_" + pageNumber.TrimStart('0'));

                                        if (!acapsFieldTemp.ContainsKey(acapsKey))
                                        {
                                            acapsFieldTemp.Add(acapsKey, lp);
                                        }
                                    }
                                    else if (string.Compare(currentXPath, ERROR_NODE, true) == 0)
                                    {
                                        if (acapsMessages == null)
                                        {
                                            acapsMessages = new List<AcapsErrorMessage>();
                                        }

                                        acapsMessages.Add(
                                            new AcapsErrorMessage
                                                {
                                                    Id = id, 
                                                    Code = errorCode, 
                                                    Message = theValue, 
                                                    Name = name, 
                                                    Type = type
                                                });
                                        if (errorCode == "303007" )
                                        {
                                            appHeaderEx.AppFound = false;
                                        }
                                    }

                                    currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                    theValue = string.Empty;
                                }

                                break;
                            case XmlNodeType.Text:
                                theValue = xmlReader.Value.Trim('_');
                                break;

                            case XmlNodeType.CDATA:
                                theValue = xmlReader.Value.Trim('_');
                                break;

                            case XmlNodeType.EndElement:
                                if (string.Compare(currentXPath, FIELD_XPATH, true) == 0)
                                {
                                    theValue = Helper.TrimString(theValue);
                                    lp = new FieldAttribute(name, theValue, length, protection, pageNumber);
                                    acapsKey = string.Concat(name, "_PG_" + pageNumber.TrimStart('0'));

                                    if (!acapsFieldTemp.ContainsKey(acapsKey))
                                    {
                                        acapsFieldTemp.Add(acapsKey, lp);
                                    }
                                    else
                                    {
                                        acapsFieldTemp[acapsKey] = lp;
                                    }
                                }
                                else if (string.Compare(currentXPath, ERROR_NODE, true) == 0)
                                {
                                    if (acapsMessages == null)
                                    {
                                        acapsMessages = new List<AcapsErrorMessage>();
                                    }

                                    acapsMessages.Add(
                                        new AcapsErrorMessage
                                            {
                                                Id = id, 
                                                Code = errorCode, 
                                                Message = theValue, 
                                                Name = cursor, 
                                                Type = type
                                            });
                                }

                                switch (xmlReader.Name.ToLower())
                                {
                                    case "applicationid":
                                        appHeaderEx.ApplicationId = theValue;
                                        break;
                                    case "applicationsuffix":
                                        appHeaderEx.ApplicationSuffix = theValue;
                                        break;
                                    case "acaps_session":
                                        appHeaderEx.AcapsSessionId = theValue;
                                        break;
                                    case "acaps_function":
                                        appHeaderEx.AcapsFunction = theValue;
                                        break;
                                    case "branch_code":
                                        appHeaderEx.BranchCode = theValue;
                                        break;
                                    case "location_code":
                                        appHeaderEx.LocationCode = theValue;
                                        break;
                                    case "panel_key":
                                        appHeaderEx.PanelKey = theValue;
                                        break;
                                    case "worklist_userid":
                                        appHeaderEx.WorklistUserid = theValue;
                                        break;
                                    case "gui_function":
                                        appHeaderEx.GuiFunction = theValue;
                                        break;
                                }

                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                theValue = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                //Logger.Instance.Error(Utilities.FormatExceptionMessage(string.Empty, string.Empty, ex.Message), ex);
            }
            finally
            {
                if (acapsMessages != null && acapsMessages.Count > 0)
                {
                    appHeaderEx.AcapsErrorMessages = JsonMapper<string>.ObjectToJsonText(acapsMessages);
                }
                else
                {
                    appHeaderEx.AcapsErrorMessages = string.Empty;
                }

                if (acapsFieldTemp != null && acapsFieldTemp.Count > 0)
                {
                    appHeaderEx.ApplicationData = JsonMapper<string>.ObjectToJsonText(acapsFieldTemp);
                }
                else
                {
                    appHeaderEx.ApplicationData = string.Empty;
                }

                //Logger.Instance.Info(
                    //"In InqResponse ParseError, AppId:" + appHeaderEx.ApplicationId + ",AcapsSessionId:"
                    //+ appHeaderEx.AcapsSessionId);
            }

            return appHeaderEx;
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="resXml">
        /// The res xml.
        /// </param>
        /// <param name="applicationData">
        /// The application data.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationData"/>.
        /// </returns>
        public static ApplicationData Parse(string resXml, ApplicationData applicationData)
        {
            var acapsFieldTemp = new Dictionary<string, FieldAttribute>();

            string name = string.Empty;
            string acapsKey = string.Empty;
            string length = string.Empty;
            string theValue = string.Empty;
            string protection = string.Empty;
            string pageNumber = string.Empty;
            string id = string.Empty;
            string cursor = string.Empty;
            string type = string.Empty;
            string errorCode = string.Empty;
            string currentXPath = string.Empty;
            List<AcapsErrorMessage> acapsMessages = null;
            FieldAttribute lp = null;

            try
            {
                using (var xmlReader = XmlReader.Create(new StringReader(resXml)))
                {
                    acapsKey = string.Empty;
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                currentXPath += "/" + xmlReader.Name.ToUpper();
                                if (string.Compare(currentXPath, FieldXpath, true) == 0)
                                {
                                    name = xmlReader.GetAttribute("name").Trim();
                                    length = xmlReader.GetAttribute("length").Trim();
                                    protection = xmlReader.GetAttribute("protected");
                                    pageNumber = xmlReader.GetAttribute("page_number").Trim();
                                }
                                else if (string.Compare(currentXPath, ErrorPanel, true) == 0)
                                {
                                    id = xmlReader.GetAttribute("id").Trim();
                                    cursor = xmlReader.GetAttribute("cursor").Trim();
                                }
                                else if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                {
                                    errorCode = xmlReader.GetAttribute("errorCode").TrimStart('0');
                                    type = xmlReader.GetAttribute("type").Trim();
                                }

                                if (xmlReader.IsEmptyElement)
                                {
                                    if (string.Compare(currentXPath, FieldXpath, true) == 0)
                                    {
                                        lp = new FieldAttribute(name, theValue.Trim(), length, protection, pageNumber);
                                        acapsKey = string.Concat(name, "_PG_" + pageNumber.TrimStart('0'));

                                        if (!acapsFieldTemp.ContainsKey(acapsKey))
                                        {
                                            acapsFieldTemp.Add(acapsKey, lp);
                                        }
                                    }
                                    else if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                    {
                                        if (acapsMessages == null)
                                        {
                                            acapsMessages = new List<AcapsErrorMessage>();
                                        }

                                        acapsMessages.Add(
                                            new AcapsErrorMessage
                                                {
                                                    Id = id, 
                                                    Code = errorCode, 
                                                    Message = theValue.Trim(), 
                                                    Name = name, 
                                                    Type = type
                                                });
                                    }

                                    currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                    theValue = string.Empty;
                                }

                                break;
                            case XmlNodeType.Text:
                                theValue = xmlReader.Value.Trim('_');
                                break;

                            case XmlNodeType.CDATA:
                                theValue = xmlReader.Value.Trim('_');
                                break;
                            case XmlNodeType.EndElement:

                                // Save in the Hashtable if its the Field
                                if (string.Compare(currentXPath, FieldXpath, true) == 0)
                                {
                                    theValue = Helper.TrimString(theValue);
                                    lp = new FieldAttribute(name, theValue, length, protection, pageNumber);
                                    acapsKey = string.Concat(name, "_PG_" + pageNumber.TrimStart('0'));

                                    if (!acapsFieldTemp.ContainsKey(acapsKey))
                                    {
                                        acapsFieldTemp.Add(acapsKey, lp);
                                    }
                                    else
                                    {
                                        acapsFieldTemp[acapsKey] = lp;
                                    }
                                }
                                else if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                {
                                    if (acapsMessages == null)
                                    {
                                        acapsMessages = new List<AcapsErrorMessage>();
                                    }

                                    // Cursor name was wrong.
                                    acapsMessages.Add(
                                        new AcapsErrorMessage
                                            {
                                                Id = id, 
                                                Code = errorCode, 
                                                Message = theValue, 
                                                Name = cursor, 
                                                Type = type
                                            });
                                }

                                switch (xmlReader.Name.ToLower())
                                {
                                    case "applicationid":
                                        applicationData.AppDataHeader.ApplicationId = theValue;
                                        break;
                                    case "applicationsuffix":
                                        applicationData.AppDataHeader.ApplicationSuffix = theValue;
                                        break;
                                    case "acaps_session":
                                        applicationData.AppDataHeader.AcapsSessionId = theValue;
                                        break;
                                    case "acaps_function":
                                        applicationData.AppDataHeader.AcapsFunction = theValue;
                                        break;
                                    case "branch_code":
                                        applicationData.AppDataHeader.BranchCode = theValue;
                                        break;
                                    case "location_code":
                                        applicationData.AppDataHeader.LocationCode = theValue;
                                        break;
                                    case "panel_key":
                                        applicationData.AppDataHeader.PanelKey = theValue;
                                        break;
                                    case "worklist_userid":
                                        applicationData.AppDataHeader.WorklistUserid = theValue;
                                        break;
                                    case "gui_function":
                                        applicationData.AppDataHeader.GuiFunction = theValue;
                                        break;
                                }

                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));

                                // Adjust current XPath
                                theValue = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Error(ex.StackTrace);
            }
            finally
            {
                applicationData.AcapsMessage = acapsMessages;
                applicationData.AcapFields = acapsFieldTemp;
            }
            return applicationData;
        }

        /// <summary>
        /// The parse error.
        /// </summary>
        /// <param name="resXmlStr">
        /// The res xml str.
        /// </param>
        /// <param name="applicationData">
        /// The application data.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationData"/>.
        /// </returns>
        public static ApplicationData ParseError(string resXmlStr, ApplicationData applicationData)
        {
            string name = string.Empty;
            string theValue = string.Empty;
            string id = string.Empty;
            string cursor = string.Empty;
            string type = string.Empty;
            string errorCode = string.Empty;
            string currentXPath = string.Empty;
            List<AcapsErrorMessage> acapsMessages = null;

            try
            {
                using (var xmlReader = XmlReader.Create(new StringReader(resXmlStr)))
                {
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                currentXPath += "/" + xmlReader.Name.ToUpper();
                                if (string.Compare(currentXPath, ErrorPanel, true) == 0)
                                {
                                    id = xmlReader.GetAttribute("id").Trim();
                                    cursor = xmlReader.GetAttribute("cursor").Trim();
                                }
                                else if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                {
                                    errorCode = xmlReader.GetAttribute("errorCode").TrimStart('0');
                                    type = xmlReader.GetAttribute("type").Trim();
                                }

                                if (xmlReader.IsEmptyElement)
                                {
                                    if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                    {
                                        if (acapsMessages == null)
                                        {
                                            acapsMessages = new List<AcapsErrorMessage>();
                                        }

                                        acapsMessages.Add(
                                            new AcapsErrorMessage
                                                {
                                                    Id = id, 
                                                    Code = errorCode, 
                                                    Message = theValue, 
                                                    Name = name, 
                                                    Type = type
                                                });
                                    }

                                    currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                    theValue = string.Empty;
                                }

                                break;
                            case XmlNodeType.Text:
                                theValue = xmlReader.Value.Trim('_');
                                break;

                            case XmlNodeType.CDATA:
                                theValue = xmlReader.Value.Trim('_');
                                break;

                            case XmlNodeType.EndElement:

                                if (string.Compare(currentXPath, ErrorNode, true) == 0)
                                {
                                    if (acapsMessages == null)
                                    {
                                        acapsMessages = new List<AcapsErrorMessage>();
                                    }

                                    acapsMessages.Add(
                                        new AcapsErrorMessage
                                            {
                                                Id = id, 
                                                Code = errorCode, 
                                                Message = theValue, 
                                                Name = name, 
                                                Type = type
                                            });
                                }

                                switch (xmlReader.Name.ToLower())
                                {
                                    case "applicationid":
                                        applicationData.AppDataHeader.ApplicationId = theValue;
                                        break;
                                    case "applicationsuffix":
                                        applicationData.AppDataHeader.ApplicationSuffix = theValue;
                                        break;
                                    case "acaps_session":
                                        applicationData.AppDataHeader.AcapsSessionId = theValue;
                                        break;
                                    case "acaps_function":
                                        applicationData.AppDataHeader.AcapsFunction = theValue;
                                        break;
                                    case "branch_code":
                                        applicationData.AppDataHeader.BranchCode = theValue;
                                        break;
                                    case "location_code":
                                        applicationData.AppDataHeader.LocationCode = theValue;
                                        break;
                                    case "panel_key":
                                        applicationData.AppDataHeader.PanelKey = theValue;
                                        break;
                                    case "worklist_userid":
                                        applicationData.AppDataHeader.WorklistUserid = theValue;
                                        break;
                                    case "gui_function":
                                        applicationData.AppDataHeader.GuiFunction = theValue;
                                        break;
                                }

                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                theValue = string.Empty;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Error(Utilities.FormatExceptionMessage(string.Empty, string.Empty, ex.Message), ex);
            }
            finally
            {
                applicationData.AcapsMessage = acapsMessages;
                //Logger.Instance.Info(
                    //"In InqResponse ParseError, AppId:" + applicationData.AppDataHeader.ApplicationId
                //+ ",AcapsSessionId:" + applicationData.AppDataHeader.AcapsSessionId);
            }

            return applicationData;
        }

        /// <summary>
        /// The parse error.
        /// </summary>
        /// <param name="resXmlStr">
        /// The res xml str.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<AcapsErrorMessage> ParseError(string resXmlStr)
        {
            var appdata = new ApplicationData();
            appdata = ParseError(resXmlStr, appdata);
            return appdata.AcapsMessage;
        }

        /// <summary>
        /// </summary>
        /// <param name="appHeader">
        /// </param>
        public static void SetGenericException(CUSPAppDataHeader appHeader, string message)
        {
            var acapsMessages = new List<AcapsErrorMessage>
                                    {
                                        new AcapsErrorMessage
                                            {
                                                Id = "xxxxxx", 
                                                Code = "xxxxxx", 
                                                Message = message == string.Empty ? Lookup.ErrorMessage
                                                : message, 
                                                Name =
                                                    Lookup.BusinessLayerException, 
                                                Type =
                                                    Lookup.BusinessLayerException
                                            }
                                    };
            appHeader.AcapsErrorMessages = JsonMapper<string>.ObjectToJsonText(acapsMessages);
        }


       public static void SetCommComments(CUSPAppDataHeader appHeader)
       {
           if (!string.IsNullOrEmpty(appHeader.ApplicationData))
           {
               object jsonObject = JsonConvert.DeserializeObject(appHeader.ApplicationData);
               if (jsonObject != null)
               {
                   CUSPApplicationData cuspApplicationData = new CUSPApplicationData();
                   cuspApplicationData.AcapFields =
                       JsonMapper<Dictionary<string, FieldAttribute>>
                           .JsonTextToAppDataHeader<Dictionary<string, FieldAttribute>>(Convert.ToString(jsonObject));


                   appHeader.ComComments = cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_1_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_2_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_3_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_4_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_5_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_6_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_7_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_8_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_9_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_10_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_11_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_12_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_13_PG_1").Value + " " +
                                           cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_14_PG_1").Value;

               }
           }
       }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appHeader"></param>
        public static void SetPrfCommComments(CUSPAppDataHeader appHeader)
        {
            object jsonObject = JsonConvert.DeserializeObject(appHeader.ApplicationData);
            var cuspApplicationData = new CUSPApplicationData
            {
                AcapFields = JsonMapper<Dictionary<string, FieldAttribute>>
                    .JsonTextToAppDataHeader<Dictionary<string, FieldAttribute>>(Convert.ToString(jsonObject))
            };

            appHeader.ComComments = cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_1_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_2_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_3_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_4_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_5_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_6_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_7_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_8_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_9_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_10_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_11_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_12_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_13_PG_1").Value + " " +
                                    cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_14_PG_1").Value;

            appHeader.AprvComments = cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_1_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_2_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_3_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_4_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_5_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_6_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_7_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_8_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_9_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_10_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_11_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_12_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_13_PG_2").Value + " " +
                                     cuspApplicationData.GetAcapsField("COM_COMMENT_TEXT_14_PG_2").Value;
        }

        #endregion


    }
}