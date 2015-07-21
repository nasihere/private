// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The filter config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.WebApi
{
    #region

    using System.Web.Http.Filters;

    using WF.UAP.UASF.App.Host.WebApi;

    #endregion

    /// <summary>
    /// The filter config.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// The register web api filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new ApiErrorHandlerAttribute());
        }
    }
}