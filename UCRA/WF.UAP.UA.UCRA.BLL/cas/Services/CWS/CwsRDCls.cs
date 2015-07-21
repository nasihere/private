namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;

    /// <summary>
    ///     The cws rd cls.
    /// </summary>
    [Serializable]
    public class CwsRDCls
    {
        #region Fields

        /// <summary>
        ///     The rd description 1.
        /// </summary>
        public string rdDescription1 = string.Empty;

        /// <summary>
        ///     The rd description 2.
        /// </summary>
        public string rdDescription2 = string.Empty;

        /// <summary>
        ///     The rd key.
        /// </summary>
        public string rdKey = string.Empty;

        /// <summary>
        ///     The rdbundleindicator.
        /// </summary>
        public string rdbundleindicator = string.Empty;

        /// <summary>
        ///     The rdrate.
        /// </summary>
        public decimal rdrate;

        /// <summary>
        ///     The rdstatus.
        /// </summary>
        public string rdstatus = string.Empty;

        #endregion
    }
}