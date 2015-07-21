namespace WF.EAI.BLL.cas.Services
{
    using System;

    /// <summary>
    /// The account balance.
    /// </summary>
    [Serializable]
    public class AccountBalance
    {
        #region Fields

        /// <summary>
        /// The balance amount.
        /// </summary>
        public string BalanceAmount;

        /// <summary>
        /// The balance effective date.
        /// </summary>
        public string BalanceEffectiveDate;

        /// <summary>
        /// The balance type code.
        /// </summary>
        public string BalanceTypeCode;

        /// <summary>
        /// The balance type description.
        /// </summary>
        public string BalanceTypeDescription;

        #endregion
    }
}