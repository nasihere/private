using WF.EAI.BLL.BO.CUSPSearch.Helper;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.UAP.UASF.App.Host.WebApi.Contract;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    public class ArchiveLookupTabBo : SearchBo
    {
        public ArchiveLookupTabBo()
        {
            RetailerviewName = Model.Lookup.RetailerSearchViewName.CreditAnalysis;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<CUSPRequestHeader></param>
        /// <returns>ViewModel</returns>
        public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
        {
            AppDataHeader appHeader = SearchHelper.GetModelObject(request);
            SearchApplicationResultsModel model = null;
            model = new SearchApplicationResultsModel();
            return model;
        }

    }
}
