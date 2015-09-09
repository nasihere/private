// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using xxxxProjec....SF.App.Host.WebApi.Contract;
using xxxxProjec....SF.App.Host.WebApi.Contract.Interface;

namespace xxxxProjec.....xxxxProjec.....WebApi
{
    #region

    using System.Web.Http;

    using Microsoft.Practices.Unity;

    using WF.EAI..BO.Apps.MaturingTools;
    using WF.EAI..BO.Apps.MaturingTools.Interfaces;
    using xxxxProjec....SF.Utils.Patterns.Unity;
    using xxxxProjec.....xxxxProjec.......Interface;
    using xxxxProjec.....xxxxProjec......;

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
            container.RegisterType<IxxxxProjec....SearchBo, xxxxProjec....SearchBo>();
        }
    }
}