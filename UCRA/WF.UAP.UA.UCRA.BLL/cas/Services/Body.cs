namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The body.
    /// </summary>
    [Serializable]
    public class Body
    {
        #region Fields

        /// <summary>
        /// The account list.
        /// </summary>
        [XmlArrayItem("Account", IsNullable = false)]
        public AccountRequest[] AccountList;

        #endregion
    }
}