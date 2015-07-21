using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using WF.EAI.BLL.CUSP;
using WF.EAI.BLL.uca;
using WF.EAI.BLL.uca.Filters;
using WF.EAI.Data.sif.Services.ECBS.GetCustomerInformation200909;
using WF.EAI.Entities.constants;
using WF.EAI.Entities.domain.c2c.Common;
using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Entities.domain.uca.Core;
using WF.EAI.Entities.domain.uca.Search;
using WF.EAI.Utils;
using WF.UAP.UA.UCRA.BLL.CUSP.Interface;
using WF.UAP.UASF.CrossCutting.Logging;
using WF.UAP.UASF.CrossCutting.Logging.LoggingListener.config;
using WF.UAP.UASF.Utils.IO.Serializers;
using CommonBo = WF.EAI.BLL.uca.CommonBo;
using Lookup = WF.EAI.Entities.constants.uca.Lookup;
using ucaCommon = WF.EAI.Entities.domain.uca.Common;

namespace WF.UAP.UA.UCRA.BLL.CUSP
{
    public class UCRASearchBo : IUCRASearchBo
    {
        /// <summary>
        /// Searches CAS Applications by Name
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public string SearchCASApplicationByName(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = WF.EAI.BLL.uca.CommonBo.SearchCASApplicationByName(appHeader);
            return jsonText;
        }

        /// <summary>
        /// Searches SearchCASApplicationByPhone
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public string SearchCASApplicationByPhone(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = WF.EAI.BLL.uca.CommonBo.SearchCASApplicationByPhone(appHeader);
            return jsonText;
        }

        public string SearchCASApplicationByFDRAccountNumber(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = WF.EAI.BLL.uca.CommonBo.SearchCASApplicationByFDRNumber(appHeader);
            return jsonText;
        }

        /// <summary>
        /// The search cas application by fdr account number.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public  string SearchCASApplicationByFDRNumber(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = string.Empty;

            var inputParams = new offerInputParams();
            inputParams.tranId = appHeader.SessionId;
            inputParams.offerId = appHeader.OfferId;
            inputParams.customerContactIndicator = true;
            inputParams.Channel = appHeader.Channel;

            SearchApplicationResultEntityCollection searchResult = new SearchApplicationResultEntityCollection();
            SearchApplicationFDRNumberEntity searchCriteriaEntity =
                 JsonMapper<SearchApplicationFDRNumberEntity>
                     .JsonTextToTabData<SearchApplicationFDRNumberEntity>(appHeader.TabDataText);

