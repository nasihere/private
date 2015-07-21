// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CASSearchResultsBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The search results builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using WF.EAI.BLL.cas;
using WF.EAI.Entities.domain.cas.CASSearch;
using WF.UAP.UASF.CrossCutting.Logging;
using WF.UAP.UDB.Repository.Transform.dal.UCRA.CAS;

namespace WF.UAP.UA.UCRA.BLL.cas.Search
{
    /// <summary>
    /// The search results builder.
    /// </summary>
    public class SearchResultsBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        /// The build auto data list.
        /// </summary>
        /// <param name="autoResponseXml">
        /// The auto response xml.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     .
        /// </returns>
        public Dictionary<string, CASSearchResponseData> BuildAutoDataList(string autoResponseXml)
        {
            var document = new XmlDocument();
            document.LoadXml(autoResponseXml);
            var appList = document.SelectNodes("EAIS002/Body/serviceResponse/data/pcmAppList/PCMSummary");
            var responseDataList = new Dictionary<string, CASSearchResponseData>();
            foreach (XmlNode node in appList)
            {
                var data = new CASSearchResponseData
                {
                    applicationId = node.SelectSingleNode("applicationId").InnerText,
                    applicationType = node.SelectSingleNode("applicationType").InnerText,
                    productType = node.SelectSingleNode("productType").InnerText,
                    appStatus = node.SelectSingleNode("appStatus").InnerText,
                    au = node.SelectSingleNode("au").InnerText,
                    bntFlag = node.SelectSingleNode("bntFlag").InnerText,
                    loanAmount = node.SelectSingleNode("loanAmount").InnerText,
                    loanClosingDate = node.SelectSingleNode("loanClosingDate").InnerText,
                    applicationDate = node.SelectSingleNode("applicationDate").InnerText,
                    hris = node.SelectSingleNode("hris").InnerText,
                    rate = node.SelectSingleNode("rate").InnerText,
                    salesId = node.SelectSingleNode("salesId").InnerText,
                    salesPersonId = node.SelectSingleNode("salesPersonId").InnerText,
                    transactionName = node.SelectSingleNode("transactionName") == null
                        ? string.Empty
                        : node.SelectSingleNode("transactionName").InnerText,
                    custFirstName = node.SelectSingleNode("custFirstName").InnerText,
                    custLastName = node.SelectSingleNode("custLastName").InnerText,
                    remodCode1 = node.SelectSingleNode("remodCode1").InnerText,
                    readyToClose = node.SelectSingleNode("readyToClose").InnerText,
                    daysInCurrentStatus = node.SelectSingleNode("daysInCurrentStatus").InnerText,
                    daysStipOutstanding = node.SelectSingleNode("daysStipOutstanding").InnerText,
                    docExpDate = node.SelectSingleNode("docExpDate").InnerText,
                    docExpired = node.SelectSingleNode("docExpired").InnerText,
                    tskFlag = node.SelectSingleNode("tskFlag").InnerText,
                    docsNotRecieved = node.SelectSingleNode("docsNotRecieved").InnerText,
                    HELoanInd = node.SelectSingleNode("HELoanInd").InnerText,
                    noStipsNotMet = node.SelectSingleNode("noStipsNotMet").InnerText,
                    pendingClosingValidationComplete =
                        node.SelectSingleNode("pendingClosingValidationComplete").InnerText,
                    schedClosingDateWarn = node.SelectSingleNode("schedClosingDateWarn").InnerText,
                    scheduledClosingDate = node.SelectSingleNode("scheduledClosingDate").InnerText,
                    secBorrowerSSN = string.Empty,
                    appStatusDate = node.SelectSingleNode("appStatusDate").InnerText,
                    bankerName = node.SelectSingleNode("bankerName").InnerText,
                    guid =
                        node.SelectSingleNode("guid") == null ? string.Empty : node.SelectSingleNode("guid").InnerText,
                    borrowerSSN = node.SelectSingleNode("borrowerSSN").InnerText
                };

// ReSharper disable once PossibleNullReferenceException

                responseDataList.Add(data.applicationId, data);
            }

            return responseDataList;
        }

