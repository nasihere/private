// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utilities.cs" company="">
//   
// </copyright>
// <summary>
//   The utilities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

using System.Linq;

namespace WF.EAI.Web.Controls.uiUtils.uuw
{
    using Telerik.Web.UI;

    using WF.EAI.Data.caching.c2c;
    using WF.EAI.Entities.constants;
    using WF.EAI.Entities.domain;
    using WF.EAI.Entities.domain.c2c;
    using WF.EAI.Entities.domain.c2c.Common;
    using WF.EAI.Entities.domain.cusp.Core;
    using WF.EAI.Entities.exception;
    using WF.EAI.Utils.xsl;
    using WF.UAP.UA.Global.BLL;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    /// The utilities.
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// The parse xml.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="xmlString">
        /// The xml string.
        /// </param>
        /// <returns>
        /// The parse xml.
        /// </returns>

        public const string TaskReprocessor = "TaskReprocessor";
        public const string CheckTaskOwnerEligibility = "CheckTaskOwnerEligibility";



        /// <summary>
        /// The build app header.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// </returns>
        public static AppDataHeader BuildAppHeader(User user)
        {
            AppDataHeader header = new AppDataHeader();
            if (user != null)
            {
                header.UserId = user.LoginUseridValue;
                header.Password = user.LoginPasswordValue;
                header.AcapsFunction = user.AcapsFunction;
            }
            else
            {
                header.UserId = "";
                header.Password = "";
                header.AcapsFunction = "";
            }
            header.SessionId = HttpContext.Current.Session.SessionID;
            return header;
        }

        /// <summary>
        /// The build app header.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <returns>
        /// </returns>
        public static CUSPAppDataHeader BuildCUSPAppHeader(User user, string applicationId)
        {
            CUSPAppDataHeader header = BuildCUSPAppHeader(user);
            header.ApplicationId = applicationId;
            return header;
        }





        /// <summary>
        /// The build app header.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// </returns>
        public static CUSPAppDataHeader BuildCUSPAppHeader(User user)
        {
            CUSPAppDataHeader header = new CUSPAppDataHeader();

            header.UserId = user.LoginUseridValue;
            header.Password = user.LoginPasswordValue;
            header.AcapsFunction = user.AcapsFunction;
            header.SessionId = HttpContext.Current.Session.SessionID;

            return header;
        }

       
        /// <summary>
        /// Transform the ACAPS Response XML into the UI Entity Object
        /// </summary>
        /// <typeparam name="TUiEntityObject">
        /// </typeparam>
        /// <param name="xmlData">
        /// </param>
        /// <param name="xsltFile">
        /// </param>
        /// <returns>
        /// </returns>
        public static TUiEntityObject BindTransformedXmlToUiEntity<TUiEntityObject>(string xmlData, string xsltFile)
        {
            StringReader sr = null;
            XmlTextReader xtr = null;
            TUiEntityObject entity = default(TUiEntityObject);
            try
            {
                if (!xmlData.Equals(string.Empty))
                {
                    // Transform the SIF response Xml
                    string transformedXml = XsltTransformer.Transform(xmlData, xsltFile);


                    // Convert the XML into Object
                    XmlSerializer serializer = null;
                    sr = new StringReader(transformedXml);
                    xtr = new XmlTextReader(sr);
                    serializer = new XmlSerializer(typeof(TUiEntityObject));
                    if (serializer.CanDeserialize(xtr))
                    {
                        // Deserialize into UI Entity object
                        entity = (TUiEntityObject)serializer.Deserialize(xtr);
                    }
                    else
                    {
                        // bannerEntity.Exception = new ApplicationException("Unable to deserialize UI Entity.");
                    }
                }
                else
                {
                    // TODO:Add logic to handle when the data is not a valid xml
                }

            }
            catch (Exception ex)
            {
                if (sr != null) sr.Close();
                if (xtr != null) xtr.Close();
                throw ex;
            }
            finally
            {
                if (sr != null) sr.Close();
                if (xtr != null) xtr.Close();
            }

            return entity;
        }

        /// <summary>
        /// The get json function.
        /// </summary>
        /// <param name="acapsFieldName">
        /// The acaps field name.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="originalValue">
        /// The original value.
        /// </param>
        /// <returns>
        /// The get json function.
        /// </returns>
        public static string GetJsonFunction(string acapsFieldName, string pageNumber, string originalValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("javascript:ConstructJson(this,'" + acapsFieldName + "','");
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            if (!string.IsNullOrEmpty(originalValue))
            {
                sbJson.Append(",\"OriginalValue\":");
                sbJson.Append("\"");
                sbJson.Append(originalValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"OriginalValue\":null");
            }

            sbJson.Append(",\"CurrentValue\":null");
            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");
            sbJson.Append("','" + originalValue + "');");//CDTS00318104

            return sbJson.ToString();
        }

        public static string GetJsonFunctionforDecision(string acapsFieldName, string pageNumber, string originalValue, string controlID)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("javascript:ConstructJsonforDecision(this,'" + acapsFieldName + "','" + controlID + "','");
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            if (!string.IsNullOrEmpty(originalValue))
            {
                sbJson.Append(",\"OriginalValue\":");
                sbJson.Append("\"");
                sbJson.Append(originalValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"OriginalValue\":null");
            }

            sbJson.Append(",\"CurrentValue\":null");
            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");
            sbJson.Append("');");

