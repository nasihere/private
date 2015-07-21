namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The header.
    /// </summary>
    [Serializable]
    public class Header
    {
        #region Fields

        /// <summary>
        /// The accounting unit.
        /// </summary>
        public string AccountingUnit;

        /// <summary>
        /// The application code.
        /// </summary>
        public string ApplicationCode;

        /// <summary>
        /// The authorization id.
        /// </summary>
        public string AuthorizationId;

        /// <summary>
        /// The channel code.
        /// </summary>
        public string ChannelCode;

        /// <summary>
        /// The correlation id.
        /// </summary>
        public string CorrelationId;

        /// <summary>
        /// The creation timestamp.
        /// </summary>
        public string CreationTimestamp;

        /// <summary>
        /// The creator id.
        /// </summary>
        public string CreatorId;

        /// <summary>
        /// The device id.
        /// </summary>
        public string DeviceId;

        /// <summary>
        /// The message type.
        /// </summary>
        public string MessageType;

        /// <summary>
        /// The requestor id.
        /// </summary>
        public string RequestorId;

        /// <summary>
        /// The timeout.
        /// </summary>
        public string Timeout;

        /// <summary>
        /// The version.
        /// </summary>
        [XmlAttribute]
        public string version;

        #endregion
    }
}