// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECBSCustomerInfoHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for ECBSCustomerInfoHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Collections;
    using System.Web;

    using Common.Logging;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.Data.sif.Services.ECBS.GetAccountInformation;
    using WF.EAI.Data.sif.Services.ECBS.GetCustomerInformation200909;
    using WF.EAI.Entities.constants;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    ///     Summary description for ECBSCustomerInfoHelper.
    /// </summary>
    public class ECBSCustomerInfoHelper
    {
        #region Constants

        /// <summary>
        /// The ecbs customer info 21.
        /// </summary>
        public const string EcbsCustomerInfo21 = "newECBSCustomerProfile21";

        /// <summary>
        /// The initiator company number.
        /// </summary>
        private const string initiatorCompanyNumber = Constants.DefaultInitiatorCompanyNumber;

        #endregion

        #region Static Fields


        #endregion

        #region Fields

        /// <summary>
        /// The exception.
        /// </summary>
        protected ApplicationException Exception;

        #endregion

        // SJ_R308: Added a new parameter to pass logged in UserID to populate the request xml to RTN Service
        // ECPR WFXml 2.1 R4.09
        #region Public Properties

        /// <summary>
        /// Gets or sets the ach acct list.
        /// </summary>
        public static string AchAcctList
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null
                    && HttpContext.Current.Session["AchAccountList"] != null)
                {
                    return (string)HttpContext.Current.Session["AchAccountList"];
                }

                return null;
            }

            set
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && value != null)
                {
                    HttpContext.Current.Session["AchAccountList"] = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get ach account list 21.
        /// </summary>
        /// <param name="customers">
        /// The customers.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="Au">
        /// The au.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAchAccountList21(
            GetCustomerInformation200909Res.customerInfo[] customers, string userId, string Au)
        {
            string accountNumberType = string.Empty;
            string sessionId = string.Empty;
            bool exists = false;

            if (customers == null || customers.Length == 0)
            {
                return string.Empty;
            }

            CustomerAccountList custAccountList = new CustomerAccountList();
            CustomerAccount custAccount;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionId = HttpContext.Current.Session.SessionID;
            }

            // DataTable dt_Products =  GetProductListDBHelper.CBD_GetEligibleProductsDT("AutoPay", "HEQ", "DDA");
            // Dictionary<string, string> ProductCodes = null;
            // if (dt_Products != null)
            // {
            // foreach (DataRow dr in dt_Products.Rows)
            // {
            // //productsList.AForEach(x => subProdCodeList.Add(x.SubproductCode != null ? x.SubproductCode : "", x.AccountType != null ? x.AccountType : ""));
            // ProductCodes.Add(dr["SubProductCode"] != null ? dr["SubProductCode"].ToString() : "",
            // dr["AccountType"] != null ? dr["AccountType"].ToString() : "");
            // }

            // }
            var ProductCodes = DataAccessHelper.CBD_GetEligibleProducts("AutoPay", "HEQ", "DDA");

            foreach (GetCustomerInformation200909Res.customerInfo customer in customers)
            {
                if (customer != null && customer.accountList != null)
                {
                    foreach (GetCustomerInformation200909Res.customerInfo.Account account in customer.accountList)
                    {
                        exists = false;
                        if (account.SORAccountStatusCode == "01" || account.SORAccountStatusCode == "09"
                            || account.SORAccountStatusCode == "99")
                        {
                            if (ProductCodes.ContainsKey(account.productSubCode))
                            {
                                if (custAccountList.Contains(account.accountNumber))
                                {
                                    exists = true;
                                }

                                if (!exists)
                                {
                                    string RTN = Invoker.AccountInquiry(
                                        Au, sessionId, account.accountNumber, customer.custNbr, userId, "ACF2");
                                    custAccount = new CustomerAccount(account.accountNumber, RTN);

                                    custAccountList.Add(custAccount);
                                    string checkingOrSaving;
                                    if (ProductCodes[account.productSubCode] == "SAV")
                                    {
                                        checkingOrSaving = "S";
                                    }
                                    else
                                    {
                                        checkingOrSaving = "C";
                                    }

                                    accountNumberType += account.accountNumber + checkingOrSaving + "|";
                                }
                            }
                        }
                    }
                }
            }

            // R2.08 P0011361 BAU Fulfillment 3/31/08 AK
            // to avoid extra GetAchAccountList call, put ACH Account Number and Account Type 
            // in Session Object and will be used in CwsUpdateHelper.cs
            if (!string.IsNullOrEmpty(accountNumberType))
            {
                HttpContext.Current.Session["AccountNumberType"] = accountNumberType;
            }

            return custAccountList.ToString();
        }

        /// <summary>
        /// The get customers 1.
        /// </summary>
        /// <param name="app1Nbr">
        /// The app 1 nbr.
        /// </param>
        /// <param name="app2Nbr">
        /// The app 2 nbr.
        /// </param>
        /// <param name="au">
        /// The au.
        /// </param>
        /// <param name="hris">
        /// The hris.
        /// </param>
        public void GetCustomers1(string app1Nbr, string app2Nbr, string au, string hris)
        {
            GetCustomerInformation200909Res priCustomerResp = null;
            GetCustomerInformation200909Res secCustomerResp = null;
            ArrayList customerInfo21List = new ArrayList();
            string sessionId = string.Empty;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionId = HttpContext.Current.Session.SessionID;
            }

            if (app1Nbr != null && app1Nbr.Trim().Length > 0)
            {
                try
                {
                    // WFXML2.1 New Customer Profile R3.09 06/17/09 AK                    
                    priCustomerResp = Invoker.GetCustomerInformation200909(
                        au, 
                        app1Nbr, 
                        hris, 
                        initiatorCompanyNumber, 
                        Constants.ServiceNameEnum.GetCustomerInformation200909.ToString(), 
                        Constants.ApplicationNameEnum.CAS.ToString(), 
                        sessionId);
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error(ex.Message + " ECBSCustomerInfoHelper" + "SessionId = " + sessionId);
                }

                if (app2Nbr != null && app2Nbr.Trim().Length > 0)
                {
                    try
                    {
                        // WFXML2.1 New Customer Profile R3.09 06/17/09 AK                        
                        secCustomerResp = Invoker.GetCustomerInformation200909(
                            au, 
                            app2Nbr, 
                            hris, 
                            initiatorCompanyNumber, 
                            Constants.ServiceNameEnum.GetCustomerInformation200909.ToString(), 
                            Constants.ApplicationNameEnum.CAS.ToString(), 
                            sessionId);
                    }
                    catch (Exception ex)
                    {
                        //Logger.Instance.Error(ex.Message + " ECBSCustomerInfoHelper" + "SessionId = " + sessionId);
                    }
                }

                try
                {
                    if (priCustomerResp != null && priCustomerResp.customerInfo21 != null)
                    {
                        // Populate Account Details of all accounts for each customer.                    
                        AccountDetailsHelper accountHelper = new AccountDetailsHelper();
                        GetAccountInformationRes.Account[] priAccountList = accountHelper.GetAccountDetails(
                            au, hris, initiatorCompanyNumber, priCustomerResp.customerInfo21, sessionId);

                        // Populate Account Calculation
                        priCustomerResp.customerInfo21.PopulateAccountDetail(priAccountList);

                        customerInfo21List.Add(priCustomerResp.customerInfo21);

                        if (secCustomerResp != null && secCustomerResp.customerInfo21 != null)
                        {
                            GetAccountInformationRes.Account[] secAccountList = accountHelper.GetAccountDetails(
                                au, hris, initiatorCompanyNumber, secCustomerResp.customerInfo21, sessionId);

                            // Populate Account Calculation
                            secCustomerResp.customerInfo21.PopulateAccountDetail(secAccountList);

                            customerInfo21List.Add(secCustomerResp.customerInfo21);
                        }
                    }

                    if (HttpContext.Current != null && HttpContext.Current.Session != null)
                    {
                        if (customerInfo21List.Count > 0)
                        {
                            HttpContext.Current.Session[EcbsCustomerInfo21] =
                                customerInfo21List.ToArray(typeof(GetCustomerInformation200909Res.customerInfo));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Instance.Error(ex.Message + " ECBSCustomerInfoHelper" + "SessionId = " + sessionId);
                }
            }
        }

        #endregion
    }
}