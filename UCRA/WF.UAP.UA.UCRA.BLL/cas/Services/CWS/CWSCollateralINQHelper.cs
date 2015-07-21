// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CWSCollateralINQHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The cws collateral inq helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.SessionState;

    using WF.EAI.Data.sif.Services.CWS.CASInq;
    using WF.EAI.Data.sif.Services.CWS.CASUpd;

    using WellsFargo.EAI.SIF.ServiceProxy.com.wellsfargo.service.provider.helper;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.BLL.cas.Services.Collateral;

    /// <summary>
    /// The cws collateral inq helper.
    /// </summary>
    [Serializable]
    public class CWSCollateralINQHelper
    {
        #region Constants

        /// <summary>
        /// The session collateral key.
        /// </summary>
        private const string SessionCollateralKey = "Collateral";

        /// <summary>
        /// The session key.
        /// </summary>
        private const string SessionKey = "CWSCollateralINQHelper";

        #endregion

        #region Fields

        /// <summary>
        /// The _error codes.
        /// </summary>
        [NonSerialized]
        private string[] _errorCodes;

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
        /// The _update helper.
        /// </summary>
        [NonSerialized]
        private CwsUPDHelper _updateHelper;

        /// <summary>
        /// The id.
        /// </summary>
        private ulong id;

        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        /// <summary>
        /// The tran_id.
        /// </summary>
        private string tran_id = string.Empty;

        #endregion

        #region Public Properties

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

        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="CWSCollateralINQHelper"/>.
        /// </returns>
        public static CWSCollateralINQHelper Get()
        {
            if (Session != null && Session[SessionKey] != null)
            {
                return (CWSCollateralINQHelper)Session[SessionKey];
            }

            return new CWSCollateralINQHelper();
        }

        /// <summary>
        /// The get collateral.
        /// </summary>
        /// <returns>
        /// The <see cref="CASCollateral"/>.
        /// </returns>
        public static CASCollateral GetCollateral()
        {
            // If in session then return
            if (Session[SessionCollateralKey] != null)
            {
                return (CASCollateral)Session[SessionCollateralKey];
            }
            else
            {
                // else return a new Collateral Object without any values in it.
                return new CASCollateral();
            }
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public static void Set(CWSCollateralINQHelper obj)
        {
            if (Session != null)
            {
                Session[SessionKey] = obj;
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

            return this.acapsCWSINQ(
                ApplicationId, UserLocationCode, UserId, UserSalesId, UserAU, PendingValidation, CheckEligibility);
        }

        /// <summary>
        /// The begin update.
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
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool BeginUpdate(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility, 
            PCMCollateral pcmCollateral, 
            int index)
        {
            if (UserSalesId.Trim().Length == 0)
            {
                UserSalesId = "12345";
            }

            casField[] casFields;
            string strIndex;
            strIndex = index == 1 ? "002" : "001";
            if (pcmCollateral.collateralType.Equals("SavingsCD"))
            {
                casFields = this.SetSavingsCDFields(pcmCollateral);
            }
            else if (pcmCollateral.collateralType.Equals("StockBonds"))
            {
                casFields = this.SetStockBondsFields(pcmCollateral);
            }
            else
            {
                casFields = this.SetCollateralFields(pcmCollateral, strIndex);
            }

            return this.acapsCWSINQUpdate(
                ApplicationId, 
                UserLocationCode, 
                UserId, 
                UserSalesId, 
                UserAU, 
                PendingValidation, 
                CheckEligibility, 
                casFields);
        }

        /// <summary>
        /// The get collateral data.
        /// </summary>
        /// <returns>
        /// The <see cref="CASCollateral"/>.
        /// </returns>
        public CASCollateral getCollateralData()
        {
            CASCollateral casCollateral = new CASCollateral();
            PCMCollateral pcmCollateralOne = new PCMCollateral();
            PCMCollateral pcmCollateralTwo = new PCMCollateral();

            CDCItem[] arrCDCItem;
            arrCDCItem = new CDCItem[9];
            for (int idx = 0; idx < 9; idx++)
            {
                arrCDCItem[idx] = new CDCItem();
            }

            SBCItem[] arrSBCItem;
            arrSBCItem = new SBCItem[9];
            for (int idx = 0; idx < 9; idx++)
            {
                arrSBCItem[idx] = new SBCItem();
            }

            // Set Different Collateral
            if (this._inqResponse != null && this._inqResponse.Body != null
                && this._inqResponse.Body.getMaskInquiryResponse != null
                && this._inqResponse.Body.getMaskInquiryResponse.info != null
                && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList != null
                && this._inqResponse.Body.getMaskInquiryResponse.info.fieldList.Length > 0)
            {
                foreach (casField field in this._inqResponse.Body.getMaskInquiryResponse.info.fieldList)
                {
                    // Set PCM Collaterals
                    if (!string.IsNullOrEmpty(field.value))
                    {
                        if (field.name.Contains("CDC_"))
                        {
                            this.GetSavingCDValue(field, arrCDCItem);
                        }
                        else if (field.name.Contains("SBC_"))
                        {
                            this.GetStockBondValue(field, arrSBCItem);
                        }
                        else
                        {
                            switch (field.name)
                            {
                                case "BKG_PCM_COLL_IS_PRIMARY_RES":
                                    casCollateral.primaryResidence = field.value;
                                    break;
                                case "ACL_WF_ACL_PAYOFF_PRICE":
                                    casCollateral.payOffPrice = CASUtils.formatData(field.value);
                                    break;
                                case "ACL_ACL_NEW_CASH_DOWN_AMT":
                                    casCollateral.downPayment = CASUtils.formatData(field.value);
                                    break;
                                case "ACL_WF_ACL_FEES_FINANCED":
                                    casCollateral.feeFinanced = field.value;
                                    break;
                                case "ACL_ACL_SPEED_TYPE":
                                    casCollateral.transmissionType = field.value;
                                    break;
                                default:
                                    this.GetFieldValue(field, "001", pcmCollateralOne);
                                    this.GetFieldValue(field, "002", pcmCollateralTwo);
                                    break;
                            }

                            if (field.name == "ABC_ABC_COLL_IND" && field.page_number == "001")
                            {
                                casCollateral.collateralOneType = this.GetCollateralType(field);
                                    
                                    // Set first collateral type
                            }

                            if (field.name == "ABC_ABC_COLL_IND" && field.page_number == "002")
                            {
                                casCollateral.collateralTwoType = this.GetCollateralType(field);
                                    
                                    // Set second collateral type
                            }
                        }
                    }
                }
            }

            // Set first collateral as Saving and CD Collateral 
            this.PopulateSavingsCD(casCollateral, pcmCollateralOne, arrCDCItem);
            arrCDCItem = null; // Set Object to null to release the memory

            // Set Second collateral as Stock Bond Collateral
            if (!string.IsNullOrEmpty(casCollateral.collateralOneType))
            {
                this.PopulateStockBonds(casCollateral, pcmCollateralTwo, arrSBCItem);
            }
            else
            {
                this.PopulateStockBonds(casCollateral, pcmCollateralOne, arrSBCItem);
            }

            arrSBCItem = null; // Set Object to null to release the memory

            // R4.10 PAC Ticket PRB000011616549 -- If first collateral is not valid and 2nd collateral is valid then set set 1st collaterl with 2nd collateral
            if (casCollateral.collateralOneType == string.Empty && casCollateral.collateralTwoType != string.Empty)
            {
                casCollateral.collateralOneType = casCollateral.collateralTwoType;
                casCollateral.collateralTwoType = string.Empty;
                pcmCollateralOne = pcmCollateralTwo;
                pcmCollateralTwo = null;
            }

            if (casCollateral.collateralOneType != string.Empty && casCollateral.collateralTwoType == string.Empty)
            {
                // Set Second collateral as Add New, since we have only one collateral
                casCollateral.collateralTwoType = "Add New";
            }
            else if (casCollateral.collateralOneType == string.Empty && casCollateral.collateralTwoType == string.Empty)
            {
                // Set both collateral to NULL, so Collateral tab will be disabled
                pcmCollateralOne = null;
                pcmCollateralTwo = null;
            }

            // Set Common collateral data
            if (pcmCollateralOne != null)
            {
                pcmCollateralOne.BKG_PCM_COLL_IS_PRIMARY_RES = casCollateral.primaryResidence;
                pcmCollateralOne.ACL_ACL_NEW_CASH_DOWN_AMT = casCollateral.downPayment;
                pcmCollateralOne.ACL_WF_ACL_FEES_FINANCED = casCollateral.feeFinanced;
                pcmCollateralOne.ACL_WF_ACL_PAYOFF_PRICE = casCollateral.payOffPrice;
                pcmCollateralOne.ACL_ACL_SPEED_TYPE = casCollateral.transmissionType;
            }

            if (pcmCollateralTwo != null)
            {
                pcmCollateralTwo.BKG_PCM_COLL_IS_PRIMARY_RES = casCollateral.primaryResidence;
                pcmCollateralTwo.ACL_ACL_NEW_CASH_DOWN_AMT = casCollateral.downPayment;
                pcmCollateralTwo.ACL_WF_ACL_FEES_FINANCED = casCollateral.feeFinanced;
                pcmCollateralTwo.ACL_WF_ACL_PAYOFF_PRICE = casCollateral.payOffPrice;
                pcmCollateralTwo.ACL_ACL_SPEED_TYPE = casCollateral.transmissionType;
            }

            // Set first collateral to CASCollaterl 
            casCollateral.setCollatralOne(pcmCollateralOne);

            // Set second collateral to CASCollaterl 
            casCollateral.setCollateralTwo(pcmCollateralTwo);

            // Set CASCollaterl into the session
            Session[SessionCollateralKey] = casCollateral;

            return casCollateral;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get collateral type.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetCollateralType(casField field)
        {
            // R4.10 PAC Ticket PRB000011616549 -- retrun String.Empty if Collateral is not valid
            if (field.name == "ABC_ABC_COLL_IND")
            {
                switch (field.value)
                {
                    case "A":
                        return "Auto";
                        break;
                    case "R":
                        return "RV";
                        break;
                    case "C":
                        return "Motor";
                        break;
                    case "B":
                        return "Boat";
                        break;
                    case "O":
                        return string.Empty; // "Motor Only"
                        break;
                    case "M":
                        return string.Empty; // "BOAT & MOTOR";
                        break;
                    case "T":
                        return string.Empty; // "Trailer";
                        break;
                    case "P":
                        return "Aircraft"; // Plane
                        break;
                    case "H":
                        return string.Empty; // "Mobile Home"
                        break;
                    case "V":
                        return string.Empty; // "Other Vehicles"
                        break;
                    case "Z":
                        return string.Empty; // "Boat, Motor & Trailer"
                        break;
                    case "E":
                        return string.Empty; // "Boat & Trailer"
                        break;
                    case "F":
                        return string.Empty; // "5th Wheel"
                        break;
                    case "I":
                        return string.Empty; // "Travel Trailer"
                        break;
                    case "J":
                        return string.Empty; // "HORSE TRLR W/ SLEEPING QTR"
                        break;
                    case "K":
                        return string.Empty; // "Pickup Camper"
                        break;
                    case "L":
                        return string.Empty; // "Camper Trailer"
                        break;
                    case "N":
                        return string.Empty; // "Van Conversion"
                        break;
                    case "S":
                        return string.Empty; // "SnowMobile"
                        break;
                    case "G":
                        return string.Empty; // "Golf Cart"
                        break;
                    case "W":
                        return string.Empty; // "Personal Water Craft"
                        break;
                    case "X":
                        return string.Empty; // "ATV"
                        break;
                    case "Y":
                        return string.Empty; // "Yacht"
                        break;

                        // case "L":
                        // return "NON MOTORIZED 5TH WHEEL  "
                        // break;
                    default:
                        return string.Empty;
                        break;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The get field value.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        private void GetFieldValue(casField field, string pageNumber, PCMCollateral pcmCollateral)
        {
            if (field.page_number != null && field.page_number == pageNumber)
            {
                switch (field.name)
                {
                    case "ABC_ABC_COLL_IND":
                        pcmCollateral.ABC_ABC_COLL_IND = field.value;
                        break;
                    case "ABC_ABC_COLL_MAKE":
                        pcmCollateral.ABC_ABC_COLL_MAKE = field.value;
                        break;
                    case "ABC_ABC_COLL_MODEL":
                        pcmCollateral.ABC_ABC_COLL_MODEL = field.value;
                        break;
                    case "ABC_ABC_COLL_MODEL_YR":
                        pcmCollateral.ABC_ABC_COLL_MODEL_YR = field.value;
                        break;
                    case "ABC_ABC_COLL_NEW_USED":
                        pcmCollateral.ABC_ABC_COLL_NEW_USED = field.value;
                        break;
                    case "ABC_COLL_BOAT_GT_5TONS":
                        pcmCollateral.ABC_COLL_BOAT_GT_5TONS = field.value;
                        break;
                    case "ABC_COLL_HULL_MAT":
                        pcmCollateral.ABC_COLL_HULL_MAT = field.value;
                        break;
                    case "ABC_COLL_LENGTH":
                        pcmCollateral.ABC_COLL_LENGTH = CASUtils.formatData(field.value);
                        break;
                    case "ABC_COLL_MILEAGE":
                        pcmCollateral.ABC_COLL_MILEAGE = CASUtils.formatData(field.value);
                        break;
                    case "ABC_COLL_PLANE_HSP":
                        pcmCollateral.ABC_COLL_PLANE_HSP = CASUtils.formatData(field.value);
                        break;
                    case "ABC_COLL_RV_TYPE":
                        pcmCollateral.ABC_COLL_RV_TYPE = field.value;
                        break;
                    case "ABC_COLL_SALES_PRICE":
                        pcmCollateral.ABC_COLL_SALES_PRICE = CASUtils.formatData(field.value);
                        break;

                        // case "ABC_COLL_SELLER_RELATION":
                        // pcmCollateral.ABC_COLL_SELLER_RELATION = field.value;
                        // break;
                    case "ABC_COLL_WEIGHT":
                        pcmCollateral.ABC_COLL_WEIGHT = field.value;
                        break;
                    case "ABC_INBRD_MODEL_1":
                        pcmCollateral.ABC_INBRD_MODEL_1 = field.value;
                        break;
                    case "ABC_INBRD_MOTOR_SN_1":
                        pcmCollateral.ABC_INBRD_MOTOR_SN_1 = field.value;
                        break;
                    case "ABC_INBRD_MOTOR_YEAR_1":
                        pcmCollateral.ABC_INBRD_MOTOR_YEAR_1 = field.value;
                        break;
                    case "ABC_OB_MOTOR_MODEL":
                        pcmCollateral.ABC_OB_MOTOR_MODEL = field.value;
                        break;
                    case "ABC_OB_MOTOR_NUMBER":
                        pcmCollateral.ABC_OB_MOTOR_NUMBER = field.value;
                        break;
                    case "ABC_OB_MOTOR_YEAR":
                        pcmCollateral.ABC_OB_MOTOR_YEAR = field.value;
                        break;
                    case "ABC_TOT_HSP_AMOUNT":
                        pcmCollateral.ABC_TOT_HSP_AMOUNT = CASUtils.formatData(field.value);
                        break;
                    case "ABC_TRAILER_MODEL_YR":
                        pcmCollateral.ABC_TRAILER_MODEL_YR = field.value;
                        break;
                    case "ABC_TRAILER_SERIAL_NUM":
                        pcmCollateral.ABC_TRAILER_SERIAL_NUM = field.value;
                        break;

                        // case "ABC_VEH_LIC_NUMBER":
                        // pcmCollateral.ABC_VEH_LIC_NUMBER = field.value;
                        // break;
                    case "ACL_ACL_NEW_CASH_DOWN_AMT":
                        pcmCollateral.ACL_ACL_NEW_CASH_DOWN_AMT = CASUtils.formatData(field.value);
                        break;
                    case "ACL_WF_ACL_FEES_FINANCED":
                        pcmCollateral.ACL_WF_ACL_FEES_FINANCED = CASUtils.formatData(field.value);
                        break;
                    case "ACL_WF_ACL_PAYOFF_PRICE":
                        pcmCollateral.ACL_WF_ACL_PAYOFF_PRICE = CASUtils.formatData(field.value);
                        break;
                    case "ABC_COLL_DEALER_ADDR":
                        pcmCollateral.ABC_COLL_DEALER_ADDR = field.value;
                        break;
                    case "ABC_COLL_DEALER_CITY":
                        pcmCollateral.ABC_COLL_DEALER_CITY = field.value;
                        break;
                    case "ABC_COLL_DEALER_STATE":
                        pcmCollateral.ABC_COLL_DEALER_STATE = field.value;
                        break;
                    case "ABC_COLL_DEALER_ZIP_CODE":
                        pcmCollateral.ABC_COLL_DEALER_ZIP_CODE = field.value;
                        break;
                    case "ABC_COLL_DLR_NAME":
                        pcmCollateral.ABC_COLL_DLR_NAME = field.value;
                        break;
                    case "ABC_COLL_SELLER_TYPE":
                        pcmCollateral.ABC_COLL_SELLER_TYPE = field.value;
                        break;
                    case "ABC_ABC_COLL_SERIAL_NUM":
                        pcmCollateral.ABC_ABC_COLL_SERIAL_NUM = field.value;
                        break;
                    case "BKG_PCM_COLL_IS_PRIMARY_RES":
                        pcmCollateral.BKG_PCM_COLL_IS_PRIMARY_RES = field.value;
                        break;
                    case "ABC_INBRD_NEW_USED_1":
                        pcmCollateral.ABC_INBRD_NEW_USED_1 = field.value;
                        break;
                    case "ABC_INBRD_MAKE_1":
                        pcmCollateral.ABC_INBRD_MAKE_1 = field.value;
                        break;
                    case "ABC_INBRD_NEW_USED_2":
                        pcmCollateral.ABC_INBRD_NEW_USED_2 = field.value;
                        break;
                    case "ABC_INBRD_MAKE_2":
                        pcmCollateral.ABC_INBRD_MAKE_2 = field.value;
                        break;
                    case "ABC_TRAILER_MAKE":
                        pcmCollateral.ABC_TRAILER_MAKE = field.value;
                        break;
                    case "ABC_TRAILER_AXLES":
                        pcmCollateral.ABC_TRAILER_AXLES = field.value;
                        break;
                    case "ABC_OB_MOTOR_MAKE":
                        pcmCollateral.ABC_OB_MOTOR_MAKE = field.value;
                        break;
                    case "ABC_COLL_SELLER_NAME":
                        pcmCollateral.ABC_COLL_SELLER_NAME = field.value;
                        break;
                    case "ABC_COLL_SELLER_ADDR":
                        pcmCollateral.ABC_COLL_SELLER_ADDR = field.value;
                        break;
                    case "ABC_COLL_SELLER_CITY":
                        pcmCollateral.ABC_COLL_SELLER_CITY = field.value;
                        break;
                    case "ABC_COLL_SELLER_STATE":
                        pcmCollateral.ABC_COLL_SELLER_STATE = field.value;
                        break;
                    case "ABC_COLL_SELLER_ZIP_CODE":
                        pcmCollateral.ABC_COLL_SELLER_ZIP_CODE = field.value;
                        break;
                    case "ABC_COLL_SELLER_NAME2":
                        pcmCollateral.ABC_COLL_SELLER_NAME2 = field.value;
                        break;
                    case "ABC_COLL_LIEN_HNAME":
                        pcmCollateral.ABC_COLL_LIEN_HNAME = field.value;
                        break;
                    case "ABC_COLL_LIEN_ADDR":
                        pcmCollateral.ABC_COLL_LIEN_ADDR = field.value;
                        break;
                    case "ABC_COLL_LIEN_CITY":
                        pcmCollateral.ABC_COLL_LIEN_CITY = field.value;
                        break;
                    case "ABC_COLL_LIEN_STATE":
                        pcmCollateral.ABC_COLL_LIEN_STATE = field.value;
                        break;
                    case "ABC_COLL_LIEN_ZIP_CODE":
                        pcmCollateral.ABC_COLL_LIEN_ZIP_CODE = field.value;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// The get saving cd value.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <param name="arrCDCItem">
        /// The arr cdc item.
        /// </param>
        private void GetSavingCDValue(casField field, CDCItem[] arrCDCItem)
        {
            int idx = 0;

            if (field.page_number.Contains("001") && field.name.Contains("_1"))
            {
                idx = 0;
            }
            else if (field.page_number.Contains("001") && field.name.Contains("_2"))
            {
                idx = 1;
            }
            else if (field.page_number.Contains("001") && field.name.Contains("_3"))
            {
                idx = 2;
            }

            if (field.page_number.Contains("002") && field.name.Contains("_1"))
            {
                idx = 3;
            }
            else if (field.page_number.Contains("002") && field.name.Contains("_2"))
            {
                idx = 4;
            }
            else if (field.page_number.Contains("002") && field.name.Contains("_3"))
            {
                idx = 5;
            }

            if (field.page_number.Contains("003") && field.name.Contains("_1"))
            {
                idx = 6;
            }
            else if (field.page_number.Contains("003") && field.name.Contains("_2"))
            {
                idx = 7;
            }
            else if (field.page_number.Contains("003") && field.name.Contains("_3"))
            {
                idx = 8;
            }

            switch (field.name)
            {
                case "CDC_CD_MAT_DATE_1":
                    arrCDCItem[idx].CDC_CD_MAT_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "CDC_CD_MAT_DATE_2":
                    arrCDCItem[idx].CDC_CD_MAT_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "CDC_CD_MAT_DATE_3":
                    arrCDCItem[idx].CDC_CD_MAT_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "CDC_CD_SAV_IND_1":
                    arrCDCItem[idx].CDC_CD_SAV_IND = field.value;
                    break;
                case "CDC_CD_SAV_IND_2":
                    arrCDCItem[idx].CDC_CD_SAV_IND = field.value;
                    break;
                case "CDC_CD_SAV_IND_3":
                    arrCDCItem[idx].CDC_CD_SAV_IND = field.value;
                    break;
                case "CDC_COLL_ACCT_NUM_1":
                    arrCDCItem[idx].CDC_COLL_ACCT_NUM = field.value;
                    break;
                case "CDC_COLL_ACCT_NUM_2":
                    arrCDCItem[idx].CDC_COLL_ACCT_NUM = field.value;
                    break;
                case "CDC_COLL_ACCT_NUM_3":
                    arrCDCItem[idx].CDC_COLL_ACCT_NUM = field.value;
                    break;
                case "CDC_SAV_CD_AMT_1":
                    arrCDCItem[idx].CDC_SAV_CD_AMT = CASUtils.formatData(field.value);
                    break;
                case "CDC_SAV_CD_AMT_2":
                    arrCDCItem[idx].CDC_SAV_CD_AMT = CASUtils.formatData(field.value);
                    break;
                case "CDC_SAV_CD_AMT_3":
                    arrCDCItem[idx].CDC_SAV_CD_AMT = CASUtils.formatData(field.value);
                    break;
                case "CDC_SAV_CD_NAME_1":
                    arrCDCItem[idx].CDC_SAV_CD_NAME = field.value;
                    break;
                case "CDC_SAV_CD_NAME_2":
                    arrCDCItem[idx].CDC_SAV_CD_NAME = field.value;
                    break;
                case "CDC_SAV_CD_NAME_3":
                    arrCDCItem[idx].CDC_SAV_CD_NAME = field.value;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The get stock bond value.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <param name="arrSBCItem">
        /// The arr sbc item.
        /// </param>
        private void GetStockBondValue(casField field, SBCItem[] arrSBCItem)
        {
            int idx = 0;

            if (field.page_number.Contains("001")
                && (field.name.Contains("_1") && !(field.name.Contains("_2") || field.name.Contains("_3"))))
            {
                idx = 0;
            }
            else if (field.page_number.Contains("001") && field.name.Contains("_2"))
            {
                idx = 1;
            }
            else if (field.page_number.Contains("001") && field.name.Contains("_3"))
            {
                idx = 2;
            }

            if (field.page_number.Contains("002")
                && (field.name.Contains("_1") && !(field.name.Contains("_2") || field.name.Contains("_3"))))
            {
                idx = 3;
            }
            else if (field.page_number.Contains("002") && field.name.Contains("_2"))
            {
                idx = 4;
            }
            else if (field.page_number.Contains("002") && field.name.Contains("_3"))
            {
                idx = 5;
            }

            if (field.page_number.Contains("003")
                && (field.name.Contains("_1") && !(field.name.Contains("_2") || field.name.Contains("_3"))))
            {
                idx = 6;
            }
            else if (field.page_number.Contains("003") && field.name.Contains("_2"))
            {
                idx = 7;
            }
            else if (field.page_number.Contains("003") && field.name.Contains("_3"))
            {
                idx = 8;
            }

            switch (field.name)
            {
                case "SBC_BOND_AMT_1":
                    arrSBCItem[idx].SBC_BOND_AMT = CASUtils.formatData(field.value);
                    break;
                case "SBC_BOND_AMT_2":
                    arrSBCItem[idx].SBC_BOND_AMT = CASUtils.formatData(field.value);
                    break;
                case "SBC_BOND_AMT_3":
                    arrSBCItem[idx].SBC_BOND_AMT = CASUtils.formatData(field.value);
                    break;
                case "SBC_CUSIP_NUM_1":
                    arrSBCItem[idx].SBC_CUSIP_NUM = field.value;
                    break;
                case "SBC_CUSIP_NUM_2":
                    arrSBCItem[idx].SBC_CUSIP_NUM = field.value;
                    break;
                case "SBC_CUSIP_NUM_3":
                    arrSBCItem[idx].SBC_CUSIP_NUM = field.value;
                    break;
                case "SBC_MATURING_DATE_1":
                    arrSBCItem[idx].SBC_MATURING_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "SBC_MATURING_DATE_2":
                    arrSBCItem[idx].SBC_MATURING_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "SBC_MATURING_DATE_3":
                    arrSBCItem[idx].SBC_MATURING_DATE = CASUtils.FormatDate(field.value, "MM/dd/yy", "yyyyMMdd");
                    break;
                case "SBC_SECUR_ISSUER_DESC_1_1":
                    arrSBCItem[idx].SBC_SECUR_ISSUER_DESC = field.value;
                    break;
                case "SBC_SECUR_ISSUER_DESC_1_2":
                    arrSBCItem[idx].SBC_SECUR_ISSUER_DESC = field.value;
                    break;
                case "SBC_SECUR_ISSUER_DESC_1_3":
                    arrSBCItem[idx].SBC_SECUR_ISSUER_DESC = field.value;
                    break;
                case "SBC_STCK_BOND_NUM_SHRS_1":
                    arrSBCItem[idx].SBC_STCK_BOND_NUM_SHRS = CASUtils.formatData(field.value);
                    break;
                case "SBC_STCK_BOND_NUM_SHRS_2":
                    arrSBCItem[idx].SBC_STCK_BOND_NUM_SHRS = CASUtils.formatData(field.value);
                    break;
                case "SBC_STCK_BOND_NUM_SHRS_3":
                    arrSBCItem[idx].SBC_STCK_BOND_NUM_SHRS = CASUtils.formatData(field.value);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The populate savings cd.
        /// </summary>
        /// <param name="casCollateral">
        /// The cas collateral.
        /// </param>
        /// <param name="pcmCollateralOne">
        /// The pcm collateral one.
        /// </param>
        /// <param name="arrCDCItem">
        /// The arr cdc item.
        /// </param>
        private void PopulateSavingsCD(
            CASCollateral casCollateral, PCMCollateral pcmCollateralOne, CDCItem[] arrCDCItem)
        {
            // Populate First CD collateral
            ArrayList arrCDCCollateral = new ArrayList();
            foreach (CDCItem cdc in arrCDCItem)
            {
                if (arrCDCCollateral.Count <= 4 && !string.IsNullOrEmpty(cdc.CDC_COLL_ACCT_NUM))
                {
                    arrCDCCollateral.Add(cdc);
                }
            }

            CDCItem cdcItem;

            if (arrCDCCollateral.Count > 0)
            {
                casCollateral.collateralOneType = "SavingsCD";
                cdcItem = (CDCItem)arrCDCCollateral[0];
                pcmCollateralOne.CDC_COLL_ACCT_NUM_1 = cdcItem.CDC_COLL_ACCT_NUM;
                pcmCollateralOne.CDC_CD_MAT_DATE_1 = cdcItem.CDC_CD_MAT_DATE;
                pcmCollateralOne.CDC_CD_SAV_IND_1 = cdcItem.CDC_CD_SAV_IND;
                pcmCollateralOne.CDC_SAV_CD_AMT_1 = cdcItem.CDC_SAV_CD_AMT;
                pcmCollateralOne.CDC_SAV_CD_NAME_1 = cdcItem.CDC_SAV_CD_NAME;
            }

            if (arrCDCCollateral.Count > 1)
            {
                cdcItem = (CDCItem)arrCDCCollateral[1];
                pcmCollateralOne.CDC_COLL_ACCT_NUM_2 = cdcItem.CDC_COLL_ACCT_NUM;
                pcmCollateralOne.CDC_CD_MAT_DATE_2 = cdcItem.CDC_CD_MAT_DATE;
                pcmCollateralOne.CDC_CD_SAV_IND_2 = cdcItem.CDC_CD_SAV_IND;
                pcmCollateralOne.CDC_SAV_CD_AMT_2 = cdcItem.CDC_SAV_CD_AMT;
                pcmCollateralOne.CDC_SAV_CD_NAME_2 = cdcItem.CDC_SAV_CD_NAME;
            }

            if (arrCDCCollateral.Count > 2)
            {
                cdcItem = (CDCItem)arrCDCCollateral[2];
                pcmCollateralOne.CDC_COLL_ACCT_NUM_3 = cdcItem.CDC_COLL_ACCT_NUM;
                pcmCollateralOne.CDC_CD_MAT_DATE_3 = cdcItem.CDC_CD_MAT_DATE;
                pcmCollateralOne.CDC_CD_SAV_IND_3 = cdcItem.CDC_CD_SAV_IND;
                pcmCollateralOne.CDC_SAV_CD_AMT_3 = cdcItem.CDC_SAV_CD_AMT;
                pcmCollateralOne.CDC_SAV_CD_NAME_3 = cdcItem.CDC_SAV_CD_NAME;
            }

            if (arrCDCCollateral.Count > 3)
            {
                cdcItem = (CDCItem)arrCDCCollateral[3];
                pcmCollateralOne.CDC_COLL_ACCT_NUM_4 = cdcItem.CDC_COLL_ACCT_NUM;
                pcmCollateralOne.CDC_CD_MAT_DATE_4 = cdcItem.CDC_CD_MAT_DATE;
                pcmCollateralOne.CDC_CD_SAV_IND_4 = cdcItem.CDC_CD_SAV_IND;
                pcmCollateralOne.CDC_SAV_CD_AMT_4 = cdcItem.CDC_SAV_CD_AMT;
                pcmCollateralOne.CDC_SAV_CD_NAME_4 = cdcItem.CDC_SAV_CD_NAME;
            }

            arrCDCCollateral = null; //// Set Object to null to release the memory
        }

        /// <summary>
        /// The populate stock bonds.
        /// </summary>
        /// <param name="casCollateral">
        /// The cas collateral.
        /// </param>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        /// <param name="arrSBCItem">
        /// The arr sbc item.
        /// </param>
        private void PopulateStockBonds(CASCollateral casCollateral, PCMCollateral pcmCollateral, SBCItem[] arrSBCItem)
        {
            // Populate First CD collateral
            ArrayList arrSBCCollateral = new ArrayList();
            foreach (SBCItem sbc in arrSBCItem)
            {
                if (arrSBCCollateral.Count <= 4 && !string.IsNullOrEmpty(sbc.SBC_CUSIP_NUM))
                {
                    arrSBCCollateral.Add(sbc);
                }
            }

            SBCItem SBCItem;

            if (arrSBCCollateral.Count > 0)
            {
                casCollateral.collateralTwoType = "StockBonds";
                SBCItem = (SBCItem)arrSBCCollateral[0];
                pcmCollateral.SBC_BOND_AMT_1 = SBCItem.SBC_BOND_AMT;
                pcmCollateral.SBC_MATURING_DATE_1 = SBCItem.SBC_MATURING_DATE;
                pcmCollateral.SBC_CUSIP_NUM_1 = SBCItem.SBC_CUSIP_NUM;
                pcmCollateral.SBC_SECUR_ISSUER_DESC_1_1 = SBCItem.SBC_SECUR_ISSUER_DESC;
                pcmCollateral.SBC_STCK_BOND_NUM_SHRS_1 = SBCItem.SBC_STCK_BOND_NUM_SHRS;
            }

            if (arrSBCCollateral.Count > 1)
            {
                SBCItem = (SBCItem)arrSBCCollateral[1];
                pcmCollateral.SBC_BOND_AMT_2 = SBCItem.SBC_BOND_AMT;
                pcmCollateral.SBC_MATURING_DATE_2 = SBCItem.SBC_MATURING_DATE;
                pcmCollateral.SBC_CUSIP_NUM_2 = SBCItem.SBC_CUSIP_NUM;
                pcmCollateral.SBC_SECUR_ISSUER_DESC_1_2 = SBCItem.SBC_SECUR_ISSUER_DESC;
                pcmCollateral.SBC_STCK_BOND_NUM_SHRS_2 = SBCItem.SBC_STCK_BOND_NUM_SHRS;
            }

            if (arrSBCCollateral.Count > 2)
            {
                SBCItem = (SBCItem)arrSBCCollateral[2];
                pcmCollateral.SBC_BOND_AMT_3 = SBCItem.SBC_BOND_AMT;
                pcmCollateral.SBC_MATURING_DATE_3 = SBCItem.SBC_MATURING_DATE;
                pcmCollateral.SBC_CUSIP_NUM_3 = SBCItem.SBC_CUSIP_NUM;
                pcmCollateral.SBC_SECUR_ISSUER_DESC_1_3 = SBCItem.SBC_SECUR_ISSUER_DESC;
                pcmCollateral.SBC_STCK_BOND_NUM_SHRS_3 = SBCItem.SBC_STCK_BOND_NUM_SHRS;
            }

            if (arrSBCCollateral.Count > 3)
            {
                SBCItem = (SBCItem)arrSBCCollateral[3];
                pcmCollateral.SBC_BOND_AMT_4 = SBCItem.SBC_BOND_AMT;
                pcmCollateral.SBC_MATURING_DATE_4 = SBCItem.SBC_MATURING_DATE;
                pcmCollateral.SBC_CUSIP_NUM_4 = SBCItem.SBC_CUSIP_NUM;
                pcmCollateral.SBC_SECUR_ISSUER_DESC_1_4 = SBCItem.SBC_SECUR_ISSUER_DESC;
                pcmCollateral.SBC_STCK_BOND_NUM_SHRS_4 = SBCItem.SBC_STCK_BOND_NUM_SHRS;
            }

            arrSBCCollateral = null; //// Set Object to null to release the memory
        }

        /// <summary>
        /// The set collateral fields.
        /// </summary>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <returns>
        /// The <see cref="casField[]"/>.
        /// </returns>
        private casField[] SetCollateralFields(PCMCollateral pcmCollateral, string pageNumber)
        {
            

            ArrayList fieldList = new ArrayList();

            fieldList.Add(new casField("ABC_ABC_COLL_IND", pageNumber, pcmCollateral.ABC_ABC_COLL_IND));
            fieldList.Add(new casField("ABC_ABC_COLL_MAKE", pageNumber, pcmCollateral.ABC_ABC_COLL_MAKE));
            fieldList.Add(
                new casField("ABC_COLL_MILEAGE", pageNumber, CASUtils.formatData(pcmCollateral.ABC_COLL_MILEAGE)));
            fieldList.Add(new casField("ABC_ABC_COLL_MODEL", pageNumber, pcmCollateral.ABC_ABC_COLL_MODEL));
            fieldList.Add(new casField("ABC_ABC_COLL_MODEL_YR", pageNumber, pcmCollateral.ABC_ABC_COLL_MODEL_YR));
            fieldList.Add(new casField("ABC_ABC_COLL_NEW_USED", pageNumber, pcmCollateral.ABC_ABC_COLL_NEW_USED));
            fieldList.Add(
                new casField(
                    "ABC_COLL_SALES_PRICE", pageNumber, CASUtils.formatData(pcmCollateral.ABC_COLL_SALES_PRICE)));
            fieldList.Add(new casField("ABC_COLL_SELLER_TYPE", pageNumber, pcmCollateral.ABC_COLL_SELLER_TYPE));
            fieldList.Add(new casField("ABC_ABC_COLL_SERIAL_NUM", pageNumber, pcmCollateral.ABC_ABC_COLL_SERIAL_NUM));

            #region Common collateral with always page 001

            fieldList.Add(
                new casField(
                    "ACL_WF_ACL_PAYOFF_PRICE", "001", CASUtils.formatData(pcmCollateral.ACL_WF_ACL_PAYOFF_PRICE)));
            fieldList.Add(
                new casField(
                    "ACL_ACL_NEW_CASH_DOWN_AMT", "001", CASUtils.formatData(pcmCollateral.ACL_ACL_NEW_CASH_DOWN_AMT)));
            fieldList.Add(new casField("ACL_WF_ACL_FEES_FINANCED", "001", pcmCollateral.ACL_WF_ACL_FEES_FINANCED));
            fieldList.Add(new casField("BKG_PCM_COLL_IS_PRIMARY_RES", "001", pcmCollateral.BKG_PCM_COLL_IS_PRIMARY_RES));
            fieldList.Add(new casField("ACL_ACL_SPEED_TYPE", "001", pcmCollateral.ACL_ACL_SPEED_TYPE));

            #endregion

            fieldList.Add(
                new casField("ABC_COLL_LENGTH", pageNumber, CASUtils.formatData(pcmCollateral.ABC_COLL_LENGTH)));
            fieldList.Add(new casField("ABC_COLL_RV_TYPE", pageNumber, pcmCollateral.ABC_COLL_RV_TYPE));

            fieldList.Add(new casField("ABC_COLL_BOAT_GT_5TONS", pageNumber, pcmCollateral.ABC_COLL_BOAT_GT_5TONS));
            fieldList.Add(
                new casField("ABC_TOT_HSP_AMOUNT", pageNumber, CASUtils.formatData(pcmCollateral.ABC_TOT_HSP_AMOUNT)));
            fieldList.Add(
                new casField("ABC_COLL_LENGTH", pageNumber, CASUtils.formatData(pcmCollateral.ABC_COLL_LENGTH)));
            fieldList.Add(new casField("ABC_COLL_HULL_MAT", pageNumber, pcmCollateral.ABC_COLL_HULL_MAT));

            fieldList.Add(new casField("ABC_INBRD_MOTOR_SN_1", pageNumber, pcmCollateral.ABC_INBRD_MOTOR_SN_1));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_SN_2", pageNumber, pcmCollateral.ABC_INBRD_MOTOR_SN_2));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_YEAR_1", pageNumber, pcmCollateral.ABC_INBRD_MOTOR_YEAR_1));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_YEAR_2", pageNumber, pcmCollateral.ABC_INBRD_MOTOR_YEAR_2));
            fieldList.Add(new casField("ABC_INBRD_MAKE_1", pageNumber, pcmCollateral.ABC_INBRD_MAKE_1));
            fieldList.Add(new casField("ABC_INBRD_MAKE_2", pageNumber, pcmCollateral.ABC_INBRD_MAKE_2));
            fieldList.Add(new casField("ABC_INBRD_MODEL_1", pageNumber, pcmCollateral.ABC_INBRD_MODEL_1));
            fieldList.Add(new casField("ABC_INBRD_MODEL_2", pageNumber, pcmCollateral.ABC_INBRD_MODEL_2));
            fieldList.Add(new casField("ABC_INBRD_NEW_USED_1", pageNumber, pcmCollateral.ABC_INBRD_NEW_USED_1));
            fieldList.Add(new casField("ABC_INBRD_NEW_USED_2", pageNumber, pcmCollateral.ABC_INBRD_NEW_USED_2));

            fieldList.Add(new casField("ABC_OB_MOTOR_YEAR", pageNumber, pcmCollateral.ABC_OB_MOTOR_YEAR));
            fieldList.Add(new casField("ABC_OB_MOTOR_NUMBER", pageNumber, pcmCollateral.ABC_OB_MOTOR_NUMBER));
            fieldList.Add(new casField("ABC_OB_MOTOR_MAKE", pageNumber, pcmCollateral.ABC_OB_MOTOR_MAKE));
            fieldList.Add(new casField("ABC_OB_MOTOR_MODEL", pageNumber, pcmCollateral.ABC_OB_MOTOR_MODEL));

            fieldList.Add(new casField("ABC_TRAILER_MAKE", pageNumber, pcmCollateral.ABC_TRAILER_MAKE));
            fieldList.Add(new casField("ABC_TRAILER_MODEL", pageNumber, pcmCollateral.ABC_TRAILER_MODEL));
            fieldList.Add(new casField("ABC_TRAILER_AXLES", pageNumber, pcmCollateral.ABC_TRAILER_AXLES));
            fieldList.Add(new casField("ABC_TRAILER_SERIAL_NUM", pageNumber, pcmCollateral.ABC_TRAILER_SERIAL_NUM));
            fieldList.Add(new casField("ABC_TRAILER_LIC_NUM", pageNumber, pcmCollateral.ABC_TRAILER_LIC_NUM));
            fieldList.Add(new casField("ABC_TRAILER_MODEL_YR", pageNumber, pcmCollateral.ABC_TRAILER_MODEL_YR));

            fieldList.Add(
                new casField("ABC_COLL_PLANE_HSP", pageNumber, CASUtils.formatData(pcmCollateral.ABC_COLL_PLANE_HSP)));

            fieldList.Add(new casField("ABC_COLL_DLR_NAME", pageNumber, pcmCollateral.ABC_COLL_DLR_NAME));
            fieldList.Add(new casField("ABC_COLL_DEALER_ADDR", pageNumber, pcmCollateral.ABC_COLL_DEALER_ADDR));
            fieldList.Add(new casField("ABC_COLL_DEALER_CITY", pageNumber, pcmCollateral.ABC_COLL_DEALER_CITY));
            fieldList.Add(new casField("ABC_COLL_DEALER_STATE", pageNumber, pcmCollateral.ABC_COLL_DEALER_STATE));
            fieldList.Add(new casField("ABC_COLL_DEALER_ZIP_CODE", pageNumber, pcmCollateral.ABC_COLL_DEALER_ZIP_CODE));

            fieldList.Add(new casField("ABC_COLL_SELLER_NAME", pageNumber, pcmCollateral.ABC_COLL_SELLER_NAME));
            fieldList.Add(new casField("ABC_COLL_SELLER_ADDR", pageNumber, pcmCollateral.ABC_COLL_SELLER_ADDR));
            fieldList.Add(new casField("ABC_COLL_SELLER_CITY", pageNumber, pcmCollateral.ABC_COLL_SELLER_CITY));
            fieldList.Add(new casField("ABC_COLL_SELLER_STATE", pageNumber, pcmCollateral.ABC_COLL_SELLER_STATE));
            fieldList.Add(new casField("ABC_COLL_SELLER_ZIP_CODE", pageNumber, pcmCollateral.ABC_COLL_SELLER_ZIP_CODE));
            fieldList.Add(new casField("ABC_COLL_SELLER_NAME2", pageNumber, pcmCollateral.ABC_COLL_SELLER_NAME2));

            fieldList.Add(new casField("ABC_COLL_LIEN_HNAME", pageNumber, pcmCollateral.ABC_COLL_LIEN_HNAME));
            fieldList.Add(new casField("ABC_COLL_LIEN_ADDR", pageNumber, pcmCollateral.ABC_COLL_LIEN_ADDR));
            fieldList.Add(new casField("ABC_COLL_LIEN_CITY", pageNumber, pcmCollateral.ABC_COLL_LIEN_CITY));
            fieldList.Add(new casField("ABC_COLL_LIEN_STATE", pageNumber, pcmCollateral.ABC_COLL_LIEN_STATE));
            fieldList.Add(new casField("ABC_COLL_LIEN_ZIP_CODE", pageNumber, pcmCollateral.ABC_COLL_LIEN_ZIP_CODE));

            

            casField[] casFields;
            casFields = (casField[])fieldList.ToArray(typeof(casField));

            return casFields;
        }

        /// <summary>
        /// The set savings cd fields.
        /// </summary>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        /// <returns>
        /// The <see cref="casField[]"/>.
        /// </returns>
        private casField[] SetSavingsCDFields(PCMCollateral pcmCollateral)
        {
            

            ArrayList fieldList = new ArrayList();

            fieldList.Add(new casField("CDC_CD_SAV_IND_1", "001", pcmCollateral.CDC_CD_SAV_IND_1));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_1", "001", pcmCollateral.CDC_COLL_ACCT_NUM_1));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_1", "001", CASUtils.formatData(pcmCollateral.CDC_SAV_CD_AMT_1)));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_1", "001", pcmCollateral.CDC_SAV_CD_NAME_1));
            fieldList.Add(
                new casField(
                    "CDC_CD_MAT_DATE_1", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.CDC_CD_MAT_DATE_1, "yyyyMMdd", "MM/dd/yy")));

            fieldList.Add(new casField("CDC_CD_SAV_IND_2", "001", pcmCollateral.CDC_CD_SAV_IND_2));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_2", "001", pcmCollateral.CDC_COLL_ACCT_NUM_2));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_2", "001", CASUtils.formatData(pcmCollateral.CDC_SAV_CD_AMT_2)));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_2", "001", pcmCollateral.CDC_SAV_CD_NAME_2));
            fieldList.Add(
                new casField(
                    "CDC_CD_MAT_DATE_2", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.CDC_CD_MAT_DATE_2, "yyyyMMdd", "MM/dd/yy")));

            fieldList.Add(new casField("CDC_CD_SAV_IND_3", "001", pcmCollateral.CDC_CD_SAV_IND_3));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_3", "001", pcmCollateral.CDC_COLL_ACCT_NUM_3));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_3", "001", CASUtils.formatData(pcmCollateral.CDC_SAV_CD_AMT_3)));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_3", "001", pcmCollateral.CDC_SAV_CD_NAME_3));
            fieldList.Add(
                new casField(
                    "CDC_CD_MAT_DATE_3", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.CDC_CD_MAT_DATE_3, "yyyyMMdd", "MM/dd/yy")));

            fieldList.Add(new casField("CDC_CD_SAV_IND_1", "002", pcmCollateral.CDC_CD_SAV_IND_4));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_1", "002", pcmCollateral.CDC_COLL_ACCT_NUM_4));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_1", "002", CASUtils.formatData(pcmCollateral.CDC_SAV_CD_AMT_4)));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_1", "002", pcmCollateral.CDC_SAV_CD_NAME_4));
            fieldList.Add(
                new casField(
                    "CDC_CD_MAT_DATE_1", 
                    "002", 
                    CASUtils.FormatDate(pcmCollateral.CDC_CD_MAT_DATE_4, "yyyyMMdd", "MM/dd/yy")));

            

            casField[] casFields;
            casFields = (casField[])fieldList.ToArray(typeof(casField));

            return casFields;
        }

        /// <summary>
        /// The set stock bonds fields.
        /// </summary>
        /// <param name="pcmCollateral">
        /// The pcm collateral.
        /// </param>
        /// <returns>
        /// The <see cref="casField[]"/>.
        /// </returns>
        private casField[] SetStockBondsFields(PCMCollateral pcmCollateral)
        {
            

            ArrayList fieldList = new ArrayList();

            fieldList.Add(new casField("SBC_CUSIP_NUM_1", "001", pcmCollateral.SBC_CUSIP_NUM_1));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_1", "001", pcmCollateral.SBC_STCK_BOND_NUM_SHRS_1));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_1", "001", pcmCollateral.SBC_SECUR_ISSUER_DESC_1_1));
            fieldList.Add(new casField("SBC_BOND_AMT_1", "001", CASUtils.formatData(pcmCollateral.SBC_BOND_AMT_1)));
            fieldList.Add(
                new casField(
                    "SBC_MATURING_DATE_1", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.SBC_MATURING_DATE_1, "yyyyMMdd", "MM/dd/yyyy")));

            fieldList.Add(new casField("SBC_CUSIP_NUM_2", "001", pcmCollateral.SBC_CUSIP_NUM_2));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_2", "001", pcmCollateral.SBC_STCK_BOND_NUM_SHRS_2));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_2", "001", pcmCollateral.SBC_SECUR_ISSUER_DESC_1_2));
            fieldList.Add(new casField("SBC_BOND_AMT_2", "001", CASUtils.formatData(pcmCollateral.SBC_BOND_AMT_2)));
            fieldList.Add(
                new casField(
                    "SBC_MATURING_DATE_2", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.SBC_MATURING_DATE_2, "yyyyMMdd", "MM/dd/yyyy")));

            fieldList.Add(new casField("SBC_CUSIP_NUM_3", "001", pcmCollateral.SBC_CUSIP_NUM_3));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_3", "001", pcmCollateral.SBC_STCK_BOND_NUM_SHRS_3));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_3", "001", pcmCollateral.SBC_SECUR_ISSUER_DESC_1_3));
            fieldList.Add(new casField("SBC_BOND_AMT_3", "001", CASUtils.formatData(pcmCollateral.SBC_BOND_AMT_3)));
            fieldList.Add(
                new casField(
                    "SBC_MATURING_DATE_3", 
                    "001", 
                    CASUtils.FormatDate(pcmCollateral.SBC_MATURING_DATE_3, "yyyyMMdd", "MM/dd/yyyy")));

            fieldList.Add(new casField("SBC_CUSIP_NUM_1", "002", pcmCollateral.SBC_CUSIP_NUM_4));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_1", "002", pcmCollateral.SBC_STCK_BOND_NUM_SHRS_4));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_1", "002", pcmCollateral.SBC_SECUR_ISSUER_DESC_1_4));
            fieldList.Add(new casField("SBC_BOND_AMT_1", "002", CASUtils.formatData(pcmCollateral.SBC_BOND_AMT_4)));
            fieldList.Add(
                new casField(
                    "SBC_MATURING_DATE_1", 
                    "002", 
                    CASUtils.FormatDate(pcmCollateral.SBC_MATURING_DATE_4, "yyyyMMdd", "MM/dd/yyyy")));

            

            casField[] casFields;
            casFields = (casField[])fieldList.ToArray(typeof(casField));

            return casFields;
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

            

            ArrayList fieldList = new ArrayList();
            fieldList.Add(new casField("ABC_ABC_COLL_CODE", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_MODEL_YR", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_RELATION", "001"));
            fieldList.Add(new casField("ABC_COLL_BOAT_GT_5TONS", "001"));
            fieldList.Add(new casField("ABC_COLL_WEIGHT", "001"));
            fieldList.Add(new casField("ABC_COLL_LENGTH", "001"));
            fieldList.Add(new casField("ABC_TONS_AMT", "001"));
            fieldList.Add(new casField("ABC_COLL_MILEAGE", "001"));
            fieldList.Add(new casField("ABC_COLL_HULL_MAT", "001"));
            fieldList.Add(new casField("ABC_TRAILER_SERIAL_NUM", "001"));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_SN_1", "001"));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_SN_2", "001"));
            fieldList.Add(new casField("ABC_OB_MOTOR_NUMBER", "001"));
            fieldList.Add(new casField("ABC_TRAILER_MODEL_YR", "001"));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_YEAR_1", "001"));
            fieldList.Add(new casField("ABC_INBRD_MOTOR_YEAR_2", "001"));
            fieldList.Add(new casField("ABC_OB_MOTOR_YEAR", "001"));
            fieldList.Add(new casField("ABC_TRAILER_MAKE", "001"));
            fieldList.Add(new casField("ABC_INBRD_MAKE_1", "001"));
            fieldList.Add(new casField("ABC_INBRD_MAKE_2", "001"));
            fieldList.Add(new casField("ABC_OB_MOTOR_MAKE", "001"));
            fieldList.Add(new casField("ABC_TRAILER_MODEL", "001"));
            fieldList.Add(new casField("ABC_INBRD_MODEL_1", "001"));
            fieldList.Add(new casField("ABC_INBRD_MODEL_2", "001"));
            fieldList.Add(new casField("ABC_OB_MOTOR_MODEL", "001"));
            fieldList.Add(new casField("ABC_TRAILER_LIC_NUM", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_TYPE", "001"));
            fieldList.Add(new casField("ABC_TOT_HSP_AMOUNT", "001"));
            fieldList.Add(new casField("ABC_COLL_PLANE_HSP", "001"));
            fieldList.Add(new casField("ABC_COLL_RV_TYPE", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_SERIAL_NUM", "001"));
            fieldList.Add(new casField("ABC_COLL_ST_REGIS", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_NEW_USED", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_MAKE", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_MODEL", "001"));
            fieldList.Add(new casField("ABC_VEH_LIC_NUMBER", "001"));
            fieldList.Add(new casField("ABC_COLL_SALES_PRICE", "001"));
            fieldList.Add(new casField("ABC_ABC_COLL_IND", "001"));

            fieldList.Add(new casField("CDC_CD_SAV_IND_1", "001"));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_1", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_1", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_1", "001"));
            fieldList.Add(new casField("CDC_CD_MAT_DATE_1", "001"));

            fieldList.Add(new casField("CDC_CD_SAV_IND_2", "001"));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_2", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_2", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_2", "001"));
            fieldList.Add(new casField("CDC_CD_MAT_DATE_2", "001"));

            fieldList.Add(new casField("CDC_CD_SAV_IND_3", "001"));
            fieldList.Add(new casField("CDC_COLL_ACCT_NUM_3", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_AMT_3", "001"));
            fieldList.Add(new casField("CDC_SAV_CD_NAME_3", "001"));
            fieldList.Add(new casField("CDC_CD_MAT_DATE_3", "001"));

            fieldList.Add(new casField("SBC_CUSIP_NUM_1", "001"));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_1", "001"));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_1", "001"));
            fieldList.Add(new casField("SBC_BOND_AMT_1", "001"));
            fieldList.Add(new casField("SBC_MATURING_DATE_1", "001"));

            fieldList.Add(new casField("SBC_CUSIP_NUM_2", "001"));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_2", "001"));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_2", "001"));
            fieldList.Add(new casField("SBC_BOND_AMT_2", "001"));
            fieldList.Add(new casField("SBC_MATURING_DATE_2", "001"));

            fieldList.Add(new casField("SBC_CUSIP_NUM_3", "001"));
            fieldList.Add(new casField("SBC_STCK_BOND_NUM_SHRS_3", "001"));
            fieldList.Add(new casField("SBC_SECUR_ISSUER_DESC_1_3", "001"));
            fieldList.Add(new casField("SBC_BOND_AMT_3", "001"));
            fieldList.Add(new casField("SBC_MATURING_DATE_3", "001"));

            fieldList.Add(new casField("ACL_ACL_NEW_CASH_DOWN_AMT", "001"));
            fieldList.Add(new casField("ACL_WF_ACL_FEES_FINANCED", "001"));
            fieldList.Add(new casField("ACL_WF_ACL_PAYOFF_PRICE", "001"));
            fieldList.Add(new casField("ACL_ACL_SPEED_TYPE", "001"));
            fieldList.Add(new casField("ABC_COLL_DLR_NAME", "001"));
            fieldList.Add(new casField("ABC_COLL_DEALER_ADDR", "001"));
            fieldList.Add(new casField("ABC_COLL_DEALER_CITY", "001"));
            fieldList.Add(new casField("ABC_COLL_DEALER_STATE", "001"));
            fieldList.Add(new casField("ABC_COLL_DEALER_ZIP_CODE", "001"));

            fieldList.Add(new casField("ABC_COLL_SELLER_NAME", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_ADDR", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_CITY", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_STATE", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_ZIP_CODE", "001"));
            fieldList.Add(new casField("ABC_COLL_SELLER_NAME2", "001"));

            fieldList.Add(new casField("ABC_COLL_LIEN_HNAME", "001"));
            fieldList.Add(new casField("ABC_COLL_LIEN_ADDR", "001"));
            fieldList.Add(new casField("ABC_COLL_LIEN_CITY", "001"));
            fieldList.Add(new casField("ABC_COLL_LIEN_STATE", "001"));
            fieldList.Add(new casField("ABC_COLL_LIEN_ZIP_CODE", "001"));

            fieldList.Add(new casField("BKG_PCM_COLL_IS_PRIMARY_RES", "001"));
            fieldList.Add(new casField("ABC_INBRD_NEW_USED_1", "001"));
            fieldList.Add(new casField("ABC_INBRD_NEW_USED_2", "001"));
            fieldList.Add(new casField("ABC_TRAILER_AXLES", "001"));

            

            casField[] casFields;
            casFields = (casField[])fieldList.ToArray(typeof(casField));

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            CWSInqRes cwsInqRes = Invoker.GetCollateralData(
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
            }

            if (this._inquiryHelper != null && this._inquiryHelper.IsOK)
            {
                this._inqResponse = this._inquiryHelper.Response;
            }

            Set(this);

            return this._inquiryHelper;
        }

        /// <summary>
        /// The acaps cwsinq update.
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
        /// The <see cref="bool"/>.
        /// </returns>
        private bool acapsCWSINQUpdate(
            string ApplicationId, 
            string UserLocationCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            bool PendingValidation, 
            bool CheckEligibility, 
            casField[] casFields)
        {
            // set tran_id for logging
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            this.id = CASUtils.GetUniqueInstanceID();
            this.tran_id = this.session_id + "." + this.id;

            CWSUpdRes getUpdateRes = Invoker.UpdateCollateralData(
                casFields, this._inqResponse, UserSalesId, this.session_id);

            if (getUpdateRes != null)
            {
                this._updateHelper = getUpdateRes.cwsUpdHelper;
            }

            return this._updateHelper != null && this._updateHelper.IsOK;
        }

        #endregion
    }
}