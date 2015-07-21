namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The acctdetai l 03.
    /// </summary>
    [Serializable]
    public class ACCTDETAIL03
    {
        #region Fields

        /// <summary>
        /// The account open date.
        /// </summary>
        public string AccountOpenDate;

        /// <summary>
        /// The account status description.
        /// </summary>
        public string AccountStatusDescription;

        /// <summary>
        /// The account title list.
        /// </summary>
        [XmlArrayItem("AccountTitle", typeof(AccountTitle), IsNullable = false)]
        public AccountTitle[] AccountTitleList;

        /// <summary>
        /// The credit line amount.
        /// </summary>
        public string CreditLineAmount;

        /// <summary>
        /// The last payment date.
        /// </summary>
        public string LastPaymentDate;

        /// <summary>
        /// The next payment due date.
        /// </summary>
        public string NextPaymentDueDate;

        /// <summary>
        /// The product code.
        /// </summary>
        public string ProductCode;

        /// <summary>
        /// The product description.
        /// </summary>
        public string ProductDescription;

        /// <summary>
        /// The status last update date.
        /// </summary>
        public string StatusLastUpdateDate;

        #endregion
    }
}