        /// <summary>
        /// The build ed search response.
        /// </summary>
        /// <param name="dataList">
        /// The data list.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BuildEdSearchResponse(List<CASSearchResponseData> dataList, string globalTransactionId)
        {
            var strXml = new StringBuilder();
            var strBldResponse = new StringBuilder();
            var responseXml = new XmlDocument();
            responseXml.Load(HttpContext.Current.Server.MapPath(@"Templates/EDSearchResponse.xml"));
            strBldResponse.Append(responseXml.InnerXml);
            foreach (var casSearchResponseData in dataList)
            {
                strXml.Append("<EDSummary>");

                strXml.Append("<applicationId>" + casSearchResponseData.applicationId + "</applicationId>");
                strXml.Append("<au>" + casSearchResponseData.au.Trim() + "</au>");
                strXml.Append("<salesId>" + casSearchResponseData.salesId.Trim() + "</salesId>");
                strXml.Append("<hris>" + casSearchResponseData.hris.Trim() + "</hris>");
                strXml.Append("<productType>" + casSearchResponseData.productType.Trim() + "</productType>");
                strXml.Append("<applicationType>" + casSearchResponseData.applicationType.Trim() + "</applicationType>");
                strXml.Append("<applicationDate>" + casSearchResponseData.applicationDate.Trim() + "</applicationDate>");
                strXml.Append("<appStatus>" + casSearchResponseData.appStatus.Trim() + "</appStatus>");
                strXml.Append("<loanClosingDate>" + casSearchResponseData.loanClosingDate.Trim() + "</loanClosingDate>");
                strXml.Append("<loanAmount>" + casSearchResponseData.loanAmount + "</loanAmount>");

                strXml.Append("<bntFlag>" + casSearchResponseData.bntFlag.Trim() + "</bntFlag>");
                strXml.Append("<rate>" + casSearchResponseData.rate + "</rate>");
                strXml.Append("<salesPersonId>" + casSearchResponseData.salesPersonId.Trim() + "</salesPersonId>");
                strXml.Append("<custLastName>" + casSearchResponseData.custLastName.Trim() + "</custLastName>");
                strXml.Append("<custFirstName>" + casSearchResponseData.custFirstName.Trim() + "</custFirstName>");
                strXml.Append("<borrowerSSN>" + casSearchResponseData.borrowerSSN.Trim() + "</borrowerSSN>");
                strXml.Append("<secBorrowerSSN>" + casSearchResponseData.secBorrowerSSN.Trim() + "</secBorrowerSSN>");

                strXml.Append("<propertyAddr1>" + casSearchResponseData.propertyAddr1.Trim() + "</propertyAddr1>");
                strXml.Append("<propertyAddr2>" + casSearchResponseData.propertyAddr2.Trim() + "</propertyAddr2>");
                strXml.Append("<propertyCity>" + casSearchResponseData.propertyCity.Trim() + "</propertyCity>");

                strXml.Append("<propertyState>" + casSearchResponseData.propertyState.Trim() + "</propertyState>");
                strXml.Append("<propertyZip>" + casSearchResponseData.propertyZip.Trim() + "</propertyZip>");

                strXml.Append("</EDSummary>");
            }

            strBldResponse.Replace("[SequenceId]", globalTransactionId);
            strBldResponse.Replace("[SessionId]", globalTransactionId);
            strBldResponse.Replace("[DateTime]", DateTime.Now.ToLongDateString());
            strBldResponse.Replace("[HostName]", HttpContext.Current.Server.MachineName);
            strBldResponse.Replace("[:Data:]", strXml.ToString());

            return strBldResponse.ToString();
        }

