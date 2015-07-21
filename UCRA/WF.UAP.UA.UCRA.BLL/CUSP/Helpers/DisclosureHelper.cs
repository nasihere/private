using System;
using System.Text;

using WF.EAI.Entities.domain.cusp.UI;

namespace WF.EAI.BLL.CUSP.Helpers
{
    public class DisclosureHelper
    {
        private string _userId;
        private string _strDate = DateTime.Now.Date.ToString("MM/dd/yy"); //DateTime.Now.Date.ToString("yy/MM/dd")
        private string _strTime = DateTime.Now.ToString("hh:mm:ss");
        private DisclosureEntity.DisclosureSelection _disclSelection;
        private string _sReadInd;
        private string _sQuesInd;

        public DisclosureHelper(string updByUserId, DisclosureEntity.DisclosureSelection disclSelection)
        {
            _userId = updByUserId;
            _disclSelection = disclSelection;
        }


        public DisclosureHelper(string updByUserId)
        {
            _userId = updByUserId;
        }


        string fPageNum = "1";
        string originalValue = "N";

        /// <summary>
        /// Gets Json Text for saving Main Disclosure
        /// </summary>
        /// <returns></returns>
        public string GetSaveJsonForMainDisclosure()
        {
            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            //Call Mon
            jsonStrw.Append(GetSaveJsonForCallMonitoring(_disclSelection.CallMonReadInd));

            // Authentication
            jsonStrw.Append(GetSaveJsonForCallAuth(_disclSelection.CallAuthReadInd));

            // Reg O - App
            if (!string.IsNullOrEmpty(_disclSelection.RegOAppQuesInd))
                jsonStrw.Append(GetSaveJsonForAppRegO(_disclSelection.RegOAppQuesInd));

            // Reg O - CoApp
            if (
                _disclSelection.IsJointApp &&
                (!string.IsNullOrEmpty(_disclSelection.RegOCoAppQuesInd))
               )
                jsonStrw.Append(GetSaveJsonForCoAppRegO(_disclSelection.RegOCoAppQuesInd));

            // SCRA
            jsonStrw.Append(GetSaveJsonForScra(_disclSelection.ScraQuesInd));

            // TILA
            jsonStrw.Append(GetSaveJsonForTila(_disclSelection.TilaDisclQuesInd));

            // Email
            jsonStrw.Append(GetSaveJsonForEmail(_disclSelection.EmailQuesInd));

            //Spanish Negotiation
            jsonStrw.Append(GetSaveJsonForSpanishNegotiation(_disclSelection.SpanNegotReadInd));

            //Generic Decline
            jsonStrw.Append(GetSaveJsonForGenericDecline(_disclSelection.GenDeclScriptReadInd));

            //Income Disclosure
            jsonStrw.Append(GetSaveJsonForIncome(_disclSelection.IncomeDisclReadInd));

            //Income Disclosure
            jsonStrw.Append(GetSaveJsonForCip(_disclSelection.CipReadInd,"A"));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();
        }

        /// <summary>
        /// returns Json for saving Call Monitoring and authentication
        /// </summary>
        /// <returns></returns>
        public string GetSaveJsonForCallMonAuth()
        {
            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            //Call Mon
            jsonStrw.Append(GetSaveJsonForCallMonitoring(_disclSelection.CallMonReadInd));

            // Authentication
            jsonStrw.Append(GetSaveJsonForCallAuth(_disclSelection.CallAuthReadInd));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();
        }

        /// <summary>
        /// returns Json for saving Call Monitoring and authentication
        /// </summary>
        /// <returns></returns>
        public string GetSaveJsonForEmailDisclosure(string sQuesInd)
        {
            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            // Authentication
            jsonStrw.Append(GetSaveJsonForEmail(sQuesInd));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();
        }
        /// <summary>
        /// Gets Json Text for Call Monitoring Disclosure information
        /// </summary>
        /// <param name="isReadInd"></param>
        /// <returns></returns>
        private string GetSaveJsonForCallMonitoring(string sReadInd)
        {
            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD3_CALL_MONTR_DISC_CMPLT",
                CompleteUidField = "AD3_CALL_MONTR_DISC_CMPLT_ID",
                CompleteDateField="AD3_CALL_MONTR_DISC_CMPLT_DT",
                CompleteTimeField ="AD3_CALL_MONTR_DISC_CMPLT_TM"
            };

            return GetSaveJson(fieldMapping,sReadInd);
        }

        /// <summary>
        /// Get Json For Call Auth
        /// </summary>
        /// <param name="isReadInd"></param>
        /// <returns></returns>
        private string GetSaveJsonForCallAuth(string sReadInd)
        {
            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD3_CUST_AUTH_DISC_CMPLT",
                CompleteUidField = "AD3_CUST_AUTH_DISC_CMPLT_ID",
                CompleteDateField="AD3_CUST_AUTH_DISC_CMPLT_DT",
                CompleteTimeField ="AD3_CUST_AUTH_DISC_CMPLT_TM"
            };

