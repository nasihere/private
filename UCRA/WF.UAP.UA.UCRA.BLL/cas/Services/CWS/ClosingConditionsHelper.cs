// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankerNoteReadHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for BankerNoteReadHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Xml;

    using global::Common.Logging;

    using WF.EAI.BLL.cas.Services;
    using WF.EAI.Entities.domain.cas;
    using WF.EAI.Data.config.sif;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    ///     Summary description for BankerNoteReadHelper.
    /// </summary>
    public class ClosingConditionsHelper : IAJAXHelper
    {
        #region Fields

        /// <summary>
        /// The iaf empapp err code.
        /// </summary>
        private const string IafEmpappErrCode = "303040";


        /// <summary>
        /// The _banker note send.
        /// </summary>
        private Stack _closingConditions;

        /// <summary>
        /// The id.
        /// </summary>
        private ulong id = 0;

        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        /// <summary>
        /// The tran_id.
        /// </summary>
        private string tran_id = string.Empty;

        /// <summary>
        /// The app id.
        /// </summary>
        private string appId = string.Empty;

         /// <summary>
        ///     The _loc.
        /// </summary>
        private string _loc = string.Empty;

        /// <summary>
        ///     The _login id.
        /// </summary>
        private string _loginId = string.Empty;
        
        /// <summary>
        ///     The _hris.
        /// </summary>
        private string _hris = string.Empty;

        /// <summary>
        ///     The _au.
        /// </summary>
        private string _au = string.Empty;

        /// <summary>
        /// The _error codes.
        /// </summary>
        private ArrayList _errorCodes;

        private string errorMessage = string.Empty;
        /// <summary>
        ///     Gets or sets Au.
        /// </summary>
        public string Au
        {
            get
            {
                return _au;
            }

            set
            {
                _au = value;
            }
        }

        /// <summary>
        ///     Gets or sets LOC.
        /// </summary>
        public string LOC
        {
            get
            {
                return _loc;
            }

            set
            {
                _loc = value;
            }
        }

        /// <summary>
        ///     Gets UserId.
        /// </summary>
        public string UserId
        {
            get
            {
                return _loginId;
            }
        }

        /// <summary>
        ///     Gets or sets HRIS.
        /// </summary>
        public string HRIS
        {
            get
            {
                return _hris;
            }

            set
            {
                _hris = value;
            }
        }

        protected ArrayList errorCodeList
        {
            get
            {
                if (_errorCodes == null)
                {
                    _errorCodes = new ArrayList();
                }

                return _errorCodes;
            }

            set
            {
                _errorCodes = value;
            }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankNoteReadHelper"/> class.
        /// </summary>
        public ClosingConditionsHelper()
        {
            this._closingConditions = new Stack();
        }

        #endregion

        #region Public Methods and Operators

        public string sendMessage(XmlDocument rDoc)
        {
            bool update = updateACAPS(rDoc);
            if (update)
            {
                errorMessage = string.Empty;
            }
            else
            {
                errorMessage = ACAPSErrorMessages.retrieveErrorMessages(errorCodeList);
            }
            return errorMessage;
        }
        
          /// <summary>
        /// The update acaps.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool updateACAPS(XmlDocument rDoc)
        {
            bool success = true;
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(rDoc.NameTable);
            nsMgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsMgr.AddNamespace("SOAP-ENC", "http://schemas.xmlsoap.org/soap/encoding/");
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            nsMgr.AddNamespace("m", "http://service.wellsfargo.com/provider/HCFG/ACAPS/shared/2005/");
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            appId =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:ACAPSContext/applicationId",
                    nsMgr) != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:ACAPSContext/applicationId",
                        nsMgr).InnerText.Trim()
                    : string.Empty;
            string requestorUserID =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:originatorId", nsMgr)
                != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:originatorId",
                        nsMgr).InnerText.Trim()
                    : string.Empty;
             Au =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:billingAU", nsMgr)
                != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:billingAU",
                        nsMgr).InnerText.Trim()
                    : string.Empty;
            string requestorLoc =
               rDoc.SelectSingleNode(
                   "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorLoc") != null
                   ? rDoc.SelectSingleNode(
                       "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorLoc")
                        .InnerText.Trim()
                   : string.Empty;
            string requestorSalesID =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorSalesID") != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorSalesID")
                         .InnerText.Trim()
                    : string.Empty;
           
            XmlNode node=
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Body/data/fields",
                    nsMgr);

        


            try
            {
                CwsUpdateHelper updateHelper = CwsUpdateHelper.Get();
                if (updateHelper != null)
                {
                    updateHelper.BeginInquiry(appId, LOC, UserId, HRIS, Au, false, false);

                    if (HttpContext.Current.Session["IAF_EMPAPP"] != null && HttpContext.Current.Session["IAF_EMPAPP"].ToString() == "true")
                    {
                        errorCodeList.Add(IafEmpappErrCode);
                        errorMessage = ACAPSErrorMessages.retrieveErrorMessages(errorCodeList);
                        HttpContext.Current.Session["IAF_EMPAPP"] = null;

                        return false;
                    }
                    else
                    {
                        updateHelper.EndInquiry();
                        if (updateHelper[CwsUpdateFieldMap.AD5_TITLE_AT_CLOSING] != null &&  updateHelper[CwsUpdateFieldMap.AD5_TITLE_AT_CLOSING_READ]!=null)
                        {
                            if (node.SelectSingleNode("AD5_TITLE_AT_CLOSING_READ") != null)
                            {
                                string title = node.SelectSingleNode("AD5_TITLE_AT_CLOSING_READ").InnerText.Trim();
                                updateHelper[CwsUpdateFieldMap.AD5_TITLE_AT_CLOSING_READ].value = title;
                            }
                        }
                        if (updateHelper[CwsUpdateFieldMap.AD5_TITLE_AT_CLOSING_OWN_CHG] != null && updateHelper[CwsUpdateFieldMap.AD5_TITLE_ATCLOS_READ_OWN_CHG]!=null)
                        {
                            if (node.SelectSingleNode("AD5_TITLE_ATCLOS_READ_OWN_CHG") != null)
                            {
                                string owner= node.SelectSingleNode("AD5_TITLE_ATCLOS_READ_OWN_CHG").InnerText.Trim();
                                updateHelper[CwsUpdateFieldMap.AD5_TITLE_ATCLOS_READ_OWN_CHG].value = owner;
                            }
                        }
                        if (updateHelper[CwsUpdateFieldMap.AD5_GOVT_ID_REQD] != null && updateHelper[CwsUpdateFieldMap.AD5_GOVT_ID_REQD_READ] != null)
                        {
                            if (node.SelectSingleNode("AD5_GOVT_ID_REQD_READ") != null)
                            {
                                string govtId = node.SelectSingleNode("AD5_GOVT_ID_REQD_READ").InnerText.Trim();
                                updateHelper[CwsUpdateFieldMap.AD5_GOVT_ID_REQD_READ].value = govtId;
                            }
                        }
                        CwsUpdateHelper.Set(updateHelper);

                        success = updateHelper.BeginUpdate(null, null, appId, Au, LOC, UserId, HRIS);
                    }
                }

                if (updateHelper != null && !updateHelper.EndUpdate())
                {
                    consolidateErrors(updateHelper.ErrorCodes);
                    CwsUpdateHelper.Set(null);
                    success = false;
                }
                else
                {
                    CwsUpdateHelper.Set(null);
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Error(ex.Message);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// The consolidate errors.
        /// </summary>
        /// <param name="errorArray">
        /// The error array.
        /// </param>
        protected void consolidateErrors(string[] errorArray)
        {
            for (int i = 0; errorArray != null && i < errorArray.Length; i++)
            {
                errorCodeList.Add(errorArray[i]);
            }
        }

        /// <summary>
        /// The receive message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string receiveMessage(XmlDocument msg)
        {
            return null;
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string sendMessage(XmlDocument msg, EAISIFServiceBaseConfig config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}