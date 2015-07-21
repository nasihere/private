namespace WF.EAI.BLL.cas.Services.CWS
{
    /// <summary>
    ///     The relationship discount.
    /// </summary>
    public struct RelationshipDiscount
    {
        #region Fields

        /// <summary>
        ///     The rd description 1.
        /// </summary>
        public string rdDescription1;

        /// <summary>
        ///     The rd description 2.
        /// </summary>
        public string rdDescription2;

        /// <summary>
        ///     The rd key.
        /// </summary>
        public string rdKey;

        /// <summary>
        ///     The rdbundleindicator.
        /// </summary>
        public string rdbundleindicator;

        /// <summary>
        ///     The rdrate.
        /// </summary>
        public decimal rdrate;

        /// <summary>
        ///     The rdstatus.
        /// </summary>
        public string rdstatus;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipDiscount"/> struct.
        /// </summary>
        /// <param name="Description1">
        /// The description 1.
        /// </param>
        /// <param name="Description2">
        /// The description 2.
        /// </param>
        /// <param name="Rate">
        /// The rate.
        /// </param>
        /// <param name="BundleInd">
        /// The bundle ind.
        /// </param>
        /// <param name="Status">
        /// The status.
        /// </param>
        /// <param name="Key">
        /// The key.
        /// </param>
        public RelationshipDiscount(
            string Description1, string Description2, decimal Rate, string BundleInd, string Status, string Key)
        {
            this.rdDescription1 = Description1;
            this.rdDescription2 = Description2;
            this.rdrate = Rate;
            this.rdbundleindicator = BundleInd;
            this.rdstatus = Status;
            this.rdKey = Key;
        }

        #endregion
    }
}