// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlerConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The handler config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.WebApi
{
    #region

    using System.Collections.ObjectModel;
    using System.Net.Http;

    using WF.UAP.UASF.App.Host.WebApi;

    #endregion

    /// <summary>
    /// The handler config.
    /// </summary>
    public class HandlerConfig
    {
        /// <summary>
        /// The register web api handler.
        /// </summary>
        /// <param name="handlers">
        /// The handlers.
        /// </param>
        public static void RegisterWebApiHandler(Collection<DelegatingHandler> handlers)
        {
            handlers.Add(new ApiMessageHandler());
        }
    }
}