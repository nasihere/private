using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WF.EAI.BLL.CUSP;
using WF.EAI.BLL.CUSP.Helpers;
using WF.EAI.Entities.constants;
using WF.EAI.Entities.domain.c2c.Common;
using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Entities.domain.cusp.UI;
using WF.EAI.Utils;
//using WF.EAI.Utils.logging;
using WF.EAI.Utils.ui;
using WF.EAI.Web.Controls.uiUtils.Invoker;
using WF.UAP.UASF.CrossCutting.Logging;

namespace WF.EAI.Web.Controls.uiUtils.uuw
{
    public static class AcapsHelper
    {
        //public static void SaveActivityCode(string appId, string actCode)
        //{
        //    var fieldList = new List<FieldAttribute>();
        //    var field = new FieldAttribute { AcapsFieldName = "SUM_CCH_ACTIVITY_CODE", PageNumber = "001", Value = actCode };
        //    fieldList.Add(field);

        //    //var jsonText = JsonMapper<string>.ObjectToJsonText(fieldList);

        //    SaveActivityCodeToAcaps(appId, fieldList);
        //}

        /// <summary>
        /// Save Acaps info
        /// </summary>
        public static void SaveDisclosure(string appId, DisclosureEntity.DisclosureSelection disclosureSelection, bool isMain)
        {

            var disclHelper = new DisclosureHelper(SessionData.UserProfile.LoginUseridValue, disclosureSelection);

            var jsonText = isMain ? disclHelper.GetSaveJsonForMainDisclosure() :disclHelper.GetSaveJsonForCallMonAuth();
                
            SaveToAcaps(appId,jsonText);
        }

        public static void SaveToAcaps(string appId,string jsonText)
        {
            var saveFields = GetFieldsToSave(jsonText);
            if (saveFields.Count > 0)
            {
                var appHeader = new CUSPAppDataHeader();
                var baseDataHeader = SessionData.GetBaseCUSPAppDataHeader(appId);
                SessionData.GetBaseAppDataHeader(baseDataHeader, appHeader);
                try
                {
                    appHeader.ApplicationData = jsonText;
                    if (appHeader.AcapsFunction == "INQ")
                    {
                        return;
                    }

                    appHeader = ServiceInvoker.MakeCAWebRequest(
                        appHeader, ServiceInvoker.AcapsServiceOperation.ProcessUpdate);
                    var appEntity = new CUSPApplicationData();
                    var obj = JsonConvert.DeserializeObject(appHeader.ApplicationData);
                    appEntity.AcapFields =
                        JsonMapper<Dictionary<string, FieldAttribute>>
                            .JsonTextToAppDataHeader<Dictionary<string, FieldAttribute>>(Convert.ToString(obj));
                    appEntity.AcapsMessage =
                        JsonConvert.DeserializeObject<List<AcapsErrorMessage>>(appHeader.AcapsErrorMessages);
    
                    if (appEntity.AcapsMessage != null)
                    {
                        //AppSessionData.AcapsMessage = appEntity.AcapsMessage[0];
                    }
                }
                catch (Exception ex)
                {
                    var errorMsg = WF.EAI.Web.Controls.uiUtils.uuw.DAUtilities.ErrorLog(
                        ex, SessionData.GetUserId(), appHeader.CurrentTabSource, Convert.ToString(appHeader.ApplicationId));
                    Logger.Instance.Error(ex.Message, ex);
                    Logger.Instance.Error(errorMsg, ex);
                }
            }
        }

        /// <summary>
        /// SaveFields
        /// </summary>
        /// <param name="jsonText">
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<FieldAttribute> GetFieldsToSave(string jsonText)
        {
            var fieldList = new List<FieldAttribute>();
            if (!string.IsNullOrEmpty(jsonText) && jsonText.Length > 0)
            {
                try
                {
                    var jsonObj = JObject.Parse(jsonText);
                    fieldList = jsonObj.SelectToken("AcapsFields").Select(s => Helper.GetFieldAttribute(s)).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(ex.Message, ex);
                    Logger.Instance.Error("jsonText value is : " + jsonText);
                    throw;
                }
            }

            return fieldList;
        }

        //public static void SaveActivityCodeToAcaps(string appId, List<FieldAttribute> saveFields)
        //{
        //    if (saveFields.Count > 0)
        //    {
        //        var jsonText = JsonMapper<string>.ObjectToJsonText(saveFields);

        //        var appHeader = new CUSPAppDataHeader();
        //        var baseDataHeader = SessionData.GetBaseCUSPAppDataHeader(appId);
        //        SessionData.GetBaseAppDataHeader(baseDataHeader, appHeader);
        //        try
        //        {
        //            appHeader.ApplicationData = jsonText;
        //            if (appHeader.AcapsFunction == "INQ")
        //            {
        //                return;
        //            }

        //            appHeader = ServiceInvoker.MakeCAWebRequest(
        //                appHeader, ServiceInvoker.AcapsServiceOperation.ProcessUpdateUIFields);
        //            var appEntity = new CUSPApplicationData();
        //            var obj = JsonConvert.DeserializeObject(appHeader.ApplicationData);
        //            appEntity.AcapFields =
        //                JsonMapper<Dictionary<string, FieldAttribute>>
        //                    .JsonTextToAppDataHeader<Dictionary<string, FieldAttribute>>(Convert.ToString(obj));
        //            appEntity.AcapsMessage =
        //                JsonConvert.DeserializeObject<List<AcapsErrorMessage>>(appHeader.AcapsErrorMessages);

        //            if (appEntity.AcapsMessage != null)
        //            {
        //                //AppSessionData.AcapsMessage = appEntity.AcapsMessage[0];
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorMsg = WF.EAI.Web.Controls.uiUtils.uuw.DAUtilities.ErrorLog(
        //                ex, SessionData.GetUserId(), appHeader.CurrentTabSource, Convert.ToString(appHeader.ApplicationId));
        //            Logger.Instance.Error(ex.Message, ex);
        //            Logger.Instance.Error(errorMsg, ex);
        //        }
        //    }
        //}
    }
}