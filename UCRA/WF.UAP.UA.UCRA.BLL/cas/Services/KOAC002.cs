namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The koa c 002.
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = false)]
    [Serializable]
    public class KOAC002
    {
        #region Fields

        /// <summary>
        /// The body.
        /// </summary>
        [XmlElement("Body")]
        public BodyResponse Body;

        /// <summary>
        /// The header.
        /// </summary>
        [XmlElement("Header")]
        public Header Header;

        /// <summary>
        /// The version.
        /// </summary>
        [XmlAttribute]
        public string version;

        #endregion
    }
}