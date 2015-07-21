// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserResponseParser.cs" company="">
//   
// </copyright>
// <summary>
//   The user response parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP.Parsers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using WF.EAI.Data.sif.Services.CWS.Login;
    using WF.EAI.Entities.domain.c2c;
    using WF.EAI.Entities.exception;

    /// <summary>
    ///     The user response parser.
    /// </summary>
    public class UserResponseParser
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get search grid data.
        /// </summary>
        /// <param name="resXmlStr">
        /// The res xml str.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<User.UserIdSearchGridEntity> GetSearchGridData(string resXmlStr)
        {
            var entityList = new List<User.UserIdSearchGridEntity>();

            const string XPath = "/ACAPS01/Body/Data/fields/field[@name='";
            var xml = new XmlDocument();
            xml.LoadXml(resXmlStr);

            User.UserIdSearchGridEntity entity;
            for (var i = 1; i < 17; i++)
            {
                entity = new User.UserIdSearchGridEntity
                             {
                                 UsrDiscLimitProdCode =
                                     xml.SelectSingleNode(
                                         XPath + "USR_DISC_LIMIT_PROD_CODE_" + i + "']")
                                        .InnerText, 
                                 USRDiscLimitAmt =
                                     xml.SelectSingleNode(
                                         XPath + "USR_DISC_LIMIT_AMT_" + i + "']").InnerText, 
                                 UsrDiscLimitPolInd =
                                     xml.SelectSingleNode(
                                         XPath + "USR_DISC_LIMIT_POL_IND_" + i + "']")
                                        .InnerText, 
                                 UsrDiscLimitOvrdInd =
                                     xml.SelectSingleNode(
                                         XPath + "USR_DISC_LIMIT_OVRD_IND_" + i + "']")
                                        .InnerText, 
                                 UsrWlAmt =
                                     xml.SelectSingleNode(
                                         XPath + "US2_WL_AMOUNT_" + i + "']").InnerText
                             };

                if (entity.UsrDiscLimitProdCode == string.Empty && entity.USRDiscLimitAmt == string.Empty
                    && entity.UsrDiscLimitPolInd == string.Empty && entity.UsrDiscLimitOvrdInd == string.Empty
                    && entity.UsrWlAmt == string.Empty)
                {
                    continue;
                }

                entityList.Add(entity);
            }

            return entityList;
        }

        /// <summary>
        /// The set user info.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="maskLoginResponse">
        /// The mask login response.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public static User SetUserInfo(User user, MaskLoginResponse maskLoginResponse)
        {
            user.LoginUseridValue = maskLoginResponse.applicationID.Trim();
            user.LoginPasswordValue = string.Empty;
            user.LoginRegionValue = maskLoginResponse.location_code.Trim();
            user.AcapsSessionValue = maskLoginResponse.acaps_session.Trim();
            try
            {
                user.UsrCapabilityLevelValue =
                    (from field in maskLoginResponse.fields[0].field
                     where field.name == "USR_CAPABILITY_LEVEL"
                     select field.Value).First();
            }
            catch
            {
                throw new BusinessLayerException(maskLoginResponse.message.First().error);
            }

            user.UsrEmployeeAccessIndValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_EMPLOYEE_ACCESS_IND"
                 select field.Value).First();

            user.UsrUserNameValue =
                (from field in maskLoginResponse.fields[0].field where field.name == "USR_USER_NAME" select field.Value)
                    .First();

            user.ManagerControlUpdatesIndicator =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_MNGR_CONTROL_UPDTS_IND"
                 select field.Value).First();

            user.UsrRptCompanyIDValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_RPT_COMPANY_ID"
                 select field.Value).First();

            user.UsrRptDivisionIDValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_RPT_DIVISION_ID"
                 select field.Value).First();

            user.UsrRptServiceCtrIDValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_RPT_SERVICE_CTR_ID"
                 select field.Value).First();

            user.UsrViewBundletask =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_VIEW_BUNDLETASK"
                 select field.Value).First();
            user.UsrUserPhoneAreaValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_USER_PHONE_AREA"
                 select field.Value).First();
            user.UsrUserPhonePrefixValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_USER_PHONE_PREFIX"
                 select field.Value).First();
            user.UsrUserPhoneCodeValue =
                (from field in maskLoginResponse.fields[0].field
                 where field.name == "USR_USER_PHONE_CODE"
                 select field.Value).First();
            return user;
        }

        /// <summary>
        /// The set user info xml.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="resXmlStr">
        /// The res xml str.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public static User SetUserInfoXml(User user, string resXmlStr)
        {
            const string FIELD_XPATH = "/ACAPS01/Body/Data/fields/field";
            const string APPLICATION_ID_PATH = "/ACAPS01/Body/Data/applicationID";
            const string LOCATION_CODE_PATH = "/ACAPS01/Body/Data/location_code";
            const string ACAPS_SESSION_PATH = "/ACAPS01/Body/Data/acaps_session";
            const string USR_CAPABILITY_LEVEL = "USR_CAPABILITY_LEVEL";
            const string USR_EMPLOYEE_ACCESS_IND = "USR_EMPLOYEE_ACCESS_IND";
            const string USR_USER_NAME = "USR_USER_NAME";
            const string USR_MNGR_CONTROL_UPDTS_IND = "USR_MNGR_CONTROL_UPDTS_IND";
            const string USR_RPT_COMPANY_ID = "USR_RPT_COMPANY_ID";
            const string USR_RPT_DIVISION_ID = "USR_RPT_DIVISION_ID";
            const string USR_RPT_SERVICE_CTR_ID = "USR_RPT_SERVICE_CTR_ID";
            const string USR_VIEW_BUNDLETASK = "USR_VIEW_BUNDLETASK";
            const string USR_USER_PHONE_AREA = "USR_USER_PHONE_AREA";
            const string USR_USER_PHONE_PREFIX = "USR_USER_PHONE_PREFIX";
            const string USR_USER_PHONE_CODE = "USR_USER_PHONE_CODE";
            const string USR_EXCLVL = "USR_EXCLVL";
            const string USR_PROFID = "USR_PROFID";
            const string US2_WL_EXCEPTION_LEVEL = "US2_WL_EXCEPTION_LEVEL";
            const string US2_WL_LOCATION_1 = "US2_WL_LOCATION_1";
            const string US2_WL_LOCATION_2 = "US2_WL_LOCATION_2";
            const string US2_WL_LOCATION_3 = "US2_WL_LOCATION_3";
            const string US2_WL_LOCATION_4 = "US2_WL_LOCATION_4";
            const string US2_WL_LOCATION_5 = "US2_WL_LOCATION_5";
            const string US2_WL_LOCATION_6 = "US2_WL_LOCATION_6";
            const string US2_WL_LOCATION_7 = "US2_WL_LOCATION_7";
            const string US2_WL_LOCATION_8 = "US2_WL_LOCATION_8";
            const string US2_WL_LOCATION_9 = "US2_WL_LOCATION_9";
            const string US2_WL_LOCATION_10 = "US2_WL_LOCATION_10";
            const string US2_WL_WAM_ID_1 = "US2_WL_WAM_ID_1";
            const string US2_WL_WAM_ID_2 = "US2_WL_WAM_ID_2";
            const string US2_WL_WAM_ID_3 = "US2_WL_WAM_ID_3";
            const string US2_WL_WAM_ID_4 = "US2_WL_WAM_ID_4";
            const string US2_WL_WAM_ID_5 = "US2_WL_WAM_ID_5";
            const string US2_WL_WAM_ID_6 = "US2_WL_WAM_ID_6";
            const string US2_WL_WAM_ID_7 = "US2_WL_WAM_ID_7";
            const string US2_WL_WAM_ID_8 = "US2_WL_WAM_ID_8";
            const string US2_WL_WAM_ID_9 = "US2_WL_WAM_ID_9";
            const string USR_RPT_AU = "USR_RPT_AU";
            const string USR_USER_PHONE_EXTN = "USR_USER_PHONE_EXTN";
            const string USR_USER_EMAIL_ADDR = "USR_USER_EMAIL_ADDR";
            const string USR_HMDA_AUTH = "USR_HMDA_AUTH";
            const string US2_DA_GETNEXT_QUEUE_1 = "US2_DA_GETNEXT_QUEUE_1";
            const string US2_DA_GETNEXT_QUEUE_2 = "US2_DA_GETNEXT_QUEUE_2";
            const string US2_DA_GETNEXT_QUEUE_3 = "US2_DA_GETNEXT_QUEUE_3";
            const string US2_DA_GETNEXT_QUEUE_4 = "US2_DA_GETNEXT_QUEUE_4";
            const string US2_DA_GETNEXT_QUEUE_5 = "US2_DA_GETNEXT_QUEUE_5";
            const string US2_ROLE = "US2_ROLE";
            const string US2_DIRECT_AUTO_FNDAUTH = "US2_DIRECT_AUTO_FNDAUTH";
            const string US2_DIRECT_AUTO_CRAPP = "US2_DIRECT_AUTO_CRAPP";
            const string US2_DIRECT_AUTO_CRANA = "US2_DIRECT_AUTO_CRANA";
            const string US2_DIRECT_AUTO_LOANBK = "US2_DIRECT_AUTO_LOANBK";
            const string US2_DIRECT_AUTO_CNTRBK = "US2_DIRECT_AUTO_CNTRBK";
            const string US2_DIRECT_AUTO_ACTCD = "US2_DIRECT_AUTO_ACTCD";
            const string US2_DIRECT_AUTO_GROUP = "US2_DIRECT_AUTO_GROUP";
            const string USR_USER_TEAM_ID = "USR_USER_TEAM_ID";
            const string US2_BUS_ACCESS_ID_1 = "US2_BUS_ACCESS_ID_1";
            const string US2_BUS_ACCESS_ID_2 = "US2_BUS_ACCESS_ID_2";
            const string US2_BUS_ACCESS_ID_3 = "US2_BUS_ACCESS_ID_3";
            const string US2_BUS_ACCESS_ID_4 = "US2_BUS_ACCESS_ID_4";
            const string US2_BUS_ACCESS_ID_5 = "US2_BUS_ACCESS_ID_5";
            const string US2_BUS_ACCESS_ID_6 = "US2_BUS_ACCESS_ID_6";
            const string US2_BUS_ACCESS_ID_7 = "US2_BUS_ACCESS_ID_7";
            const string US2_BUS_ACCESS_ID_8 = "US2_BUS_ACCESS_ID_8";
            const string US2_BUS_ACCESS_ID_9 = "US2_BUS_ACCESS_ID_9";
            const string US2_BUS_ACCESS_ID_10 = "US2_BUS_ACCESS_ID_10";
            const string US2_AUTHORITY_LEVEL = "US2_AUTHORITY_LEVEL";

            //Added Tags required for maturing tool 
            const string US2_MLO_NMLSR_ID = "US2_MLO_NMLSR_ID";
            const string US2_MLO_FIRST_NAME = "US2_MLO_FIRST_NAME";
            const string US2_MLO_MIDDLE_NAME = "US2_MLO_MIDDLE_NAME";
            const string US2_MLO_LAST_NAME = "US2_MLO_LAST_NAME";

            var currentXPath = string.Empty;
            var theValue = string.Empty;
            var name = string.Empty;
            var length = string.Empty;
            var protection = string.Empty;
            var pageNumber = string.Empty;

            using (var xmlReader = new XmlTextReader(new StringReader(resXmlStr)))
            {
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            currentXPath += "/" + xmlReader.Name;
                            if (xmlReader.IsEmptyElement)
                            {
                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                theValue = string.Empty;
                            }
                            else if (string.Compare(currentXPath, FIELD_XPATH, true) == 0)
                            {
                                name = xmlReader.GetAttribute("name").Trim();
                                length = xmlReader.GetAttribute("length").Trim();
                                protection = xmlReader.GetAttribute("protected");
                                pageNumber = xmlReader.GetAttribute("page_number").Trim();
                            }

                            break;

                        case XmlNodeType.Text:
                            theValue = xmlReader.Value.Trim('_');
                            break;

                        case XmlNodeType.CDATA:
                            theValue = xmlReader.Value.Trim('_');
                            break;

                        case XmlNodeType.EndElement:

                            if (string.Compare(currentXPath, APPLICATION_ID_PATH, true) == 0)
                            {
                                user.LoginUseridValue = theValue;
                            }
                            else if (string.Compare(currentXPath, LOCATION_CODE_PATH, true) == 0)
                            {
                                user.LoginRegionValue = theValue;
                            }
                            else if (string.Compare(currentXPath, ACAPS_SESSION_PATH, true) == 0)
                            {
                                user.AcapsSessionValue = theValue;
                            }
                            else if (string.Compare(currentXPath, FIELD_XPATH, true) == 0)
                            {
                                switch (name)
                                {
                                    case USR_CAPABILITY_LEVEL:
                                        user.UsrCapabilityLevelValue = theValue;
                                        break;

                                    case USR_EMPLOYEE_ACCESS_IND:
                                        user.UsrEmployeeAccessIndValue = theValue;
                                        break;

                                    case USR_USER_NAME:
                                        user.UsrUserNameValue = theValue;
                                        break;

                                    case USR_MNGR_CONTROL_UPDTS_IND:
                                        user.ManagerControlUpdatesIndicator = theValue;
                                        break;

                                    case USR_RPT_COMPANY_ID:
                                        user.UsrRptCompanyIDValue = theValue;
                                        break;

                                    case USR_RPT_DIVISION_ID:
                                        user.UsrRptDivisionIDValue = theValue;
                                        break;

                                    case USR_RPT_SERVICE_CTR_ID:
                                        user.UsrRptServiceCtrIDValue = theValue;
                                        break;

                                    case USR_VIEW_BUNDLETASK:
                                        user.UsrViewBundletask = theValue;
                                        break;

                                    case USR_USER_PHONE_AREA:
                                        user.UsrUserPhoneAreaValue = theValue;
                                        break;

                                    case USR_USER_PHONE_PREFIX:
                                        user.UsrUserPhonePrefixValue = theValue;
                                        break;

                                    case USR_USER_PHONE_CODE:
                                        user.UsrUserPhoneCodeValue = theValue;
                                        break;

                                    case USR_EXCLVL:
                                        user.UsrExcvl = theValue;
                                        break;
                                    case US2_WL_EXCEPTION_LEVEL:
                                        user.UsrExceptionLevel = theValue;
                                        break;
                                    case US2_WL_LOCATION_1:
                                        user.UsrWlLocation1 = theValue;
                                        break;
                                    case US2_WL_LOCATION_2:
                                        user.UsrWlLocation2 = theValue;
                                        break;
                                    case US2_WL_LOCATION_3:
                                        user.UsrWlLocation3 = theValue;
                                        break;
                                    case US2_WL_LOCATION_4:
                                        user.UsrWlLocation4 = theValue;
                                        break;
                                    case US2_WL_LOCATION_5:
                                        user.UsrWlLocation5 = theValue;
                                        break;
                                    case US2_WL_LOCATION_6:
                                        user.UsrWlLocation6 = theValue;
                                        break;
                                    case US2_WL_LOCATION_7:
                                        user.UsrWlLocation7 = theValue;
                                        break;
                                    case US2_WL_LOCATION_8:
                                        user.UsrWlLocation8 = theValue;
                                        break;
                                    case US2_WL_LOCATION_9:
                                        user.UsrWlLocation9 = theValue;
                                        break;
                                    case US2_WL_LOCATION_10:
                                        user.UsrWlLocation10 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_1:
                                        user.UsrWlWamId1 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_2:
                                        user.UsrWlWamId2 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_3:
                                        user.UsrWlWamId3 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_4:
                                        user.UsrWlWamId4 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_5:
                                        user.UsrWlWamId5 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_6:
                                        user.UsrWlWamId6 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_7:
                                        user.UsrWlWamId7 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_8:
                                        user.UsrWlWamId8 = theValue;
                                        break;
                                    case US2_WL_WAM_ID_9:
                                        user.UsrWlWamId9 = theValue;
                                        break;
                                    case USR_PROFID:
                                        user.userProfId = theValue;
                                        break;
                                    case USR_RPT_AU:
                                        user.UserAU = theValue;
                                        break;
                                    case USR_USER_PHONE_EXTN:
                                        user.UserPhoneExt = theValue;
                                        break;
                                    case USR_USER_EMAIL_ADDR:
                                        user.UserEmail = theValue;
                                        break;
                                    case USR_HMDA_AUTH:
                                        user.UserHmda = theValue;
                                        break;
                                    case US2_DA_GETNEXT_QUEUE_1:
                                        user.Queue1 = theValue;
                                        break;
                                    case US2_DA_GETNEXT_QUEUE_2:
                                        user.Queue2 = theValue;
                                        break;
                                    case US2_DA_GETNEXT_QUEUE_3:
                                        user.Queue3 = theValue;
                                        break;
                                    case US2_DA_GETNEXT_QUEUE_4:
                                        user.Queue4 = theValue;
                                        break;
                                    case US2_DA_GETNEXT_QUEUE_5:
                                        user.Queue5 = theValue;
                                        break;
                                    case US2_ROLE:
                                        user.UsrC2CRole = theValue;
                                        user.Role = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_FNDAUTH:
                                        user.FundingAuth = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_CRAPP:
                                        user.CreditApplication = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_CRANA:
                                        user.CreditAnalysis = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_LOANBK:
                                        user.LoanBooking = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_CNTRBK:
                                        user.BankLoanBooking = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_ACTCD:
                                        user.ActivityCode = theValue;
                                        break;
                                    case US2_DIRECT_AUTO_GROUP:
                                        user.GroupId = theValue;
                                        break;
                                    case USR_USER_TEAM_ID:
                                        user.TeamID = theValue;
                                        break;
                                    case  US2_BUS_ACCESS_ID_1:
                                        user.BusinessAccessId1 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_2:
                                        user.BusinessAccessId2 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_3:
                                        user.BusinessAccessId3 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_4:
                                        user.BusinessAccessId4 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_5:
                                        user.BusinessAccessId5 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_6:
                                        user.BusinessAccessId6 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_7:
                                        user.BusinessAccessId7 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_8:
                                        user.BusinessAccessId8 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_9:
                                        user.BusinessAccessId9 = theValue;
                                        break;
                                    case US2_BUS_ACCESS_ID_10:
                                        user.BusinessAccessId10 = theValue;
                                        break;
                                    case US2_AUTHORITY_LEVEL:
                                        user.Us2AuthorityLevel = theValue;
                                        break;


                                    case US2_MLO_NMLSR_ID:
                                        user.MLOID = theValue;
                                        break;

                                    case US2_MLO_FIRST_NAME:
                                        user.MLOFirstName = theValue;
                                        break;
                                    case US2_MLO_MIDDLE_NAME:
                                        user.MLOMiddleName = theValue;
                                        break;
                                    case US2_MLO_LAST_NAME:
                                        user.MLOLasttName = theValue;
                                        break;

                                }
                            }

                            // Adjust current XPath
                            currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                            theValue = string.Empty;
                            break;
                    }
                }
            }

            SetBusinessAccessForUser(user);

            return user;
        }


        public static void SetBusinessAccessForUser(User user)
        {
            if (user.BusinessAccessId1 == "M" || user.BusinessAccessId2 == "M" || user.BusinessAccessId3 == "M"
                || user.BusinessAccessId4 == "M" || user.BusinessAccessId5 == "M" || user.BusinessAccessId6 == "M" ||
            user.BusinessAccessId7 == "M" || user.BusinessAccessId8 == "M" ||
            user.BusinessAccessId9 == "M" || user.BusinessAccessId10 == "M")
            {
                user.RetailerAccess = true;
            }

            if (user.BusinessAccessId1 == "A" || user.BusinessAccessId2 == "A" || user.BusinessAccessId3 == "A"
                || user.BusinessAccessId4 == "A" || user.BusinessAccessId5 == "A" || user.BusinessAccessId6 == "A" ||
            user.BusinessAccessId7 == "A" || user.BusinessAccessId8 == "A" ||
            user.BusinessAccessId9 == "A" || user.BusinessAccessId10 == "A")
            {
                user.DirectAutoAccess = true;
            }

            if (user.BusinessAccessId1 == "P" || user.BusinessAccessId2 == "P" || user.BusinessAccessId3 == "P"
               || user.BusinessAccessId4 == "P" || user.BusinessAccessId5 == "P" || user.BusinessAccessId6 == "P" ||
           user.BusinessAccessId7 == "P" || user.BusinessAccessId8 == "P" ||
           user.BusinessAccessId9 == "P" || user.BusinessAccessId10 == "P")
            {
                user.PLLAccess = true;
            }

            if (user.BusinessAccessId1 == "H" || user.BusinessAccessId2 == "H" || user.BusinessAccessId3 == "H"
              || user.BusinessAccessId4 == "H" || user.BusinessAccessId5 == "H" || user.BusinessAccessId6 == "H" ||
          user.BusinessAccessId7 == "H" || user.BusinessAccessId8 == "H" ||
          user.BusinessAccessId9 == "H" || user.BusinessAccessId10 == "H")
            {
                user.HEQAccess = true;
            }

            //Adding the user group as "O" for HE Maturity Tool Option User

            if (user.BusinessAccessId1 == "E" || user.BusinessAccessId2 == "E" || user.BusinessAccessId3 == "E"
             || user.BusinessAccessId4 == "E" || user.BusinessAccessId5 == "E" || user.BusinessAccessId6 == "E" ||
         user.BusinessAccessId7 == "E" || user.BusinessAccessId8 == "E" ||
         user.BusinessAccessId9 == "E" || user.BusinessAccessId10 == "E")
            {
                user.HEMaturingToolAccess = true;
            }

            if (user.DirectAutoAccess == false && user.PLLAccess == false && user.RetailerAccess == false &&
                user.HEQAccess == false)
            {
                user.DirectAutoAccess = true; // to ensure, Direct Auto user can still access Direct auto search if the flag is not set for them.
            }
        }

        #endregion
    }



}