            return GetSaveJson(fieldMapping,sReadInd);
        }

        /// <summary>
        /// Get Json For Reg O App
        /// </summary>
        /// <param name="strYesNo"></param>
        /// <returns></returns>
        private string GetSaveJsonForAppRegO(string strYesNo)
        {
            string readCurrentValue = "N";

            if ((strYesNo == "Y") || (strYesNo == "N"))
                readCurrentValue = "Y";

            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD5_REG_O_DISC_CMPLT_P",
                CompleteUidField = "AD5_REG_O_DISC_CMPLT_ID_P",
                CompleteDateField = "AD5_REG_O_DISC_CMPLT_DT_P",
                CompleteTimeField = "AD5_REG_O_DISC_CMPLT_TM_P",
                QuesIndField = "AD5_REG_O_DISC_QUES_IND_P"   
            };

            return GetSaveJson(fieldMapping,readCurrentValue,strYesNo);
        }

        /// <summary>
        /// Get Json For CoApp Reg O
        /// </summary>
        /// <param name="strYesNo"></param>
        /// <returns></returns>
        private string GetSaveJsonForCoAppRegO(string strYesNo)
        {
            string readCurrentValue = "N";

            if ((strYesNo == "Y") || (strYesNo == "N"))
                readCurrentValue = "Y";

            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_REG_O_DISC_CMPLT_S",
                CompleteUidField = "AD5_REG_O_DISC_CMPLT_ID_S",
                CompleteDateField = "AD5_REG_O_DISC_CMPLT_DT_S",
                CompleteTimeField = "AD5_REG_O_DISC_CMPLT_TM_S",
                QuesIndField = "AD5_REG_O_DISC_QUES_IND_S"
            };

            return GetSaveJson(fieldMapping, readCurrentValue, strYesNo);
        }

        /// <summary>
        /// Get Json For saving Email Disclosure
        /// </summary>
        /// <param name="strYesNo"></param>
        /// <returns></returns>
        private string GetSaveJsonForEmail(string strYesNo)
        {
            string readCurrentValue = "X"; //Disclosure Not Applicable

            if ((strYesNo == "Y") || (strYesNo == "N"))
                readCurrentValue = "Y";

            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD5_EMAIL_DISC_CMPLT",
                CompleteUidField = "AD5_EMAIL_DISC_CMPLT_ID",
                CompleteDateField="AD5_EMAIL_DISC_CMPLT_DT",
                CompleteTimeField ="AD5_EMAIL_DISC_CMPLT_TM" ,
                QuesIndField = "AD5_EMAIL_DISC_QUES_IND"   
            };
            return GetSaveJson(fieldMapping, readCurrentValue, strYesNo);
        }

        /// <summary>
        /// Get Json For saving Spanish Negotiation
        /// </summary>
        /// <param name="isReadInd"></param>
        /// <returns></returns>
        private string GetSaveJsonForSpanishNegotiation(string sReadInd)
        {
            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD5_SPAN_NEGO_DECL_DISC_CMPLT",
                CompleteUidField = "AD5_SPAN_NEGO_DECL_DISC_CMPLT_ID",
                CompleteDateField="AD5_SPAN_NEGO_DECL_DISC_CMPLT_DT",
                CompleteTimeField ="AD5_SPAN_NEGO_DECL_DISC_CMPLT_TM"                    
            };

            return GetSaveJson(fieldMapping,sReadInd);
        }

        /// <summary>
        /// Get Json For saving Generic Decline Script
        /// </summary>
        /// <param name="isReadInd"></param>
        /// <returns></returns>
        private string GetSaveJsonForGenericDecline(string sReadInd)
        {
            var fieldMapping = new DisclosureMapping {
                        CompleteIndField = "AD5_GENE_DECL_DISC_CMPLT",
                        CompleteUidField = "AD5_GENE_DECL_DISC_CMPLT_ID",
                        CompleteDateField="AD5_GENE_DECL_DISC_CMPLT_DT",
                        CompleteTimeField ="AD5_GENE_DECL_DISC_CMPLT_TM"                    
            };

            return GetSaveJson(fieldMapping, sReadInd);
        }

        /// <summary>
        /// Get Json For saving Income
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <returns></returns>
        private string GetSaveJsonForIncome(string sReadInd)
        {
            var fieldMapping = new DisclosureMapping {
                CompleteIndField = "AD5_OTH_INC_DISC_CMPLT",
                CompleteUidField = "AD5_OTH_INC_DISC_CMPLT_ID",
                CompleteDateField="AD5_OTH_INC_DISC_CMPLT_DT",
                CompleteTimeField ="AD5_OTH_INC_DISC_CMPLT_TM"
            };

            return GetSaveJson(fieldMapping,sReadInd);
        }

        /// <summary>
        /// Get Json For Reg O App
        /// </summary>
        /// <param name="strYesNo"></param>
        /// <returns></returns>
        private string GetSaveJsonForCip(string sReadInd,  string appOrCoApp)
        {
            DisclosureMapping fieldMapping;

            if (appOrCoApp == "A")
            {
                fieldMapping = new DisclosureMapping
                {
                    CompleteIndField = "AD5_DA_CIP_DISC_CMPLT_P",
                    CompleteUidField = "AD5_DA_CIP_DISC_CMPLT_ID_P",
                    CompleteDateField = "AD5_DA_CIP_DISC_CMPLT_DT_P",
                    CompleteTimeField = "AD5_DA_CIP_DISC_CMPLT_TM_P"
                };
            }
            else
            {
                fieldMapping = new DisclosureMapping
                {
                    CompleteIndField = "AD5_DA_CIP_DISC_CMPLT_S",
                    CompleteUidField = "AD5_DA_CIP_DISC_CMPLT_ID_S",
                    CompleteDateField = "AD5_DA_CIP_DISC_CMPLT_DT_S",
                    CompleteTimeField = "AD5_DA_CIP_DISC_CMPLT_TM_S"
                };
            }

            return GetSaveJson(fieldMapping, sReadInd);
        }
        /// <summary>
        ///  Get Save Json For SCRA
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <param name="sMilitary"></param>
        /// <returns></returns>
        public string GetSaveJsonForScra(string sMilitary)
        {
            string sReadInd = "N";

            if (!string.IsNullOrEmpty(sMilitary))
                sReadInd = "Y";
            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_SCRA_DISC_CMPLT",
                CompleteUidField = "AD5_SCRA_DISC_CMPLT_ID",
                CompleteDateField = "AD5_SCRA_DISC_CMPLT_DT",
                CompleteTimeField = "AD5_SCRA_DISC_CMPLT_TM",
                QuesIndField = "AD5_SCRA_DISC_QUES_IND"
            };

            return GetSaveJson(fieldMapping, sReadInd,sMilitary);
        }

        /// <summary>
        ///  Get Save Json For SCRA
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <param name="sMilitary"></param>
        /// <returns></returns>
        public string GetSaveJsonForTila(string sYesOrNo)
        {
            string sReadInd = "N";

            if (!string.IsNullOrEmpty(sYesOrNo))
                sReadInd = "Y";
            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_TILA_DISC_CMPLT",
                CompleteUidField = "AD5_TILA_DISC_CMPLT_ID",
                CompleteDateField = "AD5_TILA_DISC_CMPLT_DT",
                CompleteTimeField = "AD5_TILA_DISC_CMPLT_TM",
                QuesIndField = "AD5_TILA_DISC_QUES_IND"
            };

            return GetSaveJson(fieldMapping, sReadInd, sYesOrNo);
        }


        /// <summary>
        ///  Get Save Json For SCRA
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <param name="sMilitary"></param>
        /// <returns></returns>
        public string GetSaveJsonForScraOnly(DisclosureEntity.DisclosureSelection disclSelection)
        {
            string sReadInd = "N";
            string sQuesInd = "N";
            if (disclSelection.ScraQuesInd == "Y")
            {
                if ((disclSelection.IsScraQuesApp) && (disclSelection.IsScraQuesCoApp))
                    sQuesInd = "B"; //both App and Coapp answered yes to SCRA
                else if (disclSelection.IsScraQuesApp)
                    sQuesInd = "P"; //App answered yes to SCRA
                else if (disclSelection.IsScraQuesCoApp)
                    sQuesInd = "S"; //Co-App answered yes to SCRA
                else
                    sQuesInd = "N"; //Not answered
            }
            if (sQuesInd != "N")
                sReadInd = "Y";
            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_SCRA_DISC_CMPLT",
                CompleteUidField = "AD5_SCRA_DISC_CMPLT_ID",
                CompleteDateField = "AD5_SCRA_DISC_CMPLT_DT",
                CompleteTimeField = "AD5_SCRA_DISC_CMPLT_TM",
                QuesIndField = "AD5_SCRA_DISC_QUES_IND"
            };

            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            jsonStrw.Append(GetSaveJson(fieldMapping, sReadInd, sQuesInd));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();

        }

        /// <summary>
        ///  Get Save Json For SCRA
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <param name="sMilitary"></param>
        /// <returns></returns>
        public string GetSaveAppJsonForWiNonSpouseOnly(string sYesNo)
        {
            string sReadInd = sYesNo;

            //if (!string.IsNullOrEmpty(sYesNo))
            //    sReadInd = "Y";

            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_WI_NON_SP_DISC_CMPLT_P",
                CompleteUidField = "AD5_WI_NON_SP_DISC_CMPLT_ID_P",
                CompleteDateField = "AD5_WI_NON_SP_DISC_CMPLT_DT_P",
                CompleteTimeField = "AD5_WI_NON_SP_DISC_CMPLT_TM_P",
                QuesIndField = "AD5_WI_NON_SP_DISC_QUES_IND_P"
            };

            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            jsonStrw.Append(GetSaveJson(fieldMapping, sReadInd, sYesNo));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();

        }


        /// <summary>
        ///  Get Save Json For SCRA
        /// </summary>
        /// <param name="sReadInd"></param>
        /// <param name="sMilitary"></param>
        /// <returns></returns>
        public string GetSaveCoAppJsonForWiNonSpouseOnly(string sYesNo)
        {
            string sReadInd = sYesNo;

            //if (!string.IsNullOrEmpty(sYesNo))
            //    sReadInd = "Y";

            var fieldMapping = new DisclosureMapping
            {
                CompleteIndField = "AD5_WI_NON_SP_DISC_CMPLT_S",
                CompleteUidField = "AD5_WI_NON_SP_DISC_CMPLT_ID_S",
                CompleteDateField = "AD5_WI_NON_SP_DISC_CMPLT_DT_S",
                CompleteTimeField = "AD5_WI_NON_SP_DISC_CMPLT_TM_S",
                QuesIndField = "AD5_WI_NON_SP_DISC_QUES_IND_S"
            };

            var jsonStrw = new StringBuilder();

            jsonStrw.Append("{\"AcapsFields\":[");

            jsonStrw.Append(GetSaveJson(fieldMapping, sReadInd, sYesNo));

            jsonStrw.Remove(jsonStrw.Length - 1, 1); //remove trailing comma

            jsonStrw.Append("]}");

            return jsonStrw.ToString();

        }


        /// <summary>
        /// create save json with discl field mapping and read indicator
        /// </summary>
        /// <param name="fieldMapping"></param>
        /// <param name="sReadInd"></param>
        /// <param name="quesInd"></param>
        /// <returns></returns>
        private string GetSaveJson(DisclosureMapping fieldMapping, string sReadInd, string quesInd = "")
        {

            //string readCurrentValue = sReadInd ? "Y" : "N";

            var jsonStrw = new StringBuilder();

            if ( (!string.IsNullOrEmpty(quesInd)) &&
                (!string.IsNullOrEmpty(fieldMapping.QuesIndField))
              )
            {
                jsonStrw.Append(
                    "{\"AcapsFieldName\": \"" + string.Concat(fieldMapping.QuesIndField) + "\",\"PageNumber\":\"00" + fPageNum
                    + "\",\"OriginalValue\": \"" + originalValue + "\", \"CurrentValue\": \"" + quesInd
                    + "\", \"FieldId\": \"\"},");
            }
            jsonStrw.Append(
                "{\"AcapsFieldName\": \"" + string.Concat(fieldMapping.CompleteIndField) + "\",\"PageNumber\":\"00" + fPageNum
                + "\",\"OriginalValue\": \"" + originalValue + "\", \"CurrentValue\": \"" + sReadInd
                + "\", \"FieldId\": \"\"},");
            jsonStrw.Append(
                "{\"AcapsFieldName\": \"" + string.Concat(fieldMapping.CompleteUidField) + "\",\"PageNumber\":\"00" + fPageNum
                + "\",\"OriginalValue\": \"" + originalValue + "\", \"CurrentValue\": \"" + _userId
                + "\", \"FieldId\": \"\"},");
            jsonStrw.Append(
                "{\"AcapsFieldName\": \"" + string.Concat(fieldMapping.CompleteDateField) + "\",\"PageNumber\":\"00" + fPageNum
                + "\",\"OriginalValue\": \"" + originalValue + "\", \"CurrentValue\": \"" + _strDate
                + "\", \"FieldId\": \"\"},");
            jsonStrw.Append(
                "{\"AcapsFieldName\": \"" + string.Concat(fieldMapping.CompleteTimeField) + "\",\"PageNumber\":\"00" + fPageNum
                + "\",\"OriginalValue\": \"" + originalValue + "\", \"CurrentValue\": \"" + _strTime
                + "\", \"FieldId\": \"\"},");

            return jsonStrw.ToString();

        }

        class DisclosureMapping
        {
            public string CompleteIndField;
            public string CompleteUidField;
            public string CompleteDateField;
            public string CompleteTimeField;
            public string QuesIndField;
            
        }

    }
}
