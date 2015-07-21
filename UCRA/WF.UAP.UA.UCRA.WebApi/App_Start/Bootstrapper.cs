// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using WF.UAP.UASF.App.Host.WebApi.Contract;
using WF.UAP.UASF.App.Host.WebApi.Contract.Interface;

namespace WF.UAP.UA.UCRA.WebApi
{
    #region

    using System.Web.Http;

    using Microsoft.Practices.Unity;

    using WF.EAI.BLL.BO.CUSPApps.MaturingTools;
    using WF.EAI.BLL.BO.CUSPApps.MaturingTools.Interfaces;
    using WF.UAP.UASF.Utils.Patterns.Unity;
    using WF.UAP.UA.UCRA.BLL.CUSP.Interface;
    using WF.UAP.UA.UCRA.BLL.CUSP;

    #endregion

    /// <summary>
    /// The bootstrapper.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        ///     Initialises the unity and other factories
        /// </summary>
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityApiDependencyResolver(container);
        }

        /// <summary>
        ///     BuildUnityContainer
        /// </summary>
        /// <returns>unity container</returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="container">
        /// </param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IMaturingToolsBo, MaturingToolsBo>();
            container.RegisterType<IUCRASearchBo, UCRASearchBo>();
        }
    }
}