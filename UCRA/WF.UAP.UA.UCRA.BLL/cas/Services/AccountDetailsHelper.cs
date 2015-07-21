// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountDetailsHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The account details helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Collections;
    using System.Xml;

    using Common.Logging;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.Data.LegecyServiceClients.Providers.Http;
    using WF.EAI.Data.sif.Services.ECBS.GetAccountInformation;
    using WF.EAI.Data.sif.Services.ECBS.GetCustomerInformation200909;
    using WF.EAI.Entities.constants;
    using WF.UAP.UASF.CrossCutting.Logging;

    // AccountDetailsHelper issues the async request.
    /// <summary>
    /// The account details helper.
    /// </summary>
    public class AccountDetailsHelper : HttpAsyncBase
    {
        // Static Variables
        #region Static Fields

        /// <summary>
        /// The _request template.
        /// </summary>
        private static XmlDocument _requestTemplate;


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDetailsHelper"/> class.
        /// </summary>
        public AccountDetailsHelper()
        {
        //    if (this.local.Services.Certificates != null && this.local.Services.Certificates.AccountDetails != null
        //        && this.local.Services.Certificates.AccountDetails.enabled.ToUpper() == "TRUE"
        //        && File.Exists(this.local.Services.Certificates.AccountDetails.certificateFilePath))
        //    {
        //        this.Certificate =
        //            X509Certificate.CreateFromCertFile(
        //                this.local.Services.Certificates.AccountDetails.certificateFilePath);
        //    }

        //    this.Timeout = int.Parse(this.local.Services.EXTERNAL.AccountDetailsTimeOut) * 1000;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get account details.
        /// </summary>
        /// <param name="AU">
        /// The au.
        /// </param>
        /// <param name="hris">
        /// The hris.
        /// </param>
        /// <param name="initiatorCompanyNbr">
        /// The initiator company nbr.
        /// </param>
        /// <param name="customersInfo21">
        /// The customers info 21.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The <see cref="Account[]"/>.
        /// </returns>
        public GetAccountInformationRes.Account[] GetAccountDetails(
            string AU, 
            string hris, 
            string initiatorCompanyNbr, 
            GetCustomerInformation200909Res.customerInfo customersInfo21, 
            string sessionId)
        {
            ArrayList accountsList = new ArrayList();
            GetAccountInformationReq.Account_Type[] AccountList;

            // Check CustomerInfo21 object and get Account Details for each customer
            if (customersInfo21 != null)
            {
                GetAccountInformationReq.Account_Type accoutType;

                // Create AccountList
                if (customersInfo21.accountList != null && customersInfo21.accountList.Length > 0)
                {
                    foreach (GetCustomerInformation200909Res.customerInfo.Account account in customersInfo21.accountList
                        )
                    {
                        string accType = this.GetAccountType(account.hoganProductCode);
                        if (accType != string.Empty)
                        {
                            accoutType = new GetAccountInformationReq.Account_Type();
                            accoutType.accountKey = new GetAccountInformationReq.AccountKey_Type();
                            accoutType.accountKey.accountNumber = account.accountNumber;
                            accoutType.accountKey.companyNumber = account.companyNumber;
                            accoutType.accountKey.hoganProductCode = account.hoganProductCode;

                            // Set Preferences
                            accoutType.accountPreferences = new GetAccountInformationReq.AccountPreferences_Type();
                            accoutType.accountPreferences.accountDetailInformationIndicator = true;
                            accoutType.accountPreferences.accountDetailInformationIndicatorSpecified = true;
                            accoutType.accountPreferences.accountCustomerRelationshipIndicator = true;
                            accoutType.accountPreferences.accountCustomerRelationshipIndicatorSpecified = true;
                            accoutType.accountPreferences.accountAddressIndicator = false;
                            accoutType.accountPreferences.accountAddressIndicatorSpecified = false;

                            accountsList.Add(accoutType);
                        }
                    }

                    try
                    {
                        // WFXML2.1 New Account Details 200711
                        string custNum = customersInfo21.custNbr;
                        if (accountsList.Count > 0)
                        {
                            AccountList =
                                (GetAccountInformationReq.Account_Type[])
                                accountsList.ToArray(typeof(GetAccountInformationReq.Account_Type));

                            GetAccountInformationRes getAccountInformationRes = Invoker.GetAccountInformation(
                                AU, 
                                custNum, 
                                hris, 
                                initiatorCompanyNbr, 
                                Constants.ServiceNameEnum.GetAccountInformation.ToString(), 
                                Constants.ApplicationNameEnum.CAS.ToString(), 
                                AccountList, 
                                this.session_id);

                            if (getAccountInformationRes != null && getAccountInformationRes.accountList != null
                                && getAccountInformationRes.accountList.Length > 0)
                            {
                                return getAccountInformationRes.accountList;
                            }

                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logger.Instance.Error(ex.Message + " AccountDetailHelper" + "SessionId = " + sessionId);

                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// The get account type.
        /// </summary>
        /// <param name="hoganProductCode">
        /// The hogan product code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAccountType(string hoganProductCode)
        {
            switch (hoganProductCode)
            {
                case "DDA":
                    return "demandDepositAccountInfo";
                case "ILA":
                    return "installmentLoanAccountInfo";
                case "XCC":
                    return "creditCardAccountInfo";
                case "XIV":
                    return "investmentAccountInfo";
                case "LCA":
                    return "lineOfCreditAccountInfo";
                case "CDA":
                    return "termDepositAccountInfo";
                case "REA":
                    return "retirementAccountInfo";
                case "XML":
                    return "mortgageAccountInfo";
                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}