            return sbJson.ToString();
        }

        /// <summary>
        /// The get json function.
        /// </summary>
        /// <param name="acapsFieldName">
        /// The acaps field name.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="currentValue">
        /// The current value.
        /// </param>
        /// <param name="fieldId">
        /// The field id.
        /// </param>
        /// <returns>
        /// The get json function.
        /// </returns>
        public static string GetJsonFunction(string acapsFieldName, string pageNumber, string currentValue, string fieldId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("javascript:ConstructJson(document.getElementById('" + fieldId + "'),'" + acapsFieldName + "','");
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            sbJson.Append(",\"OriginalValue\":null");
            sbJson.Append(",\"CurrentValue\":");
            sbJson.Append("\"");
            sbJson.Append(currentValue);
            sbJson.Append("\"");
            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");
            sbJson.Append("');");

            return sbJson.ToString();
        }
        public static string GetJsonFunctionForMultipleTags(List<string> acapsFieldNameList, string acapFieldNames, string charList, int startIndex, string pageNumber, string originalValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("javascript:ConstructJsonForMultipleTag(this,'" + acapFieldNames + "','" + charList + "','" + startIndex + "','");
            int cnt = acapsFieldNameList.Count;
            int loopCount = 1;
            foreach (var acapsfield in acapsFieldNameList)
            {

                sbJson.Append("{");
                sbJson.Append("\"AcapsFieldName\":");
                sbJson.Append("\"");
                sbJson.Append(acapsfield);
                sbJson.Append("\"");
                sbJson.Append(",\"PageNumber\":");
                sbJson.Append("\"");
                sbJson.Append(pageNumber);
                sbJson.Append("\"");
                if (!string.IsNullOrEmpty(originalValue))
                {
                    sbJson.Append(",\"OriginalValue\":");
                    sbJson.Append("\"");
                    sbJson.Append("\"");
                }
                else
                {
                    sbJson.Append(",\"OriginalValue\":null");
                }

                sbJson.Append(",\"CurrentValue\":null");
                sbJson.Append(",\"FieldId\":null");
                if (loopCount != cnt)
                    sbJson.Append("}|");
                else
                    sbJson.Append("}");
                loopCount++;

            }
            sbJson.Append("');");

            return sbJson.ToString();
        }
        public static string GetJsonFunctionForAttachExtraTags(List<string> acapsFieldNameList, string acapFieldNames, string valueList, string pageNumber, string originalValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("javascript:ConstructJsonForAttachExtraTag(this,'" + acapFieldNames + "','" + valueList + "','");
            int cnt = acapsFieldNameList.Count;
            int loopCount = 1;
            foreach (var acapsfield in acapsFieldNameList)
            {

                sbJson.Append("{");
                sbJson.Append("\"AcapsFieldName\":");
                sbJson.Append("\"");
                sbJson.Append(acapsfield);
                sbJson.Append("\"");
                sbJson.Append(",\"PageNumber\":");
                sbJson.Append("\"");
                sbJson.Append(pageNumber);
                sbJson.Append("\"");
                if (!string.IsNullOrEmpty(originalValue))
                {
                    sbJson.Append(",\"OriginalValue\":");
                    sbJson.Append("\"");
                    sbJson.Append("\"");
                }
                else
                {
                    sbJson.Append(",\"OriginalValue\":null");
                }

                sbJson.Append(",\"CurrentValue\":null");
                sbJson.Append(",\"FieldId\":null");
                if (loopCount != cnt)
                    sbJson.Append("}|");
                else
                    sbJson.Append("}");
                loopCount++;

            }
            sbJson.Append("');");

            return sbJson.ToString();
        }
        public string GetJsonFunctionwithoutFieldId(string acapsFieldName, string pageNumber, string originalValue, string currentValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            if (!string.IsNullOrEmpty(originalValue))
            {
                sbJson.Append(",\"OriginalValue\":");
                sbJson.Append("\"");
                sbJson.Append(originalValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"OriginalValue\":null");
            }

            if (!string.IsNullOrEmpty(currentValue))
            {
                sbJson.Append(",\"CurrentValue\":");
                sbJson.Append("\"");
                sbJson.Append(currentValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"CurrentValue\":null");
            }

            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");


            return sbJson.ToString();
        }

        /// <summary>
        /// The get json for buttons.
        /// </summary>
        /// <param name="acapsFieldName">
        /// The acaps field name.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="originalValue">
        /// The original value.
        /// </param>
        /// <param name="currentValue">
        /// The current value.
        /// </param>
        /// <returns>
        /// The get json for buttons.
        /// </returns>
        public static string GetJsonForButtons(string acapsFieldName, string pageNumber, string originalValue, string currentValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            if (!string.IsNullOrEmpty(originalValue))
            {
                sbJson.Append(",\"OriginalValue\":");
                sbJson.Append("\"");
                sbJson.Append(originalValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"OriginalValue\": \"null\"");
            }

            if (!string.IsNullOrEmpty(currentValue))
            {
                sbJson.Append(",\"CurrentValue\":");
                sbJson.Append("\"");
                sbJson.Append(currentValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"CurrentValue\":\"null\"");
            }

            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");


            // ,sbJson.Append("');");
            return sbJson.ToString();
        }

        /// <summary>
        /// The field mode style class.
        /// </summary>
        /// <param name="css">
        /// The css.
        /// </param>
        /// <param name="fieldMode">
        /// The field mode.
        /// </param>
        /// <returns>
        /// The field mode style class.
        /// </returns>
        public static string FieldModeStyleClass(string css, FieldAttribute.FieldModes fieldMode)
        {
            string oldCss = css;
            if (css.LastIndexOf(' ') > 0)
            {
                oldCss = css.Remove(css.LastIndexOf(' '), css.Length - css.LastIndexOf(' '));
            }

            return oldCss + " " + fieldMode.ToString();
        }


        /// <summary>
        /// Make Dropdown Read Only - Added on July-19th
        /// </summary>
        /// <param name="css">
        /// The css.
        /// </param>
        /// <param name="fieldMode">
        /// The field mode.
        /// </param>
        /// <returns>
        /// The field mode style class.
        /// </returns>
        public static string ReadonlyDropDownStyle(string css, FieldAttribute.FieldModes fieldMode)
        {
            return css + " " + fieldMode.ToString();
        }
        /// <summary>
        /// The set binding for textbox for stip comments.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForTextboxForStipComments(TextBox fieldControl, FieldAttribute fieldAttribute)
        {

            if (fieldAttribute != null)
            {

                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Text = fieldAttribute.Value;
                fieldControl.MaxLength = fieldAttribute.Length;
                fieldControl.ReadOnly = fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName + "~" + fieldAttribute.PageNumber);
                fieldControl.Attributes.Add("onchange", GetJsonFunction(fieldAttribute.AcapsFieldName, fieldAttribute.PageNumber, fieldAttribute.Value));
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;

            }
        }
        public static string GetFormattedDate()
        {
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString().Length < 2
                               ? DateTime.Today.Month.ToString().PadLeft(2, '0')
                               : DateTime.Today.Month.ToString();

            string day = DateTime.Today.Day.ToString().Length < 2
                             ? DateTime.Today.Day.ToString().PadLeft(2, '0')
                             : DateTime.Today.Day.ToString();

            return year + month + day;
        }

        public static string GetFormattedDate(DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month.ToString().Length < 2
                               ? date.Month.ToString().PadLeft(2, '0')
                               : date.Month.ToString();

            string day = date.Day.ToString().Length < 2
                             ? date.Day.ToString().PadLeft(2, '0')
                             : date.Day.ToString();

            return year + month + day;
        }


        public static StringBuilder GetJsonString(List<FieldAttribute> mergedSPList)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{\"AcapsFields\":[");
            foreach (var tempfield in mergedSPList)
            {
                sbJson.Append(GetJsonFunctionCustom(tempfield.AcapsFieldName, tempfield.PageNumber, tempfield.OriginalValue,
                                              tempfield.Value));
                sbJson.Append(",");

            }
            sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            sbJson.Append("]}");

            return sbJson;
        }

        public static string GetJsonFunctionCustom(string acapsFieldName, string pageNumber, string originalValue, string currentValue)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            sbJson.Append("\"AcapsFieldName\":");
            sbJson.Append("\"");
            sbJson.Append(acapsFieldName);
            sbJson.Append("\"");
            sbJson.Append(",\"PageNumber\":");
            sbJson.Append("\"");
            sbJson.Append(pageNumber);
            sbJson.Append("\"");
            if (!string.IsNullOrEmpty(originalValue))
            {
                sbJson.Append(",\"OriginalValue\":");
                sbJson.Append("\"");
                sbJson.Append(originalValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"OriginalValue\":null");
            }

            if (!string.IsNullOrEmpty(currentValue))
            {
                sbJson.Append(",\"CurrentValue\":");
                sbJson.Append("\"");
                sbJson.Append(currentValue.Replace("'", "\\'"));
                sbJson.Append("\"");
            }
            else
            {
                sbJson.Append(",\"CurrentValue\":null");
            }

            sbJson.Append(",\"FieldId\":null");
            sbJson.Append("}");


            return sbJson.ToString();
        }
        public static void SetBindingForTextbox(TextBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                if (fieldAttribute.AcapsFieldName == null)
                {
                    fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                    fieldControl.ReadOnly = true;
                    return;
                }

                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Text = fieldAttribute.Value.Trim();
                fieldControl.MaxLength = fieldAttribute.Length;
                fieldControl.ReadOnly = fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.Attributes.Add("onchange",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                    fieldControl.Attributes.Add("onkeyup",
                                               GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                         fieldAttribute.PageNumber,
                                                                         fieldAttribute.Value));
                }


            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
        }
       



        /// <summary>
        /// The set binding for textbox.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="additionalAttribute">
        /// The additional attribute.
        /// </param>
        /// <param name="addionalField">
        /// The addional field.
        /// </param>
        public static void SetBindingForTextbox(TextBox fieldControl, FieldAttribute fieldAttribute, FieldAttribute additionalAttribute, HiddenField addionalField)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Text = fieldAttribute.Value.Trim();
                fieldControl.MaxLength = fieldAttribute.Length;
                fieldControl.ReadOnly = fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.Attributes.Add("onchange",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                    fieldControl.Attributes.Add("AdditionalJson",
                                                GetJsonFunction(additionalAttribute.AcapsFieldName,
                                                                          additionalAttribute.PageNumber,
                                                                          addionalField.Value, addionalField.ClientID));
                }
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingMutlipleTagForTextbox(TextBox fieldControl, List<FieldAttribute> AttributeList, string charList)
        {
            List<string> acapsBuild = new List<string>();
            string textBuild = string.Empty;
            string pageNumber = string.Empty;
            int startIndex = 0;
            if (AttributeList != null)
            {
                foreach (var fieldAttribute in AttributeList)
                {
                    if (fieldAttribute.AcapsFieldName == null)
                    {
                        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                        fieldControl.ReadOnly = true;
                        continue;
                    }
                    fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                    if (!string.IsNullOrEmpty(fieldAttribute.Value))
                        textBuild = textBuild + " " + fieldAttribute.Value;
                    fieldControl.MaxLength = fieldAttribute.Length;
                    fieldControl.ReadOnly = fieldAttribute.IsProtected;
                    if (!fieldAttribute.IsProtected)
                    {
                        acapsBuild.Add(fieldAttribute.AcapsFieldName);
                        pageNumber = fieldAttribute.PageNumber;

                    }
                }
                fieldControl.Text = textBuild;
                string acapsName = string.Empty;
                int cnt = acapsBuild.Count;
                int loopcount = 1;
                foreach (var item in acapsBuild)
                {
                    if (loopcount != cnt)
                        acapsName = acapsName + item + ",";
                    else
                        acapsName = acapsName + item;
                    loopcount++;
                }
                var jsonOutput = GetJsonFunctionForMultipleTags(acapsBuild, acapsName, charList, startIndex, pageNumber, textBuild);
                fieldControl.Attributes.Add("onchange", jsonOutput);
                fieldControl.Attributes.Add("onkeyup", jsonOutput);
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingAttachExtraTagForTextbox(TextBox fieldControl, FieldAttribute MainAttribute, List<FieldAttribute> ExtraAttributeList)
        {
            List<string> acapsBuild = new List<string>();
            string textBuild = string.Empty;
            string pageNumber = string.Empty;
            string acapsName = string.Empty;
            if (MainAttribute != null)
            {
                if (MainAttribute.AcapsFieldName == null)
                {
                    fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                    fieldControl.ReadOnly = true;
                }
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, MainAttribute.FieldMode);
                fieldControl.Text = MainAttribute.Value.Trim();
                textBuild = MainAttribute.Value.Trim();
                fieldControl.MaxLength = MainAttribute.Length;
                fieldControl.ReadOnly = MainAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(MainAttribute.AcapsFieldName);
                acapsBuild.Add(MainAttribute.AcapsFieldName);
                pageNumber = MainAttribute.PageNumber;
                if (ExtraAttributeList != null)
                {
                    foreach (var fieldAttribute in ExtraAttributeList)
                    {
                        acapsBuild.Add(fieldAttribute.AcapsFieldName);
                        textBuild = textBuild + "|" + fieldAttribute.Value;
                        pageNumber = fieldAttribute.PageNumber;
                    }
                }


                int cnt = acapsBuild.Count;
                int loopcount = 1;
                foreach (var item in acapsBuild)
                {
                    if (loopcount != cnt)
                        acapsName = acapsName + item + "|";
                    else
                        acapsName = acapsName + item;
                    loopcount++;
                }
                var jsonOutput = GetJsonFunctionForAttachExtraTags(acapsBuild, acapsName, textBuild, pageNumber, MainAttribute.Value);
                fieldControl.Attributes.Add("onchange", jsonOutput);
                fieldControl.Attributes.Add("onkeyup", jsonOutput);
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
        }
        /// <summary>
        /// The set binding for check box.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForCheckBox(CheckBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                if (fieldAttribute.Value == "Y" || fieldAttribute.Value == "Yes")
                {
                    fieldControl.Checked = true;
                }
                else
                {
                    fieldControl.Checked = false;
                }

                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.Attributes.Add("onclick", "javascript:CheckBoxChanged(this);");
                    fieldControl.InputAttributes.Add("JsonFunction",
                                                     GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                               fieldAttribute.PageNumber,
                                                                               fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.Enabled = false;
            }

        }

        /// <summary>
        /// The set binding for radio button list.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForRadioButtonList(RadioButtonList fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                if (fieldAttribute.Value.ToUpper() == "Y" || fieldAttribute.Value.ToUpper() == "YES")
                {
                    fieldControl.SelectedValue = "Y";
                }
                else if (fieldAttribute.Value.ToUpper() == "N" || fieldAttribute.Value.ToUpper() == "NO")
                {
                    fieldControl.SelectedValue = "N";
                }

                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.Attributes.Add("onclick", "javascript:RadioButtonListChanged(this);");
                    fieldControl.Attributes.Add("JsonFunction",
                                                     GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                               fieldAttribute.PageNumber,
                                                                               fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.Enabled = false;
            }

        }

        /// <summary>
        /// The set binding for dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        public static void SetBindingForDropdown(RadComboBox fieldControl, string key, string currentBizOwner)
        {
            List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

            if (Ilist != null)
            {
                fieldControl.DataSource = Ilist;
                fieldControl.DataBind();
            }


        }

        // kokila @ 09/29 for CCUpdate dropdowns
        /// <summary>
        /// The set binding for cch dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        public static void SetBindingForCCHDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        {
            List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

            if (Ilist != null)
            {
                fieldControl.DataSource = Ilist;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientTextChange = "DropDownSelectedForCCH";
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelectedForCCH";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }

        }


        /// <summary>
        /// The set binding for cch dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="IList">
        /// The i list.
        /// </param>
        public static void SetBindingForCCHDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, List<RouteToState> IList)
        {

            if (IList != null)
            {
                fieldControl.DataSource = IList;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientTextChange = "DropDownSelectedForCCH";
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelectedForCCH";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }


        /// <summary>
        /// The set binding for dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        public static void SetBindingForDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        {

            List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

            if (Ilist != null)
            {
                fieldControl.DataSource = Ilist;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }

        /// <summary>
        /// The set binding for dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="listNameValues">
        /// List of values for dropdown
        /// </param>
        public static void SetBindingForDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, IList<NameValue> listNameValues)
        {

            //List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

            if (listNameValues != null && listNameValues.Count > 0)
            {
                fieldControl.ClearSelection(); // ET Fix - Added line to handle Selection out of range Exception
                fieldControl.DataSource = listNameValues;
                fieldControl.DataTextField = "Name";
                fieldControl.DataValueField = "Value";
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.Text = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }


        public static void SetDropdownValue(RadComboBox fieldControl, FieldAttribute fieldAttribute)
        {

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.Text = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }


        //public static void SetDropdownValue(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        fieldControl.SelectedValue = fieldAttribute.Value;
        //        fieldControl.Text = fieldAttribute.Value;
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);

        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}

        /// <summary>
        /// The set binding for dropdown. Use this for dropdowns with 
        /// values already populated (e.g.caching)
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute)
        {
            var listNameValues = GetDropdownListItems(fieldControl);
            SetBindingForDropdown(fieldControl, fieldAttribute, listNameValues);
        }


        /// <summary>
        /// The set binding for dropdown.
        /// </summary>
        /// <param name="fieldControl">The field control.</param>
        /// <param name="fieldAttribute">The field attribute</param>
        public static void SetBindingForCCHDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute)
        {

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientTextChange = "DropDownSelectedForCCH";
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelectedForCCH";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }

        }
        public static void SetBindingForDropdownAppraisalInfo(RadComboBox fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        {

            List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

            if (Ilist != null)
            {
                bool missingEntry = true;
                foreach (var item in Ilist)
                {
                    if (item.Value == fieldAttribute.Value)
                    {
                        missingEntry = false;
                        break;
                    }
                }
                if (missingEntry == true)
                {
                    NameValue newItem = new NameValue();
                    newItem.Value = fieldAttribute.Value;
                    newItem.Name = fieldAttribute.Value;
                    Ilist.Add(newItem);
                }

                fieldControl.DataSource = Ilist;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }
        /// <summary>
        /// The set binding for dropdown. Use this for dropdowns with 
        /// values already populated (e.g.caching)
        /// </summary>
        /// <param name="fieldControl">The field control.</param>
        /// <param name="fieldAttribute">The field attribute.</param>
        public static void SetBindingForReadOnlyDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute)
        {
            //var IList = GetDropdownListItems(fieldControl);
            //if (IList != null)
            //{
            //    fieldControl.DataSource = IList;
            //    fieldControl.DataBind();
            //}

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
                fieldControl.Enabled = false;
                fieldAttribute.IsProtected = true;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }


        /// <summary>
        /// The set binding for dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="IList">
        /// The i list.
        /// </param>
        public static void SetBindingForDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, List<RouteToState> IList)
        {

            if (IList != null)
            {
                fieldControl.DataSource = IList;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
                    fieldControl.Attributes.Add("JsonFunction",
                                                GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                          fieldAttribute.PageNumber,
                                                                          fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }

        /// <summary>
        /// The set binding for dropdown without events.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        public static void SetBindingForDropdownWithoutEvents(RadComboBox fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        {

            // key = TranslateByBizGroup(key);
            List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);
            if (Ilist != null)
            {
                fieldControl.DataSource = Ilist;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }

        /// <summary>
        /// The set binding for vendor hierarchy dropdown.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        public static void SetBindingForVendorHierarchyDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, string key)
        {

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);

            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }

        }

        public static void SetBindingForUpdateDropdown(RadComboBox fieldControl, FieldAttribute fieldAttribute, IList<NameValue> listNameValues)
        {
            if (listNameValues != null)
            {
                fieldControl.DataSource = listNameValues;
                fieldControl.DataTextField = "Name";
                fieldControl.DataValueField = "Value";
                fieldControl.DataBind();
            }
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
            fieldAttribute.IsProtected = false;
            fieldControl.Enabled = fieldAttribute.IsProtected;
            fieldControl.SelectedValue = fieldAttribute.Value;
            fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            fieldControl.OnClientSelectedIndexChanged = "DropDownSelected";
            fieldControl.Attributes.Add("JsonFunction",
                                            GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                        fieldAttribute.PageNumber,
                                                                       fieldAttribute.Value));

        }

        /// <summary>
        /// //get dropdown values as list of name values
        /// </summary>
        /// <param name="ddlControl"></param>
        public static List<NameValue> GetDropdownListItems(RadComboBox ddlControl)
        {
            var listValues = new List<NameValue>();

            if (ddlControl != null)
            {
                foreach (RadComboBoxItem nameValue in ddlControl.Items)
                {
                    listValues.Add(new NameValue { Name = nameValue.Text, Value = nameValue.Value });
                }
            }
            return listValues;

        }
        /// <summary>
        /// Populate Controls From Cache
        /// </summary>
        /// <param name="FieldControl">RadComboBox</param>
        /// <param name="ApplicationGroup"></param>
        /// <param name="Application"></param>
        /// <param name="Page">Page Name</param>
        public static void PopulateControlsFromCache(RadComboBox FieldControl, string ApplicationGroup, string Application, string Page)
        {
            //Set List of all Controls
            var controlReqList = new List<ControlPopulateRequest>();
            controlReqList.Add(new ControlPopulateRequest(FieldControl, FieldControl.ID));
            CachingUtils.PopulateControlFromCache(controlReqList, ApplicationGroup, Application, Page);

        }

        /// <summary>
        /// The set binding for list box.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="namevalList">
        /// The nameval list.
        /// </param>
        public static void SetBindingForListBox(ListBox fieldControl, FieldAttribute fieldAttribute, List<NameValue> namevalList)
        {
            if (namevalList != null)
            {
                fieldControl.DataSource = namevalList;
                fieldControl.DataBind();
            }

            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.SelectedValue = fieldAttribute.Value;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                fieldControl.TabIndex = 42;
            }
            else
            {
                fieldControl.Enabled = false;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }

        /// <summary>
        /// The set binding for list box.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="namevalList">
        /// The nameval list.
        /// </param>
        public static void SetBindingForListBox(ListBox fieldControl, List<NameValue> namevalList)
        {
            if (namevalList != null)
            {
                fieldControl.DataSource = namevalList;
                fieldControl.DataBind();

            }
        }
        /// <summary>
        /// The set binding readonly multiple text.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>

        public static void SetBindingMutlipleTagForReadOnlyTextbox(TextBox fieldControl, List<FieldAttribute> AttributeList, string charList)
        {
            List<string> acapsBuild = new List<string>();
            string textBuild = string.Empty;
            string pageNumber = string.Empty;
            int startIndex = 0;
            if (AttributeList != null)
            {
                foreach (var fieldAttribute in AttributeList)
                {
                    if (fieldAttribute.AcapsFieldName == null)
                    {
                        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                        fieldControl.ReadOnly = true;
                        continue;
                    }
                    fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
                    if (!string.IsNullOrEmpty(fieldAttribute.Value))
                        textBuild = textBuild + " " + fieldAttribute.Value;
                    fieldControl.MaxLength = fieldAttribute.Length;
                    fieldControl.ReadOnly = true;
                    if (!fieldAttribute.IsProtected)
                    {
                        acapsBuild.Add(fieldAttribute.AcapsFieldName);
                        pageNumber = fieldAttribute.PageNumber;

                    }
                }
                fieldControl.Text = textBuild;
                string acapsName = string.Empty;
                int cnt = acapsBuild.Count;
                int loopcount = 1;
                foreach (var item in acapsBuild)
                {
                    if (loopcount != cnt)
                        acapsName = acapsName + item + ",";
                    else
                        acapsName = acapsName + item;
                    loopcount++;
                }
                var jsonOutput = GetJsonFunctionForMultipleTags(acapsBuild, acapsName, charList, startIndex, pageNumber, textBuild);
                fieldControl.Attributes.Add("onchange", jsonOutput);
                fieldControl.Attributes.Add("onkeyup", jsonOutput);
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
        }
        /// <summary>
        /// The remove empty field.
        /// </summary>
        /// <param name="namevalList">
        /// The nameval list.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<NameValue> RemoveEmptyField(List<NameValue> namevalList)
        {
            List<NameValue> lst = new List<NameValue>();
            foreach (var nameLst in namevalList)
            {
                if (!string.IsNullOrEmpty(nameLst.Value))
                {
                    lst.Add(nameLst);

                }
            }

            return lst;
        }

        /// <summary>
        /// The process history comments.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="requestedLength">
        /// The requested length.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<string> ProcessHistoryComments(string val, int requestedLength)
        {
            List<string> ls = new List<string>();
            string s = string.Empty;

            while (val.Length > 0)
            {
                if (val.Length < requestedLength)
                {
                    ls.Add(val);
                    break;
                }
                else
                {
                    s = val.Substring(0, requestedLength);
                    ls.Add(s);
                    val = val.Substring(s.Length);
                }
            }

            return ls;
        }

        /// <summary>
        /// The process history comments1.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="requestedLength">
        /// The requested length.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<string> ProcessHistoryComments1(string val, int requestedLength)
        {
            int LineCount = 8;
            List<string> ls = new List<string>();
            // holds the index that would be used as the starting index
            int startIndex = 0;
            // Loop through the 8 lines
            if (LineCount > 0 && requestedLength > 0)
            {
                for (int eachLine = 1; eachLine < (LineCount); eachLine++)
                {
                    // Get the length of the string that has to be parsed
                    int minLength = Math.Min(requestedLength, val.Length - startIndex);
                    // If the length <= 0 break out of the loop
                    if (minLength <= 0)
                        break;
                    // Find the newline break.
                    int nextLineBreak = val.IndexOf("\r\n", (startIndex + 1));
                    // If newline break is found
                    if (nextLineBreak > 0)
                    {
                        // Get the length of the string
                        int length = (nextLineBreak - startIndex);
                        // Check if the length of the string is < the requestedLength
                        if (length < requestedLength)
                            minLength = length;
                        val = val.Remove(nextLineBreak, 2);
                    }
                    // Populate the list

                    ls.Add(val.Substring(startIndex, minLength));


                    startIndex += minLength;
                    LineCount++;
                }
            }
            return ls;
        }

        /// <summary>
        /// The set binding for date picker.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="imageBtn">
        /// The image btn.
        /// </param>
        public static void SetBindingForDatePicker(TextBox fieldControl, FieldAttribute fieldAttribute, ImageButton imageBtn)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
            fieldControl.Text = fieldAttribute.Value;
            fieldControl.MaxLength = fieldAttribute.Length;
            fieldControl.ReadOnly = fieldAttribute.IsProtected;
            fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            imageBtn.Enabled = !fieldAttribute.IsProtected;
            if (fieldAttribute.IsProtected)
            {
                imageBtn.ImageUrl = "~/General/Images/1x1.gif";
                imageBtn.Width = Unit.Pixel(16);
                imageBtn.CssClass = "hideCalendar";
                imageBtn.OnClientClick = string.Empty;
                imageBtn.AlternateText = string.Empty;
            }

            fieldControl.Attributes.Add("onchange", GetJsonFunction(fieldAttribute.AcapsFieldName, fieldAttribute.PageNumber, fieldAttribute.Value));
        }

        /// <summary>
        /// The set binding for date picker without events.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        /// <param name="imageBtn">
        /// The image btn.
        /// </param>
        public static void SetBindingForDatePickerWithoutEvents(TextBox fieldControl, FieldAttribute fieldAttribute, ImageButton imageBtn)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
            fieldControl.Text = fieldAttribute.Value;
            fieldControl.MaxLength = fieldAttribute.Length;
            fieldControl.ReadOnly = fieldAttribute.IsProtected;
            fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            imageBtn.Enabled = !fieldAttribute.IsProtected;
            if (fieldAttribute.IsProtected)
            {
                imageBtn.ImageUrl = "~/General/Images/1x1.gif";
                imageBtn.Width = Unit.Pixel(16);
                imageBtn.CssClass = "hideCalendar";
                imageBtn.OnClientClick = string.Empty;
                imageBtn.AlternateText = string.Empty;
            }
        }

        /// <summary>
        /// The set binding for date picker.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForDatePicker(RadDatePicker fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute.Value != "00/00/00")
            {
                fieldControl.SelectedDate = Convert.ToDateTime(fieldAttribute.Value);
            }

            fieldControl.DateInput.CssClass += " " + fieldAttribute.FieldMode.ToString();
            fieldControl.ClientEvents.OnDateSelected = "DateSelected";
            fieldControl.Attributes.Add("JsonFunction", GetJsonFunction(fieldAttribute.AcapsFieldName, fieldAttribute.PageNumber, fieldAttribute.Value));
        }

        /// <summary>
        /// The format acaps message.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="acapsMessage">
        /// The acaps message.
        /// </param>
        /// <returns>
        /// The format acaps message.
        /// </returns>
        public static string FormatAcapsMessage(string sessionId, string applicationId, AcapsMessage acapsMessage)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", sessionId, applicationId, acapsMessage.Id, acapsMessage.Name, acapsMessage.Type, acapsMessage.Code, acapsMessage.Message);
        }

        /// <summary>
        /// The format exception message.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="exMessage">
        /// The ex message.
        /// </param>
        /// <returns>
        /// The format exception message.
        /// </returns>
        public static string FormatExceptionMessage(string sessionId, string applicationId, string exMessage)
        {
            return string.Format("{0},{1},{2}", sessionId, applicationId, exMessage);
        }

        /// <summary>
        /// The set binding for label.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForLabel(Label fieldControl, FieldAttribute fieldAttribute)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
            fieldControl.Text = fieldAttribute.Value;
        }

        /// <summary>
        /// The convert to time format.
        /// </summary>
        /// <param name="strTime">
        /// The str time.
        /// </param>
        /// <returns>
        /// The convert to time format.
        /// </returns>
        public static string convertToTimeFormat(string strTime)
        {
            string timeFormat = string.Empty;
            int timeTextExpLength = 6;
            int timeTextActLength = strTime.Length;
            char padChar = '0';
            if (timeTextActLength < timeTextExpLength)
            {
                strTime = strTime.PadLeft(timeTextExpLength, padChar);
            }

            timeFormat = String.Format("{0}:{1}:{2}", strTime.Substring(0, 2), strTime.Substring(2, 2), strTime.Substring(4));
            return timeFormat;
        }

        /// <summary>
        /// The convert to date format.
        /// </summary>
        /// <param name="dateString">
        /// The date string.
        /// </param>
        /// <returns>
        /// The convert to date format.
        /// </returns>
        public static string convertToDateFormat(string dateString)
        {
            string dateFormat = string.Empty;
            dateFormat = String.Format("{0}/{1}/{2}", dateString.Substring(4, 2), dateString.Substring(6, 2), dateString.Substring(0, 4));
            return dateFormat;
        }

        /// <summary>
        /// The split string at word.
        /// </summary>
        /// <param name="origString">
        /// The orig string.
        /// </param>
        /// <param name="chars">
        /// The chars.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <returns>
        /// The split string at word.
        /// </returns>
        public static string SplitStringAtWord(string origString, int chars, int startIndex)
        {
            // Eliminate the empty string
            if ((origString == null) || (origString.Length <= 0)) return string.Empty;


            // Eliminate invalid indices
            if ((chars <= 0) || (startIndex < 0) || (startIndex >= origString.Length)) return string.Empty;


            // Truncate the string
            string newString = origString.Substring(startIndex, origString.Length - startIndex);


            // Remove the newline chars
            if (newString.Length > 0)
                newString = newString.Replace("\r\n", string.Empty);


            // Check the newString length
            if (newString.Length <= chars) return newString;

            newString = newString.Substring(0, chars).Trim();
            int nextWordBreak = newString.LastIndexOf(' ', newString.Length - 1);

            if (nextWordBreak == -1)
                chars = newString.Length;
            else
                chars = nextWordBreak;

            return newString.Substring(0, chars).Trim();
        }

        /// <summary>
        /// The split string at word.
        /// </summary>
        /// <param name="origString">
        /// The orig string.
        /// </param>
        /// <param name="chars">
        /// The chars.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <returns>
        /// The split string at word.
        /// </returns>

        public static string SplitStringAtWordCounterOffer(string origString, int chars, int startIndex)
        {

            // Eliminate the empty string
            if ((origString == null) || (origString.Length <= 0)) return string.Empty;


            // Eliminate invalid indices
            if ((chars <= 0) || (startIndex < 0) || (startIndex >= origString.Length)) return string.Empty;


            // Truncate the string
            string newString = origString.Substring(startIndex, origString.Length - startIndex);


            // Remove the newline chars
            if (newString.Length > 0)
                newString = newString.Replace("\r\n", string.Empty);


            // //Check the newString length
            //if (newString.Length <= chars) return newString;
            if (newString.Length <= chars)
                chars = newString.Length;

            //newString = newString.Substring(0, chars).Trim();
            //int nextWordBreak = newString.LastIndexOf(' ', newString.Length - 1);

            //if (nextWordBreak == -1)
            //    chars = newString.Length;
            //else
            //    chars = nextWordBreak;

            return newString.Substring(0, chars);
        }


        /// <summary>
        /// The set binding for textbox without events.
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForTextboxWithoutEvents(TextBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                fieldControl.Text = fieldAttribute.Value;
                fieldControl.MaxLength = fieldAttribute.Length;
                fieldControl.ReadOnly = fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            }
            else
            {
                fieldControl.ReadOnly = true;
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

            }
        }




        /// <summary>
        /// The translate by biz group.
        /// </summary>
        /// <param name="lookupName">
        /// The lookup name.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        /// <returns>
        /// The translate by biz group.
        /// </returns>
        public static string TranslateByBizGroup(string lookupName, string currentBizOwner)
        {
            string newLookupName = string.Empty;

            switch (lookupName)
            {
                case "ACTIVITYCD":
                case "ANF_REASONS":
                case "APPRAISALSRC":
                case "ARQ_ACT_CD":
                case "COLLATERAL":
                case "COMP_FACTOR":
                case "CONTACT_METH":
                case "CONTACT_STAT":
                case "CONTACT_TYPE":
                case "CONTACT_WHOM":
                case "CTR_OFFR_LTR":
                case "DECLINE_LTR":
                case "DECLINECODE":
                case "EXTERNAL_CON":
                case "FLOOD_INSURE":
                case "INS_TYPE":
                case "LETTERCODE":
                case "MASKPMT_MODE":
                case "MAX_NFIP":
                case "MIN_APPRAISE":
                case "MIN_INCOME":
                case "MIN_TITLE":
                case "NOTIFACTCODE":
                case "OCCUPANCY":
                case "OTHER-CRA":
                case "OVERRIDE_CD":
                case "PAYOFF_INFO":
                case "POLICYWAIVER":
                case "PRODUCT":
                case "PURPOSE":
                case "RESUB_APPR":
                case "REVERSAL_CD":
                case "STP_ACT_CD":
                case "STPBORRCD":
                case "UWRPIPEWKST":
                case "WHO_PAID_WHO":
                case "WPW_OVERRIDE":
                case "P_CLS_COST":
                case "NP_CLS_COST":
                    newLookupName = currentBizOwner + lookupName;
                    break;
                default:
                    newLookupName = lookupName;
                    break;
            }

            return newLookupName;
        }

        /// <summary>
        /// The get lookup list.
        /// </summary>
        /// <param name="lookupName">
        /// The lookup name.
        /// </param>
        /// <param name="currentBizOwner">
        /// The current Biz Owner.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<NameValue> GetLookupList(string lookupName, string currentBizOwner, string fieldName)
        {
            string newLookupName = TranslateByBizGroup(lookupName, currentBizOwner);
            List<NameValue> list = null;
            try
            {
                list = C2CCacheManager.Instance.GetItem(newLookupName) as List<NameValue>;

                if (list != null)
                {
                    return list;
                }
                else
                {
                    list = C2CCacheManager.Instance.GetItem(lookupName) as List<NameValue>;
                }
            }
            catch (BusinessLayerException ex)
            {
                Logger.Instance.Error(ex.Message + " Control name is :" + fieldName, ex);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.Message + " Control name is :" + fieldName, ex);
            }
            return list;
        }

        /// <summary>
        /// The populateddl pages.
        /// </summary>
        /// <param name="pagesCount">
        /// The pages count.
        /// </param>
        /// <returns>
        /// </returns>
        public static List<NameValue> PopulateddlPages(string pagesCount)
        {
            int PageCount = Convert.ToInt32(pagesCount);
            List<NameValue> namevalList = new List<NameValue>();

            for (int Count = 0; Count < PageCount; Count++)
            {
                string keyvalue = (Count + 1).ToString();
                namevalList.Add(new NameValue() { Value = keyvalue, Name = keyvalue });

            }

            return namevalList;
        }

        /// <summary>
        /// Gets GetHttpAppPath.
        /// </summary>
        public static string GetHttpAppPath
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string url = context.Request.ApplicationPath;
                if (url.EndsWith("/"))
                    return url;
                else
                    return url + "/";
            }
        }

        /// <summary>
        /// The get multi line for update.
        /// </summary>
        /// <param name="CompleteText">
        /// The complete text.
        /// </param>
        /// <param name="requestedLength">
        /// The requested length.
        /// </param>
        /// <returns>
        /// </returns>
        public static string[] GetMultiLineForUpdate(string CompleteText, int[] requestedLength)
        {
            if (string.IsNullOrEmpty(CompleteText) || (requestedLength == null) || (requestedLength.Length <= 0))
                return null;

            if (CompleteText.Length > 0)
                CompleteText = CompleteText.Replace("\r\n", "\n");

            List<string> al = new List<string>();

            int i = -1;

            string[] allNewLines = CompleteText.Split('\n');

            foreach (string newLine in allNewLines)
            {

                if (newLine.Length == 0) al.Add(newLine);

                string tmpNewLine = newLine;
                while (tmpNewLine.Length > 0)
                {
                    i++;
                    if (i >= requestedLength.Length) i = requestedLength.Length - 1;

                    if (tmpNewLine.Length > requestedLength[i])
                    {
                        string splitedNewLine = tmpNewLine.Substring(0, requestedLength[i]);
                        al.Add(splitedNewLine);
                        tmpNewLine = tmpNewLine.Substring(requestedLength[i], tmpNewLine.Length - splitedNewLine.Length);
                    }
                    else
                    {
                        al.Add(tmpNewLine);
                        tmpNewLine = string.Empty;
                        break;
                    }
                }
            }

            return al.ToArray();
        }

        /// <summary>
        /// The get tool tip.
        /// </summary>
        /// <param name="acapsField">
        /// The acaps field.
        /// </param>
        /// <returns>
        /// The get tool tip.
        /// </returns>
        public static string GetToolTip(string acapsField)
        {
            //Dictionary<string, string> diCache = C2CCacheManager.Instance.GetItem(C2CCacheManager.ToolTipKey) as Dictionary<string, string>;

            List<NameValue> diCacheNameValue = C2CCacheManager.Instance.GetItem(C2CCacheManager.ToolTipKey) ;

            var diCache = new Dictionary<string, string>();
            diCacheNameValue.ForEach(d=>diCache.Add(d.Name,d.Value));

            string toolTip = string.Empty;

            if (diCache != null)
            {
                if (diCache.ContainsKey(string.Concat(acapsField, "_PG_1")))
                {
                    toolTip = "Name:" + acapsField + Environment.NewLine + "Help:" + diCache[string.Concat(acapsField, "_PG_1")];
                }
                else if (diCache.ContainsKey(string.Concat(acapsField, "_PG")))
                {
                    toolTip = "Name:" + acapsField + Environment.NewLine + "Help:" + diCache[string.Concat(acapsField, "_PG")];

                }
                else
                {
                    toolTip = acapsField;
                }
            }
            else
            {
                toolTip = acapsField;
            }

            return toolTip;
        }

        /// <summary>
        /// The error log.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="TabName">
        /// The tab name.
        /// </param>
        /// <param name="AppId">
        /// The app id.
        /// </param>
        /// <returns>
        /// The error log.
        /// </returns>
        public static string ErrorLog(Exception ex, string UserId, string TabName, string AppId)
        {
            string errorLog = string.Empty;
            string strNewLine = "\r\n";

            errorLog = "\r\n [Error-Info]\r\n";
            errorLog += "Message:" + ex.Message + strNewLine;
            errorLog += "InnnerException:" + ex.InnerException + strNewLine;
            errorLog += "Source:" + ex.Source + strNewLine;
            errorLog += "StackTrace:" + ex.StackTrace + strNewLine;
            errorLog += "Data:" + ex.Data + strNewLine;

            if (!string.IsNullOrEmpty(UserId)) { errorLog += "UserId:" + UserId + strNewLine; };
            if (!string.IsNullOrEmpty(TabName)) { errorLog += "Tab:" + TabName + strNewLine; };
            if (!string.IsNullOrEmpty(AppId)) { errorLog += "AppId:" + AppId + strNewLine; };

            return errorLog;
        }


        /// <summary>
        /// The remove whitespace from html.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <returns>
        /// The remove whitespace from html.
        /// </returns>
        public static string RemoveWhitespaceFromHtml(string html)
        {
            Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
            Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);

            html = RegexBetweenTags.Replace(html, ">");
            html = RegexLineBreaks.Replace(html, "<");

            return html.Trim();
        }

        // If it any harderror don't delete value from multiline textbox
        public static void CheckErrorMessageType(TextBox fieldControl, string appid, AcapsErrorMessage acapsMessages)
        {
            bool errorflag = false;
            if (acapsMessages != null)
            {
                if (acapsMessages.Type == Lookup.ErrorKey)
                {
                    errorflag = true;
                }
            }
            if (errorflag == false)
            {
                fieldControl.Text = string.Empty;
            }
        }
        public static void SetBindingForFinalCallCheckBox(CheckBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
                if (fieldAttribute.Value == "1")
                {
                    fieldControl.Checked = true;
                }
                else
                {
                    fieldControl.Checked = false;
                }

                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                if (!fieldAttribute.IsProtected)
                {
                    fieldControl.Attributes.Add("onclick", "javascript:CheckBoxChanged(this);");
                    fieldControl.InputAttributes.Add("JsonFunction",
                                                     GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                               fieldAttribute.PageNumber,
                                                                               fieldAttribute.Value));
                }
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.Enabled = false;
            }

        }

        //Title vesting

        public static string[] GetMultiLineForUpdateTextbox(string CompleteText, int[] requestedLength, int charlength)
        {
            if (string.IsNullOrEmpty(CompleteText) || (requestedLength == null) || (requestedLength.Length <= 0))
                return null;
            string Completetextlength = string.Empty;
            if (CompleteText.Length > 0)
            {
                CompleteText = CompleteText.Replace("\r\n", "\n");
                //CompleteText = CompleteText.Trim().Replace("\n", " ");
            }

            if (CompleteText.Length < charlength)
            {
                List<string> al = new List<string>();

                int i = -1;

                string[] allNewLines = CompleteText.Split('\n');

                foreach (string newLine in allNewLines)
                {

                    if (newLine.Length == 0) al.Add(newLine);

                    string tmpNewLine = newLine;
                    while (tmpNewLine.Length > 0)
                    {
                        i++;
                        if (i >= requestedLength.Length) i = requestedLength.Length - 1;

                        if (tmpNewLine.Length > requestedLength[i])
                        {
                            string splitedNewLine = tmpNewLine.Substring(0, requestedLength[i]);
                            al.Add(splitedNewLine);
                            tmpNewLine = tmpNewLine.Substring(requestedLength[i], tmpNewLine.Length - splitedNewLine.Length);
                        }
                        else
                        {
                            al.Add(tmpNewLine);
                            tmpNewLine = string.Empty;
                            break;
                        }
                    }
                }

                return al.ToArray();
            }
            else
            {
                return null;
            }
        }

        //Newly added methods for Bank Loan Booking

        /// <summary>
        /// //Added Newly for making updatable on clickign edit button in FInancial Tab - Jun-4th
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingForUpdatingTextbox(TextBox fieldControl, FieldAttribute fieldAttribute)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
            fieldControl.Text = fieldAttribute.Value.Trim();
            fieldControl.MaxLength = fieldAttribute.Length;
            fieldAttribute.IsProtected = false;
            fieldControl.ReadOnly = fieldAttribute.IsProtected;
            fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            fieldControl.Attributes.Add("onchange",
                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                  fieldAttribute.PageNumber,
                                                                  fieldAttribute.Value));
            fieldControl.Attributes.Add("onkeyup",
                                       GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                 fieldAttribute.PageNumber,
                                                                 fieldAttribute.Value));
        }

        /// <summary>
        /// //Added Newly for making Readonly fields in Validation Tab - Jun-6th
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingForReadOnlyTextbox(TextBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
                fieldControl.Text = fieldAttribute.Value.Trim();
                fieldControl.MaxLength = fieldAttribute.Length;
                fieldAttribute.IsProtected = true;
                fieldControl.ReadOnly = true;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);
                fieldControl.ReadOnly = true;
            }
            // fieldControl.Attributes.Add("title", GetToolTip(fieldAttribute.AcapsFieldName));

        }




        public static void SetBindingForSalesPlanReadOnlyTextbox(TextBox fieldControl, FieldAttribute fieldAttribute)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
            fieldControl.Text = fieldAttribute.Value.Trim();
            fieldControl.MaxLength = fieldAttribute.Length;
            //fieldControl.Enabled = false;
            fieldAttribute.IsProtected = true;
            fieldControl.ReadOnly = true;

            fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        }

        /// <summary>
        /// SetBindingForSalesPlanReadOnlyTextbox
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingForSalesPlanReadOnlyTextbox(TextBox fieldControl, FieldAttribute fieldAttribute, RadComboBox ddlCtl)
        {
            var listNameValue = GetDropdownListItems(ddlCtl);
            var text = listNameValue.Where(n => n.Value == fieldAttribute.Value).FirstOrDefault<NameValue>().Name;
            var fAttr = new FieldAttribute {
                Value = text,
                AcapsFieldName = fieldAttribute.AcapsFieldName
            };
            SetBindingForSalesPlanReadOnlyTextbox(fieldControl, fAttr);
        }

        /// <summary>
        /// Added Newly for making Readonly fields while page is loading and needs to upatabale once application pickup button is selected.
        /// This  method is for specific reqirement in Validation Tab , BLB - July-25th
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetBindingForAppPickUpSpecificTextbox(TextBox fieldControl, FieldAttribute fieldAttribute)
        {

            SetBindingForReadOnlyTextbox(fieldControl, fieldAttribute);
            fieldControl.Attributes.Add("onchange",
                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                  fieldAttribute.PageNumber,
                                                                  fieldAttribute.Value));
            fieldControl.Attributes.Add("onkeyup",
                                       GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                 fieldAttribute.PageNumber,
                                                                 fieldAttribute.Value));
        }

        /// <summary>
        /// //Added Newly for making Readonly fields in Validation Tab - Jun-6th
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForReadOnlyCheckBox(CheckBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
                if (fieldAttribute.Value == "Y" || fieldAttribute.Value == "Yes")
                {
                    fieldControl.Checked = true;
                }
                else
                {
                    fieldControl.Checked = false;
                }

                //fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldAttribute.IsProtected = true;
                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                fieldControl.Attributes.Add("onclick", "javascript:CheckBoxChanged(this);");
                fieldControl.InputAttributes.Add("JsonFunction",
                                                 GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                           fieldAttribute.PageNumber,
                                                                           fieldAttribute.Value));

            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
                fieldControl.Enabled = false;
            }

        }

        /// <summary>
        /// //Added Newly for making Readonly fields in Validation Tab - Jun-6th
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetReadOnlyCheckBox(CheckBox fieldControl)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
            fieldControl.Checked = false;
            fieldControl.Enabled = false;
        }

        /// <summary>
        /// //Added Newly for making Updatable fields in Validation Tab - Jun-6th
        /// </summary>
        /// <param name="fieldControl">
        /// The field control.
        /// </param>
        /// <param name="fieldAttribute">
        /// The field attribute.
        /// </param>
        public static void SetBindingForUpdateCheckBox(CheckBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
                if (fieldAttribute.Value == "Y" || fieldAttribute.Value == "Yes")
                {
                    fieldControl.Checked = true;
                }
                else
                {
                    fieldControl.Checked = false;
                }

                fieldAttribute.IsProtected = true;
                fieldControl.Enabled = fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
                fieldControl.Attributes.Add("onclick", "javascript:CheckBoxChanged(this);");
                fieldControl.InputAttributes.Add("JsonFunction",
                                                     GetJsonFunction(fieldAttribute.AcapsFieldName,
                                                                               fieldAttribute.PageNumber,
                                                                               fieldAttribute.Value));

            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
                fieldControl.Enabled = false;
            }

        }

        public static void SetBindingForApprovalCheckBox(CheckBox fieldControl, FieldAttribute fieldAttribute)
        {
            if (fieldAttribute != null)
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
                if (fieldAttribute.Value == "Y" || fieldAttribute.Value == "Yes")
                {
                    fieldControl.Checked = true;
                }
                else
                {
                    fieldControl.Checked = false;
                }

                fieldControl.Enabled = !fieldAttribute.IsProtected;
                fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);

            }
            else
            {
                fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
                fieldControl.Enabled = false;
            }

        }


        public static string getClosingMethod(string closingCode)
        {
            string closingMethod = "Bank Store";

            if (closingCode.ToUpper() == "B")
                closingMethod = "Bank Store";
            else if (closingCode.ToUpper() == "M")
                closingMethod = "Mail";

            return closingMethod;
        }

        public static string getVehicleAge(string vehicleYear)
        {
            string vehicleAge = "1";

            if (vehicleYear != string.Empty)
            {
                if (vehicleYear == "0000" || int.Parse(vehicleYear) > DateTime.Now.Year)
                    vehicleAge = "0";
                else
                    vehicleAge = (DateTime.Now.Year - int.Parse(vehicleYear)).ToString();
            }
            return vehicleAge;
        }


        public static int GetLTVValue(string Purpose)
        {
            int intLTV = 100;

            switch (Purpose)
            {
                case "AURF":
                case "AUFC":
                case "AUPP":
                case "AULP":
                case "AUNP":
                case "AUUP":
                case "AULB":
                    intLTV = 85;
                    break;
                case "AUCO":
                    intLTV = 100;
                    break;
                default:
                    intLTV = 100;
                    break;
            }
            return intLTV;

        }
        /// 
        public static string GetPurposeForSS(string purpose)
        {
            string scenarioPurpose = string.Empty;

            switch (purpose)
            {
                case "AUUP":
                case "AULB":
                    scenarioPurpose = "Used Purchase";
                    break;
                case "AURF":
                    scenarioPurpose = "Refi";
                    break;
                case "AUNP":
                    scenarioPurpose = "New Purchase";
                    break;
                case "AUCO":
                    scenarioPurpose = "Refi Cashout";
                    break;
                case "AUFC":
                    scenarioPurpose = "Free and Clear";
                    break;
                case "AUPP":
                    scenarioPurpose = "Private Party";
                    break;
            }

            return scenarioPurpose;
        }

        public static string GetPurpose(string purpose)
        {
            string scenarioPurpose = string.Empty;

            switch (purpose)
            {
                case "AUUP":
                    scenarioPurpose = "Dealer Purchase – Used";
                    break;
                case "AULB":
                    scenarioPurpose = "Lease Buyout";
                    break;
                case "AURF":
                    scenarioPurpose = "Refinance Only";
                    break;
                case "AUNP":
                    scenarioPurpose = "Dealer Purchase – New";
                    break;
                case "AUCO":
                    scenarioPurpose = "Non-Purchase (cashout)";
                    break;
                case "AUFC":
                    scenarioPurpose = "Non-Purchase CT";
                    break;
                case "AUPP":
                    scenarioPurpose = "CS – AP Private Party";
                    break;
            }

            return scenarioPurpose;
        }

        /// <summary>
        /// returns if user is central sales(SLM,SLS)
        /// </summary>
        /// <returns></returns>
        public static bool IsCentralSalesUser()
        {
            var userInfo = SessionData.UserProfile as User;
            if (userInfo.GroupId == Constants.CASUserGroups.SLM.ToString() ||
                    userInfo.GroupId == Constants.CASUserGroups.SLS.ToString()
                )
                return true;
            else
                return false;

        }
        /// <summary>
        /// returns if user is fulfillment(FFM,FFL)
        /// </summary>
        /// <returns></returns>
        public static bool IsFulfillmentUser()
        {
            var userInfo = SessionData.UserProfile as User;
            if (userInfo.GroupId == Constants.CASUserGroups.FFM.ToString() ||
                    userInfo.GroupId == Constants.CASUserGroups.FFL.ToString()
                )
                return true;
            else
                return false;
        }
        /// <summary>
        /// returns if user is underwriter(UNM,UNW)
        /// </summary>
        /// <returns></returns>
        public static bool IsUnderWriterUser()
        {
            var userInfo = SessionData.UserProfile as User;
            if (userInfo.GroupId == Constants.CASUserGroups.UNM.ToString() ||
                    userInfo.GroupId == Constants.CASUserGroups.UNW.ToString()
                )
                return true;
            else
                return false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsBLBBLMUser()
        {
            var userInfo = SessionData.UserProfile as User;
            if (userInfo.GroupId == Constants.CASUserGroups.BLB.ToString() ||
                    userInfo.GroupId == Constants.CASUserGroups.BLM.ToString()
                )
                return true;
            else
                return false;

        }
        /// <summary>
        /// //Added Newly for making TextBox as Readonly field
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetReadOnlyTextbox(TextBox fieldControl)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
            fieldControl.ReadOnly = true;

        }

        /// <summary>
        /// //Added Newly for making TextBox as Readonly field
        /// </summary>
        /// <param name="fieldControl"></param>
        /// <param name="fieldAttribute"></param>
        public static void SetUpdateModeTextbox(TextBox fieldControl)
        {
            fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
            fieldControl.ReadOnly = false;
            fieldControl.Enabled = true;
        }


        /// <summary>
        /// Return List having value Yes or No.
        /// </summary>
        /// <returns></returns>
        public static List<NameValue> YesNoList()
        {
            return new List<NameValue> { 
                        new NameValue { Name = "Yes", Value = "Y" } ,
                        new NameValue { Name = "No", Value = "N" } 
                      };
        }


        /// <summary>
        /// Return List having value Yes or No Or Select.
        /// </summary>
        /// <returns></returns>
        public static List<NameValue> YesNoSelectList()
        {
            return new List<NameValue> { 
                        new NameValue { Name = "", Value = "" } ,
                        new NameValue { Name = "Yes", Value = "Y" } ,
                        new NameValue { Name = "No", Value = "N" } 
                      };
        }
        public static FieldAttribute EmptyFieldAttribute
        {
            get
            {
                return new FieldAttribute() { Value = "" };
            }
        }
        /// <summary>
        /// Take all the words in the input string and separate them.
        /// </summary>
        public static string[] SplitWords(string s)
        {
            //
            // Split on all non-word characters.
            // ... Returns an array of all the words.
            //
            return Regex.Split(s, @"\W+");
            // @      special verbatim string syntax
            // \W+    one or more non-word characters together
        }

        public static void ResetJson(Page page)
        {
            var s = new StringBuilder();
            s.Append("jsonObj = { \"AcapsFields\": [] };");
            s.Append("jsonMap = {};");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "ResetScript", s.ToString(), true);

        }

        public static void DisableChildControls(Control tabControl)
        {
            foreach (Control cntl in tabControl.Controls)
            {
                if (cntl != null)
                    Utilities.DisableTabControls(cntl);
            }
        }

        public static void DisableTabControls(Control cntl)
        {
            if (cntl is TextBox)
            {
                ((TextBox)cntl).ReadOnly = true;
                ((TextBox)cntl).CssClass = Utilities.FieldModeStyleClass(((TextBox)cntl).CssClass, FieldAttribute.FieldModes.Protected_Normal);
            }
            else if (cntl is RadComboBox)
            {
                ((RadComboBox)cntl).Enabled = false;
                ((RadComboBox)cntl).CssClass = Utilities.FieldModeStyleClass(((RadComboBox)cntl).CssClass, FieldAttribute.FieldModes.Protected_Normal);
            }
            else if (cntl is CheckBox)
            {
                ((CheckBox)cntl).Enabled = false;
                ((CheckBox)cntl).CssClass = Utilities.FieldModeStyleClass(((CheckBox)cntl).CssClass, FieldAttribute.FieldModes.Protected_Normal);
            }
            else if (cntl is Button)
            {
                ((Button)cntl).Enabled = false;
            }
            else if (cntl is ListBox)
            {
                ((ListBox)cntl).Enabled = false;
            }
            else if (cntl is RadioButton)
            {
                ((RadioButton)cntl).Enabled = false;
            }
            else if (cntl is RadioButtonList)
            {
                ((RadioButtonList)cntl).Enabled = false;
            }
            else if (cntl is ImageButton)
            {
                ((ImageButton)cntl).Enabled = false;
            }
            else if (cntl is DataList)
            {
                ((DataList)cntl).Enabled = false;
            }
            else if (cntl is RadDatePicker)
            {
                ((RadDatePicker)cntl).Enabled = false;
            }
            else if (cntl is RadTimePicker)
            {
                ((RadTimePicker)cntl).Enabled = false;
            }
            else if (cntl is HtmlAnchor)
            {
                ((HtmlAnchor)cntl).Disabled = true;
            }
            else if (cntl is RadGrid)
            {
                RadGrid radDebGrid = cntl as RadGrid;
                foreach (GridDataItem item in radDebGrid.MasterTableView.Items)
                {
                    foreach (Control cntItem in item.Controls)
                    {
                        foreach (Control cnt in cntItem.Controls)
                        {
                            if (cnt is TextBox)
                            {
                                ((TextBox)cnt).ReadOnly = true;
                                ((TextBox)cnt).CssClass = Utilities.FieldModeStyleClass(((TextBox)cnt).CssClass, FieldAttribute.FieldModes.Protected_Normal);
                            }
                            else if (cnt is RadComboBox)
                            {
                                ((RadComboBox)cnt).Enabled = false;
                                ((RadComboBox)cnt).CssClass = Utilities.FieldModeStyleClass(((RadComboBox)cnt).CssClass, FieldAttribute.FieldModes.Protected_Normal);
                            }
                            else if (cnt is CheckBox)
                            {
                                ((CheckBox)cnt).Enabled = false;
                                ((CheckBox)cnt).CssClass = Utilities.FieldModeStyleClass(((CheckBox)cnt).CssClass, FieldAttribute.FieldModes.Protected_Normal);
                            }
                            else if (cnt is RadioButton)
                            {
                                ((RadioButton)cnt).Enabled = false;
                            }
                            else if (cnt is RadioButtonList)
                            {
                                ((RadioButtonList)cnt).Enabled = false;
                            }
                            else if (cnt is Button)
                            {
                                ((Button)cnt).Enabled = false;
                            }
                            else if (cntl is RadDatePicker)
                            {
                                ((RadDatePicker)cntl).Enabled = false;
                            }
                            else if (cntl is RadTimePicker)
                            {
                                ((RadTimePicker)cntl).Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        ////// Direct Auto : For Asp DropDown support functions


        ///// <summary>
        ///// The set binding for dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="key">
        ///// The key.
        ///// </param>
        ///// <param name="currentBizOwner">
        ///// The current Biz Owner.
        ///// </param>
        //public static void SetBindingForDropdown(DropDownList fieldControl, string key, string currentBizOwner)
        //{
        //    List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

        //    if (Ilist != null)
        //    {
        //        fieldControl.DataSource = Ilist;
        //        fieldControl.DataBind();
        //    }


        //}

        ///// <summary>
        ///// The set binding for  dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="key">
        ///// The key.
        ///// </param>
        ///// <param name="currentBizOwner">
        ///// The current Biz Owner.
        ///// </param>
        //public static void SetBindingForCCHDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        //{
        //    List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

        //    if (Ilist != null)
        //    {
        //        fieldControl.DataSource = Ilist;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;

        //        SetDropDownProperty(fieldControl, fieldAttribute);

        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {

        //            fieldControl.Attributes.Add("onchange", "DropDownSelectedForCCH(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }

        //}


        ///// <summary>
        ///// The set binding for cch dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="IList">
        ///// The i list.
        ///// </param>
        //public static void SetBindingForCCHDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, List<RouteToState> IList)
        //{

        //    if (IList != null)
        //    {
        //        fieldControl.DataSource = IList;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "DropDownSelectedForCCH(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}


        ///// <summary>
        ///// The set binding for dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="key">
        ///// The key.
        ///// </param>
        ///// <param name="currentBizOwner">
        ///// The current Biz Owner.
        ///// </param>
        //public static void SetBindingForDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        //{

        //    List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

        //    if (Ilist != null)
        //    {
        //        fieldControl.DataSource = Ilist;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "DropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}

        ///// <summary>
        ///// The set binding for dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="listNameValues">
        ///// List of values for dropdown
        ///// </param>
        //public static void SetBindingForDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, IList<NameValue> listNameValues)
        //{

        //    //List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

        //    if (listNameValues != null && listNameValues.Count > 0)
        //    {
        //        fieldControl.ClearSelection(); // ET Fix - Added line to handle Selection out of range Exception
        //        fieldControl.DataSource = listNameValues;
        //        fieldControl.DataTextField = "Name";
        //        fieldControl.DataValueField = "Value";
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;

        //        SetDropDownProperty(fieldControl, fieldAttribute);

        //        //fieldControl.Text = fieldAttribute.Value;
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}

        //public static void SetDropDownProperty(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{
        //    if (fieldControl.Items.FindByValue(fieldAttribute.Value) != null)
        //    {
        //        fieldControl.SelectedIndex = -1;
        //        fieldControl.Items.FindByValue(fieldAttribute.Value).Selected = true;
        //    }
        //    else if (fieldControl.Items.FindByText(fieldAttribute.Value)!=null)
        //    {
        //        fieldControl.SelectedIndex = -1;
        //        fieldControl.Items.FindByText(fieldAttribute.Value).Selected = true;
        //    }
        //}


        //public static void SetDropdownValue(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        //fieldControl.Text = fieldAttribute.Value;
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}


        ////public static void SetDropdownValue(DropDownList fieldControl, FieldAttribute fieldAttribute)
        ////{

        ////    if (fieldAttribute != null)
        ////    {
        ////        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        ////        fieldControl.Enabled = !fieldAttribute.IsProtected;
        ////        SetDropDownProperty(fieldControl, fieldAttribute);
        ////        fieldControl.Text = fieldAttribute.Value;
        ////        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);

        ////    }
        ////    else
        ////    {
        ////        fieldControl.Enabled = false;
        ////        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        ////    }
        ////}

        ///// <summary>
        ///// The set binding for dropdown. Use this for dropdowns with 
        ///// values already populated (e.g.caching)
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        //public static void SetBindingForDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{
        //    var listNameValues = GetDropdownListItems(fieldControl);
        //    SetBindingForDropdown(fieldControl, fieldAttribute, listNameValues);
        //}


        ///// <summary>
        ///// The set binding for dropdown.
        ///// </summary>
        ///// <param name="fieldControl">The field control.</param>
        ///// <param name="fieldAttribute">The field attribute</param>
        //public static void SetBindingForCCHDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }

        //}
        //public static void SetBindingForDropdownAppraisalInfo(DropDownList fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        //{

        //    List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);

        //    if (Ilist != null)
        //    {
        //        bool missingEntry = true;
        //        foreach (var item in Ilist)
        //        {
        //            if (item.Value == fieldAttribute.Value)
        //            {
        //                missingEntry = false;
        //                break;
        //            }
        //        }
        //        if (missingEntry == true)
        //        {
        //            NameValue newItem = new NameValue();
        //            newItem.Value = fieldAttribute.Value;
        //            newItem.Name = fieldAttribute.Value;
        //            Ilist.Add(newItem);
        //        }

        //        fieldControl.DataSource = Ilist;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}
        ///// <summary>
        ///// The set binding for dropdown. Use this for dropdowns with 
        ///// values already populated (e.g.caching)
        ///// </summary>
        ///// <param name="fieldControl">The field control.</param>
        ///// <param name="fieldAttribute">The field attribute.</param>
        //public static void SetBindingForReadOnlyDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute)
        //{
        //    //var IList = GetDropdownListItems(fieldControl);
        //    //if (IList != null)
        //    //{
        //    //    fieldControl.DataSource = IList;
        //    //    fieldControl.DataBind();
        //    //}

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_Normal);
        //        fieldControl.Enabled = false;
        //        fieldAttribute.IsProtected = true;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}


        ///// <summary>
        ///// The set binding for dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="IList">
        ///// The i list.
        ///// </param>
        //public static void SetBindingForDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, List<RouteToState> IList)
        //{

        //    if (IList != null)
        //    {
        //        fieldControl.DataSource = IList;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //        if (!fieldAttribute.IsProtected)
        //        {
        //            fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //            fieldControl.Attributes.Add("JsonFunction",
        //                                        GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                  fieldAttribute.PageNumber,
        //                                                                  fieldAttribute.Value));
        //        }
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}

        ///// <summary>
        ///// The set binding for dropdown without events.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="key">
        ///// The key.
        ///// </param>
        ///// <param name="currentBizOwner">
        ///// The current Biz Owner.
        ///// </param>
        //public static void SetBindingForDropdownWithoutEvents(DropDownList fieldControl, FieldAttribute fieldAttribute, string key, string currentBizOwner)
        //{

        //    // key = TranslateByBizGroup(key);
        //    List<NameValue> Ilist = GetLookupList(key, currentBizOwner, fieldControl.ID);
        //    if (Ilist != null)
        //    {
        //        fieldControl.DataSource = Ilist;
        //        fieldControl.DataBind();
        //    }

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        SetDropDownProperty(fieldControl, fieldAttribute);
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }
        //}

        ///// <summary>
        ///// The set binding for vendor hierarchy dropdown.
        ///// </summary>
        ///// <param name="fieldControl">
        ///// The field control.
        ///// </param>
        ///// <param name="fieldAttribute">
        ///// The field attribute.
        ///// </param>
        ///// <param name="key">
        ///// The key.
        ///// </param>
        //public static void SetBindingForVendorHierarchyDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, string key)
        //{

        //    if (fieldAttribute != null)
        //    {
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, fieldAttribute.FieldMode);
        //        fieldControl.Enabled = !fieldAttribute.IsProtected;
        //        fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);

        //    }
        //    else
        //    {
        //        fieldControl.Enabled = false;
        //        fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Protected_NULL);

        //    }

        //}

        //public static void SetBindingForUpdateDropdown(DropDownList fieldControl, FieldAttribute fieldAttribute, IList<NameValue> listNameValues)
        //{
        //    if (listNameValues != null)
        //    {
        //        fieldControl.DataSource = listNameValues;
        //        fieldControl.DataTextField = "Name";
        //        fieldControl.DataValueField = "Value";
        //        fieldControl.DataBind();
        //    }
        //    fieldControl.CssClass = FieldModeStyleClass(fieldControl.CssClass, FieldAttribute.FieldModes.Unprotected_Normal);
        //    fieldAttribute.IsProtected = false;
        //    fieldControl.Enabled = fieldAttribute.IsProtected;
        //    SetDropDownProperty(fieldControl, fieldAttribute);
        //    fieldControl.ToolTip = GetToolTip(fieldAttribute.AcapsFieldName);
        //    fieldControl.Attributes.Add("onchange", "ASPDropDownSelected(this)");
        //    fieldControl.Attributes.Add("JsonFunction",
        //                                    GetJsonFunction(fieldAttribute.AcapsFieldName,
        //                                                                fieldAttribute.PageNumber,
        //                                                               fieldAttribute.Value));

        //}

        ///// <summary>
        ///// //get dropdown values as list of name values
        ///// </summary>
        ///// <param name="ddlControl"></param>
        //public static List<NameValue> GetDropdownListItems(DropDownList ddlControl)
        //{
        //    var listValues = new List<NameValue>();

        //    if (ddlControl != null)
        //    {
        //        foreach (ListItem nameValue in ddlControl.Items)
        //        {
        //            listValues.Add(new NameValue { Name = nameValue.Text, Value = nameValue.Value });
        //        }
        //    }
        //    return listValues;

        //}
        ///// <summary>
        ///// Populate Controls From Cache
        ///// </summary>
        ///// <param name="FieldControl">DropDownList</param>
        ///// <param name="ApplicationGroup"></param>
        ///// <param name="Application"></param>
        ///// <param name="Page">Page Name</param>
        //public static void PopulateControlsFromCache(DropDownList FieldControl, string ApplicationGroup, string Application, string Page)
        //{
        //    //Set List of all Controls
        //    var controlReqList = new List<ControlPopulateRequest>();
        //    controlReqList.Add(new ControlPopulateRequest(FieldControl, FieldControl.ID));
        //    CachingUtils.PopulateControlFromCache(controlReqList, ApplicationGroup, Application, Page);

        //}

        //public static void MergeDropDownListItems(DropDownList source, DropDownList Target)
        //{
        //    foreach (ListItem lstItem in Target.Items)
        //    {
        //        source.Items.Add(lstItem);
        //    }
        //}
    }
}
