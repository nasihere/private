using Newtonsoft.Json;
using System;
using WF.EAI.BLL.BO.CUSPSearch.Helper;
using WF.EAI.Entities.constants.uca;
using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Model;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Utils;

namespace WF.EAI.BLL.BO.CUSPSearch
{
    using WF.EAI.BLL.uca;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// CreditAnalysisBo
    /// </summary>
    public class CreditAnalysisBo : SearchBo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreditAnalysisBo()
        {
            ViewName = Model.Lookup.CUSPSearchViewName.CreditAnalysis;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<CUSPRequestHeader></param>
        /// <returns>ViewModel</returns>
        public ViewModel Invoke(RequestDto<CUSPRequestHeader> request)
        {
            AppDataHeader appHeader = SearchHelper.GetModelObject(request);
            try
            {
                if (request.RequestHeader.ServiceUri == Lookup.ServiceUri.SearchCASApplicationByName.ToString())
                {
                    return SearchHelper.GetModelObject(SearchCASApplicationByName(appHeader));
                }
            }
            catch(Exception ex)
            {
                LogErrorInfo("Error occured in CreditAnalysisBo", ex);
                return null;
            }
            return null;
        }
        /// <summary>
        /// SearchCASApplicationByName
        /// </summary>
        /// <param name="appHeader">AppDataHeader</param>
        /// <returns>SearchApplicationResultEntityCollection</returns>
        public SearchApplicationResultEntityCollection SearchCASApplicationByName(AppDataHeader appHeader)
        {            
            string jsonText = CommonBo.SearchCASApplicationByName(appHeader);
            object jsonObject = JsonConvert.DeserializeObject(jsonText);
            if (jsonText.Contains("SearchParty200909Res") || jsonText.Contains("customerInfo") || jsonText.Contains("SavedApplicationResultEntitys") || jsonText.Contains("SearchApplicationResultEntity"))
            {
                appHeader.TabDataText = jsonText;
            }
            else
            {
                if (!string.IsNullOrEmpty(Convert.ToString(jsonObject)))
                    appHeader = JsonMapper<AppDataHeader>.JsonTextToAppDataHeader<AppDataHeader>(Convert.ToString(jsonObject));
            }

            object resJsonObject = JsonConvert.DeserializeObject(appHeader.TabDataText);
            var listResult = JsonMapper<SearchApplicationResultEntityCollection>.DeserializeList<SearchApplicationResultEntityCollection>(Convert.ToString(resJsonObject));
            return listResult;
        }
    }
}
