namespace WF.EAI.BLL.cas.Services.CWS
{
    using System.Collections;

    /// <summary>
    /// The my sort.
    /// </summary>
    public class MySort : IComparer
    {
        #region Fields

        /// <summary>
        /// The is ascending order.
        /// </summary>
        private bool IsAscendingOrder = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MySort"/> class.
        /// </summary>
        /// <param name="blnIsAscendingOrder">
        /// The bln is ascending order.
        /// </param>
        public MySort(bool blnIsAscendingOrder)
        {
            this.IsAscendingOrder = blnIsAscendingOrder;
        }

        #endregion

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
        /// The <see cref="int"/>.
        /// </returns>
        public int Compare(object x, object y)
        {
            if (this.IsAscendingOrder)
            {
                return int.Parse(x.ToString()) - int.Parse(y.ToString());
            }
            else
            {
                return int.Parse(y.ToString()) - int.Parse(x.ToString());
            }
        }

        #endregion
    }
}