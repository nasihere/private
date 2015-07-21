namespace WF.EAI.BLL.cas.Services.Collateral
{
    using System;

    /// <summary>
    /// The sbc item.
    /// </summary>
    [Serializable]
    public class SBCItem
    {
        #region Fields

        /// <summary>
        /// The sb c_ bon d_ amt.
        /// </summary>
        public string SBC_BOND_AMT = string.Empty;

        /// <summary>
        /// The sb c_ cusi p_ num.
        /// </summary>
        public string SBC_CUSIP_NUM = string.Empty;

        /// <summary>
        /// The sb c_ maturin g_ date.
        /// </summary>
        public string SBC_MATURING_DATE = string.Empty;

        /// <summary>
        /// The sb c_ secu r_ issue r_ desc.
        /// </summary>
        public string SBC_SECUR_ISSUER_DESC = string.Empty;

        /// <summary>
        /// The sb c_ stc k_ bon d_ nu m_ shrs.
        /// </summary>
        public string SBC_STCK_BOND_NUM_SHRS = string.Empty;

        #endregion
    }
}