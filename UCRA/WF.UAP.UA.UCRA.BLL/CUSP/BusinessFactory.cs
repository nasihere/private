// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The business factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP
{
    using System;

    /// <summary>
    ///     The business factory.
    /// </summary>
    public class BusinessFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets the search bo.
        /// </summary>
        public static SearchBO SearchBO
        {
            get
            {
                return new SearchBO();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get bo.
        /// </summary>
        /// <typeparam name="TBussObj">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TBussObj"/>.
        /// </returns>
        public static TBussObj GetBo<TBussObj>()
        {
            TBussObj bo = default(TBussObj);
            bo = Activator.CreateInstance<TBussObj>();
            return bo;
        }

        #endregion
    }
}