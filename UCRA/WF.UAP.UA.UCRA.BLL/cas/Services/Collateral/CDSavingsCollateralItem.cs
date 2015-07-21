namespace WF.EAI.BLL.cas.Services.Collateral
{
    using System;

    /// <summary>
    /// The cd savings collateral item.
    /// </summary>
    [Serializable]
    public class CDSavingsCollateralItem
    {
        #region Fields

        /// <summary>
        /// The acct num.
        /// </summary>
        public string acctNum;

        /// <summary>
        /// The amount.
        /// </summary>
        public string amount;

        /// <summary>
        /// The financial coll category.
        /// </summary>
        public string financialCollCategory;

        /// <summary>
        /// The financial coll sub category.
        /// </summary>
        public string financialCollSubCategory;

        /// <summary>
        /// The in name of.
        /// </summary>
        public string inNameOf;

        /// <summary>
        /// The maturing date.
        /// </summary>
        public string maturingDate;

        /// <summary>
        /// The own cd.
        /// </summary>
        public string ownCD;

        #endregion
    }
}