        /// <summary>
        /// The build pcm search response.
        /// </summary>
        /// <param name="dataList">
        /// The data list.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BuildPcmSearchResponse(List<CASSearchResponseData> dataList, string globalTransactionId)
        {
            var strXml = new StringBuilder();
            var strBldResponse = new StringBuilder();
            var responseXml = new XmlDocument();
            responseXml.Load(HttpContext.Current.Server.MapPath(@"Templates/CASSearchResponse.xml"));
            strBldResponse.Append(responseXml.InnerXml);
            foreach (var casSearchResponseData in dataList)
            {
                strXml.Append("<PCMSummary>");
                string appId = casSearchResponseData.applicationId.Trim().Contains("SavedAppId_")
                                   ? string.Empty
                                   : casSearchResponseData.applicationId.Trim();
                strXml.Append("<applicationId>" + appId + "</applicationId>");
                strXml.Append("<au>" + casSearchResponseData.au.Trim() + "</au>");
                strXml.Append("<salesId>" + casSearchResponseData.salesId.Trim() + "</salesId>");
                strXml.Append("<hris>" + casSearchResponseData.hris.Trim() + "</hris>");
                strXml.Append("<productType>" + casSearchResponseData.productType.Trim() + "</productType>");
                strXml.Append("<applicationType>" + casSearchResponseData.applicationType.Trim() + "</applicationType>");
                strXml.Append("<applicationDate>" + casSearchResponseData.applicationDate.Trim() + "</applicationDate>");
                strXml.Append("<appStatus>" + casSearchResponseData.appStatus.Trim() + "</appStatus>");
                strXml.Append("<loanClosingDate>" + casSearchResponseData.loanClosingDate.Trim() + "</loanClosingDate>");
                strXml.Append("<loanAmount>" + casSearchResponseData.loanAmount + "</loanAmount>");
                strXml.Append("<remodCode1>" + casSearchResponseData.remodCode1.Trim() + "</remodCode1>");
                strXml.Append("<bntFlag>" + casSearchResponseData.bntFlag.Trim() + "</bntFlag>");
                strXml.Append("<rate>" + casSearchResponseData.rate + "</rate>");
                strXml.Append("<salesPersonId>" + casSearchResponseData.salesPersonId.Trim() + "</salesPersonId>");
                strXml.Append("<custLastName>" + casSearchResponseData.custLastName.Trim() + "</custLastName>");
                strXml.Append("<custFirstName>" + casSearchResponseData.custFirstName.Trim() + "</custFirstName>");
                strXml.Append("<borrowerSSN>" + casSearchResponseData.borrowerSSN.Trim() + "</borrowerSSN>");
                strXml.Append("<secBorrowerSSN>" + casSearchResponseData.secBorrowerSSN.Trim() + "</secBorrowerSSN>");
                strXml.Append("<tskFlag>" + casSearchResponseData.tskFlag.Trim() + "</tskFlag>");
                strXml.Append("<docExpDate>" + casSearchResponseData.docExpDate.Trim() + "</docExpDate>");
                strXml.Append("<docExpired>" + casSearchResponseData.docExpired.Trim() + "</docExpired>");
                strXml.Append("<appStatusDate>" + casSearchResponseData.appStatusDate.Trim() + "</appStatusDate>");
                strXml.Append("<noStipsNotMet>" + casSearchResponseData.noStipsNotMet.Trim() + "</noStipsNotMet>");
                strXml.Append(
                    "<daysStipOutstanding>" + casSearchResponseData.daysStipOutstanding.Trim()
                    + "</daysStipOutstanding>");
                strXml.Append("<readyToClose>" + casSearchResponseData.readyToClose.Trim() + "</readyToClose>");
                strXml.Append(
                    "<daysStipOutstanding>" + casSearchResponseData.daysStipOutstanding.Trim()
                    + "</daysStipOutstanding>");
                strXml.Append("<docsNotRecieved>" + casSearchResponseData.docsNotRecieved.Trim() + "</docsNotRecieved>");
                strXml.Append(
                    "<daysStipOutstanding>" + casSearchResponseData.daysStipOutstanding.Trim()
                    + "</daysStipOutstanding>");
                strXml.Append("<guid>" + casSearchResponseData.guid + "</guid>");
                strXml.Append("<transactionName>" + casSearchResponseData.transactionName.Trim() + "</transactionName>");
                strXml.Append(
                    "<scheduledClosingDate>" + casSearchResponseData.scheduledClosingDate.Trim()
                    + "</scheduledClosingDate>");
                strXml.Append(
                    "<schedClosingDateWarn>" + casSearchResponseData.schedClosingDateWarn.Trim()
                    + "</schedClosingDateWarn>");
                strXml.Append("<bankerName>" + casSearchResponseData.bankerName.Trim() + "</bankerName>");
                strXml.Append(
                    "<pendingClosingValidationComplete>" + casSearchResponseData.pendingClosingValidationComplete.Trim()
                    + "</pendingClosingValidationComplete>");
                strXml.Append("<HELoanInd>" + casSearchResponseData.HELoanInd.Trim() + "</HELoanInd>");
                strXml.Append("</PCMSummary>");
            }

            strBldResponse.Replace("[SequenceId]", globalTransactionId);
            strBldResponse.Replace("[SessionId]", globalTransactionId);
            strBldResponse.Replace("[DateTime]", DateTime.Now.ToLongDateString());
            strBldResponse.Replace("[HostName]", HttpContext.Current.Server.MachineName);
            strBldResponse.Replace("[:Data:]", strXml.ToString());

            return strBldResponse.ToString();
        }

