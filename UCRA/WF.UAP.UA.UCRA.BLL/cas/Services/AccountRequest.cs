namespace WF.EAI.BLL.cas.Services
{
    using System;

    /// <summary>
    /// The account request.
    /// </summary>
    [Serializable]
    public class AccountRequest
    {
        #region Fields

        /// <summary>
        /// The account company number.
        /// </summary>
        public string AccountCompanyNumber = string.Empty;

        /// <summary>
        /// The account number.
        /// </summary>
        public string AccountNumber = string.Empty;

        /// <summary>
        /// The account view id.
        /// </summary>
        public string AccountViewId;

        /// <summary>
        /// The account view list.
        /// </summary>
        public AccountViewList AccountViewList;

        /// <summary>
        /// The hogan product code.
        /// </summary>
        public string HoganProductCode = string.Empty;

        /// <summary>
        /// The relationship code.
        /// </summary>
        public string RelationshipCode;

        #endregion
    }
}