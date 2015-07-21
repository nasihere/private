// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaidResponseParser.cs" company="">
//   
// </copyright>
// <summary>
//   The baid response parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP.Parsers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    using WF.EAI.Data.sif.Services.CWS.BAID;
    using WF.EAI.Entities.domain.c2c;

    /// <summary>
    ///     The baid response parser.
    /// </summary>
    public class BaidResponseParser
    {
        #region Public Methods and Operators

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="baidData">
        /// The baid data.
        /// </param>
        /// <returns>
        /// The Baid Entity
        /// </returns>
        public static BAIDEntity Parse(BaidData baidData)
        {
            var baidEntity = new BAIDEntity
                                 {
                                     WF_SALES_PARTNER_PILOT = baidData.WF_SALES_PARTNER_PILOT, 
                                     WF_LENDSCAPE_IS_UPDATING = baidData.WF_LENDSCAPE_IS_UPDATING, 
                                     SUM_PRI_RQST_LOAN_AMT = baidData.SUM_PRI_RQST_LOAN_AMT, 
                                     SUM_HOMESTEAD_IND = baidData.SUM_HOMESTEAD_IND, 
                                     CUS_SALES_BRANCH_CODE = baidData.CUS_SALES_BRANCH_CODE, 
                                     CUS_APPL_RECEIVED_DATE = baidData.CUS_APPL_RECEIVED_DATE, 
                                     NAS_LDS_CLOSING_DATE = baidData.NAS_LDS_CLOSING_DATE, 
                                     LIS_BROKER_ID = baidData.LIS_BROKER_ID, 
                                     WFF_ELIG_FLAG = baidData.WFF_ELIG_FLAG, 
                                     SUM_HVC_DESC = baidData.SUM_HVC_DESC, 
                                     SUM_DUPLICATE_IND_FIELD = baidData.SUM_DUPLICATE_IND_FIELD, 
                                     SUM_FRAUD_IND = baidData.SUM_FRAUD_IND, 
                                     SUM_NOTES_IND_FIELD = baidData.SUM_NOTES_IND_FIELD, 
                                     SUM_BANKER_NOTE = baidData.SUM_BANKER_NOTE, 
                                     RSB_RPT_NUMBER = baidData.RSB_RPT_NUMBER, 
                                     XSELL_IND = baidData.XSELL_IND, 
                                     NON_APPL_SPOUSE_IND = baidData.NON_APPL_SPOUSE_IND, 
                                     SUM_LOAN_AGGREGATION_FLAG = baidData.SUM_LOAN_AGGREGATION_FLAG, 
                                     CUS_HOLD_DATE = baidData.CUS_HOLD_DATE, 
                                     SUM_LTV_RATIO = baidData.SUM_LTV_RATIO, 
                                     SUM_TDSR = baidData.SUM_TDSR, 
                                     SUM_CREDIT_GRADE = baidData.SUM_CREDIT_GRADE, 
                                     CUS_RELATED_APP_CODE = baidData.CUS_RELATED_APP_CODE, 
                                     CUS_FIB_BUSINESS_NAME = baidData.CUS_FIB_BUSINESS_NAME, 
                                     CUS_FIB_BUSINESS_TAX_ID = baidData.CUS_FIB_BUSINESS_TAX_ID, 
                                     CUS_PRIORITY_SORT_CODE = baidData.CUS_PRIORITY_SORT_CODE, 
                                     LOC_HIER_SERV_IND = baidData.LOC_HIER_SERV_IND, 
                                     TITLE_REPORT_ID = baidData.TITLE_REPORT_ID, 
                                     CBE_MIDDLE_FICO_PRI = baidData.CBE_MIDDLE_FICO_PRI, 
                                     CBE_MIDDLE_FICO_SEC = baidData.CBE_MIDDLE_FICO_SEC, 
                                     WF_B2B_FINAL_RESULTS_IND = baidData.WF_B2B_FINAL_RESULTS_IND, 
                                     REW_WF_REMOD_FLAG = baidData.REW_WF_REMOD_FLAG, 
                                     GWF_WF_GWOFAC_IND = baidData.GWF_WF_GWOFAC_IND, 
                                     WF_RECALC_FEE_IND = baidData.WF_RECALC_FEE_IND, 
                                     SUM_CKP_CHECKPOINT_IND = baidData.SUM_CKP_CHECKPOINT_IND, 
                                     ADR_BRKR_IS_UPDATING = baidData.ADR_BRKR_IS_UPDATING, 
                                     ADR_BRKR_IS_UPDATING_SINCE = baidData.ADR_BRKR_IS_UPDATING_SINCE, 
                                     ADR_BLOCK_UPDATES_IN_ILOL = baidData.ADR_BLOCK_UPDATES_IN_ILOL, 
                                     MOR_SIMO_IND = baidData.MOR_SIMO_IND, 
                                     APP_RECVD_TIME = baidData.APP_RECVD_TIME, 
                                     ADDRESS_ERROR_FLAG = baidData.ADDRESS_ERROR_FLAG, 
                                     WF_CREDIT_EXP_IS_UPDATING = baidData.WF_CREDIT_EXP_IS_UPDATING, 
                                     WF_DR3_CUSTOMER_IN_STORE = baidData.WF_DR3_CUSTOMER_IN_STORE, 
                                     FIRSTTIMEDISPLAYCBRNUM = baidData.FIRSTTIMEDISPLAYCBRNUM, 
                                     WF_PCM_PYMT_TO_INC = baidData.WF_PCM_PYMT_TO_INC, 
                                     WF_PCM_HIGHLIGHT_PTI_FLAG = baidData.WF_PCM_HIGHLIGHT_PTI_FLAG, 
                                     APP_HAS_NOTES_FLAG = baidData.APP_HAS_NOTES_FLAG, 
                                     ACTIVITY_VA_FOUND = baidData.ACTIVITY_VA_FOUND, 
                                     PCM_OWNER_CLEARED_FLAG = baidData.PCM_OWNER_CLEARED_FLAG, 
                                     SIMO_CIP_FLAG = baidData.SIMO_CIP_FLAG, 
                                     SPP_IND = baidData.SPP_IND, 
                                     PRE_APR_IND = baidData.PRE_APR_IND, 
                                     LIA_TOTAL_LIAB_ENTRIES = baidData.LIA_TOTAL_LIAB_ENTRIES, 
                                     FINAL_EARLY_TIL_LOAN = baidData.FINAL_EARLY_TIL_LOAN, 
                                     WELLS_FARGO_ADVISOR = baidData.WELLS_FARGO_ADVISOR, 
                                     LOAN_OFFICER_NAME = baidData.LOAN_OFFICER.LOAN_OFFICER_NAME, 
                                     CON_SALES_OFFICER_NAME = baidData.SALES_OFFICER.CON_SALES_OFFICER_NAME, 
                                     CURRENTLY_ASSIGNED_TO = baidData.CURRENTLY_ASSIGNED_TO, 
                                     WF_PRI_OTHR_INC_USED_IN_DSR = baidData.WF_PRI_OTHR_INC_USED_IN_DSR, 
                                     WF_SEC_OTHR_INC_USED_IN_DSR = baidData.WF_SEC_OTHR_INC_USED_IN_DSR,
                                     ptiTestRequired=baidData.ptiTestRequired,                                     
                                     OOFState = baidData.OOFState,
                                     priRegoFlag = baidData.priRegoFlag,
                                     secRegoFlag = baidData.secRegoFlag,
                                     preScreenFlag = baidData.preScreenFlag,
                                     multiLoanInd = baidData.multiLoanInd,
                                     DC1_SET_TITL_FLAG_ON_GUI = baidData.DC1_SET_TITL_FLAG_ON_GUI, //R1.15
                                     AppCurrState = baidData.AppCurrState,
                                     prvLpActivityCode = baidData.prvLpActivityCode,     //Added on July-7th
                                     vehicleAge = baidData.vehicleAge,
                                     DPActivityDone=baidData.DPActivityDone,
                                     VerificationInProgress = baidData.VerificationInProgress,
                                     SUM_ACTION_STATUS_CODE = baidData.SUM_ACTION_STATUS.CODE,
                                     closingBranchName=baidData.ClosingBranchName,
                                     IsLatestDocsPrinted = baidData.LatestDocsPrinted,
                                     appLoadProcessComplete = baidData.appLoadProcessComplete,
                                     dataCompleteInd =baidData.dataCompleteInd
                                 };
            if (baidData.APP_OWN_ID != null)
            {
                var appOwnId = new AppOwnID
                                   {
                                       CODE = baidData.APP_OWN_ID.CODE, 
                                       TELEPHONE = baidData.APP_OWN_ID.TELEPHONE, 
                                       Value = baidData.APP_OWN_ID.Value
                                   };

                baidEntity.APP_OWN_ID = appOwnId;
            }

            if (baidData.PRIMARY != null)
            {
                baidEntity.Primary = new PRIMARY
                                         {
                                             CUS_PRI_FIRST_NAME = baidData.PRIMARY.CUS_PRI_FIRST_NAME, 
                                             CUS_PRI_LAST_NAME = baidData.PRIMARY.CUS_PRI_LAST_NAME, 
                                             CUS_PRI_MIDDLE_INITIAL = baidData.PRIMARY.CUS_PRI_MIDDLE_INITIAL, 
                                             CUS_PRI_SUFFIX = baidData.PRIMARY.CUS_PRI_SUFFIX, 
                                             CUS_PRI_SSN = baidData.PRIMARY.CUS_PRI_SSN
                                         };
            }

            if (baidData.SECONDARY != null)
            {
                baidEntity.Secondary = new SECONDARY
                                           {
                                               CUS_SEC_FIRST_NAME = baidData.SECONDARY.CUS_SEC_FIRST_NAME, 
                                               CUS_SEC_LAST_NAME = baidData.SECONDARY.CUS_SEC_LAST_NAME, 
                                               CUS_SEC_MIDDLE_INITIAL =
                                                   baidData.SECONDARY.CUS_SEC_MIDDLE_INITIAL, 
                                               CUS_SEC_SUFFIX = baidData.SECONDARY.CUS_SEC_SUFFIX, 
                                               CUS_SEC_SSN = baidData.SECONDARY.CUS_SEC_SSN
                                           };
            }

            if (baidData.RELATED_APPS != null)
            {
                var relAppList = new List<RelateApplication>();
                foreach (var relApp in baidData.RELATED_APPS)
                {
                    if (relApp != null)
                    {
                        var newRelApp = new RelateApplication
                                            {
                                                APPID = relApp.APPID, 
                                                BORWR = relApp.BORWR, 
                                                CODE = relApp.CODE, 
                                                CUS_FIRST_NAME = relApp.CUS_FIRST_NAME, 
                                                CUS_MIDDLE_INITIAL = relApp.CUS_MIDDLE_INITIAL, 
                                                CUS_LAST_NAME = relApp.CUS_LAST_NAME, 
                                                CUS_SUFFIX = relApp.CUS_SUFFIX, 
                                                CUS_MARITAL_STATUS = relApp.CUS_MARITAL_STATUS
                                            };
                        relAppList.Add(newRelApp);
                    }
                }

                baidEntity.RELATED_APPS = relAppList;
            }

            if (baidData.DA_CBR_PRJ_LIST1 != null)
            {
                var cbrProjsList = new List<CBRProjects>();
                foreach (var cbrProj in baidData.DA_CBR_PRJ_LIST1)
                {
                    if (cbrProj != null)
                    {
                        var cbrProjects = new CBRProjects { CBR = cbrProj.CBR, Projects = new List<string>() };
                        foreach (DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ prj in cbrProj.ProjectList)
                        {
                            cbrProjects.Projects.Add(prj.Value);
                        }

                        cbrProjsList.Add(cbrProjects);
                    }
                }

                baidEntity.DA_CBR_PRJ_LIST = cbrProjsList;
            }

            if (baidData.SUM_COMPLIANCE_STATUS != null)
            {
                var complStatus = new ComplianceStatus
                                      {
                                          CODE = baidData.SUM_COMPLIANCE_STATUS.CODE, 
                                          VALUE = baidData.SUM_COMPLIANCE_STATUS.Value
                                      };

                baidEntity.SUM_COMPLIANCE_STATUS = complStatus;
            }

            if (baidData.LIS_PRODUCT_DESCR != null)
            {
                var lisProdDesc = new LisProductDescription
                                      {
                                          CODE = baidData.LIS_PRODUCT_DESCR.CODE, 
                                          TYPE = baidData.LIS_PRODUCT_DESCR.TYPE, 
                                          Value = baidData.LIS_PRODUCT_DESCR.Value
                                      };

                baidEntity.LIS_PRODUCT_DESCR = lisProdDesc;
            }

            if (baidData.CUS_OWNING_BRANCH_CODE != null)
            {
                var cusOwnCode = new CusOwningBranchCode
                                     {
                                         CODE = baidData.CUS_OWNING_BRANCH_CODE.CODE, 
                                         TELEPHONE = baidData.CUS_OWNING_BRANCH_CODE.TELEPHONE, 
                                         Value = baidData.CUS_OWNING_BRANCH_CODE.Value
                                     };

                baidEntity.CUS_OWNING_BRANCH_CODE = cusOwnCode;
            }

            if (baidData.ClosMethodBank != null)
            {
                baidEntity.ClosMethodBank = new ClosMethodBank
                {
                    SRC = baidData.ClosMethodBank.SRC,
                    PURP = baidData.ClosMethodBank.PURP,
                    TIER = baidData.ClosMethodBank.TIER,
                    RESST = baidData.ClosMethodBank.RESST,
                    CLOST = baidData.ClosMethodBank.CLOST,
                    REGST = baidData.ClosMethodBank.REGST,
                    AGE = baidData.ClosMethodBank.AGE,
                    MILEAGE = baidData.ClosMethodBank.MILEAGE,
                    RESTRICTIONS = baidData.ClosMethodBank.RESTRICTIONS
                };
            }

            if (baidData.ClosMethodLoan != null)
            {
                baidEntity.ClosMethodLoan = new ClosMethodLoan
                {
                    SRC = baidData.ClosMethodLoan.SRC,
                    PURP = baidData.ClosMethodLoan.PURP,
                    TIER = baidData.ClosMethodLoan.TIER,
                    RESST = baidData.ClosMethodLoan.RESST,
                    CLOST = baidData.ClosMethodLoan.CLOST,
                    REGST = baidData.ClosMethodLoan.REGST,
                    AGE = baidData.ClosMethodLoan.AGE,
                    MILEAGE = baidData.ClosMethodLoan.MILEAGE,
                    RESTRICTIONS = baidData.ClosMethodLoan.RESTRICTIONS
                };
            }

            return baidEntity;
        }

        /// <summary>
        /// The parse baid xml.
        /// </summary>
        /// <param name="baidResponse">
        /// The baid response.
        /// </param>
        /// <returns>
        /// The <see cref="BaidData"/>.
        /// </returns>
        public static BaidData ParseBAIDXml(string baidResponse)
        {
            var baidData = new BaidData();

            

            const string RELATED_APP = "RELATED_APPS";             
            const string DA_CBR_PRJ_LIST = "DA_CBR_PRJ_LIST";
            const string SUM_ACTION_STATUS = "SUM_ACTION_STATUS";
            const string PRIMARY = "PRIMARY";
            const string SECONDARY = "SECONDARY";
            const string ADDRESS = "ADDRESS";
            const string SALES_OFFICER = "SALES_OFFICER";
            const string LIS_RELATIONSHP = "LIS_RELATIONSHP";
            const string CUS_LOCATION = "CUS_LOCATION";
            const string LOAN_OFFICER = "LOAN_OFFICER";
            const string APP_OWN_ID = "APP_OWN_ID";
            const string CUS_LOAN_TYPE = "CUS_LOAN_TYPE";
            const string QDE_RQST_DOC_TYPE = "QDE_RQST_DOC_TYPE";
            const string CUS_RQST_ORIG_STATE = "CUS_RQST_ORIG_STATE";
            const string LIS_PRODUCT_DESCR = "LIS_PRODUCT_DESCR";
            const string CUS_OWNING_BRANCH_CODE = "CUS_OWNING_BRANCH_CODE";
            const string LIS_PURPOSE_DESCR = "LIS_PURPOSE_DESCR";
            const string SUM_COMPLIANCE_STATUS = "SUM_COMPLIANCE_STATUS";
            const string SUM_TEX_COMPL_STATUS = "SUM_TEX_COMPL_STATUS";
            const string CUS_PRI_MARITAL_STATUS = "CUS_PRI_MARITAL_STATUS";
            const string CUS_SEC_MARITAL_STATUS = "CUS_SEC_MARITAL_STATUS";
            const string LIS_BROKER_FLAG = "LIS_BROKER_FLAG";
            const string ClosMethodBank = "ClosMethodBank";
            const string ClosMethodLoan = "ClosMethodLoan";
            const string AppCurrState = "appCurrState";

            // const string RELATED_APPS_PATH = "/ACAPS01/Body/Data/RELATED_APPS";
            const string RELATED_APP_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP";
            const string REL_APPS_CUS_SUFFIX_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP/CUS_SUFFIX";
            const string REL_APPS_CUS_FIRST_NAME_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP/CUS_FIRST_NAME";
            const string REL_APPS_MIDDLE_INITIAL_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP/CUS_MIDDLE_INITIAL";
            const string REL_APPS_CUS_LAST_NAME_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP/CUS_LAST_NAME";
            const string REL_APPS_MARITAL_STATUS_PATH = "/ACAPS01/Body/Data/RELATED_APPS/RELATED_APP/CUS_MARITAL_STATUS";
            const string DA_CBR_PRJ_LIST_PATH = "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST";
            const string DA_CBR_PRJ_PATH = "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST/DA_CBR_PRJ";
            const string DA_CBR_PRJ_LIST_PRJ_PATH = "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST/DA_CBR_PRJ/CBR/DA_PRJ_LIST/PRJ";
            const string DA_CBR_PRJ_CBR_PATH = "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST/DA_CBR_PRJ/CBR";
            const string DA_CBR_PRJ_CBR_PROJ_LIST_PATH = "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST/DA_CBR_PRJ/DA_PRJ_LIST";
            const string SUM_ACTION_STATUS_PATH = "/ACAPS01/Body/Data/SUM_ACTION_STATUS";
            const string CUS_PRI_FIRST_NAME_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_FIRST_NAME";
            const string CUS_PRI_LAST_NAME_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_LAST_NAME";
            const string CUS_PRI_MIDDLE_INITIAL_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_MIDDLE_INITIAL";
            const string CUS_PRI_BIRTH_DATE_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_BIRTH_DATE";
            const string CUS_PRI_SSN_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_SSN";
            const string CUS_PRI_SUFFIX_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_SUFFIX";
            const string CUS_PRI_NON_APPL_SPOUSE_IND_PATH = "/ACAPS01/Body/Data/PRIMARY/CUS_PRI_NON_APPL_SPOUSE_IND";

            const string CUS_SEC_FIRST_NAME_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_FIRST_NAME";
            const string CUS_SEC_LAST_NAME_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_LAST_NAME";
            const string CUS_SEC_MIDDLE_INITIAL_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_MIDDLE_INITIAL";
            const string CUS_SEC_BIRTH_DATE_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_BIRTH_DATE";
            const string CUS_SEC_SSN_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_SSN";
            const string CUS_SEC_SUFFIX_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_SUFFIX";
            const string CUS_SEC_NON_APPL_SPOUSE_IND_PATH = "/ACAPS01/Body/Data/SECONDARY/CUS_SEC_NON_APPL_SPOUSE_IND";

            const string PRI_CUR_STREET_NUM_PATH = "/ACAPS01/Body/Data/PRI_CUR_STREET_NUM";
            const string PRI_CUR_STREET_NAME_PATH = "/ACAPS01/Body/Data/PRI_CUR_STREET_NAME";
            const string PRI_CUR_STREET_TYPE_PATH = "/ACAPS01/Body/Data/PRI_CUR_STREET_TYPE";
            const string PRI_CUR_STREET_DIR_PATH = "/ACAPS01/Body/Data/PRI_CUR_STREET_DIR";
            const string PRI_CUR_APT_NUM_PATH = "/ACAPS01/Body/Data/PRI_CUR_APT_NUM";
            const string PRI_CUR_PO_BOX_NUM_PATH = "/ACAPS01/Body/Data/PRI_CUR_PO_BOX_NUM";
            const string PRI_CUR_CITY_PATH = "/ACAPS01/Body/Data/PRI_CUR_CITY";
            const string PRI_CUR_STATE_PATH = "/ACAPS01/Body/Data/PRI_CUR_STATE";
            const string PRI_CUR_ZIP_CODE_PATH = "/ACAPS01/Body/Data/PRI_CUR_ZIP_CODE";

            // const string SALES_OFFICER_PATH = "/ACAPS01/Body/Data/SALES_OFFICER";
            const string CON_SALES_OFFICER_NAME_PATH = "/ACAPS01/Body/Data/SALES_OFFICER/CON_SALES_OFFICER_NAME";
            const string CON_SALES_OFFICER_PH_AREA_PATH = "/ACAPS01/Body/Data/SALES_OFFICER/CON_SALES_OFFICER_PH_AREA";
            const string CON_SALES_OFFICER_PH_PREFIX_PATH =
                "/ACAPS01/Body/Data/SALES_OFFICER/CON_SALES_OFFICER_PH_PREFIX";
            const string CON_SALES_OFFICER_PH_SUFFIX_PATH =
                "/ACAPS01/Body/Data/SALES_OFFICER/CON_SALES_OFFICER_PH_SUFFIX";
            const string CON_SALES_OFFICER_PH_EXT_PATH = "/ACAPS01/Body/Data/SALES_OFFICER/CON_SALES_OFFICER_PH_EXT";
            const string SUM_PRI_RQST_LOAN_AMT_PATH = "/ACAPS01/Body/Data/SUM_PRI_RQST_LOAN_AMT";
            const string LIS_RELATIONSHP_PATH = "/ACAPS01/Body/Data/LIS_RELATIONSHP";
            const string CUS_LOCATION_PATH = "/ACAPS01/Body/Data/CUS_LOCATION";
            const string LOAN_OFFICER_NAME_PATH = "/ACAPS01/Body/Data/LOAN_OFFICER/LOAN_OFFICER_NAME";
            const string SUM_HOMESTEAD_IND_PATH = "/ACAPS01/Body/Data/SUM_HOMESTEAD_IND";
            const string CUS_SALES_BRANCH_CODE_PATH = "/ACAPS01/Body/Data/CUS_SALES_BRANCH_CODE";
            const string CUS_APPL_RECEIVED_DATE_PATH = "/ACAPS01/Body/Data/CUS_APPL_RECEIVED_DATE";
            const string NAS_LDS_CLOSING_DATE_PATH = "/ACAPS01/Body/Data/NAS_LDS_CLOSING_DATE";
            const string APP_OWN_ID_PATH = "/ACAPS01/Body/Data/APP_OWN_ID";
            const string CUS_LOAN_TYPE_PATH = "/ACAPS01/Body/Data/CUS_LOAN_TYPE";
            const string LIS_BROKER_ID_PATH = "/ACAPS01/Body/Data/LIS_BROKER_ID";
            const string QDE_RQST_DOC_TYPE_PATH = "/ACAPS01/Body/Data/QDE_RQST_DOC_TYPE";
            const string WFF_ELIG_FLAG_PATH = "/ACAPS01/Body/Data/WFF_ELIG_FLAG";
            const string CUS_RQST_ORIG_STATE_PATH = "/ACAPS01/Body/Data/CUS_RQST_ORIG_STATE";
            const string LIS_PRODUCT_DESCR_PATH = "/ACAPS01/Body/Data/LIS_PRODUCT_DESCR";
            const string CUS_OWNING_BRANCH_CODE_PATH = "/ACAPS01/Body/Data/CUS_OWNING_BRANCH_CODE";
            const string LIS_PURPOSE_DESCR_PATH = "/ACAPS01/Body/Data/LIS_PURPOSE_DESCR";
            const string SUM_COMPLIANCE_STATUS_PATH = "/ACAPS01/Body/Data/SUM_COMPLIANCE_STATUS";
            const string SUM_TEX_COMPL_STATUS_PATH = "/ACAPS01/Body/Data/SUM_TEX_COMPL_STATUS";
            const string SUM_HVC_DESC_PATH = "/ACAPS01/Body/Data/SUM_HVC_DESC";
            const string SUM_DUPLICATE_IND_FIELD_PATH = "/ACAPS01/Body/Data/SUM_DUPLICATE_IND_FIELD";
            const string SUM_FRAUD_IND_PATH = "/ACAPS01/Body/Data/SUM_FRAUD_IND";
            const string SUM_NOTES_IND_FIELD_PATH = "/ACAPS01/Body/Data/SUM_NOTES_IND_FIELD";
            const string SUM_BANKER_NOTE_PATH = "/ACAPS01/Body/Data/SUM_BANKER_NOTE";
            const string RSB_RPT_NUMBER_PATH = "/ACAPS01/Body/Data/RSB_RPT_NUMBER";
            const string XSELL_IND_PATH = "/ACAPS01/Body/Data/XSELL_IND";
            const string NON_APPL_SPOUSE_IND_PATH = "/ACAPS01/Body/Data/NON_APPL_SPOUSE_IND";
            const string SUM_LOAN_AGGREGATION_FLAG_PATH = "/ACAPS01/Body/Data/SUM_LOAN_AGGREGATION_FLAG";
            const string CUS_PRI_MARITAL_STATUS_PATH = "/ACAPS01/Body/Data/CUS_PRI_MARITAL_STATUS";
            const string CUS_SEC_MARITAL_STATUS_PATH = "/ACAPS01/Body/Data/CUS_SEC_MARITAL_STATUS";
            const string CUS_HOLD_DATE_PATH = "/ACAPS01/Body/Data/CUS_HOLD_DATE";
            const string SUM_LTV_RATIO_PATH = "/ACAPS01/Body/Data/SUM_LTV_RATIO";
            const string SUM_TDSR_PATH = "/ACAPS01/Body/Data/SUM_TDSR";
            const string SUM_CREDIT_GRADE_PATH = "/ACAPS01/Body/Data/SUM_CREDIT_GRADE";
            const string CUS_RELATED_APP_CODE_PATH = "/ACAPS01/Body/Data/CUS_RELATED_APP_CODE";
            const string CUS_FIB_BUSINESS_NAME_PATH = "/ACAPS01/Body/Data/CUS_FIB_BUSINESS_NAME";
            const string CUS_FIB_BUSINESS_TAX_ID = "/ACAPS01/Body/Data/CUS_FIB_BUSINESS_TAX_ID";
            const string LIS_BROKER_FLAG_PATH = "/ACAPS01/Body/Data/LIS_BROKER_FLAG";
            const string CUS_PRIORITY_SORT_CODE_PATH = "/ACAPS01/Body/Data/CUS_PRIORITY_SORT_CODE";
            const string LOC_HIER_SERV_IND_PATH = "/ACAPS01/Body/Data/LOC_HIER_SERV_IND";
            const string TITLE_REPORT_ID_PATH = "/ACAPS01/Body/Data/TITLE_REPORT_ID";
            const string CBE_MIDDLE_FICO_PRI_PATH = "/ACAPS01/Body/Data/CBE_MIDDLE_FICO_PRI";
            const string CBE_MIDDLE_FICO_SEC_PATH = "/ACAPS01/Body/Data/CBE_MIDDLE_FICO_SEC";
            const string WF_B2B_FINAL_RESULTS_IND_PATH = "/ACAPS01/Body/Data/WF_B2B_FINAL_RESULTS_IND";
            const string REW_WF_REMOD_FLAG_PATH = "/ACAPS01/Body/Data/REW_WF_REMOD_FLAG";
            const string GWF_WF_GWOFAC_IND_PATH = "/ACAPS01/Body/Data/GWF_WF_GWOFAC_IND";
            const string WF_RECALC_FEE_IND_PATH = "/ACAPS01/Body/Data/WF_RECALC_FEE_IND";
            const string SUM_CKP_CHECKPOINT_IND_PATH = "/ACAPS01/Body/Data/SUM_CKP_CHECKPOINT_IND";
            const string ADR_BRKR_IS_UPDATING_PATH = "/ACAPS01/Body/Data/ADR_BRKR_IS_UPDATING";
            const string ADR_BRKR_IS_UPDATING_SINCE_PATH = "/ACAPS01/Body/Data/ADR_BRKR_IS_UPDATING_SINCE";
            const string ADR_BLOCK_UPDATES_IN_ILOL_PATH = "/ACAPS01/Body/Data/ADR_BLOCK_UPDATES_IN_ILOL";
            const string MOR_SIMO_IND_PATH = "/ACAPS01/Body/Data/MOR_SIMO_IND";
            const string APP_RECVD_TIME_PATH = "/ACAPS01/Body/Data/APP_RECVD_TIME";
            const string ADDRESS_ERROR_FLAG_PATH = "/ACAPS01/Body/Data/ADDRESS_ERROR_FLAG";
            const string WF_CREDIT_EXP_IS_UPDATING_PATH = "/ACAPS01/Body/Data/WF_CREDIT_EXP_IS_UPDATING";
            const string WF_DR3_CUSTOMER_IN_STORE_PATH = "/ACAPS01/Body/Data/WF_DR3_CUSTOMER_IN_STORE";
            const string FIRST_TIME_DISPLAY_CBR_NUM_PATH = "/ACAPS01/Body/Data/FIRST_TIME_DISPLAY_CBR_NUM";
            const string WF_PCM_PYMT_TO_INC_PATH = "/ACAPS01/Body/Data/WF_PCM_PYMT_TO_INC";
            const string WF_PCM_HIGHLIGHT_PTI_FLAG_PATH = "/ACAPS01/Body/Data/WF_PCM_HIGHLIGHT_PTI_FLAG";
            const string APP_HAS_NOTES_FLAG_PATH = "/ACAPS01/Body/Data/APP_HAS_NOTES_FLAG";
            const string ACTIVITY_VA_FOUND_PATH = "/ACAPS01/Body/Data/ACTIVITY_VA_FOUND";
            const string PCM_OWNER_CLEARED_FLAG_PATH = "/ACAPS01/Body/Data/PCM_OWNER_CLEARED_FLAG";
            const string SIMO_CIP_FLAG_PATH = "/ACAPS01/Body/Data/SIMO_CIP_FLAG";
            const string SPP_IND_PATH = "/ACAPS01/Body/Data/SPP_IND";
            const string PRE_APR_IND_PATH = "/ACAPS01/Body/Data/PRE_APR_IND";
            const string LIA_TOTAL_LIAB_ENTRIES_PATH = "/ACAPS01/Body/Data/LIA_TOTAL_LIAB_ENTRIES";
            const string FINAL_EARLY_TIL_LOAN_PATH = "/ACAPS01/Body/Data/FINAL_EARLY_TIL_LOAN";
            const string WELLS_FARGO_ADVISOR_PATH = "/ACAPS01/Body/Data/WELLS_FARGO_ADVISOR";
            const string WF_LENDSCAPE_IS_UPDATING_PATH = "/ACAPS01/Body/Data/WF_LENDSCAPE_IS_UPDATING";
            const string WF_SALES_PARTNER_PILOT_PATH = "/ACAPS01/Body/Data/WF_SALES_PARTNER_PILOT";
            const string WF_PRI_OTHR_INC_USED_IN_DSR_PATH = "/ACAPS01/Body/Data/WF_PRI_OTHR_INC_USED_IN_DSR";
            const string WF_SEC_OTHR_INC_USED_IN_DSR_PATH = "/ACAPS01/Body/Data/WF_SEC_OTHR_INC_USED_IN_DSR";
            const string CURRENTLY_ASSIGNED_TO_PATH = "/ACAPS01/Body/Data/CURRENTLY_ASSIGNED_TO";
            // const string ClosMethodBank_PATH = "/ACAPS01/Body/Data/ClosMethodBank";
            const string ClosMethodBank_SRC_PATH = "/ACAPS01/Body/Data/ClosMethodBank/SRC";
            const string ClosMethodBank_PURP_PATH = "/ACAPS01/Body/Data/ClosMethodBank/PURP";
            const string ClosMethodBank_TIER_PATH = "/ACAPS01/Body/Data/ClosMethodBank/TIER";
            const string ClosMethodBank_RESST_PATH = "/ACAPS01/Body/Data/ClosMethodBank/RESST";
            const string ClosMethodBank_CLOST_PATH = "/ACAPS01/Body/Data/ClosMethodBank/CLOST";
            const string ClosMethodBank_REGST_PATH = "/ACAPS01/Body/Data/ClosMethodBank/REGST";
            const string ClosMethodBank_AGE_PATH = "/ACAPS01/Body/Data/ClosMethodBank/AGE";
            const string ClosMethodBank_MILEAGE_PATH = "/ACAPS01/Body/Data/ClosMethodBank/MILEAGE";
            const string ClosMethodBank_RESTRICTIONS_PATH = "/ACAPS01/Body/Data/ClosMethodBank/RESTRICTIONS";

            // const string ClosMethodLoan_PATH = "/ACAPS01/Body/Data/ClosMethodLoan";
            const string ClosMethodLoan_SRC_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/SRC";
            const string ClosMethodLoan_PURP_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/PURP";
            const string ClosMethodLoan_TIER_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/TIER";
            const string ClosMethodLoan_RESST_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/RESST";
            const string ClosMethodLoan_CLOST_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/CLOST";
            const string ClosMethodLoan_REGST_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/REGST";
            const string ClosMethodLoan_AGE_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/AGE";
            const string ClosMethodLoan_MILEAGE_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/MILEAGE";
            const string ClosMethodLoan_RESTRICTIONS_PATH = "/ACAPS01/Body/Data/ClosMethodLoan/RESTRICTIONS";

            const string ptiTestRequired_PATH = "/ACAPS01/Body/Data/ptiTestRequired";
            const string OOFState_PATH = "/ACAPS01/Body/Data/OOFState";
            const string priRegoFlag_PATH = "/ACAPS01/Body/Data/priRegoFlag";
            const string secRegoFlag_PATH = "/ACAPS01/Body/Data/secRegoFlag";
            const string preScreenFlag_PATH = "/ACAPS01/Body/Data/preScreenFlag";
            const string multiLoanInd_PATH = "/ACAPS01/Body/Data/multiLoanInd";
            const string appState_PATH = "/ACAPS01/Body/Data/appCurrState";
            const string prvLpActivityCode_PATH = "/ACAPS01/Body/Data/prvLpActivityCode";  //Added on July-7th
            const string vehicleAge_PATH = "/ACAPS01/Body/Data/vehicleAge";
            const string DPActivityDone_PATH = "/ACAPS01/Body/Data/DPActivityDone";
            const string verificationInProgress_PATH = "/ACAPS01/Body/Data/verificationInProgress";
            const string closingBranchName_PATH = "/ACAPS01/Body/Data/closingBranchName";
            const string latestDocsPrinted_PATH = "/ACAPS01/Body/Data/latestDocsPrinted";
            const string appLoadProcessComplete_PATH = "/ACAPS01/Body/Data/appLoadProcessComplete";
            const string dataCompleteInd_PATH = "/ACAPS01/Body/Data/dataCompleteInd";
            const string DC1_SET_TITL_FLAG_ON_GUI_PATH = "/ACAPS01/Body/Data/DC1_SET_TITL_FLAG_ON_GUI";  //R1.15
        
            var relAppList = new DataRELATED_APP[10];
            DataRELATED_APP relApp = null;
            var cbrPrjList1 = new List<DataDA_CBR_PRJ_LISTDA_CBR_PRJ>();
            DataDA_CBR_PRJ_LISTDA_CBR_PRJ cbrProj = null;
            var cbrProjProjList = new List<DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ>();
            DataSUM_ACTION_STATUS sumActionStatus = null;
            DataPRIMARY primary = null;
            DataSECONDARY secondary = null;
            DataADDRESS address = null;
            DataSALES_OFFICER salesOfficer = null;
            DataLIS_RELATIONSHP lisRelationship = null;
            DataCUS_LOCATION cusLocation = null;
            DataLOAN_OFFICER loanOfficer = null;
            DataLOAN_OFFICERPHONE loanOfficerPhone = null;
            DataAPP_OWN_ID appOwnId = null;
            DataCUS_LOAN_TYPE cusLoanType = null;
            DataQDE_RQST_DOC_TYPE qdeRqstDocType = null;
            DataCUS_RQST_ORIG_STATE cusRqstOrigState = null;
            DataLIS_PRODUCT_DESCR lisProductDesc = null;
            DataCUS_OWNING_BRANCH_CODE cusOwningBranchCode = null;
            DataLIS_PURPOSE_DESCR lisPurposeDesc = null;
            DataSUM_COMPLIANCE_STATUS sumComplianceStatus = null;
            DataSUM_TEX_COMPL_STATUS sumTexComplStatus = null;
            DataCUS_PRI_MARITAL_STATUS cusPriMaritalStatus = null;
            DataCUS_SEC_MARITAL_STATUS cusSecMaritalStatus = null;
            DataLIS_BROKER_FLAG lisBrokerFlag = null;
            DataClosMethodBank closMethodBank = null;
            DataClosMethodLoan closMethodLoan = null;
            string appCurrState = null;

            #region Other Variables

            var currentXPath = string.Empty;
            var theValue = string.Empty;
            var currentRelApp = 0;

            #endregion

            #region ParseXML using XMLReader

            using (var xmlReader = new XmlTextReader(new StringReader(baidResponse)))
            {
                #region while

                while (xmlReader.Read())
                {
                    #region switch

                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            currentXPath += "/" + xmlReader.Name;
                            if (xmlReader.IsEmptyElement)
                            {
                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                theValue = string.Empty;
                            }
                            else
                            {
                                switch (xmlReader.Name)
                                {
                                    case "RELATED_APP":
                                        relApp = new DataRELATED_APP
                                                     {
                                                         CODE = xmlReader.GetAttribute("CODE").Trim(), 
                                                         BORWR = xmlReader.GetAttribute("BORWR").Trim(), 
                                                         APPID = xmlReader.GetAttribute("APPID").Trim()
                                                     };
                                        break;

                                    case "DA_CBR_PRJ":
                                        cbrProj = new DataDA_CBR_PRJ_LISTDA_CBR_PRJ();
                                        cbrProjProjList = new List<DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ>();
                                        cbrProj.ProjectList = cbrProjProjList;
                                        break;

                                        // case "PRJ":
                                        // var proj = new DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ();
                                        // cbrProjProjList.Add(proj);
                                        // break;
                                    case "SUM_ACTION_STATUS":
                                        sumActionStatus = new DataSUM_ACTION_STATUS
                                                              {
                                                                  CODE =
                                                                      xmlReader.GetAttribute(
                                                                          "CODE").Trim()
                                                              };
                                        break;

                                    case "PRIMARY":
                                        primary = new DataPRIMARY();
                                        break;

                                    case "SECONDARY":
                                        secondary = new DataSECONDARY();
                                        break;

                                    case "ADDRESS":
                                        address = new DataADDRESS();
                                        break;

                                    case "SALES_OFFICER":
                                        salesOfficer = new DataSALES_OFFICER();
                                        break;

                                    case "LIS_RELATIONSHP":
                                        lisRelationship = new DataLIS_RELATIONSHP
                                                              {
                                                                  CODE =
                                                                      xmlReader.GetAttribute("CODE")
                                                                               .Trim()
                                                              };
                                        break;

                                    case "CUS_LOCATION":
                                        cusLocation = new DataCUS_LOCATION
                                                          {
                                                              CODE =
                                                                  xmlReader.GetAttribute("CODE").Trim(), 
                                                              BUSINESS_OWNER =
                                                                  xmlReader.GetAttribute(
                                                                      "BUSINESS_OWNER").Trim()
                                                          };
                                        break;

                                    case "LOAN_OFFICER":
                                        loanOfficer = new DataLOAN_OFFICER();
                                        break;

                                    case "APP_OWN_ID":
                                        appOwnId = new DataAPP_OWN_ID
                                                       {
                                                           CODE = xmlReader.GetAttribute("CODE").Trim(), 
                                                           TELEPHONE =
                                                               xmlReader.GetAttribute("TELEPHONE").Trim()
                                                       };
                                        break;

                                    case "CUS_LOAN_TYPE":
                                        cusLoanType = new DataCUS_LOAN_TYPE
                                                          {
                                                              code =
                                                                  xmlReader.GetAttribute("code")
                                                                           .Trim()
                                                          };
                                        break;

                                    case "QDE_RQST_DOC_TYPE":
                                        qdeRqstDocType = new DataQDE_RQST_DOC_TYPE
                                                             {
                                                                 CODE =
                                                                     xmlReader.GetAttribute(
                                                                         "CODE").Trim()
                                                             };
                                        break;

                                    case "CUS_RQST_ORIG_STATE":
                                        cusRqstOrigState = new DataCUS_RQST_ORIG_STATE
                                                               {
                                                                   CODE =
                                                                       xmlReader.GetAttribute(
                                                                           "CODE").Trim()
                                                               };
                                        break;

                                    case "LIS_PRODUCT_DESCR":
                                        lisProductDesc = new DataLIS_PRODUCT_DESCR
                                                             {
                                                                 CODE =
                                                                     xmlReader.GetAttribute(
                                                                         "CODE").Trim(), 
                                                                 TYPE =
                                                                     xmlReader.GetAttribute(
                                                                         "TYPE").Trim()
                                                             };
                                        break;

                                    case "CUS_OWNING_BRANCH_CODE":
                                        cusOwningBranchCode = new DataCUS_OWNING_BRANCH_CODE
                                                                  {
                                                                      CODE =
                                                                          xmlReader
                                                                          .GetAttribute(
                                                                              "CODE").Trim(), 
                                                                      TELEPHONE =
                                                                          xmlReader
                                                                          .GetAttribute(
                                                                              "TELEPHONE")
                                                                          .Trim()
                                                                  };
                                        break;

                                    case "LIS_PURPOSE_DESCR":
                                        lisPurposeDesc = new DataLIS_PURPOSE_DESCR
                                                             {
                                                                 CODE =
                                                                     xmlReader.GetAttribute(
                                                                         "CODE").Trim()
                                                             };
                                        break;

                                    case "SUM_COMPLIANCE_STATUS":
                                        sumComplianceStatus = new DataSUM_COMPLIANCE_STATUS
                                                                  {
                                                                      CODE =
                                                                          xmlReader
                                                                          .GetAttribute(
                                                                              "CODE").Trim()
                                                                  };
                                        break;

                                    case "SUM_TEX_COMPL_STATUS":
                                        sumTexComplStatus = new DataSUM_TEX_COMPL_STATUS
                                                                {
                                                                    CODE =
                                                                        xmlReader.GetAttribute(
                                                                            "CODE").Trim()
                                                                };
                                        break;

                                    case "CUS_PRI_MARITAL_STATUS":
                                        cusPriMaritalStatus = new DataCUS_PRI_MARITAL_STATUS
                                                                  {
                                                                      CODE =
                                                                          xmlReader
                                                                          .GetAttribute(
                                                                              "CODE").Trim()
                                                                  };
                                        break;

                                    case "CUS_SEC_MARITAL_STATUS":
                                        cusSecMaritalStatus = new DataCUS_SEC_MARITAL_STATUS
                                                                  {
                                                                      CODE =
                                                                          xmlReader
                                                                          .GetAttribute(
                                                                              "CODE").Trim()
                                                                  };
                                        break;

                                    case "LIS_BROKER_FLAG":
                                        lisBrokerFlag = new DataLIS_BROKER_FLAG
                                                            {
                                                                CODE =
                                                                    xmlReader.GetAttribute("CODE")
                                                                             .Trim()
                                                            };
                                        break;
                                    case "ClosMethodBank":
                                        closMethodBank = new DataClosMethodBank();
                                       
                                        break;
                                    case "ClosMethodLoan":
                                        closMethodLoan = new DataClosMethodLoan();

                                        break;
                                    case "appCurrState":
                                        //appCurrState = new appCurrState();
                                        string s = "test";
                                        break;
                                }
                            }

                            break;

                        case XmlNodeType.Text:
                            theValue = xmlReader.Value.Trim('_');
                            break;

                        case XmlNodeType.CDATA:
                            theValue = xmlReader.Value.Trim('_');
                            break;

                        case XmlNodeType.EndElement:

                            switch (currentXPath)
                            {
                                case RELATED_APP_PATH:
                                    relAppList[currentRelApp] = relApp;
                                    currentRelApp += 1;
                                    break;

                                case REL_APPS_CUS_SUFFIX_PATH:
                                    relApp.CUS_SUFFIX = theValue;
                                    break;

                                case REL_APPS_CUS_FIRST_NAME_PATH:
                                    relApp.CUS_FIRST_NAME = theValue;
                                    break;

                                case REL_APPS_CUS_LAST_NAME_PATH:
                                    relApp.CUS_LAST_NAME = theValue;
                                    break;

                                case REL_APPS_MIDDLE_INITIAL_PATH:
                                    relApp.CUS_MIDDLE_INITIAL = theValue;
                                    break;

                                case REL_APPS_MARITAL_STATUS_PATH:
                                    relApp.CUS_MARITAL_STATUS = theValue;
                                    break;
                                case DA_CBR_PRJ_PATH:
                                    cbrPrjList1.Add(cbrProj);
                                    break;
                                case DA_CBR_PRJ_CBR_PATH:
                                    cbrProj.CBR = theValue;
                                    break;

                                    // case DA_CBR_PRJ_CBR_PROJ_LIST_PATH:
                                    // cbrProj.ProjectList = theValue;
                                    // break;
                                case DA_CBR_PRJ_LIST_PRJ_PATH:
                                    var proj = new DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ { Value = theValue };
                                    cbrProj.ProjectList.Add(proj);
                                    break;
                                case "/ACAPS01/Body/Data/DA_CBR_PRJ_LIST/DA_CBR_PRJ/DA_PRJ_LIST/PRJ":
                                    var proj1 = new DataDA_CBR_PRJ_LISTDA_CBR_PRJDA_PRJ_LISTPRJ { Value = theValue };
                                    cbrProj.ProjectList.Add(proj1);
                                    break;

                                case SUM_ACTION_STATUS_PATH:
                                    sumActionStatus.Value = theValue;
                                    baidData.SUM_ACTION_STATUS = sumActionStatus;
                                    break;

                                case CUS_PRI_FIRST_NAME_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_FIRST_NAME = theValue;
                                    }

                                    break;

                                case CUS_PRI_LAST_NAME_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_LAST_NAME = theValue;
                                    }

                                    break;

                                case CUS_PRI_MIDDLE_INITIAL_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_MIDDLE_INITIAL = theValue;
                                    }

                                    break;

                                case CUS_PRI_BIRTH_DATE_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_BIRTH_DATE = theValue;
                                    }

                                    break;

                                case CUS_PRI_SSN_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_SSN = theValue;
                                    }

                                    break;

                                case CUS_PRI_SUFFIX_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_SUFFIX = theValue;
                                    }

                                    break;

                                case CUS_PRI_NON_APPL_SPOUSE_IND_PATH:
                                    if (primary != null)
                                    {
                                        primary.CUS_PRI_NON_APPL_SPOUSE_IND = theValue;
                                    }

                                    break;

                                case CUS_SEC_FIRST_NAME_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_FIRST_NAME = theValue;
                                    }

                                    break;

                                case CUS_SEC_LAST_NAME_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_LAST_NAME = theValue;
                                    }

                                    break;

                                case CUS_SEC_MIDDLE_INITIAL_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_MIDDLE_INITIAL = theValue;
                                    }

                                    break;

                                case CUS_SEC_BIRTH_DATE_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_BIRTH_DATE = theValue;
                                    }

                                    break;

                                case CUS_SEC_SSN_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_SSN = theValue;
                                    }

                                    break;

                                case CUS_SEC_SUFFIX_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_SUFFIX = theValue;
                                    }

                                    break;

                                case CUS_SEC_NON_APPL_SPOUSE_IND_PATH:
                                    if (secondary != null)
                                    {
                                        secondary.CUS_SEC_NON_APPL_SPOUSE_IND = theValue;
                                    }

                                    break;

                                case PRI_CUR_STREET_NUM_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_STREET_NUM = theValue;
                                    }

                                    break;

                                case PRI_CUR_STREET_NAME_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_STREET_NAME = theValue;
                                    }

                                    break;

                                case PRI_CUR_STREET_TYPE_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_STREET_TYPE = theValue;
                                    }

                                    break;

                                case PRI_CUR_STREET_DIR_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_STREET_DIR = theValue;
                                    }

                                    break;

                                case PRI_CUR_APT_NUM_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_APT_NUM = theValue;
                                    }

                                    break;

                                case PRI_CUR_PO_BOX_NUM_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_PO_BOX_NUM = theValue;
                                    }

                                    break;

                                case PRI_CUR_CITY_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_CITY = theValue;
                                    }

                                    break;

                                case PRI_CUR_STATE_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_STATE = theValue;
                                    }

                                    break;

                                case PRI_CUR_ZIP_CODE_PATH:
                                    if (address != null)
                                    {
                                        address.PRI_CUR_ZIP_CODE = theValue;
                                    }

                                    break;

                                case CON_SALES_OFFICER_NAME_PATH:
                                    if (salesOfficer != null)
                                    {
                                        salesOfficer.CON_SALES_OFFICER_NAME = theValue;
                                    }

                                    break;

                                case CON_SALES_OFFICER_PH_AREA_PATH:
                                    if (salesOfficer != null)
                                    {
                                        salesOfficer.CON_SALES_OFFICER_PH_AREA = theValue;
                                    }

                                    break;

                                case CON_SALES_OFFICER_PH_PREFIX_PATH:
                                    if (salesOfficer != null)
                                    {
                                        salesOfficer.CON_SALES_OFFICER_PH_PREFIX = theValue;
                                    }

                                    break;

                                case CON_SALES_OFFICER_PH_SUFFIX_PATH:
                                    if (salesOfficer != null)
                                    {
                                        salesOfficer.CON_SALES_OFFICER_PH_SUFFIX = theValue;
                                    }

                                    break;

                                case CON_SALES_OFFICER_PH_EXT_PATH:
                                    if (salesOfficer != null)
                                    {
                                        salesOfficer.CON_SALES_OFFICER_PH_EXT = theValue;
                                    }

                                    break;

                                case SUM_PRI_RQST_LOAN_AMT_PATH:
                                    baidData.SUM_PRI_RQST_LOAN_AMT = theValue;
                                    break;

                                case LIS_RELATIONSHP_PATH:
                                    if (lisRelationship != null)
                                    {
                                        lisRelationship.Value = theValue;
                                    }

                                    break;

                                case CUS_LOCATION_PATH:
                                    if (cusLocation != null)
                                    {
                                        cusLocation.Value = theValue;
                                    }

                                    break;

                                case LOAN_OFFICER_NAME_PATH:
                                    if (loanOfficer != null)
                                    {
                                        loanOfficer.LOAN_OFFICER_NAME = theValue;
                                    }

                                    break;

                                case SUM_HOMESTEAD_IND_PATH:
                                    baidData.SUM_HOMESTEAD_IND = theValue;
                                    break;

                                case CUS_SALES_BRANCH_CODE_PATH:
                                    baidData.CUS_SALES_BRANCH_CODE = theValue;
                                    break;

                                case CUS_APPL_RECEIVED_DATE_PATH:
                                    baidData.CUS_APPL_RECEIVED_DATE = theValue;
                                    break;

                                case NAS_LDS_CLOSING_DATE_PATH:
                                    baidData.NAS_LDS_CLOSING_DATE = theValue;
                                    break;

                                case APP_OWN_ID_PATH:
                                    if (appOwnId != null)
                                    {
                                        appOwnId.Value = theValue;
                                    }

                                    break;

                                case CUS_LOAN_TYPE_PATH:
                                    if (cusLoanType != null)
                                    {
                                        cusLoanType.Value = theValue;
                                    }

                                    break;

                                case LIS_BROKER_ID_PATH:
                                    baidData.LIS_BROKER_ID = theValue;
                                    break;

                                case QDE_RQST_DOC_TYPE_PATH:
                                    if (qdeRqstDocType != null)
                                    {
                                        qdeRqstDocType.Value = theValue;
                                    }

                                    break;

                                case WFF_ELIG_FLAG_PATH:
                                    baidData.WFF_ELIG_FLAG = theValue;
                                    break;

                                case CUS_RQST_ORIG_STATE_PATH:
                                    if (cusRqstOrigState != null)
                                    {
                                        cusRqstOrigState.Value = theValue;
                                    }

                                    break;

                                case LIS_PRODUCT_DESCR_PATH:
                                    if (lisProductDesc != null)
                                    {
                                        lisProductDesc.Value = theValue;
                                    }

                                    break;

                                case CUS_OWNING_BRANCH_CODE_PATH:
                                    if (cusOwningBranchCode != null)
                                    {
                                        cusOwningBranchCode.Value = theValue;
                                    }

                                    break;

                                case LIS_PURPOSE_DESCR_PATH:
                                    if (lisPurposeDesc != null)
                                    {
                                        lisPurposeDesc.Value = theValue;
                                    }

                                    break;

                                case SUM_COMPLIANCE_STATUS_PATH:
                                    if (sumComplianceStatus != null)
                                    {
                                        sumComplianceStatus.Value = theValue;
                                    }

                                    break;

                                case SUM_TEX_COMPL_STATUS_PATH:
                                    if (sumTexComplStatus != null)
                                    {
                                        sumTexComplStatus.Value = theValue;
                                    }

                                    break;

                                case SUM_HVC_DESC_PATH:
                                    baidData.SUM_HVC_DESC = theValue;
                                    break;

                                case SUM_DUPLICATE_IND_FIELD_PATH:
                                    baidData.SUM_DUPLICATE_IND_FIELD = theValue;
                                    break;

                                case SUM_FRAUD_IND_PATH:
                                    baidData.SUM_FRAUD_IND = theValue;
                                    break;

                                case SUM_NOTES_IND_FIELD_PATH:
                                    baidData.SUM_NOTES_IND_FIELD = theValue;
                                    break;

                                case SUM_BANKER_NOTE_PATH:
                                    baidData.SUM_BANKER_NOTE = theValue;
                                    break;

                                case RSB_RPT_NUMBER_PATH:
                                    baidData.RSB_RPT_NUMBER = theValue;
                                    break;
                                case SUM_LOAN_AGGREGATION_FLAG_PATH:
                                    baidData.SUM_LOAN_AGGREGATION_FLAG = theValue;
                                    break;

                                case XSELL_IND_PATH:
                                    baidData.XSELL_IND = theValue;
                                    break;

                                case NON_APPL_SPOUSE_IND_PATH:
                                    baidData.NON_APPL_SPOUSE_IND = theValue;
                                    break;

                                case CUS_PRI_MARITAL_STATUS_PATH:
                                    if (cusPriMaritalStatus != null)
                                    {
                                        cusPriMaritalStatus.Value = theValue;
                                    }

                                    break;

                                case CUS_SEC_MARITAL_STATUS_PATH:
                                    if (cusSecMaritalStatus != null)
                                    {
                                        cusSecMaritalStatus.Value = theValue;
                                    }

                                    break;

                                case CUS_HOLD_DATE_PATH:
                                    baidData.CUS_HOLD_DATE = theValue;
                                    break;

                                case SUM_LTV_RATIO_PATH:
                                    baidData.SUM_LTV_RATIO = theValue;
                                    break;

                                case SUM_TDSR_PATH:
                                    baidData.SUM_TDSR = theValue;
                                    break;

                                case SUM_CREDIT_GRADE_PATH:
                                    baidData.SUM_CREDIT_GRADE = theValue;
                                    break;

                                case CUS_RELATED_APP_CODE_PATH:
                                    baidData.CUS_RELATED_APP_CODE = theValue;
                                    break;

                                case CUS_FIB_BUSINESS_NAME_PATH:
                                    baidData.CUS_FIB_BUSINESS_NAME = theValue;
                                    break;

                                case CUS_FIB_BUSINESS_TAX_ID:
                                    baidData.CUS_FIB_BUSINESS_TAX_ID = theValue;
                                    break;

                                case LIS_BROKER_FLAG_PATH:
                                    if (lisBrokerFlag != null)
                                    {
                                        lisBrokerFlag.Value = theValue;
                                    }

                                    break;
                                case vehicleAge_PATH:
                                    baidData.vehicleAge = theValue;
                                    break;
                                case CUS_PRIORITY_SORT_CODE_PATH:
                                    baidData.CUS_PRIORITY_SORT_CODE = theValue;
                                    break;

                                case LOC_HIER_SERV_IND_PATH:
                                    baidData.LOC_HIER_SERV_IND = theValue;
                                    break;

                                case TITLE_REPORT_ID_PATH:
                                    baidData.TITLE_REPORT_ID = theValue;
                                    break;

                                case CBE_MIDDLE_FICO_PRI_PATH:
                                    baidData.CBE_MIDDLE_FICO_PRI = theValue;
                                    break;

                                case CBE_MIDDLE_FICO_SEC_PATH:
                                    baidData.CBE_MIDDLE_FICO_SEC = theValue;
                                    break;

                                case WF_B2B_FINAL_RESULTS_IND_PATH:
                                    baidData.WF_B2B_FINAL_RESULTS_IND = theValue;
                                    break;

                                case REW_WF_REMOD_FLAG_PATH:
                                    baidData.REW_WF_REMOD_FLAG = theValue;
                                    break;

                                case GWF_WF_GWOFAC_IND_PATH:
                                    baidData.GWF_WF_GWOFAC_IND = theValue;
                                    break;

                                case WF_RECALC_FEE_IND_PATH:
                                    baidData.WF_RECALC_FEE_IND = theValue;
                                    break;

                                case SUM_CKP_CHECKPOINT_IND_PATH:
                                    baidData.SUM_CKP_CHECKPOINT_IND = theValue;
                                    break;

                                case ADR_BRKR_IS_UPDATING_PATH:
                                    baidData.ADR_BRKR_IS_UPDATING = theValue;
                                    break;

                                case ADR_BRKR_IS_UPDATING_SINCE_PATH:
                                    baidData.ADR_BRKR_IS_UPDATING_SINCE = theValue;
                                    break;

                                case ADR_BLOCK_UPDATES_IN_ILOL_PATH:
                                    baidData.ADR_BLOCK_UPDATES_IN_ILOL = theValue;
                                    break;

                                case MOR_SIMO_IND_PATH:
                                    baidData.MOR_SIMO_IND = theValue;
                                    break;

                                case APP_RECVD_TIME_PATH:
                                    baidData.APP_RECVD_TIME = theValue;
                                    break;

                                case ADDRESS_ERROR_FLAG_PATH:
                                    baidData.ADDRESS_ERROR_FLAG = theValue;
                                    break;

                                case WF_CREDIT_EXP_IS_UPDATING_PATH:
                                    baidData.WF_CREDIT_EXP_IS_UPDATING = theValue;
                                    break;

                                case WF_DR3_CUSTOMER_IN_STORE_PATH:
                                    baidData.WF_DR3_CUSTOMER_IN_STORE = theValue;
                                    break;

                                case FIRST_TIME_DISPLAY_CBR_NUM_PATH:
                                    baidData.FIRSTTIMEDISPLAYCBRNUM = theValue;
                                    break;

                                case WF_PCM_PYMT_TO_INC_PATH:
                                    baidData.WF_PCM_PYMT_TO_INC = theValue;
                                    break;

                                case WF_PCM_HIGHLIGHT_PTI_FLAG_PATH:
                                    baidData.WF_PCM_HIGHLIGHT_PTI_FLAG = theValue;
                                    break;

                                case APP_HAS_NOTES_FLAG_PATH:
                                    baidData.APP_HAS_NOTES_FLAG = theValue;
                                    break;

                                case ACTIVITY_VA_FOUND_PATH:
                                    baidData.ACTIVITY_VA_FOUND = theValue;
                                    break;

                                case PCM_OWNER_CLEARED_FLAG_PATH:
                                    baidData.PCM_OWNER_CLEARED_FLAG = theValue;
                                    break;

                                case SIMO_CIP_FLAG_PATH:
                                    baidData.SIMO_CIP_FLAG = theValue;
                                    break;

                                case SPP_IND_PATH:
                                    baidData.SPP_IND = theValue;
                                    break;

                                case PRE_APR_IND_PATH:
                                    baidData.PRE_APR_IND = theValue;
                                    break;

                                case LIA_TOTAL_LIAB_ENTRIES_PATH:
                                    baidData.LIA_TOTAL_LIAB_ENTRIES = theValue;
                                    break;

                                case FINAL_EARLY_TIL_LOAN_PATH:
                                    baidData.FINAL_EARLY_TIL_LOAN = theValue;
                                    break;

                                case WELLS_FARGO_ADVISOR_PATH:
                                    baidData.WELLS_FARGO_ADVISOR = theValue;
                                    break;

                                case WF_LENDSCAPE_IS_UPDATING_PATH:
                                    baidData.WF_LENDSCAPE_IS_UPDATING = theValue;
                                    break;

                                case WF_SALES_PARTNER_PILOT_PATH:
                                    baidData.WF_SALES_PARTNER_PILOT = theValue;
                                    break;

                                case WF_PRI_OTHR_INC_USED_IN_DSR_PATH:
                                    baidData.WF_PRI_OTHR_INC_USED_IN_DSR = theValue;
                                    break;

                                case WF_SEC_OTHR_INC_USED_IN_DSR_PATH:
                                    baidData.WF_SEC_OTHR_INC_USED_IN_DSR = theValue;
                                    break;

                                case CURRENTLY_ASSIGNED_TO_PATH:
                                    baidData.CURRENTLY_ASSIGNED_TO = theValue;
                                    break;
                               
                                case ClosMethodBank_SRC_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.SRC = theValue;
                                    }
                                    break;
                                case ClosMethodBank_PURP_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.PURP = theValue;
                                    }
                                    break;
                                case ClosMethodBank_TIER_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.TIER = theValue;
                                    }
                                    break;
                                case ClosMethodBank_RESST_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.RESST = theValue;
                                    }
                                    break;
                                case ClosMethodBank_CLOST_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.CLOST = theValue;
                                    }
                                    break;
                                case ClosMethodBank_REGST_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.REGST = theValue;
                                    }
                                    break;
                                case ClosMethodBank_AGE_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.AGE = theValue;
                                    }
                                    break;
                                case ClosMethodBank_MILEAGE_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.MILEAGE = theValue;
                                    }
                                    break;
                                case ClosMethodBank_RESTRICTIONS_PATH:
                                    if (closMethodBank != null)
                                    {
                                        closMethodBank.RESTRICTIONS = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_SRC_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.SRC = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_PURP_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.PURP = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_TIER_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.TIER = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_RESST_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.RESST = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_CLOST_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.CLOST = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_REGST_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.REGST = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_AGE_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.AGE = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_MILEAGE_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.MILEAGE = theValue;
                                    }
                                    break;
                                case ClosMethodLoan_RESTRICTIONS_PATH:
                                    if (closMethodLoan != null)
                                    {
                                        closMethodLoan.RESTRICTIONS = theValue;
                                    }
                                    break;

                                case ptiTestRequired_PATH:
                                    baidData.ptiTestRequired = theValue;
                                    break;

                                case OOFState_PATH:
                                    baidData.OOFState = theValue;
                                    break;

                                case priRegoFlag_PATH:
                                    baidData.priRegoFlag = theValue;
                                    break;

                                case secRegoFlag_PATH:
                                    baidData.secRegoFlag = theValue;
                                    break;

                                case preScreenFlag_PATH:
                                    baidData.preScreenFlag = theValue;
                                    break;

                                case multiLoanInd_PATH:
                                    baidData.multiLoanInd = theValue;
                                    break;
                                //R1.15
                                case DC1_SET_TITL_FLAG_ON_GUI_PATH:
                                    baidData.DC1_SET_TITL_FLAG_ON_GUI = theValue;
                                    break;
                                case appState_PATH:
                                    baidData.AppCurrState = theValue;
                                    break;
                                //Added on July-7th
                                case prvLpActivityCode_PATH:
                                    baidData.prvLpActivityCode = theValue;
                                    break;
                                case DPActivityDone_PATH:
                                    baidData.DPActivityDone = theValue;
                                    break;
                                case verificationInProgress_PATH:
                                    baidData.VerificationInProgress = theValue;
                                    break;
                                case closingBranchName_PATH:
                                    baidData.ClosingBranchName = theValue;
                                    break;
                                case latestDocsPrinted_PATH:
                                    baidData.LatestDocsPrinted = theValue;
                                    break;
                                case appLoadProcessComplete_PATH:
                                    baidData.appLoadProcessComplete = theValue;
                                    break;
                                case dataCompleteInd_PATH:
                                    baidData.dataCompleteInd = theValue;
                                    break;
                            }

                            switch (xmlReader.Name)
                            {
                                case RELATED_APP:
                                    baidData.RELATED_APPS = relAppList;
                                    break;

                                case DA_CBR_PRJ_LIST:
                                    baidData.DA_CBR_PRJ_LIST1 = cbrPrjList1;
                                    break;

                                case SUM_ACTION_STATUS:
                                    baidData.SUM_ACTION_STATUS = sumActionStatus;
                                    break;

                                case PRIMARY:
                                    baidData.PRIMARY = primary;
                                    break;

                                case SECONDARY:
                                    baidData.SECONDARY = secondary;
                                    break;

                                case ADDRESS:
                                    baidData.ADDRESS = address;
                                    break;

                                case SALES_OFFICER:
                                    baidData.SALES_OFFICER = salesOfficer;
                                    break;

                                case LIS_RELATIONSHP:
                                    baidData.LIS_RELATIONSHP = lisRelationship;
                                    break;

                                case CUS_LOCATION:
                                    baidData.CUS_LOCATION = cusLocation;
                                    break;

                                case LOAN_OFFICER:
                                    baidData.LOAN_OFFICER = loanOfficer;
                                    break;

                                case APP_OWN_ID:
                                    baidData.APP_OWN_ID = appOwnId;
                                    break;

                                case CUS_LOAN_TYPE:
                                    baidData.CUS_LOAN_TYPE = cusLoanType;
                                    break;

                                case QDE_RQST_DOC_TYPE:
                                    baidData.QDE_RQST_DOC_TYPE = qdeRqstDocType;
                                    break;

                                case CUS_RQST_ORIG_STATE:
                                    baidData.CUS_RQST_ORIG_STATE = cusRqstOrigState;
                                    break;

                                case LIS_PRODUCT_DESCR:
                                    baidData.LIS_PRODUCT_DESCR = lisProductDesc;
                                    break;

                                case CUS_OWNING_BRANCH_CODE:
                                    baidData.CUS_OWNING_BRANCH_CODE = cusOwningBranchCode;
                                    break;

                                case LIS_PURPOSE_DESCR:
                                    baidData.LIS_PURPOSE_DESCR = lisPurposeDesc;
                                    break;

                                case SUM_COMPLIANCE_STATUS:
                                    baidData.SUM_COMPLIANCE_STATUS = sumComplianceStatus;
                                    break;

                                case SUM_TEX_COMPL_STATUS:
                                    baidData.SUM_TEX_COMPL_STATUS = sumTexComplStatus;
                                    break;

                                case CUS_PRI_MARITAL_STATUS:
                                    baidData.CUS_PRI_MARITAL_STATUS = cusPriMaritalStatus;
                                    break;

                                case CUS_SEC_MARITAL_STATUS:
                                    baidData.CUS_SEC_MARITAL_STATUS = cusSecMaritalStatus;
                                    break;

                                case LIS_BROKER_FLAG:
                                    baidData.LIS_BROKER_FLAG = lisBrokerFlag;
                                    break;

                                case ClosMethodBank:
                                    baidData.ClosMethodBank = closMethodBank;
                                    break;

                                case ClosMethodLoan:
                                    baidData.ClosMethodLoan = closMethodLoan;
                                    break;
                                case AppCurrState:
                                    //baidData.AppCurrState = appCurrState;
                                    break;
                            }

                            // Adjust current XPath
                            currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                            theValue = string.Empty;
                            break;
                    }
 // switch

                    #endregion
                }

                #endregion
            }
 // using

            #endregion

            return baidData;
        }

        #endregion
    }
}