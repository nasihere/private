using System.Collections.Generic;
using WF.EAI.Model.DTO.CMT.AltOffers;
using WF.EAI.Model.Lookup;
using WF.EAI.Model.ViewModels.CMT.AltOffers;
using WF.UAP.UASF.App.Host.UI.ApiInvoker;
using DataModel = WF.UAP.UDB.Repository.Domain.Entities.DataModel;

namespace WF.UAP.UA.UCRA.BLL.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class AltOffersServiceInvokerBuilder:IAltOffersServiceInvokerBuilder
    {
        private Dictionary<string,dynamic> _invokers = new Dictionary<string,dynamic>();

        /// <summary>
        /// 
        /// </summary>
        public AltOffersServiceInvokerBuilder()
        {
            LoadInvokers();
        }

        private void LoadInvokers()
        {
            _invokers.Add(AltOffersViewName.Index.ToString(), new ServiceInvoker<AltOffersRequestHeader, AltOffersModel, DataModel>());
            _invokers.Add(AltOffersViewName.Offer.ToString(), new ServiceInvoker<AltOffersRequestHeader, AltOffersModel, DataModel>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public dynamic GetInvoker(string viewName)
        {
            return _invokers[viewName];
        }
    }
}