        /// <summary>
        /// The get auto request.
        /// </summary>
        /// <param name="requestMsg">
        /// The request msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAutoRequest(string requestMsg)
        {
            var document = new XmlDocument();
            document.LoadXml(requestMsg);

            document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/applicationType").InnerText =
                "Auto Finance";

            return document.InnerXml;
        }

        /// <summary>
        /// The get cas applications for all.
        /// </summary>
        /// <param name="requestData">
        /// The request data.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <param name="requestMsg">
        /// The request msg.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCasApplicationsForAll(
            CASSearchRequestData requestData, string globalTransactionId, string requestMsg, string channel)
        {
            var ecn = requestData.ecn;
            //For ECN search, update with SSN instead
            if (!string.IsNullOrEmpty(ecn))
            {
                var ssnHelper = new CASApplicationSearchDataHelper();
                requestData.custSSN = ssnHelper.GetSsnForEcn(requestData.ecn);
                if ((!string.IsNullOrEmpty(requestData.custSSN)) && (requestData.custSSN.Trim() != ""))
                    requestData.ecn = null;
            }
            var wipSearch = new WIPSearchDataHelper();
            Dictionary<string, CASSearchResponseData> wipDataList = wipSearch.getWIPApps(requestData, true);
            Logger.Instance.Info("wipDataList.Count=" + wipDataList.Count);
            
            //restore ECN value
            if (!string.IsNullOrEmpty(ecn))
            {
                requestData.custSSN = null;
                requestData.ecn = ecn;
            }
            var applicationSearchDataHelper = new CASApplicationSearchDataHelper();
            var db2DataList =
                applicationSearchDataHelper.GetCASSearchResponseData(requestData);
            if (db2DataList == null) throw new ArgumentNullException("requestData");
            Logger.Instance.Info("DB2dataList.Count="+ db2DataList.Count);
            //string responseXml =
            //    (Invoker.GetSRCH(this.GetAutoRequest(requestMsg), channel, globalTransactionId)).resXmlStr;
            //Dictionary<string, CASSearchResponseData> AutoDataList = this.BuildAutoDataList(responseXml);

            //Dictionary<string, CASSearchResponseData> mergedWIPAutoDataList =
            //    this.addComparedWIPDB2System2Results(wipDataList, AutoDataList);
            var mergedWipDb2DataList =
                AddComparedDb2AndWipResults(wipDataList, db2DataList);
                //this.addComparedDB2AndWIPResults(mergedWIPAutoDataList, DB2dataList);
            Logger.Instance.Info("mergedWipDb2DataList.Count=" + mergedWipDb2DataList.Count);
            return BuildPcmSearchResponse(mergedWipDb2DataList, globalTransactionId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="globalTransactionId"></param>
        /// <returns></returns>
        public string GetCasApplicationsForPcmheq(CASSearchRequestData data, string globalTransactionId)
        {
            var wipSearch = new WIPSearchDataHelper();
            var wipDataList = wipSearch.getWIPApps(data, true);
            var applicationSearchDataHelper = new CASApplicationSearchDataHelper();
            var db2DataList =
                applicationSearchDataHelper.GetCASSearchResponseData(data);
            var mergedDataList = AddComparedDb2AndWipResults(wipDataList, db2DataList);

            return BuildPcmSearchResponse(mergedDataList, globalTransactionId);
        }

        /// <summary>
        /// The get merged acaps data.
        /// </summary>
        /// <param name="wipItem">
        /// The wip item.
        /// </param>
        /// <param name="db2DataList">
        /// The d b 2 data list.
        /// </param>
        /// <returns>
        /// The <see cref="CASSearchResponseData"/>.
        /// </returns>
        public CASSearchResponseData GetMergedAcapsData(
            CASSearchResponseData wipItem, CASSearchResponseData db2DataList)
        {
            db2DataList.salesId = wipItem.salesId;
            db2DataList.guid = wipItem.guid;
            return db2DataList;
        }

        /// <summary>
        /// The get search request data.
        /// </summary>
        /// <param name="requestXml">
        /// The request xml.
        /// </param>
        /// <returns>
        /// The <see cref="CASSearchRequestData"/>.
        /// </returns>
        public CASSearchRequestData GetSearchRequestData(string requestXml)
        {
            var requestData = new CASSearchRequestData();
            var document = new XmlDocument();

            // document.Load(HttpContext.Current.Server.MapPath(@"Templates/EDSRCH.Request.Template.xml"));
            document.LoadXml(requestXml);
            var selectSingleNode = document.SelectSingleNode("EAIS001/Body/serviceRequest/action");
            if (selectSingleNode != null)
                requestData.action = selectSingleNode.InnerText;


            if (requestData.action.ToUpper() == CASUtils.CAS_PCM_SEARCH)
            {
                var singleNode = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/applicationType");
                if (singleNode != null)
                    requestData.applicationType =
                        singleNode
                            .InnerText.Trim();
                var xmlNode = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/au");
                if (xmlNode != null)
                    requestData.au =
                        xmlNode.InnerText.Trim();
                var node = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/locNum");
                if (node != null)
                    requestData.locNum =
                        node.InnerText.Trim();
                var selectSingleNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/salesId");
                if (selectSingleNode1 != null)
                    requestData.salesId =
                        selectSingleNode1.InnerText.Trim();
                var singleNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/fromDate");
                if (singleNode1 != null)
                    requestData.fromDate =
                        singleNode1
                            .InnerText.Trim();

                var xmlNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/toDate");
                if (xmlNode1 != null)
                {
                    string toDate =
                        xmlNode1.InnerText.Trim();
                    if (!string.IsNullOrEmpty(toDate))
                    {
                        toDate = Convert.ToDateTime(toDate).ToShortDateString();
                    }

                    requestData.toDate = toDate;
                }
                var node1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/custFirstName");
                if (node1 != null)
                    requestData.custFirstName =
                        node1
                            .InnerText.Trim();
                var selectSingleNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/custLastName");
                if (selectSingleNode2 != null)
                    requestData.custLastName =
                        selectSingleNode2
                            .InnerText.Trim();
                var singleNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/custSSN");
                if (singleNode2 != null)
                    requestData.custSSN =
                        singleNode2.InnerText.Trim();

                var xmlNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/sourceID");
                if (xmlNode2 != null)
                    requestData.sourceID =
                        xmlNode2
                            .InnerText.Trim();

                requestData.hris =
                    document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/hris").InnerText.Trim();
                requestData.appStatus =
                    document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/appStatus")
                            .InnerText.Trim();
                requestData.ecn =
                    document.SelectSingleNode("EAIS001/Body/serviceRequest/data/PCMSearchData/ecn")
                            .InnerText.Trim();
            }
                

                #region ED
            else
            {
                var xmlNode = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/fromDate");
                if (xmlNode != null)
                    requestData.fromDate =
                        xmlNode.InnerText;

                var singleNode = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/toDate");
                if (singleNode != null)
                {
                    string toDate =
                        singleNode.InnerText;
                    if (!string.IsNullOrEmpty(toDate))
                    {
                        toDate = Convert.ToDateTime(toDate).ToShortDateString();
                    }

                    requestData.toDate = toDate;
                }
                var selectSingleNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/custFirstName");
                if (selectSingleNode1 != null)
                    requestData.custFirstName =
                        selectSingleNode1
                            .InnerText.Trim();
                var singleNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/custLastName");
                if (singleNode1 != null)
                    requestData.custLastName =
                        singleNode1
                            .InnerText.Trim();
                var xmlNode1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/customerSSN");
                if (xmlNode1 != null)
                    requestData.custSSN =
                        xmlNode1
                            .InnerText.Trim();
                var node1 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/appStatus");
                if (node1 != null)
                    requestData.appStatus =
                        node1
                            .InnerText.Trim();
                var selectSingleNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/productType");
                if (selectSingleNode2 != null)
                    requestData.productType =
                        selectSingleNode2
                            .InnerText.Trim();
                var singleNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/repFirstName");
                if (singleNode2 != null)
                    requestData.repFirstName =
                        singleNode2
                            .InnerText.Trim();
                var xmlNode2 = document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/repLastName");
                if (xmlNode2 != null)
                    requestData.repLastName =
                        xmlNode2
                            .InnerText.Trim();

                requestData.applicationId =
                    document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/applicationId")
                            .InnerText.Trim();

                var ssnList = new StringBuilder();
                var repSsnList =
                    document.SelectSingleNode("EAIS001/Body/serviceRequest/data/EDSearchData/repSSNList");
                if (repSsnList != null)
                {
                    var repSsNs = repSsnList.SelectNodes("ssn");
                    foreach (XmlNode node in repSsNs)
                    {
                        ssnList.Append("," + "'" + node.InnerText + "'");
                    }

                    if (ssnList != null && ssnList.ToString() != string.Empty && ssnList[0] == ',')
                    {
                        ssnList.Remove(0, 1);
                    }
                }

                requestData.repSSNList = ssnList.ToString();
            }

            #endregion

            return requestData;
        }

        /// <summary>
        /// The get unique d b 2 data.
        /// </summary>
        /// <param name="wipDataList">
        /// The wip data list.
        /// </param>
        /// <param name="db2DataList">
        /// The d b 2 data list.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>List</cref>
        ///     </see>
        ///     .
        /// </returns>
        public List<CASSearchResponseData> GetUniqueDb2Data(
            Dictionary<string, CASSearchResponseData> wipDataList, Dictionary<string, CASSearchResponseData> db2DataList)
        {
            return (from db2Item in db2DataList let appId = db2Item.Value.applicationId where !wipDataList.ContainsKey(appId) select db2Item.Value).ToList();
        }

        /// <summary>
        /// The get wip app results.
        /// </summary>
        /// <param name="requestData">
        /// The request data.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetWipAppResults(CASSearchRequestData requestData, string globalTransactionId)
        {
            var wipSearch = new WIPSearchDataHelper();
            var dataList1 = wipSearch.getWIPApps(requestData, true);
            var responseList = dataList1.Select(data => data.Value).ToList();

            return BuildPcmSearchResponse(responseList, globalTransactionId);
        }

        /// <summary>
        /// The process search results.
        /// </summary>
        /// <param name="requestMsg">
        /// The request msg.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ProcessSearchResults(string requestMsg, string channel, string globalTransactionId)
        {
            var responseXml = string.Empty;
            var requestData = GetSearchRequestData(requestMsg);

            if (requestData.action.ToUpper() == CASUtils.CAS_PCM_SEARCH)
            {
                var applicationType = requestData.applicationType;

                switch (applicationType.ToUpper().Trim())
                {
                    case CASUtils.AUTOFINANCE:
                        //responseXml = (Invoker.GetSRCH(requestMsg, channel, globalTransactionId)).resXmlStr;
                        responseXml = GetCasApplicationsForAll(
                           requestData, globalTransactionId, requestMsg, channel);
                        break;

                    case CASUtils.SAVEDAPP:
                    case CASUtils.CREDITCARD:
                        responseXml = GetWipAppResults(requestData, globalTransactionId);
                        break;

                        ////responseXml = GetWIPAppResults(requestData, globalTransactionId);
                        ////break;
                    case CASUtils.HOMEEQUITY:
                    case CASUtils.PLL:
                        responseXml = GetCasApplicationsForPcmheq(requestData, globalTransactionId);
                        break;

                    case "":
                        responseXml = GetCasApplicationsForAll(
                            requestData, globalTransactionId, requestMsg, channel);
                        break;
                }
            }
            else
            {
                responseXml = ProcessSearchResultsForEd(requestData, globalTransactionId);
            }

            return responseXml;
        }

        /// <summary>
        /// The process search results for ed.
        /// </summary>
        /// <param name="requestData">
        /// The request data.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ProcessSearchResultsForEd(CASSearchRequestData requestData, string globalTransactionId)
        {
            var applicationSearchDataHelper = new CASApplicationSearchDataHelper();
            var wipDataList = applicationSearchDataHelper.GetEDApps(requestData);
            var responseXml = BuildEdSearchResponse(wipDataList, globalTransactionId);

            return responseXml;
        }

        /// <summary>
        /// The add compared d b 2 and wip results.
        /// </summary>
        /// <param name="wipDataList">
        /// The wip data list.
        /// </param>
        /// <param name="db2DataList">
        /// The d b 2 data list.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>List</cref>
        ///     </see>
        ///     .
        /// </returns>
        public List<CASSearchResponseData> AddComparedDb2AndWipResults(
            Dictionary<string, CASSearchResponseData> wipDataList, Dictionary<string, CASSearchResponseData> db2DataList)
        {
            if (db2DataList == null) throw new ArgumentNullException("db2DataList");
            var mergedList = (from wipItem in wipDataList let appId = wipItem.Value.applicationId select !db2DataList.ContainsKey(appId) ? wipItem.Value : GetMergedAcapsData(wipItem.Value, db2DataList[appId])).ToList();

            mergedList.AddRange(GetUniqueDb2Data(wipDataList, db2DataList)); //fix for PAC  16284227 Manger search for Internet applications
            return mergedList;
        }

        /// <summary>
        /// The add compared wipd b 2 system 2 results.
        /// </summary>
        /// <param name="wipDataList">
        /// The wip data list.
        /// </param>
        /// <param name="autoDataList">
        /// The auto data list.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     .
        /// </returns>
        public Dictionary<string, CASSearchResponseData> AddComparedWipdb2System2Results(
            Dictionary<string, CASSearchResponseData> wipDataList, 
            Dictionary<string, CASSearchResponseData> autoDataList)
        {
            var mergedList = new Dictionary<string, CASSearchResponseData>();
            foreach (var wipItem in wipDataList)
            {
                string appId = wipItem.Value.applicationId;
                if (!string.IsNullOrEmpty(appId))
                {
                    if (!autoDataList.ContainsKey(appId))
                    {
                        mergedList.Add(appId, wipItem.Value);
                    }
                }
            }

            foreach (var autoItem in autoDataList)
            {
                string autoAppId = autoItem.Value.applicationId;
                mergedList.Add(autoAppId, autoDataList[autoAppId]);
            }

            return mergedList;
        }

        #endregion
    }
}