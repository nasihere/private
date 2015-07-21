// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcapsDataChangesParser.cs" company="">
//   
// </copyright>
// <summary>
//   The acaps data changes parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.EAI.BLL.CUSP.Parsers
{
    using System;
    using System.IO;
    using System.Xml;

    using WF.EAI.Entities.constants;
    using WF.EAI.Entities.domain.c2c.TaskMgmt;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    /// The acaps data changes parser.
    /// </summary>
    public class AcapsDataChangesParser
    {
        #region Public Methods and Operators

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="resXml">
        /// The res xml.
        /// </param>
        /// <returns>
        /// The <see cref="AcapsAppDataEntity"/>.
        /// </returns>
        public static AcapsAppDataEntity Parse(string resXml)
        {
            var theValue = string.Empty;
            var currentXPath = string.Empty;
            var appDataEntity = new AcapsAppDataEntity { AppDataXml = resXml };
            try
            {
                using (var xmlReader = XmlReader.Create(new StringReader(resXml)))
                {
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                currentXPath += "/" + xmlReader.Name.ToUpper();
                                break;
                            case XmlNodeType.Text:
                                theValue = xmlReader.Value.Trim('_');
                                break;
                            case XmlNodeType.CDATA:
                                theValue = xmlReader.Value.Trim('_');
                                break;
                            case XmlNodeType.EndElement:

                                switch (xmlReader.Name)
                                {
                                    case "ApplicationID":
                                        appDataEntity.ApplicationId = theValue.Trim();
                                        break;
                                    case "ApplicationStatus":
                                        appDataEntity.AppStatus = theValue;
                                        break;
                                    case "ApplicationOwner":
                                        appDataEntity.AppOwner = theValue;
                                        break;
                                    case "EmployeeInd":
                                        appDataEntity.EmployeeIndicator = theValue;
                                        break;
                                    case "MORAUNumber":
                                        appDataEntity.AU = theValue;
                                        break;
                                    case "Branch":
                                        appDataEntity.Branch = theValue;
                                        break;
                                    case "MORDivision":
                                        appDataEntity.Division = theValue;
                                        break;
                                    case "MORRegion":
                                        appDataEntity.Region = theValue;
                                        break;
                                    case "MarketCode":
                                        appDataEntity.MarketCode = theValue;
                                        break;
                                    case "CollateralState":
                                        appDataEntity.CollateralState = theValue;
                                        break;
                                    case "OriginationState":
                                        appDataEntity.OriginationState = theValue;
                                        break;
                                    case "ChannelCode":
                                        appDataEntity.ChannelCode = theValue;
                                        break;
                                    case "FinalSegment":
                                        appDataEntity.FinalSegment = theValue;
                                        break;
                                    case "InitialSegment":
                                        appDataEntity.InitialSegment = theValue;
                                        break;
                                    case "PropertyType":
                                        appDataEntity.PropertyType = theValue;
                                        break;
                                    case "ApprovedProduct":
                                        appDataEntity.ApprovedProduct = theValue;
                                        break;
                                    case "PurposeCode":
                                        appDataEntity.PurposeCode = theValue;
                                        break;
                                    case "ZootDecision":
                                        appDataEntity.ZootDecision = theValue;
                                        break;
                                    case "LocationCode":
                                        appDataEntity.LocationCode = theValue;
                                        break;
                                    case "MapCenter":
                                        appDataEntity.MapCenter = theValue;
                                        break;
                                    case "CurrentState":
                                        appDataEntity.CurrentWorkState = theValue;
                                        break;
                                    case "TeamId":
                                        appDataEntity.TeamId = theValue;
                                        break;
                                    case "MORProcessingSystem":
                                        appDataEntity.MorProcessingSystem = theValue;
                                        break;
                                    case "HomesteadInd":
                                        appDataEntity.HomesteadInd = theValue;
                                        break;
                                    case "CCHCode":
                                        appDataEntity.CCHCode = theValue;
                                        break;
                                    case "CCHUser":
                                        appDataEntity.CCHUser = theValue;
                                        break;
                                    case "CCHAppOwnerInd":
                                        appDataEntity.CCHAppOwnerInd = theValue.Trim();
                                        break;
                                    case "ApplicationDate":
                                        string applicationdate = theValue;
                                        if (!applicationdate.Equals(string.IsNullOrEmpty(applicationdate)))
                                        {
                                            if (applicationdate == "00/00/00" || applicationdate == "00000000"
                                                || applicationdate == string.Empty)
                                            {
                                                appDataEntity.ApplicationDate = null;
                                            }
                                            else
                                            {
                                                var formAppDate = theValue;
                                                formAppDate = formAppDate.Substring(0, 4) + "/"
                                                              + formAppDate.Substring(4, 2) + "/"
                                                              + formAppDate.Substring(6, 2);
                                                appDataEntity.ApplicationDate = Convert.ToDateTime(formAppDate);
                                            }
                                        }
                                        else
                                        {
                                            appDataEntity.ApplicationDate = null;
                                        }

                                        break;
                                    case "ClosingDate":
                                        var closingDate = theValue;
                                        if (!closingDate.Equals(string.IsNullOrEmpty(closingDate)))
                                        {
                                            if (closingDate == "00/00/00" || closingDate == "00000000"
                                                || closingDate == string.Empty)
                                            {
                                                appDataEntity.ClosingDate = null;
                                            }
                                            else
                                            {
                                                string formClosingDate = theValue;
                                                formClosingDate = formClosingDate.Substring(0, 4) + "/"
                                                                  + formClosingDate.Substring(4, 2) + "/"
                                                                  + formClosingDate.Substring(6, 2);
                                                appDataEntity.ClosingDate = Convert.ToDateTime(formClosingDate);
                                            }
                                        }
                                        else
                                        {
                                            appDataEntity.ClosingDate = null;
                                        }

                                        break;
                                    case "ApprovedAmount":
                                        var approvedAmt = theValue;
                                        appDataEntity.ApprovedAmount =
                                            !approvedAmt.Equals(string.IsNullOrEmpty(approvedAmt))
                                                ? Convert.ToDecimal(theValue)
                                                : 0;
                                        break;
                                    case "RequestedAmount":
                                        var requestedAmt = theValue;
                                        appDataEntity.RequestedAmount =
                                            !requestedAmt.Equals(string.IsNullOrEmpty(requestedAmt))
                                                ? Convert.ToDecimal(theValue)
                                                : 0;
                                        break;
                                    case "OutstandingSTPARQS":
                                        var stpArqCount = theValue;
                                        int val;
                                        if (int.TryParse(stpArqCount, out val))
                                        {
                                            appDataEntity.OutstandingStpArq = val;
                                        }

                                        break;
                                    case "languagePreference":
                                        appDataEntity.LanguagePreference = theValue;
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
                //Logger.Instance.Error("Failed in Acaps App Data Xml Parse Error=" + ex + " ::: XML=" + resXml);
            }
            finally
            {
                appDataEntity.CreatedBy = Lookup.ACAPSAPP;
                appDataEntity.LastUpdatedBy = Lookup.ACAPSAPP;
            }

            return appDataEntity;
        }

        /// <summary>
        /// The parse get data.
        /// </summary>
        /// <param name="resXml">
        /// The res xml.
        /// </param>
        /// <returns>
        /// The <see cref="AcapsAppDataEntity"/>.
        /// </returns>
        public static AcapsAppDataEntity ParseGetData(string resXml)
        {
            const string FieldXpath = "/ACAPS01/Body/Data/fields/field";
            var theValue = string.Empty;
            var currentXPath = string.Empty;
            var appDataEntity = new AcapsAppDataEntity { AppDataXml = resXml };
            var location = string.Empty;
            var cusLocCompanyCd = string.Empty;
            var cusLocDivisionCd = string.Empty;
            var cusLocServCtrCd = string.Empty;
            var fieldname = string.Empty;
            try
            {
                using (var xmlReader = XmlReader.Create(new StringReader(resXml)))
                {
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                currentXPath += "/" + xmlReader.Name.ToUpper();
                                if (string.Compare(currentXPath, FieldXpath, true) == 0)
                                {
                                    fieldname = xmlReader.GetAttribute("name").Trim();
                                }

                                if (xmlReader.IsEmptyElement)
                                {
                                    switch (fieldname)
                                    {
                                        case "SUM_ACTION_STATUS":
                                            appDataEntity.AppStatus = theValue;
                                            break;
                                        case "SUM_RESPONSIBLE_USER_ID":
                                            appDataEntity.AppOwner = theValue;
                                            break;
                                        case "CUS_EMPLOYEE_IND":
                                            appDataEntity.EmployeeIndicator = theValue;
                                            break;
                                        case "MOR_AU":
                                            appDataEntity.AU = theValue;
                                            break;
                                        case "MOR_AU_DIV":
                                            appDataEntity.Division = theValue;
                                            break;
                                        case "MOR_AU_REG":
                                            appDataEntity.Region = theValue;
                                            break;
                                        case "CUS_APPL_SOURCE_CODE":
                                            appDataEntity.MarketCode = theValue;
                                            break;
                                        case "RCL_PROPERTY_ST":
                                            appDataEntity.CollateralState = theValue;
                                            break;
                                        case "SUM_DS_ORIG_STATE":
                                            appDataEntity.OriginationState = theValue;
                                            break;
                                        case "CUS_CLOSE_BRANCH_CODE":
                                            appDataEntity.ChannelCode = theValue;
                                            break;
                                        case "LIS_FINAL_PROCESS_SEG":
                                            appDataEntity.FinalSegment = theValue;
                                            break;
                                        case "CON_INITIAL_PROCESS_SEG":
                                            appDataEntity.InitialSegment = theValue;
                                            break;
                                        case "RCL_PROPERTY_TYPE":
                                            appDataEntity.PropertyType = theValue;
                                            break;
                                        case "SUM_DS_APRV_PRODUCT":
                                            appDataEntity.ApprovedProduct = theValue;
                                            break;
                                        case "SUM_DS_REQ_PURPOSE_CODE":
                                            appDataEntity.PurposeCode = theValue;
                                            break;
                                        case "ADR_DCS_ID_DECISION":
                                            appDataEntity.ZootDecision = theValue;
                                            break;
                                        case "CUS_LOC_COMPANY_CD":
                                            cusLocCompanyCd = theValue;
                                            break;
                                        case "CUS_LOC_DIVISION_CD":
                                            cusLocDivisionCd = theValue;
                                            break;
                                        case "CUS_LOC_SERV_CTR_CD":
                                            cusLocServCtrCd = theValue;
                                            break;
                                        case "MOR_MAP_CENTER":
                                            appDataEntity.MapCenter = theValue;
                                            break;
                                        case "SUM_PREV_CCH_ROUTE_TO_STATE":
                                            appDataEntity.CurrentWorkState = theValue;
                                            break;
                                        case "CUS_APPL_RECEIVED_DATE":
                                            string applicationdate = theValue;
                                            if (!applicationdate.Equals(string.IsNullOrEmpty(applicationdate)))
                                            {
                                                if (applicationdate == "00/00/00" || applicationdate == "00000000"
                                                    || applicationdate == string.Empty)
                                                {
                                                    appDataEntity.ApplicationDate = null;
                                                }
                                                else
                                                {
                                                    string formAppDate = theValue;
                                                    appDataEntity.ApplicationDate = Convert.ToDateTime(formAppDate);
                                                }
                                            }
                                            else
                                            {
                                                appDataEntity.ApplicationDate = null;
                                            }

                                            break;
                                        case "NAS_LDS_CLOSING_DATE":
                                            string closingDate = theValue;
                                            if (!closingDate.Equals(string.IsNullOrEmpty(closingDate)))
                                            {
                                                if (closingDate == "00/00/00" || closingDate == "00000000"
                                                    || closingDate == string.Empty)
                                                {
                                                    appDataEntity.ClosingDate = null;
                                                }
                                                else
                                                {
                                                    var formClosingDate = theValue;
                                                    appDataEntity.ClosingDate = Convert.ToDateTime(formClosingDate);
                                                }
                                            }
                                            else
                                            {
                                                appDataEntity.ClosingDate = null;
                                            }

                                            break;
                                        case "SUM_DS_APRV_AMOUNT":
                                            string approvedAmt = theValue;
                                            appDataEntity.ApprovedAmount =
                                                !approvedAmt.Equals(string.IsNullOrEmpty(approvedAmt))
                                                    ? Convert.ToDecimal(theValue)
                                                    : 0;
                                            break;
                                        case "SUM_DS_REQ_AMOUNT":
                                            var requestedAmt = theValue;
                                            appDataEntity.RequestedAmount =
                                                !requestedAmt.Equals(string.IsNullOrEmpty(requestedAmt))
                                                    ? Convert.ToDecimal(theValue)
                                                    : 0;
                                            break;
                                        case "CUS_SALES_BRANCH_CODE":
                                            appDataEntity.TeamId = theValue;
                                            break;
                                        case "MOR_PROCESSING_SYSTEM":
                                            appDataEntity.MorProcessingSystem = theValue;
                                            break;
                                        case "RCL_HOMESTEAD_IND":
                                            appDataEntity.HomesteadInd = theValue;
                                            break;
                                        case "CUS_OWNING_BRANCH_CODE":
                                            appDataEntity.Branch = theValue;
                                            break;
                                        case "DRP_ACCT_LEVEL_LANG_PREF":
                                            appDataEntity.LanguagePreference = theValue;
                                            break;
                                    };

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
                                if (string.Compare(currentXPath, FieldXpath, true) == 0)
                                {
                                    switch (fieldname)
                                    {
                                        case "SUM_ACTION_STATUS":
                                            appDataEntity.AppStatus = theValue;
                                            break;
                                        case "SUM_RESPONSIBLE_USER_ID":
                                            appDataEntity.AppOwner = theValue;
                                            break;
                                        case "CUS_EMPLOYEE_IND":
                                            appDataEntity.EmployeeIndicator = theValue;
                                            break;
                                        case "MOR_AU":
                                            appDataEntity.AU = theValue;
                                            break;
                                        case "MOR_AU_DIV":
                                            appDataEntity.Division = theValue;
                                            break;
                                        case "MOR_AU_REG":
                                            appDataEntity.Region = theValue;
                                            break;
                                        case "CUS_APPL_SOURCE_CODE":
                                            appDataEntity.MarketCode = theValue;
                                            break;
                                        case "RCL_PROPERTY_ST":
                                            appDataEntity.CollateralState = theValue;
                                            break;
                                        case "SUM_DS_ORIG_STATE":
                                            appDataEntity.OriginationState = theValue;
                                            break;
                                        case "CUS_CLOSE_BRANCH_CODE":
                                            appDataEntity.ChannelCode = theValue;
                                            break;
                                        case "LIS_FINAL_PROCESS_SEG":
                                            appDataEntity.FinalSegment = theValue;
                                            break;
                                        case "CON_INITIAL_PROCESS_SEG":
                                            appDataEntity.InitialSegment = theValue;
                                            break;
                                        case "RCL_PROPERTY_TYPE":
                                            appDataEntity.PropertyType = theValue;
                                            break;

                                        case "SUM_DS_APRV_PRODUCT":
                                            appDataEntity.ApprovedProduct = theValue;
                                            break;
                                        case "SUM_DS_REQ_PURPOSE_CODE":
                                            appDataEntity.PurposeCode = theValue;
                                            break;
                                        case "ADR_DCS_ID_DECISION":
                                            appDataEntity.ZootDecision = theValue;
                                            break;
                                        case "CUS_LOC_COMPANY_CD":
                                            cusLocCompanyCd = theValue;
                                            break;
                                        case "CUS_LOC_DIVISION_CD":
                                            cusLocDivisionCd = theValue;
                                            break;
                                        case "CUS_LOC_SERV_CTR_CD":
                                            cusLocServCtrCd = theValue;
                                            break;
                                        case "MOR_MAP_CENTER":
                                            appDataEntity.MapCenter = theValue;
                                            break;
                                        case "SUM_PREV_CCH_ROUTE_TO_STATE":
                                            appDataEntity.CurrentWorkState = theValue;
                                            break;
                                        case "CUS_APPL_RECEIVED_DATE":
                                            var applicationdate = theValue;
                                            if (!applicationdate.Equals(string.IsNullOrEmpty(applicationdate)))
                                            {
                                                if (applicationdate == "00/00/00" || applicationdate == "00000000"
                                                    || applicationdate == string.Empty)
                                                {
                                                    appDataEntity.ApplicationDate = null;
                                                }
                                                else
                                                {
                                                    var formAppDate = theValue;
                                                    appDataEntity.ApplicationDate = Convert.ToDateTime(formAppDate);
                                                }
                                            }
                                            else
                                            {
                                                appDataEntity.ApplicationDate = null;
                                            }

                                            break;
                                        case "NAS_LDS_CLOSING_DATE":
                                            var closingDate = theValue;
                                            if (closingDate.Equals(string.IsNullOrEmpty(closingDate)))
                                            {
                                                appDataEntity.ClosingDate = null;
                                            }
                                            else
                                            {
                                                if (closingDate == "00/00/00" || closingDate == "00000000"
                                                    || closingDate == string.Empty)
                                                {
                                                    appDataEntity.ClosingDate = null;
                                                }
                                                else
                                                {
                                                    string formClosingDate = theValue;
                                                    appDataEntity.ClosingDate = Convert.ToDateTime(formClosingDate);
                                                }
                                            }

                                            break;
                                        case "SUM_DS_APRV_AMOUNT":
                                            var approvedAmt = theValue;
                                            appDataEntity.ApprovedAmount =
                                                !approvedAmt.Equals(string.IsNullOrEmpty(approvedAmt))
                                                    ? Convert.ToDecimal(theValue)
                                                    : 0;
                                            break;
                                        case "SUM_DS_REQ_AMOUNT":
                                            var requestedAmt = theValue;
                                            appDataEntity.RequestedAmount =
                                                !requestedAmt.Equals(string.IsNullOrEmpty(requestedAmt))
                                                    ? Convert.ToDecimal(theValue)
                                                    : 0;
                                            break;
                                        case "CUS_SALES_BRANCH_CODE":
                                            appDataEntity.TeamId = theValue;
                                            break;
                                        case "MOR_PROCESSING_SYSTEM":
                                            appDataEntity.MorProcessingSystem = theValue;
                                            break;
                                        case "RCL_HOMESTEAD_IND":
                                            appDataEntity.HomesteadInd = theValue;
                                            break;
                                        case "CUS_OWNING_BRANCH_CODE":
                                            appDataEntity.Branch = theValue;
                                            break;
                                        case "DRP_ACCT_LEVEL_LANG_PREF":
                                            appDataEntity.LanguagePreference = theValue;
                                            break;
                                    }
                                }

                                switch (xmlReader.Name)
                                {
                                    case "applicationID":
                                        appDataEntity.ApplicationId = theValue.Trim();
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
                //Logger.Instance.Error("Failed in Acaps App Data Xml Parse Error=" + ex + " ::: XML=" + resXml);
            }
            finally
            {
                appDataEntity.LocationCode = string.Concat(cusLocCompanyCd, cusLocDivisionCd, cusLocServCtrCd);
                appDataEntity.CreatedBy = Lookup.ACAPSAPP;
                appDataEntity.LastUpdatedBy = Lookup.ACAPSAPP;
            }

            return appDataEntity;
        }

        #endregion
    }
}