            try
            {
                try
                {
                    var searchBO = new SearchPartyBo();

                    searchResult = searchBO.SearchByFDRNumber(searchCriteriaEntity.FDRAccountNumber, searchCriteriaEntity.LineOfBusiness);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(
                        "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
                }

                // Map Initial data  to entities 
                // int iRet = MapInitialDatatoTabs(appHeader, SessionObject);
                jsonText = JsonMapper<string>.ObjectToJsonText(searchResult);
            }
            catch (Exception ex)
            {
                // Save intitial data incase of exception for tracing purposes

                Logger.Instance.Error(
                    "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
            }

            return jsonText;
        }

        /// <summary>
        /// The search CAS Application by SSN, Client, CAMS
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public  string SearchCASApplicationBySSNClientCAMS(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = string.Empty;

            // AppDataHeader appHeaderEx = new AppDataHeader();
            var inputParams = new offerInputParams();
            inputParams.tranId = appHeader.SessionId;
            inputParams.offerId = appHeader.OfferId;
            inputParams.customerContactIndicator = true;
            inputParams.Channel = appHeader.Channel;

            SearchApplicationResultEntityCollection searchResult = new SearchApplicationResultEntityCollection();
            SearchApplicationSSNCamsEntity searchCriteriaEntity =
                 JsonMapper<SearchApplicationSSNCamsEntity>
                     .JsonTextToTabData<SearchApplicationSSNCamsEntity>(appHeader.TabDataText);

            try
            {
                try
                {
                    var searchBO = new SearchPartyBo();

                    searchResult = searchBO.SearchBySSNClientCAMS(searchCriteriaEntity);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(
                        "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
                }

                // Map Initial data  to entities 
                // int iRet = MapInitialDatatoTabs(appHeader, SessionObject);
                jsonText = JsonMapper<string>.ObjectToJsonText(searchResult);
            }
            catch (Exception ex)
            {
                // Save intitial data incase of exception for tracing purposes

                Logger.Instance.Error(
                    "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
            }

            return jsonText;
        }


        /// <summary>
        /// The get customer details.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public  string GetCustomerDetails(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = string.Empty;
            AppDataHeader appHeaderEx = new AppDataHeader();
            var inputParams = new offerInputParams();
            inputParams.tranId = appHeader.SessionId;
            inputParams.offerId = appHeader.OfferId;
            inputParams.customerContactIndicator = true;
            inputParams.Channel = appHeader.Channel;

            var customerProfile = new GetCustomerInformation200909Res.customerInfo();
            AccountDetailsSearchCriteriaEntity criteria =
                JsonMapper<CoreEntity>.JsonTextToTabData<CoreEntity>(appHeader.TabDataText) as
                AccountDetailsSearchCriteriaEntity;
            try
            {
                try
                {
                    // Get Offer ECBS Applicant and Co Applicant  calls in Parallel
                    customerProfile = SearchPartyBo.GetCustomerDetails(criteria.SessionID, criteria.CustID);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(
                        "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
                }

                // Map Initial data  to entities 
                // int iRet = MapInitialDatatoTabs(appHeader, SessionObject);
                jsonText = JsonMapper<string>.ObjectToJsonText(customerProfile);
            }
            catch (Exception ex)
            {
                // Save intitial data incase of exception for tracing purposes

                Logger.Instance.Error(
                    "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
            }

            return jsonText;
        }


        /// <summary>
        /// The search cas application.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public  string SearchCASApplication(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = string.Empty;

            // AppDataHeader appHeaderEx = new AppDataHeader();
            var inputParams = new offerInputParams();
            inputParams.tranId = appHeader.SessionId;
            inputParams.offerId = appHeader.OfferId;
            inputParams.customerContactIndicator = true;
            inputParams.Channel = appHeader.Channel;

            SearchApplicationResultEntityCollection searchResult = new SearchApplicationResultEntityCollection();
            WF.EAI.Entities.domain.cusp.Search.SearchCriteriaEntity searchCriteriaEntity =
                JsonMapper<WF.EAI.Entities.domain.cusp.Search.SearchCriteriaEntity>
                    .JsonTextToTabData<WF.EAI.Entities.domain.cusp.Search.SearchCriteriaEntity>(appHeader.TabDataText);

            // WellsFargo.EAI.UCA.Entities.Core.SearchCriteriaEntity searchCriteriaEntity = JsonMapper<CoreEntity>.JsonTextToTabData<CoreEntity>(appHeader.TabDataText) as WellsFargo.EAI.UCA.Entities.Core.SearchCriteriaEntity;
            try
            {
                try
                {
                    var searchBO = new SearchPartyBo();

                    searchResult = searchBO.Search(searchCriteriaEntity);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(
                        "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
                }

                // Map Initial data  to entities 
                // int iRet = MapInitialDatatoTabs(appHeader, SessionObject);
                jsonText = JsonMapper<string>.ObjectToJsonText(searchResult);
            }
            catch (Exception ex)
            {
                // Save intitial data incase of exception for tracing purposes

                Logger.Instance.Error(
                    "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
            }

            return jsonText;
        }
        /// <summary>
        /// The search cas application by app id.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SearchCASApplicationByAppID(ucaCommon.AppDataHeader appHeader)
        {
            string jsonText = string.Empty;
            var inputParams = new offerInputParams();
            inputParams.tranId = appHeader.SessionId;
            inputParams.offerId = appHeader.OfferId;
            inputParams.customerContactIndicator = true;
            inputParams.Channel = appHeader.Channel;

            WF.EAI.Entities.domain.cusp.Search.SearchApplicationResultEntityCollection searchResult = new WF.EAI.Entities.domain.cusp.Search.SearchApplicationResultEntityCollection();
            WF.EAI.Entities.domain.cusp.Search.SearchApplicationIDEntity searchCriteriaEntity =
                 JsonMapper<WF.EAI.Entities.domain.cusp.Search.SearchApplicationIDEntity>
                     .JsonTextToTabData<WF.EAI.Entities.domain.cusp.Search.SearchApplicationIDEntity>(appHeader.TabDataText);

            try
            {
                try
                {
                    var searchBO = new SearchPartyBo();
                    searchResult = searchBO.SearchByAppID(searchCriteriaEntity.ApplicationID, searchCriteriaEntity.LineOfBusiness);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(
                        "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
                }

                // Map Initial data  to entities 
                // int iRet = MapInitialDatatoTabs(appHeader, SessionObject);
                jsonText = JsonMapper<string>.ObjectToJsonText(searchResult);
            }
            catch (Exception ex)
            {
                // Save intitial data incase of exception for tracing purposes

                Logger.Instance.Error(
                    "Error Processing Intial Data:" + appHeader.SessionId + ":" + appHeader.TabDataText, ex);
            }

            return jsonText;
        }



        /// <summary>
        /// The process request.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The process request.
        /// </returns>
        public string SearchPartyData(ucaCommon.AppDataHeader appHeader)
        {
            string transactionId = Guid.NewGuid().ToString();
            string jsonSearchResultText = string.Empty;
            try
            {
                jsonSearchResultText = CommonBo.SearchPartyData(appHeader);
            }
            catch (Exception ex)
            {
                jsonSearchResultText = string.Empty;

                Logger.Instance.Error(appHeader.SessionId, ex);
                var logItem = new LogItem(
                    appHeader.SessionId, 
                    transactionId, 
                    GetApplicationNameByChannel(appHeader.Channel, appHeader.UCAWFAOrig),
                    "UCRASearch", 
                    string.Empty, 
                    "SearchPartyData", 
                    LogItem.MSG_TYPE_ERROR, 
                    string.Empty, 
                    ex.Message);
                AsyncLogger.log(logItem);
            }
            finally
            {
                var logswitch = System.Configuration.ConfigurationManager.AppSettings[Lookup.UCAWebServiceAsyncLogSwitch];
                if (logswitch.ToUpper() == "TRUE")
                {
                    var xmlSerializerDeserializer = new XmlSerializerDeserializer();
                    string wcfRequest = xmlSerializerDeserializer.Serialize(appHeader);
                    var logItem = new LogItem(
                        appHeader.SessionId, 
                        transactionId, 
                        GetApplicationNameByChannel(appHeader.Channel, appHeader.UCAWFAOrig),
                        "UCRASearch", 
                        string.Empty, 
                        "SearchPartyData", 
                        LogItem.MSG_TYPE_REQUEST, 
                        string.Empty, 
                        wcfRequest);
                    logItem.setTimeStamp(DateTime.Now);
                    AsyncLogger.log(logItem);



                    string wcfResponse = JsonStringToXmlString(jsonSearchResultText, "jsonTextResponse");
                    logItem = new LogItem(
                        appHeader.SessionId, 
                        transactionId, 
                        GetApplicationNameByChannel(appHeader.Channel, appHeader.UCAWFAOrig),
                        "UCRASearch", 
                        string.Empty, 
                        "SearchPartyData", 
                        LogItem.MSG_TYPE_RESPONSE, 
                        string.Empty, 
                        wcfResponse);
                    AsyncLogger.log(logItem);

                    

                    #region Log Search Criteria: Channel, LOB

                    logItem = new LogItem(appHeader.SessionId, appHeader.Channel, LogItem.KeyNameEnum.Guid_Channel);
                    AsyncLogger.log(logItem);
                    logItem = new LogItem(appHeader.SessionId, appHeader.LOB, LogItem.KeyNameEnum.Guid_LOB);
                    AsyncLogger.log(logItem);

                    #endregion
                }
            }

            appHeader.TabDataText = jsonSearchResultText;

            var jsonAppHeaderText = JsonMapper<string>.ObjectToJsonText(appHeader);

            return jsonAppHeaderText;
        }

        /// <summary>
        /// The search saved application.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SearchSavedApplication(ucaCommon.AppDataHeader appHeader)
        {
            return CommonBo.SearchSavedApplication(appHeader);
        }


        public WF.EAI.Entities.domain.uca.Common.AppDataHeader SearchESignature(string acctNum, string sessionId)
        {
            return SearchBO.RetailerEsignatureSearch(acctNum, sessionId);
        }

        #region Methods

        /// <summary>
        /// The get application name by channel.
        /// </summary>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="UCAWFAOrig">
        /// The ucawfa orig.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetApplicationNameByChannel(string channel, string UCAWFAOrig)
        {
            string applicationName = string.Empty;
            switch (channel)
            {
                case Lookup.Channel.WFA:
                    if (!string.IsNullOrEmpty(UCAWFAOrig) && UCAWFAOrig == Lookup.UCAWFAOrig.DMZSeamlessRouter)
                    {
                        applicationName = Constants.ApplicationNameEnum.WFADMZ.ToString();
                    }
                    else
                    {
                        applicationName = Constants.ApplicationNameEnum.WFA.ToString();
                    }

                    break;
                case Lookup.Channel.PhoneBank:
                    applicationName = Constants.ApplicationNameEnum.PB.ToString();
                    break;
                default:
                    break;
            }

            return applicationName;
        }

        /// <summary>
        /// The json string to xml string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="deserializeRootElementName">
        /// The deserialize root element name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string JsonStringToXmlString(string value, string deserializeRootElementName)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }

                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(value, deserializeRootElementName);
                var sw = new StringWriter();
                var xw = new XmlTextWriter(sw);
                xmlDoc.WriteTo(xw);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.Message, ex);
                return value;
            }
        }

        #endregion
    }
}
