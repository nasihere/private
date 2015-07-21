// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CwsUpdateHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for CwsUpdateHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.SessionState;

    using Common.Logging;

    using WF.EAI.Data.sif.Services.CWS.CASInq;
    using WF.EAI.Data.sif.Services.CWS.CASUpd;

    using WellsFargo.EAI.SIF.ServiceProxy.com.wellsfargo.service.provider.helper;

    using WF.EAI.BLL.cas.Invokers;
    //using WF.EAI.Data.sif.Services.System2.GetUpdatebleFields;
    //using WF.EAI.Data.sif.Services.System2.UpdateAppFields;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    ///     Summary description for CwsUpdateHelper.
    /// </summary>
    [Serializable]
    public class CwsUpdateHelper
    {
        #region Constants

        /// <summary>
        /// The iaf empapp err code.
        /// </summary>
        private const string IafEmpappErrCode = "303040";

        /// <summary>
        /// The mcc fields.
        /// </summary>
        private const string MCCFields = "MCCFields";

        /// <summary>
        /// The rd fields.
        /// </summary>
        private const string RDFields = "RDFields";

        /// <summary>
        /// The session key.
        /// </summary>
        private const string SessionKey = "CWSUpdateHelper";

        #endregion

        #region Static Fields
        #endregion

        // stores initial field values returned from ACAPS
        #region Fields

        /// <summary>
        /// The _cws update initial field values.
        /// </summary>
        private Hashtable _cwsUpdateInitialFieldValues;

        /// <summary>
        /// The _error codes.
        /// </summary>
        [NonSerialized]
        private string[] _errorCodes;

        // private int _state;
        /// <summary>
        /// The _exception.
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// The _fields.
        /// </summary>
        private Hashtable _fields;

        /// <summary>
        /// The _inq request.
        /// </summary>
        private CwsInqRequest _inqRequest;

        /// <summary>
        /// The _inq response.
        /// </summary>
        private CwsInqResponse _inqResponse;

        /// <summary>
        /// The _inquiry helper.
        /// </summary>
        [NonSerialized]
        private CwsInqHelper _inquiryHelper;

        /// <summary>
        /// The _mcc fields.
        /// </summary>
        private Hashtable _mccFields;

        /// <summary>
        /// The _rd fields.
        /// </summary>
        private Hashtable _rdFields;

        /// <summary>
        /// The _update helper.
        /// </summary>
        [NonSerialized]
        private CwsUPDHelper _updateHelper;

        /// <summary>
        /// The get updatable fields res.
        /// </summary>
        //private UpdateAppDetailsRequestRes getUpdatableFieldsRes;

        /// <summary>
        /// The id.
        /// </summary>
        private ulong id;

        /// <summary>
        /// The requested updated docs.
        /// </summary>
        private string requestedUpdatedDocs = string.Empty;

        // wfaf
        // EAIService eaiService = null;
        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        /// <summary>
        /// The tran_id.
        /// </summary>
        private string tran_id = string.Empty;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CwsUpdateHelper"/> class.
        /// </summary>
        public CwsUpdateHelper()
        {
            // this.State = UpdateHelperState.Initialize;
            this._fields = new Hashtable();
            this._rdFields = new Hashtable();
            this._mccFields = new Hashtable();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cws update initial field values.
        /// </summary>
        public Hashtable CwsUpdateInitialFieldValues
        {
            get
            {
                return this._cwsUpdateInitialFieldValues;
            }

            set
            {
                this._cwsUpdateInitialFieldValues = value;
            }
        }

        /// <summary>
        /// Gets or sets the error codes.
        /// </summary>
        public string[] ErrorCodes
        {
            get
            {
                // string[] errorCodes = null;
                // if (_errorCodes != null)
                // {
                // errorCodes = (string[])_errorCodes.ToArray(typeof(string));
                // }
                return this._errorCodes;
            }

            set
            {
                this._errorCodes = value;
            }
        }

        /// <summary>
        /// Gets or sets the requested updated docs.
        /// </summary>
        public string RequestedUpdatedDocs
        {
            get
            {
                return this.requestedUpdatedDocs;
            }

            set
            {
                this.requestedUpdatedDocs = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the session.
        /// </summary>
        protected static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Session;
                }

                return null;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="Name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="casField"/>.
        /// </returns>
        public casField this[string Name]
        {
            get
            {
                if (this._fields != null && this._fields.Count > 0 && this._fields.Contains(Name))
                {
                    return (casField)this._fields[Name];
                }

                return null;
            }

            set
            {
                if (this._fields != null && this._fields.Count > 0 && this._fields.Contains(Name))
                {
                    this._fields[Name] = value;
                }
                else if (this._fields != null && this._fields.Count > 0 && this._fields.Contains(Name))
                {
                    this._fields.Add(Name, value);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="CwsUpdateHelper"/>.
        /// </returns>
        public static CwsUpdateHelper Get()
        {
            if (Session != null && Session["CWSUpdateHelper"] != null)
            {
                return (CwsUpdateHelper)Session["CWSUpdateHelper"];
            }

            return new CwsUpdateHelper();
        }

        /// <summary>
        /// The get mcc fields.
        /// </summary>
        /// <returns>
        /// The <see cref="Hashtable"/>.
        /// </returns>
        public static Hashtable GetMCCFields()
        {
            if (Session != null && Session[MCCFields] != null)
            {
                return (Hashtable)Session[MCCFields];
            }

            return new Hashtable();
        }

        /// <summary>
        /// The get rd fields.
        /// </summary>
        /// <returns>
        /// The <see cref="Hashtable"/>.
        /// </returns>
        public static Hashtable GetRDFields()
        {
            if (Session != null && Session[RDFields] != null)
            {
                return (Hashtable)Session[RDFields];
            }

            return new Hashtable();
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public static void Set(CwsUpdateHelper obj)
        {
            if (Session != null && Session["CWSUpdateHelper"] != null)
            {
                Session["CWSUpdateHelper"] = obj;
            }
        }

        /// <summary>
        /// The begin inquiry.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="PendingValidation">
        /// The pending validation.
        /// </param>
        /// <param name="CheckEligibility">
        /// The check eligibility.
        /// </param>
        /// <returns>
        /// The <see cref="CwsInqHelper"/>.
        /// </returns>
        public CwsInqHelper BeginInquiry(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility)
        {
            if (UserSalesId.Trim().Length == 0)
            {
                UserSalesId = "12345";
            }

            if (ApplicationId.Length == 11 || ApplicationId.Length == 12)
            {
                return this.wfafCWSINQ(
                    ApplicationId, UserLocationCode, UserId, UserSalesId, UserAU, PendingValidation, CheckEligibility);
            }
            else
            {
                return this.acapsCWSINQ(
                    ApplicationId, UserLocationCode, UserId, UserSalesId, UserAU, PendingValidation, CheckEligibility);
            }
        }

        /// <summary>
        /// The begin inquiry.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="PendingValidation">
        /// The pending validation.
        /// </param>
        /// <param name="CheckEligibility">
        /// The check eligibility.
        /// </param>
        /// <param name="casFields">
        /// The cas fields.
        /// </param>
        /// <returns>
        /// The <see cref="CwsInqHelper"/>.
        /// </returns>
        public CwsInqHelper BeginInquiry(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility, 
            casField[] casFields)
        {
            if (UserSalesId.Trim().Length == 0)
            {
                UserSalesId = "12345";
            }

            if (ApplicationId.Length == 11 || ApplicationId.Length == 12)
            {
                return this.wfafCWSINQ(
                    ApplicationId, UserLocationCode, UserId, UserSalesId, UserAU, PendingValidation, CheckEligibility);
            }
            else
            {
                return this.acapsCWSINQ(
                    ApplicationId, 
                    UserLocationCode, 
                    UserId, 
                    UserSalesId, 
                    UserAU, 
                    PendingValidation, 
                    CheckEligibility, 
                    casFields);
            }
        }

        /// <summary>
        /// The begin update.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="applicationStatus">
        /// The application status.
        /// </param>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="userAU">
        /// The user au.
        /// </param>
        /// <param name="userLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userSalesId">
        /// The user sales id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool BeginUpdate(
            ArrayList list, 
            string applicationStatus, 
            string appId, 
            string userAU, 
            string userLocationCode, 
            string userId, 
            string userSalesId)
        {
            //if (appId.Length == 11 || appId.Length == 12)
            //{
            //    // to do wfaf
            //    return this.wfafCWSUPD(list, applicationStatus, appId, userAU, userLocationCode, userId, userSalesId);
            //}
            //else
            //{
                return this.acapsCWSUPD(list, applicationStatus, appId, userAU, userLocationCode, userId, userSalesId);
            //}
        }

        // wfaf

        /// <summary>
        /// The build table.
        /// </summary>
        /// <param name="myHashTable">
        /// The my hash table.
        /// </param>
        public void BuildTable(Hashtable myHashTable)
        {
            // Create a array of correct type with correct length   
            int k = myHashTable.Count;
            string[] datasortedvalue = new string[k];

            // Populate the array with correct keys.
            int i = 0;
            foreach (DictionaryEntry de in myHashTable)
            {
                datasortedvalue[i] = de.Key.ToString();
                i++;
            }

            Array.Sort(datasortedvalue);

            Hashtable newRDTable;
            newRDTable = new Hashtable();
            object oldRDVal;
            for (int j = 0; j < datasortedvalue.Length; j++)
            {
                if (myHashTable.ContainsKey(datasortedvalue[j]))
                {
                    oldRDVal = myHashTable[datasortedvalue[j]];

                    newRDTable.Add(datasortedvalue[j], oldRDVal);
                }
            }
        }

        /// <summary>
        /// The end inquiry.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool EndInquiry()
        {
            if (this._inquiryHelper != null)
            {
                try
                {
                    this._mccFields.Clear();

                    this._inqResponse = this._inquiryHelper.Response;

                    if (this._inqResponse != null && this._inqResponse.Body != null
                        && this._inqResponse.Body.getMaskInquiryResponse != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList.Length > 0)
                    {
                        // caching initial field values from ACAPS
                        this._cwsUpdateInitialFieldValues = new Hashtable();
                        ArrayList keyList = new ArrayList();

                        foreach (casField field in this._inqResponse.Body.getMaskInquiryResponse.info.fieldList)
                        {
                            string key;
                            if (field.name == "PE5_RBP_QTE_RD_DESC1_1" || field.name == "PE5_RBP_QTE_RD_DESC1_2"
                                || field.name == "PE5_RBP_QTE_RD_DESC1_3" || field.name == "PE5_RBP_QTE_RD_DESC1_4"
                                || field.name == "PE5_RBP_QTE_RD_DESC2_1" || field.name == "PE5_RBP_QTE_RD_DESC2_2"
                                || field.name == "PE5_RBP_QTE_RD_DESC2_3" || field.name == "PE5_RBP_QTE_RD_DESC2_4"
                                || field.name == "PE5_RBP_QTE_RD_STATUS_1" || field.name == "PE5_RBP_QTE_RD_STATUS_2"
                                || field.name == "PE5_RBP_QTE_RD_STATUS_3" || field.name == "PE5_RBP_QTE_RD_STATUS_4"
                                || field.name == "PE5_RBP_QTE_RD_RATE_1" || field.name == "PE5_RBP_QTE_RD_RATE_2"
                                || field.name == "PE5_RBP_QTE_RD_RATE_3" || field.name == "PE5_RBP_QTE_RD_RATE_4"
                                || field.name == "PE5_RBP_QTE_RD_BNDL_1" || field.name == "PE5_RBP_QTE_RD_BNDL_2"
                                || field.name == "PE5_RBP_QTE_RD_BNDL_3" || field.name == "PE5_RBP_QTE_RD_BNDL_4")
                            {
                                if (field.value != null && field.value.Length > 0)
                                {
                                    string[] arr = field.name.Split('_');
                                    key = arr[arr.Length - 1] + "_" + field.page_number;
                                    string fieldName = arr[0];
                                    for (int i = 1; i <= arr.Length - 1; i++)
                                    {
                                        fieldName = fieldName + "_" + arr[i];
                                    }

                                    if (this._rdFields.Contains(key))
                                    {
                                        CwsRDCls rdCls = (CwsRDCls)this._rdFields[key];
                                        this._rdFields[key] = setRDCls(rdCls, fieldName, field.value);
                                        this._cwsUpdateInitialFieldValues[key] = this._rdFields[key];
                                    }
                                    else
                                    {
                                        CwsRDCls rdCls = new CwsRDCls();
                                        rdCls = setRDCls(rdCls, fieldName, field.value.Trim());
                                        rdCls.rdKey = key;

                                        this._rdFields.Add(key, rdCls);
                                        this._cwsUpdateInitialFieldValues.Add(key, rdCls);
                                    }
                                }
                            }

                                #region @@@ R2.11 MCC backed out

                                // else if 
                                // (
                                // field.name == "FEE_CLS_COST_PAID_BY" ||

                                // field.name == "FEE_FEE_CODE_1" ||
                                // field.name == "FEE_FEE_AMT_1" ||
                                // field.name == "FEE_FEE_FIN_IND_1" ||
                                // field.name == "FEE_FEE_POC_1" ||
                                // field.name == "FEE_FEE_PIC_1" ||

                                // field.name == "FEE_FEE_CODE_2" ||
                                // field.name == "FEE_FEE_AMT_2" ||
                                // field.name == "FEE_FEE_FIN_IND_2" ||
                                // field.name == "FEE_FEE_POC_2" ||
                                // field.name == "FEE_FEE_PIC_2" ||

                                // field.name == "FEE_FEE_CODE_3" ||
                                // field.name == "FEE_FEE_AMT_3" ||
                                // field.name == "FEE_FEE_FIN_IND_3" ||
                                // field.name == "FEE_FEE_POC_3" ||
                                // field.name == "FEE_FEE_PIC_3" ||

                                // field.name == "FEE_FEE_CODE_4" ||
                                // field.name == "FEE_FEE_AMT_4" ||
                                // field.name == "FEE_FEE_FIN_IND_4" ||
                                // field.name == "FEE_FEE_POC_4" ||
                                // field.name == "FEE_FEE_PIC_4" ||

                                // field.name == "FEE_FEE_CODE_5" ||
                                // field.name == "FEE_FEE_AMT_5" ||
                                // field.name == "FEE_FEE_FIN_IND_5" ||
                                // field.name == "FEE_FEE_POC_5" ||
                                // field.name == "FEE_FEE_PIC_5" ||

                                // field.name == "FEE_FEE_CODE_6" ||
                                // field.name == "FEE_FEE_AMT_6" ||
                                // field.name == "FEE_FEE_FIN_IND_6" ||
                                // field.name == "FEE_FEE_POC_6" ||
                                // field.name == "FEE_FEE_PIC_6")
                                // {
                                // if (!string.IsNullOrEmpty(field.value))
                                // {
                                // // create MCC CWSINQ fields Hashtable
                                // if (field.name.IndexOf("FEE_FEE_CODE") >= 0)
                                // {
                                // key = field.value;
                                // }
                                // else key = field.name.Trim() + ":" + field.page_number.Trim() + ":";

                                // if (!keyList.Contains(key))
                                // {
                                // string mccValue = field.name.Trim() + ";" + field.length.Trim() + ";" + field.page_number.Trim() + ";" +
                                // field.value.Trim() + ";" + field.@protected;
                                // _mccFields.Add(key, mccValue);

                                // // avoid duplicated fee code
                                // if (field.name.IndexOf("FEE_FEE_CODE") >= 0)
                                // {
                                // keyList.Add(key);
                                // }
                                // }
                                // }

                                // }
                                #endregion
                            else
                            {
                                if (this._fields.Contains(field.name))
                                {
                                    this._fields[field.name] = field;
                                    this._cwsUpdateInitialFieldValues[field.name] = field.value == null
                                                                                        ? string.Empty
                                                                                        : field.value;
                                }
                                else
                                {
                                    this._fields.Add(field.name, field);
                                    this._cwsUpdateInitialFieldValues.Add(
                                        field.name, field.value == null ? string.Empty : field.value);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error(this.GetType(), ex);
                    return false;
                }

                if (this._rdFields != null)
                {
                    Session[RDFields] = this._rdFields;
                }

                if (this._mccFields != null)
                {
                    Session[MCCFields] = this._mccFields;
                }

                Session[SessionKey] = this;

                return true;
            }
            else if (this._inquiryHelper != null)
            {
                this._exception = this._inquiryHelper.Exception;
            }

            return false;
        }

        /// <summary>
        /// The end update.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool EndUpdate()
        {
            bool _eFlag = true;

            if (this._updateHelper != null)
            {
                if (this._updateHelper.Exception != null)
                {
                    return false;
                }
                else
                {
                    if (this._updateHelper.ErrorCodes != null && this._updateHelper.ErrorCodes.Count > 0)
                    {
                        this.ErrorCodes = (string[])this._updateHelper.ErrorCodes.ToArray(typeof(string));
                        _eFlag = false;
                    }
                }
            }
            else
            {
                _eFlag = false;
            }

            this._inquiryHelper = null;
            this._updateHelper = null;

            return _eFlag;
        }

        /// <summary>
        /// The get updated fields.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="applicationStatus">
        /// The application status.
        /// </param>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <returns>
        /// The <see cref="casField[]"/>.
        /// </returns>
        public casField[] GetUpdatedFields(ArrayList list, string applicationStatus, string appId)
        {
            // This foreach-loop adds *only fields modified by the user* to an ArrayList to be sent to ACAPS.
            ArrayList _tempFields = new ArrayList();
            foreach (object key in this._fields.Keys)
            {
                casField field = (casField)this._fields[key];

                // retrieves initial field values
                string initField = (string)this._cwsUpdateInitialFieldValues[field.name];
                string fieldValue = field.value == null ? string.Empty : field.value.Trim().ToUpper();
                initField = initField == null ? string.Empty : initField.Trim().ToUpper();

                // Need to capitalize every field when doing a comparison, 
                // because ACAPS capitalizes every value by default, and we don't on the UI.
                if (initField != fieldValue || (field.name == "CAL_FC_NOTE_DATE" && applicationStatus != "TD"))
                {
                    if (!_tempFields.Contains(this._fields[key]))
                    {
                        _tempFields.Add(this._fields[key]);
                    }

                    // This is for the "Mother's Maiden Name" field, because the value is never returned by ACAPS or
                    // displayed on the UI for security purpose, so if the user changes Equity from Y to N or N/A, we need
                    // to nullify the Mother's Maiden Name field as well.
                    if (field.name == "ADE_CR_CRD_OPT_IND" && (field.value == "N" || field.value == "U"))
                    {
                        _tempFields.Add(this._fields["CAS_MOTHERS_MAIDEN_NAME"]);
                    }

                    // This is for pre-populated ACHAccountType field, because the field is overloaded to display
                    // account numbers as well, so we need to split them up to their appropriate fields before
                    // sending to ACAPS.
                    // TODO (2/13/2006): fix this to resolve issues --
                    // 		1. What should ACHAccountType value be if an account number is selected?
                    // 		2. Setting account number to ACHAccountType value, what about routing number?
                    // 		3. What happens is account number is not found in ECBS? (issue brought up by Jonathan)
                    if (field.name == "NAS_DDA_OR_SAVINGS_CODE" && "SC".IndexOf(field.value) < 0)
                    {
                        // Issue #1 above. Setting to "Other" for now.
                        casField swapFieldAcctType = (casField)this._fields["NAS_DDA_OR_SAVINGS_CODE"];
                        string tmpAcctNum = swapFieldAcctType.value;
                        swapFieldAcctType.value = "C";

                        // Issue #2 above.
                        casField swapFieldAcctNum = (casField)this._fields["NAS_DEPOSIT_ACCOUNT_NUMBER"];
                        casField swapFieldRtnNum = (casField)this._fields["NAS_ACH_NUMBER"];
                        int tmpAcctNumLoc = tmpAcctNum.IndexOf('-');
                        if (tmpAcctNumLoc >= 0)
                        {
                            swapFieldAcctNum.value = tmpAcctNum.Substring(0, tmpAcctNumLoc);
                            swapFieldRtnNum.value = tmpAcctNum.Substring(tmpAcctNumLoc + 1);

                            // R2.08 P0011361 BAU Fulfillment 3/31/08 AK
                            string _accountNumberType = Session["AccountNumberType"].ToString();
                            if (_accountNumberType != string.Empty)
                            {
                                string[] arr = _accountNumberType.Split('|');
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (arr[i].IndexOf(swapFieldAcctNum.value) >= 0)
                                    {
                                        swapFieldAcctType.value = arr[i].Substring(arr[i].Length - 1, 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // "Other" was selected from drop-down
                            swapFieldAcctType.value = "C";
                        }

                        if (!_tempFields.Contains(this._fields["NAS_DEPOSIT_ACCOUNT_NUMBER"]))
                        {
                            _tempFields.Add(this._fields["NAS_DEPOSIT_ACCOUNT_NUMBER"]);
                        }

                        if (!_tempFields.Contains(this._fields["NAS_ACH_NUMBER"]))
                        {
                            _tempFields.Add(this._fields["NAS_ACH_NUMBER"]);
                        }
                    }

                    if (field.name == "NAS_ACH_FLAG" && field.value.ToUpper().Trim().Equals("Y"))
                    {
                        string sessionId = string.Empty;
                        if (HttpContext.Current != null && HttpContext.Current.Session != null
                            && HttpContext.Current.Session.SessionID != string.Empty)
                        {
                            sessionId = HttpContext.Current.Session.SessionID;
                        }

                        //Logger.Instance.Info(
                        //"NAS_ACH_FLAG value = Y : " + DateTime.Now + " AppId: " + appId + " SessionId: " + sessionId);
                    }
                }
            }

            // MPA Project R2.09
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    _tempFields.Add(list[i]);
                }
            }

            return (casField[])_tempFields.ToArray(typeof(casField));
        }

        /// <summary>
        /// The is writeable.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsWriteable(string key)
        {
            casField _f = this[key];

            if (_f != null)
            {
                string _p = _f.@protected.Replace("_", string.Empty);
                switch (_p)
                {
                    case " ":
                        return true;
                    case "A":
                        return true;
                    case "&":
                        return true;
                    case "H":
                        return true;
                    case "I":
                        return true;
                    case "<":
                        return true;
                    case "0":
                        return false;
                    case "8":
                        return false;
                    case "@":
                        return false;
                    case "-":
                        return false;
                    case "Y":
                        return false;
                    case "%":
                        return false;
                    default:
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// The revert.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Revert()
        {
            this._fields.Clear();

            if (this._inqResponse != null && this._inqResponse.Body != null
                && this._inqResponse.Body.getMaskInquiryResponse != null
                && this._inqResponse.Body.getMaskInquiryResponse.info != null
                && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList != null
                && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList.Length > 0)
            {
                foreach (casField field in this._inqResponse.Body.getMaskInquiryResponse.info.fieldList)
                {
                    if (this._fields.Contains(field.name))
                    {
                        this._fields[field.name] = field;
                    }
                    else
                    {
                        this._fields.Add(field.name, field);
                    }
                }
            }

            Session[SessionKey] = this;
            return true;
        }

        /// <summary>
        /// The acaps cwsupd.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="applicationStatus">
        /// The application status.
        /// </param>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="userAU">
        /// The user au.
        /// </param>
        /// <param name="userLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userSalesId">
        /// The user sales id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool acapsCWSUPD(
            ArrayList list, 
            string applicationStatus, 
            string appId, 
            string userAU, 
            string userLocationCode, 
            string userId, 
            string userSalesId)
        {
            // set tran_id for logging
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            this.id = CASUtils.GetUniqueInstanceID();
            this.tran_id = this.session_id + "." + this.id;
            CWSUpdRes getUpdateRes;
            casField[] casFields = this.GetUpdatedFields(list, applicationStatus, appId);

            getUpdateRes = Invoker.UpdateACAPS(casFields, this._inqResponse, userSalesId, this.session_id);

            if (getUpdateRes != null)
            {
                this._updateHelper = getUpdateRes.cwsUpdHelper;
            }

            return this._updateHelper != null && this._updateHelper.IsOK;
        }

        /// <summary>
        /// The wfaf cwsupd.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="applicationStatus">
        /// The application status.
        /// </param>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="userAU">
        /// The user au.
        /// </param>
        /// <param name="userLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userSalesId">
        /// The user sales id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        //public bool wfafCWSUPD(
        //    ArrayList list, 
        //    string applicationStatus, 
        //    string appId, 
        //    string userAU, 
        //    string userLocationCode, 
        //    string userId, 
        //    string userSalesId)
        //{
        //    // set tran_id for logging
        //    if (HttpContext.Current != null && HttpContext.Current.Session != null)
        //    {
        //        this.session_id = HttpContext.Current.Session.SessionID;
        //    }

        //    this.id = CASUtils.GetUniqueInstanceID();
        //    this.tran_id = this.session_id + "." + this.id;

        //    casField[] casFields = this.GetUpdatedFields(list, applicationStatus, appId);

        //    //this.getUpdatableFieldsRes = Invoker.UpdateAppDetailsRequest(
        //    //    userAU, 
        //    //    string.Empty, 
        //    //    appId, 
        //    //    userLocationCode, 
        //    //    userId, 
        //    //    userSalesId, 
        //    //    casFields, 
        //    //    "UpdateAppDetailsRequest", 
        //    //    "CAS", 
        //    //    this.session_id);

        //    if (this.getUpdatableFieldsRes != null)
        //    {
        //        this._updateHelper = this.getUpdatableFieldsRes.cwsUpdHelper;
        //    }

        //    return this._updateHelper != null && this._updateHelper.IsOK;
        //}

        #endregion

        #region Methods

        /// <summary>
        /// The set rd cls.
        /// </summary>
        /// <param name="rdCls">
        /// The rd cls.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <param name="fieldValue">
        /// The field value.
        /// </param>
        /// <returns>
        /// The <see cref="CwsRDCls"/>.
        /// </returns>
        private static CwsRDCls setRDCls(CwsRDCls rdCls, string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "PE5_RBP_QTE_RD_DESC1_1":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC1_2":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC1_3":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC1_4":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC2_1":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC2_2":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC2_3":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_DESC2_4":
                    rdCls.rdDescription1 = rdCls.rdDescription1 + fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_STATUS_1":
                    rdCls.rdstatus = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_STATUS_2":
                    rdCls.rdstatus = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_STATUS_3":
                    rdCls.rdstatus = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_STATUS_4":
                    rdCls.rdstatus = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_RATE_1":
                    rdCls.rdrate = Convert.ToDecimal(fieldValue);
                    return rdCls;
                case "PE5_RBP_QTE_RD_RATE_2":
                    rdCls.rdrate = Convert.ToDecimal(fieldValue);
                    return rdCls;
                case "PE5_RBP_QTE_RD_RATE_3":
                    rdCls.rdrate = Convert.ToDecimal(fieldValue);
                    return rdCls;
                case "PE5_RBP_QTE_RD_RATE_4":
                    rdCls.rdrate = Convert.ToDecimal(fieldValue);
                    return rdCls;
                case "PE5_RBP_QTE_RD_BNDL_1":
                    rdCls.rdbundleindicator = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_BNDL_2":
                    rdCls.rdbundleindicator = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_BNDL_3":
                    rdCls.rdbundleindicator = fieldValue;
                    return rdCls;
                case "PE5_RBP_QTE_RD_BNDL_4":
                    rdCls.rdbundleindicator = fieldValue;
                    return rdCls;

                default:
                    return rdCls;
            }
        }

        /// <summary>
        /// The check acaps message.
        /// </summary>
        private void CheckACAPSMessage()
        {
            if (this._inquiryHelper != null && this._inquiryHelper.IsOK)
            {
                try
                {
                    this._inqResponse = this._inquiryHelper.Response;
                    if (this._inqResponse != null && this._inqResponse.Body != null
                        && this._inqResponse.Body.getMaskInquiryResponse != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info.ACAPSMessage != null
                        && this._inqResponse.Body.getMaskInquiryResponse.info.ACAPSMessage.Length > 0)
                    {
                        foreach (
                            casErrorPanel errorPanel in this._inqResponse.Body.getMaskInquiryResponse.info.ACAPSMessage)
                        {
                            if (errorPanel != null && errorPanel.error != null && errorPanel.error.type != null
                                && errorPanel.error.type.Trim().ToUpper() == "WARNING"
                                && errorPanel.error.errorCode.Trim() == IafEmpappErrCode)
                            {
                                // EMP APP
                                Session["IAF_EMPAPP"] = "true";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error(this.GetType(), ex);
                }
            }
        }

        /// <summary>
        /// The acaps cwsinq.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="PendingValidation">
        /// The pending validation.
        /// </param>
        /// <param name="CheckEligibility">
        /// The check eligibility.
        /// </param>
        /// <returns>
        /// The <see cref="CwsInqHelper"/>.
        /// </returns>
        private CwsInqHelper acapsCWSINQ(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility)
        {
            this._inqResponse = null;
            this._inqRequest = null;

            this._fields.Clear();
            this._rdFields.Clear();
            this._exception = null;
            CWSInqRes cwsInqRes;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            // acaps
            cwsInqRes = Invoker.GetAcapsData(
                ApplicationId, 
                UserLocationCode, 
                UserId, 
                UserSalesId, 
                UserAU, 
                PendingValidation, 
                CheckEligibility, 
                this.session_id);
            if (cwsInqRes != null)
            {
                this._inquiryHelper = cwsInqRes.cwsInqHelper;

                // PB000014394861 - PCM - BANKERS ARE UNABLE TO PRINT LOAN DOCUMENTS ON EMPLOYEE APPLICATIONS for IAF
                if (Session["LOB"] != null && Session["LOB"].ToString() == "P")
                {
                    this.CheckACAPSMessage();
                }
            }

            return this._inquiryHelper;
        }

        /// <summary>
        /// The acaps cwsinq.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="PendingValidation">
        /// The pending validation.
        /// </param>
        /// <param name="CheckEligibility">
        /// The check eligibility.
        /// </param>
        /// <param name="casFields">
        /// The cas fields.
        /// </param>
        /// <returns>
        /// The <see cref="CwsInqHelper"/>.
        /// </returns>
        private CwsInqHelper acapsCWSINQ(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility, 
            casField[] casFields)
        {
            this._inqResponse = null;
            this._inqRequest = null;
            this._exception = null;
            CWSInqRes cwsInqRes;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            // acaps
            cwsInqRes = Invoker.GetCollateralData(
                ApplicationId, 
                UserLocationCode, 
                UserId, 
                UserSalesId, 
                UserAU, 
                PendingValidation, 
                CheckEligibility, 
                casFields, 
                this.session_id);
            if (cwsInqRes != null)
            {
                this._inquiryHelper = cwsInqRes.cwsInqHelper;

                // PB000014394861 - PCM - BANKERS ARE UNABLE TO PRINT LOAN DOCUMENTS ON EMPLOYEE APPLICATIONS for IAF
                if (Session["LOB"] != null && Session["LOB"].ToString() == "P")
                {
                    this.CheckACAPSMessage();
                }
            }

            return this._inquiryHelper;
        }

        /// <summary>
        /// The wfaf cwsinq.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserLocationCode">
        /// The user location code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="PendingValidation">
        /// The pending validation.
        /// </param>
        /// <param name="CheckEligibility">
        /// The check eligibility.
        /// </param>
        /// <returns>
        /// The <see cref="CwsInqHelper"/>.
        /// </returns>
        private CwsInqHelper wfafCWSINQ(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility)
        {
            this._inqResponse = null;
            this._inqRequest = null;
            this._fields.Clear();
            this._rdFields.Clear();
            this._exception = null;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }
            Logger.Instance.Info("GetUpdatableFields Call NOT made to decommissioned System2 service.");
            //P0501837-3_CFS-Auto-Sys2-Decommission
            //GetUpdatableFieldsRes getUpdatableFieldsRes = Invoker.GetUpdatableFields(
            //    UserAU, 
            //    string.Empty, 
            //    ApplicationId, 
            //    UserLocationCode, 
            //    UserId, 
            //    UserSalesId, 
            //    "GetUpdatableFields", 
            //    "CAS", 
            //    this.session_id);

            //if (getUpdatableFieldsRes != null)
            //{
            //    this._inquiryHelper = getUpdatableFieldsRes.cwsInqHelper;
            //}

            return this._inquiryHelper;
        }

        #endregion
    }
}