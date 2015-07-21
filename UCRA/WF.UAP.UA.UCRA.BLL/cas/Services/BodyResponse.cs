namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The body response.
    /// </summary>
    [Serializable]
    public class BodyResponse
    {
        #region Fields

        /// <summary>
        /// The account list.
        /// </summary>
        [XmlArrayItem("Account", IsNullable = false)]
        public Account[] AccountList;

        #endregion
    }
}