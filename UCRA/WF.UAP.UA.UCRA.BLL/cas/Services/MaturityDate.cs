namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The maturity date.
    /// </summary>
    [Serializable]
    public class MaturityDate
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