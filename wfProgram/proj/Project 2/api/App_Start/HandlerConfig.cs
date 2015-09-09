// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlerConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The handler config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace xxxxProjec.....xxxxProjec.....WebApi
{
    #region

    using System.Collections.ObjectModel;
    using System.Net.Http;

    using xxxxProjec....SF.App.Host.WebApi;

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