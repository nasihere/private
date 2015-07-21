namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The loan balance.
    /// </summary>
    [Serializable]
    public class LoanBalance
    {
        #region Fields

        /// <summary>
        /// The value.
        /// </summary>
        [XmlText]
        public string Value;

        #endregion
    }
}