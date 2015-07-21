namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The account.
    /// </summary>
    [Serializable]
    public class Account
    {
        #region Fields

        /// <summary>
        /// The acctdetail.
        /// </summary>
        [XmlElement("ACCTDETAIL01", typeof(ACCTDETAIL01))]
        [XmlElement("ACCTDETAIL02", typeof(ACCTDETAIL02))]
        [XmlElement("ACCTDETAIL03", typeof(ACCTDETAIL03))]
        [XmlElement("ACCTDETAIL04", typeof(ACCTDETAIL04))]
        [XmlElement("ACCTDETAIL05", typeof(ACCTDETAIL05))]
        [XmlElement("ACCTDETAIL06", typeof(ACCTDETAIL06))]
        [XmlElement("ACCTDETAIL07", typeof(ACCTDETAIL07))]
        [XmlElement("ACCTDETAIL08", typeof(ACCTDETAIL08))]
        public object ACCTDETAIL;

        /// <summary>
        /// The account company number.
        /// </summary>
        public string AccountCompanyNumber;

        /// <summary>
        /// The account number.
        /// </summary>
        public string AccountNumber;

        /// <summary>
        /// The hogan product code.
        /// </summary>
        public string HoganProductCode;

        #endregion

        // public string RelationshipCode;
    }
}