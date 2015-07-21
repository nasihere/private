namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The acctdetai l 07.
    /// </summary>
    [Serializable]
    public class ACCTDETAIL07
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
        /// The contract date.
        /// </summary>
        public string ContractDate;

        /// <summary>
        /// The credit life indicator.
        /// </summary>
        public string CreditLifeIndicator;

        /// <summary>
        /// The general ledger code.
        /// </summary>
        public string GeneralLedgerCode;

        /// <summary>
        /// The iu indicator.
        /// </summary>
        public string IUIndicator;

        /// <summary>
        /// The interest rate.
        /// </summary>
        public string InterestRate;

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
        /// The next payment due date.
        /// </summary>
        public string NextPaymentDueDate;

        /// <summary>
        /// The notice code.
        /// </summary>
        public string NoticeCode;

        /// <summary>
        /// The officer code.
        /// </summary>
        public string OfficerCode;

        /// <summary>
        /// The original note amount.
        /// </summary>
        public string OriginalNoteAmount;

        /// <summary>
        /// The payoff amount.
        /// </summary>
        public string PayoffAmount;

        /// <summary>
        /// The payoff date.
        /// </summary>
        public string PayoffDate;

        /// <summary>
        /// The recovery status.
        /// </summary>
        public string RecoveryStatus;

        /// <summary>
        /// The restriction.
        /// </summary>
        public string Restriction;

        /// <summary>
        /// The store code.
        /// </summary>
        public string StoreCode;

        /// <summary>
        /// The term length.
        /// </summary>
        public string TermLength;

        #endregion
    }
}