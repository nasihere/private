// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="">
//   
// </copyright>
// <summary>
//   The helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using WF.EAI.Data.sif.Services.CWS.WorkListSearch;
using WF.EAI.Entities.domain.c2c;
using WF.EAI.Entities.domain.c2c.UI;
using WF.EAI.Data.sif.Services.Hula.CSScenarioService;
using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Entities.domain.cusp.UI;
using FraudPopupEntity = WF.EAI.Entities.domain.c2c.UI.FraudPopupEntity;


namespace WF.EAI.BLL.CUSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using WF.EAI.Data.sif.Services.CWS.Fraud;
    using Newtonsoft.Json.Linq;

    using WellsFargo.EAI.SIF.Services.CWS.MaskInq;
    using WellsFargo.EAI.SIF.Services.CWS.MaskInq.MaskUpdRequest;

    using WF.EAI.Entities.domain.c2c.Common;
    using WF.EAI.Utils;

    /// <summary>
    ///     The helper.
    /// </summary>
    public class Helper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The acaps upd fields.
        /// </summary>
        /// <param name="uiField">
        /// The ui field.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<ACAPS01BodyDataField> AcapsUpdFields(FieldAttribute uiField)
        {
            var acapsFields = new List<ACAPS01BodyDataField>(1);
            var acapField = new ACAPS01BodyDataField
            {
                name = uiField.AcapsFieldName,
                Value = uiField.Value,
                page_number = uiField.PageNumber
            };
            acapsFields.Add(acapField);
            return acapsFields;
        }

        /// <summary>
        /// The acaps upd fields.
        /// </summary>
        /// <param name="uiFields">
        /// The ui fields.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<ACAPS01BodyDataField> AcapsUpdFields(List<FieldAttribute> uiFields)
        {
            var acapsFields = new List<ACAPS01BodyDataField>(uiFields.Count);
            acapsFields.AddRange(
                uiFields.Select(
                    uiField =>
                    new ACAPS01BodyDataField
                    {
                        name = uiField.AcapsFieldName,
                        Value = HttpUtility.HtmlDecode(uiField.Value),
                        page_number = uiField.PageNumber
                    }));

            return acapsFields;
        }

        /// <summary>
        /// The get field attribute.
        /// </summary>
        /// <param name="jToken">
        /// The j token.
        /// </param>
        /// <returns>
        /// The <see cref="FieldAttribute"/>.
        /// </returns>
        public static FieldAttribute GetFieldAttribute(JToken jToken)
        {
            var field = new FieldAttribute
            {
                AcapsFieldName = Convert.ToString(jToken["AcapsFieldName"].Value<string>()),
                PageNumber = Convert.ToString(jToken["PageNumber"].Value<string>()),
                Value = Convert.ToString(jToken["CurrentValue"].Value<string>())
            };
            return field;
        }



        public static FieldAttribute GetFieldAttributewithOriginalValue(JToken jToken)
        {
            var field = new FieldAttribute
            {
                AcapsFieldName = Convert.ToString(jToken["AcapsFieldName"].Value<string>()),
                PageNumber = Convert.ToString(jToken["PageNumber"].Value<string>()),
                Value = Convert.ToString(jToken["CurrentValue"].Value<string>()),
                OriginalValue = Convert.ToString(jToken["OriginalValue"].Value<string>())
            };
            return field;
        }

        /// <summary>
        /// The get sif update field.
        /// </summary>
        /// <param name="jToken">
        /// The j token.
        /// </param>
        /// <returns>
        /// The <see cref="ACAPS01BodyDataField"/>.
        /// </returns>
        public static ACAPS01BodyDataField GetSifUpdateField(JToken jToken)
        {
            var acapsField = new ACAPS01BodyDataField
            {
                name =
                    Convert.ToString(jToken["AcapsFieldName"].Value<string>()),
                page_number =
                    Convert.ToString(jToken["PageNumber"].Value<string>()),
                Value =
                    HttpUtility.HtmlDecode(
                        Convert.ToString(
                            jToken["CurrentValue"].Value<string>()))
            };

            return acapsField;
        }

        /// <summary>
        /// The set entity header.
        /// </summary>
        /// <param name="maskInquiryResponse">
        /// The mask inquiry response.
        /// </param>
        /// <param name="header">
        /// The header.
        /// </param>
        public static void SetEntityHeader(MaskInqRes maskInquiryResponse, AppDataHeader header)
        {
            header.ApplicationId = Utilities.parseCDataXML("applicationID", maskInquiryResponse.resXmlStr);
            header.ApplicationSuffix = Utilities.parseCDataXML("applicationSuffix", maskInquiryResponse.resXmlStr);
            header.AcapsSessionId = Utilities.parseXML("acaps_session", maskInquiryResponse.resXmlStr);
            header.AcapsFunction = Utilities.parseXML("acaps_function", maskInquiryResponse.resXmlStr);
            header.GuiFunction = Utilities.parseXML("gui_function", maskInquiryResponse.resXmlStr);
            header.BranchCode = Utilities.parseCDataXML("BRANCH_CODE", maskInquiryResponse.resXmlStr);
            header.LocationCode = Utilities.parseCDataXML("location_code", maskInquiryResponse.resXmlStr);
            header.PanelKey = Utilities.parseCDataXML("PANEL_KEY", maskInquiryResponse.resXmlStr);
            header.WorklistUserid = Utilities.parseCDataXML("worklist_userid", maskInquiryResponse.resXmlStr);
        }

        public static SalesPlanMapper SetSalesPlan(ScenarioServiceRes scenarioServiceRes)
        {
            var salesPlan = new SalesPlanMapper
            {
                apr = !string.IsNullOrEmpty(scenarioServiceRes.eaiCreditScenario002.body.APR)
                          ? Math.Round(
                              (Convert.ToDouble(scenarioServiceRes.eaiCreditScenario002.body.APR)*
                               100), 3).
                                ToString()
                          : "",
                currentPmt = scenarioServiceRes.eaiCreditScenario002.body.Pmt,
                amtFinanced = scenarioServiceRes.eaiCreditScenario002.body.AmtFin,
                interestRate = scenarioServiceRes.eaiCreditScenario002.body.FinalRate,
                interestCharge = scenarioServiceRes.eaiCreditScenario002.body.IntChrg
            };
            return salesPlan;

        }

        /// <summary>
        /// The trim string.
        /// </summary>
        /// <param name="trimValue">
        /// The trim value.
        /// </param>
        /// <returns>
        /// The trim string.
        /// </returns>
        public static string TrimString(string trimValue)
        {
            trimValue = trimValue.Replace("\n", "X");
            trimValue = trimValue.Replace("\r", "X");

            return trimValue;
        }

        public static FraudPopupEntity SetFraudEntity(FraudInquiryResponse fraudInquiryResponse)
        {
            FraudPopupEntity fraudPopupEntity = new FraudPopupEntity();
            fraudPopupEntity.FrdFraudFileInd = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                where field.name == "FRD_FRAUD_FILE_IND"
                                                select field.Value).First();
            if (fraudPopupEntity.FrdFraudFileInd == null)
                fraudPopupEntity.FrdFraudFileInd = string.Empty;
            fraudPopupEntity.FrdLastUpdateDate = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                  where field.name == "FRD_LAST_UPDATE_DATE"
                                                  select field.Value).First();
            if (fraudPopupEntity.FrdLastUpdateDate == null)
                fraudPopupEntity.FrdLastUpdateDate = string.Empty;
            fraudPopupEntity.FrdLastUpdateTime = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                  where field.name == "FRD_LAST_UPDATE_TIME"
                                                  select field.Value).First();
            if (fraudPopupEntity.FrdLastUpdateTime == null)
                fraudPopupEntity.FrdLastUpdateTime = string.Empty;

            fraudPopupEntity.FrdLastUpdateUserId =
                (from field in fraudInquiryResponse.FraudData.Data.fields.field
                 where field.name == "FRD_LAST_UPDATE_USER_ID"
                 select field.Value).First();
            if (fraudPopupEntity.FrdLastUpdateUserId == null)
                fraudPopupEntity.FrdLastUpdateUserId = string.Empty;
            fraudPopupEntity.FrdEnteredUserID = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                 where field.name == "FRD_ENTERED_USER_ID"
                                                 select field.Value).First();
            if (fraudPopupEntity.FrdEnteredUserID == null)
                fraudPopupEntity.FrdEnteredUserID = string.Empty;
            fraudPopupEntity.FrdEnteredTime = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_ENTERED_TIME"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEnteredTime == null)
                fraudPopupEntity.FrdEnteredTime = string.Empty;
            fraudPopupEntity.FrdEnteredDate = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_ENTERED_DATE"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEnteredDate == null)
                fraudPopupEntity.FrdEnteredDate = string.Empty;

            fraudPopupEntity.FrdEmplSt = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                          where field.name == "FRD_EMPL_ST"
                                          select field.Value).First();
            if (fraudPopupEntity.FrdEmplSt == null)
                fraudPopupEntity.FrdEmplSt = string.Empty;
            fraudPopupEntity.FrdEmplCity = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                            where field.name == "FRD_EMPL_CITY"
                                            select field.Value).First();
            if (fraudPopupEntity.FrdEmplCity == null)
                fraudPopupEntity.FrdEmplCity = string.Empty;
            fraudPopupEntity.FrdEmplPhone03 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_EMPL_PHONE_03"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEmplPhone03 == null)
                fraudPopupEntity.FrdEmplPhone03 = string.Empty;
            fraudPopupEntity.FrdEmplPhone02 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_EMPL_PHONE_02"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEmplPhone02 == null)
                fraudPopupEntity.FrdEmplPhone02 = string.Empty;

            fraudPopupEntity.FrdEmplPhone02 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_EMPL_PHONE_01"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEmplPhone02 == null)
                fraudPopupEntity.FrdEmplPhone02 = string.Empty;

            fraudPopupEntity.FrdComments2 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                             where field.name == "FRD_COMMENTS_2"
                                             select field.Value).First();
            if (fraudPopupEntity.FrdComments2 == null)
                fraudPopupEntity.FrdComments2 = string.Empty;
            fraudPopupEntity.FrdComments1 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                             where field.name == "FRD_COMMENTS_1"
                                             select field.Value).First();
            if (fraudPopupEntity.FrdComments1 == null)
                fraudPopupEntity.FrdComments1 = string.Empty;

            fraudPopupEntity.FrdReasonCode = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                              where field.name == "FRD_REASON_CODE"
                                              select field.Value).First();
            if (fraudPopupEntity.FrdReasonCode == null)
                fraudPopupEntity.FrdReasonCode = string.Empty;
            fraudPopupEntity.FrdLastCount = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                             where field.name == "FRD_LAST_COUNT"
                                             select field.Value).First();
            if (fraudPopupEntity.FrdLastCount == null)
                fraudPopupEntity.FrdLastCount = string.Empty;
            fraudPopupEntity.FrdLastHit = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                           where field.name == "FRD_LAST_HIT"
                                           select field.Value).First();
            if (fraudPopupEntity.FrdLastHit == null)
                fraudPopupEntity.FrdLastHit = string.Empty;
            fraudPopupEntity.FrdSocialSecInsNum =
                (from field in fraudInquiryResponse.FraudData.Data.fields.field
                 where field.name == "FRD_SOCIAL_SEC_INS_NUM"
                 select field.Value).First();
            if (fraudPopupEntity.FrdSocialSecInsNum == null)
                fraudPopupEntity.FrdSocialSecInsNum = string.Empty;
            fraudPopupEntity.FrdOriginatingAu = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                 where field.name == "FRD_ORIGINATING_AU"
                                                 select field.Value).First();
            if (fraudPopupEntity.FrdOriginatingAu == null)
                fraudPopupEntity.FrdOriginatingAu = string.Empty;


            fraudPopupEntity.FrdPhoneNum03 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                              where field.name == "FRD_PHONE_NUM_03"
                                              select field.Value).First();
            if (fraudPopupEntity.FrdPhoneNum03 == null)
                fraudPopupEntity.FrdPhoneNum03 = string.Empty;


            fraudPopupEntity.FrdPhoneNum02 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                              where field.name == "FRD_PHONE_NUM_02"
                                              select field.Value).First();
            if (fraudPopupEntity.FrdPhoneNum02 == null)
                fraudPopupEntity.FrdPhoneNum02 = string.Empty;

            fraudPopupEntity.FrdPhoneNum01 = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                              where field.name == "FRD_PHONE_NUM_01"
                                              select field.Value).First();
            if (fraudPopupEntity.FrdPhoneNum01 == null)
                fraudPopupEntity.FrdPhoneNum01 = string.Empty;


            fraudPopupEntity.FrdZipPostalCode = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                 where field.name == "FRD_ZIP_POSTAL_CODE"
                                                 select field.Value).First();
            if (fraudPopupEntity.FrdZipPostalCode == null)
                fraudPopupEntity.FrdZipPostalCode = string.Empty;
            fraudPopupEntity.FrdStProv = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                          where field.name == "FRD_ST_PROV"
                                          select field.Value).First();
            if (fraudPopupEntity.FrdStProv == null)
                fraudPopupEntity.FrdStProv = string.Empty;
            fraudPopupEntity.FrdCity = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                        where field.name == "FRD_CITY"
                                        select field.Value).First();
            if (fraudPopupEntity.FrdCity == null)
                fraudPopupEntity.FrdCity = string.Empty;
            fraudPopupEntity.FrdPoBoxNum = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                            where field.name == "FRD_PO_BOX_NUM"
                                            select field.Value).First();
            if (fraudPopupEntity.FrdPoBoxNum == null)
                fraudPopupEntity.FrdPoBoxNum = string.Empty;
            fraudPopupEntity.FrdStreetName = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                              where field.name == "FRD_STREET_NAME"
                                              select field.Value).First();
            if (fraudPopupEntity.FrdStreetName == null)
                fraudPopupEntity.FrdStreetName = string.Empty;
            fraudPopupEntity.FrdLastName = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                            where field.name == "FRD_LAST_NAME"
                                            select field.Value).First();
            if (fraudPopupEntity.FrdLastName == null)
                fraudPopupEntity.FrdLastName = string.Empty;
            fraudPopupEntity.FrdMiddleInitial = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                                 where field.name == "FRD_MIDDLE_INITIAL"
                                                 select field.Value).First();
            if (fraudPopupEntity.FrdMiddleInitial == null)
                fraudPopupEntity.FrdMiddleInitial = string.Empty;
            fraudPopupEntity.FrdStreetNum = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                             where field.name == "FRD_STREET_NUM"
                                             select field.Value).First();
            if (fraudPopupEntity.FrdStreetNum == null)
                fraudPopupEntity.FrdStreetNum = string.Empty;
            fraudPopupEntity.FrdFirstName = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                             where field.name == "FRD_FIRST_NAME"
                                             select field.Value).First();
            if (fraudPopupEntity.FrdFirstName == null)
                fraudPopupEntity.FrdFirstName = string.Empty;

            fraudPopupEntity.FrdEnteredDate = (from field in fraudInquiryResponse.FraudData.Data.fields.field
                                               where field.name == "FRD_ENTERED_DATE"
                                               select field.Value).First();
            if (fraudPopupEntity.FrdEnteredDate == null)
                fraudPopupEntity.FrdEnteredDate = string.Empty;

            return fraudPopupEntity;
        }

        public static UserIDSearchEntity SetUserIDSearchEntity(User user, dynamic queueList)
        {
            UserIDSearchEntity userIdSearchEntity = new UserIDSearchEntity();
            userIdSearchEntity.Queue1 = user.Queue1 ?? string.Empty;
            userIdSearchEntity.Queue2 = user.Queue2 ?? string.Empty;
            userIdSearchEntity.Queue3 = user.Queue3 ?? string.Empty;
            userIdSearchEntity.Queue4 = user.Queue4 ?? string.Empty;
            userIdSearchEntity.Queue5 = user.Queue5 ?? string.Empty;
            if (queueList.OrderQueue1 != null)
            {
                userIdSearchEntity.OrderQueue1 = (short)queueList.OrderQueue1;
            }

            if (queueList.OrderQueue2 != null)
            {
                userIdSearchEntity.OrderQueue2 = (short)queueList.OrderQueue2;
            }

            if (queueList.OrderQueue3 != null)
            {
                userIdSearchEntity.OrderQueue3 = (short)queueList.OrderQueue3;
            }

            if (queueList.OrderQueue4 != null)
            {
                userIdSearchEntity.OrderQueue4 = (short)queueList.OrderQueue4;
            }

            if (queueList.OrderQueue5 != null)
            {
                userIdSearchEntity.OrderQueue5 = (short)queueList.OrderQueue5;
            }

            userIdSearchEntity.FundingAuth = user.FundingAuth ?? string.Empty;
            userIdSearchEntity.CreditApplication = user.CreditApplication ?? string.Empty;
            userIdSearchEntity.CreditAnalysis = user.CreditAnalysis ?? string.Empty;
            userIdSearchEntity.LoanBooking = user.LoanBooking ?? string.Empty;
            userIdSearchEntity.BankLoanBooking = user.BankLoanBooking ?? string.Empty;
            userIdSearchEntity.ActivityCode = user.ActivityCode ?? string.Empty;
            userIdSearchEntity.GroupId = user.GroupId ?? string.Empty;
            userIdSearchEntity.TeamId = user.TeamID ?? string.Empty;
            userIdSearchEntity.UserRole = user.UsrC2CRole ?? string.Empty;
            var dupeusruserid = user.LoginUseridValue;
            if (!string.IsNullOrEmpty(dupeusruserid))
            {
                userIdSearchEntity.Dupeusruserid = dupeusruserid;
            }

            var dupeusrprofid = user.userProfId;
            if (!string.IsNullOrEmpty(dupeusrprofid))
            {
                userIdSearchEntity.Dupeusrprofid = dupeusrprofid;
            }

            var dupeusrrptau = user.UserAU;
            if (!string.IsNullOrEmpty(dupeusrrptau))
            {
                userIdSearchEntity.Dupeusrrptau = dupeusrrptau;
            }

            var dupeusrusername = user.UsrUserNameValue;
            if (!string.IsNullOrEmpty(dupeusrusername))
            {
                userIdSearchEntity.Dupeusrusername = dupeusrusername;
            }

            var dupeusruserphonearea = user.UsrUserPhoneAreaValue;
            if (!string.IsNullOrEmpty(dupeusruserphonearea))
            {
                userIdSearchEntity.Dupeusruserphonearea = dupeusruserphonearea;
            }

            var dupeusruserphoneprefix = user.UsrUserPhonePrefixValue;
            if (!string.IsNullOrEmpty(dupeusruserphoneprefix))
            {
                userIdSearchEntity.Dupeusruserphoneprefix = dupeusruserphoneprefix;
            }

            var dupeusruserphonecode = user.UsrUserPhoneCodeValue;
            if (!string.IsNullOrEmpty(dupeusruserphonecode))
            {
                userIdSearchEntity.Dupeusruserphonecode = dupeusruserphonecode;
            }

            var dupeusruserphoneextn = user.UserPhoneExt;
            if (!string.IsNullOrEmpty(dupeusruserphoneextn))
            {
                userIdSearchEntity.Dupeusruserphoneextn = dupeusruserphoneextn;
            }

            var dupeusruseremailaddr = user.UserEmail;
            if (!string.IsNullOrEmpty(dupeusruseremailaddr))
            {
                userIdSearchEntity.Dupeusruseremailaddr = dupeusruseremailaddr;
            }

            var dupeusruserhmdaauth = user.UserHmda;
            if (!string.IsNullOrEmpty(dupeusruserhmdaauth))
            {
                userIdSearchEntity.Dupeusruserhmdaauth = dupeusruserhmdaauth;
            }

            var dupeusrusercapabilitylevel = user.UsrCapabilityLevelValue;
            if (!string.IsNullOrEmpty(dupeusrusercapabilitylevel))
            {
                userIdSearchEntity.Dupeusrusercapabilitylevel = dupeusrusercapabilitylevel;
            }

            var dupeusrexclvl = user.UsrExcvl;
            if (!string.IsNullOrEmpty(dupeusrexclvl))
            {
                userIdSearchEntity.Dupeusrexclvl = dupeusrexclvl;
            }

            var dupeusremployeeaccessind = user.UsrEmployeeAccessIndValue;
            if (!string.IsNullOrEmpty(dupeusremployeeaccessind))
            {
                userIdSearchEntity.Dupeusremployeeaccessind = dupeusremployeeaccessind;
            }

            var dupeus2Wlexceptionlevel = user.UsrExceptionLevel;
            if (!string.IsNullOrEmpty(dupeus2Wlexceptionlevel))
            {
                userIdSearchEntity.Dupeus2wlexceptionlevel = dupeus2Wlexceptionlevel;
            }

            var dupeus2Wllocation1 = user.UsrWlLocation1;
            if (!string.IsNullOrEmpty(dupeus2Wllocation1))
            {
                userIdSearchEntity.Dupeus2wllocation1 = dupeus2Wllocation1;
            }

            var dupeus2Wllocation2 = user.UsrWlLocation2;
            if (!string.IsNullOrEmpty(dupeus2Wllocation2))
            {
                userIdSearchEntity.Dupeus2wllocation2 = dupeus2Wllocation2;
            }

            var dupeus2Wllocation3 = user.UsrWlLocation3;
            if (!string.IsNullOrEmpty(dupeus2Wllocation3))
            {
                userIdSearchEntity.Dupeus2wllocation3 = dupeus2Wllocation3;
            }

            var dupeus2Wllocation4 = user.UsrWlLocation4;
            if (!string.IsNullOrEmpty(dupeus2Wllocation4))
            {
                userIdSearchEntity.Dupeus2wllocation4 = dupeus2Wllocation4;
            }

            var dupeus2Wllocation5 = user.UsrWlLocation5;
            if (!string.IsNullOrEmpty(dupeus2Wllocation5))
            {
                userIdSearchEntity.Dupeus2wllocation5 = dupeus2Wllocation5;
            }

            var dupeus2Wllocation6 = user.UsrWlLocation6;
            if (!string.IsNullOrEmpty(dupeus2Wllocation6))
            {
                userIdSearchEntity.Dupeus2wllocation6 = dupeus2Wllocation6;
            }

            var dupeus2Wllocation7 = user.UsrWlLocation7;
            if (!string.IsNullOrEmpty(dupeus2Wllocation7))
            {
                userIdSearchEntity.Dupeus2wllocation7 = dupeus2Wllocation7;
            }

            var dupeus2Wllocation8 = user.UsrWlLocation8;
            if (!string.IsNullOrEmpty(dupeus2Wllocation8))
            {
                userIdSearchEntity.Dupeus2wllocation8 = dupeus2Wllocation8;
            }

            var dupeus2Wllocation9 = user.UsrWlLocation9;
            if (!string.IsNullOrEmpty(dupeus2Wllocation9))
            {
                userIdSearchEntity.Dupeus2wllocation9 = dupeus2Wllocation9;
            }

            var dupeus2Wllocation10 = user.UsrWlLocation10;
            if (!string.IsNullOrEmpty(dupeus2Wllocation10))
            {
                userIdSearchEntity.Dupeus2wllocation10 = dupeus2Wllocation10;
            }

            var dupeus2Wlwamid1 = user.UsrWlWamId1;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid1))
            {
                userIdSearchEntity.Dupeus2wlwamid1 = dupeus2Wlwamid1;
            }

            var dupeus2Wlwamid2 = user.UsrWlWamId2;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid2))
            {
                userIdSearchEntity.Dupeus2wlwamid2 = dupeus2Wlwamid2;
            }

            var dupeus2Wlwamid3 = user.UsrWlWamId3;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid3))
            {
                userIdSearchEntity.Dupeus2wlwamid3 = dupeus2Wlwamid3;
            }

            var dupeus2Wlwamid4 = user.UsrWlWamId4;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid4))
            {
                userIdSearchEntity.Dupeus2wlwamid4 = dupeus2Wlwamid4;
            }

            var dupeus2Wlwamid5 = user.UsrWlWamId5;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid5))
            {
                userIdSearchEntity.Dupeus2wlwamid5 = dupeus2Wlwamid5;
            }

            var dupeus2Wlwamid6 = user.UsrWlWamId6;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid6))
            {
                userIdSearchEntity.Dupeus2wlwamid6 = dupeus2Wlwamid6;
            }

            var dupeus2Wlwamid7 = user.UsrWlWamId7;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid7))
            {
                userIdSearchEntity.Dupeus2wlwamid7 = dupeus2Wlwamid7;
            }

            var dupeus2Wlwamid8 = user.UsrWlWamId8;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid8))
            {
                userIdSearchEntity.Dupeus2wlwamid8 = dupeus2Wlwamid8;
            }

            var dupeus2Wlwamid9 = user.UsrWlWamId9;
            if (!string.IsNullOrEmpty(dupeus2Wlwamid9))
            {
                userIdSearchEntity.Dupeus2wlwamid9 = dupeus2Wlwamid9;
            }

            return userIdSearchEntity;
        }

        public static List<WorkListSearchEntity> SetWorkListEntity(WorkListSearchRes workListRes)
        {
            var workListsearchEntityList = new List<WorkListSearchEntity>();
            WorkListSearchEntity workListSearchEntity = null;
            for (var i = 0; i <= workListRes.WorkListSearchResults.Count - 1; i++)
            {
                workListSearchEntity = new WorkListSearchEntity
                {
                    StateCode = workListRes.WorkListSearchResults[i].StateCode,
                    StateName = workListRes.WorkListSearchResults[i].StateName,
                    UserId = workListRes.WorkListSearchResults[i].UserId,
                    UserName = workListRes.WorkListSearchResults[i].UserName,
                    CarryOverCnt = workListRes.WorkListSearchResults[i].CarryOverCnt,
                    NewEntryCnt = workListRes.WorkListSearchResults[i].NewEntryCnt,
                    TotalListCnt = workListRes.WorkListSearchResults[i].TotalListCnt,
                    WorkedCnt = workListRes.WorkListSearchResults[i].WorkedCnt,
                    PendingCnt = workListRes.WorkListSearchResults[i].PendingCnt,
                    WaitTime = workListRes.WorkListSearchResults[i].WaitTime
                };
                workListsearchEntityList.Add(workListSearchEntity);
            }
            return workListsearchEntityList;
        }


        public static string GetAcapsField(CUSPApplicationData appData, string acapFieldKey)
        {
            if (acapFieldKey == null)
            {
                throw new ArgumentNullException("acapFieldKey");
            }
            var value = string.Empty;
            if (appData.AcapFields.ContainsKey(acapFieldKey))
            {
                value = ((appData.AcapFields[acapFieldKey])).Value;
            }

            return value;
        }

        public static IEnumerable<FieldAttribute> GetFieldsFromJsonText(string jsonText)
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
                    throw;
                }
            }

            return fieldList;
        }

        #endregion
    }
}