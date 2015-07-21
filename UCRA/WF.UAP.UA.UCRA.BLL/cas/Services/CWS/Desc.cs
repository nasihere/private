namespace WF.EAI.BLL.cas.Services.CWS
{
    using System.Collections;

    /// <summary>
    ///     The desc.
    /// </summary>
    public class Desc : IComparer
    {
        #region Public Methods and Operators

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The compare.
        /// </returns>
        public int Compare(object x, object y)
        {
            if (((RelationshipDiscount)x).rdrate > ((RelationshipDiscount)y).rdrate)
            {
                return 1;
            }

            if (((RelationshipDiscount)x).rdrate == ((RelationshipDiscount)y).rdrate)
            {
                return 0;
            }

            if (((RelationshipDiscount)x).rdrate < ((RelationshipDiscount)y).rdrate)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}