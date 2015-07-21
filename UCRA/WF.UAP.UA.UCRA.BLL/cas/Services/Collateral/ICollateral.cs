namespace WF.EAI.BLL.cas.Services.Collateral
{
    /// <summary>
    /// The Collateral interface.
    /// </summary>
    public interface ICollateral
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        PCMCollateral Data { get; set; }

        /// <summary>
        /// Sets the title.
        /// </summary>
        string Title { set; }

        #endregion
    }
}