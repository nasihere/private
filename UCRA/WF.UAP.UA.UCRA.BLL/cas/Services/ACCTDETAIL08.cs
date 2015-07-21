namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The acctdetai l 08.
    /// </summary>
    [Serializable]
    public class ACCTDETAIL08
    {
        #region Fields

        /// <summary>
        /// The ah indicator.
        /// </summary>
        public string AHIndicator;

        /// <summary>
        /// The account title list.
        /// </summary>
        [XmlArrayItem("AccountTitle", typeof(AccountTitle), IsNullable = false)]
        public AccountTitle[] AccountTitleList;

        /// <summary>
        /// The auto debit code.
        /// </summary>
        public string AutoDebitCode;

        /// <summary>
        /// The available balance.
        /// </summary>
        public string AvailableBalance;

        /// <summary>
        /// The collateral code.
        /// </summary>
        public string CollateralCode;

        /// <summary>
        /// The combined statement indicator.
        /// </summary>
        public string CombinedStatementIndicator;

        /// <summary>
        /// The credit life indicator.
        /// </summary>
        public string CreditLifeIndicator;

        /// <summary>
        /// The current balance.
        /// </summary>
        public string CurrentBalance;

        /// <summary>
        /// The interest paid ytd.
        /// </summary>
        public string InterestPaidYTD;

        /// <summary>
        /// The interest rate.
        /// </summary>
        public string InterestRate;

        /// <summary>
        /// The limit amount.
        /// </summary>
        public string LimitAmount;

        /// <summary>
        /// The loan balance.
        /// </summary>
        public string LoanBalance;

        /// <summary>
        /// The loan type code.
        /// </summary>
        public string LoanTypeCode;

        /// <summary>
        /// The maturity date.
        /// </summary>
        public string MaturityDate;

        /// <summary>
        /// The notice code.
        /// </summary>
        public string NoticeCode;

        /// <summary>
        /// The officer code.
        /// </summary>
        public string OfficerCode;

        /// <summary>
        /// The original agreement date.
        /// </summary>
        public string OriginalAgreementDate;

        // public string OriginalAgreementDate;

        /// <summary>
        /// The payoff amount.
        /// </summary>
        public string PayoffAmount;

        /// <summary>
        /// The recovery status.
        /// </summary>
        public string RecoveryStatus;

        /// <summary>
        /// The restriction.
        /// </summary>
        public string Restriction;

        /// <summary>
        /// The scheduled payment amount.
        /// </summary>
        public string ScheduledPaymentAmount;

        /// <summary>
        /// The stop pay indicator.
        /// </summary>
        public string StopPayIndicator;

        /// <summary>
        /// The store code.
        /// </summary>
        public string StoreCode;

        #endregion
    }
}