namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The acctdetai l 02.
    /// </summary>
    [Serializable]
    public class ACCTDETAIL02
    {
        #region Fields

        /// <summary>
        /// The account balance list.
        /// </summary>
        public AccountBalance[] AccountBalanceList;

        /// <summary>
        /// The account open date.
        /// </summary>
        public string AccountOpenDate;

        /// <summary>
        /// The account title list.
        /// </summary>
        [XmlArrayItem("AccountTitle", typeof(AccountTitle), IsNullable = false)]
        public AccountTitle[] AccountTitleList;

        #endregion
    }
}