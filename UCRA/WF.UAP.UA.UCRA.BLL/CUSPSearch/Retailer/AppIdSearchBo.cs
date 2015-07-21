using Newtonsoft.Json;
using System;
using WF.EAI.BLL.BO.CUSPSearch.Helper;

using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Model;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Utils;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    using WF.EAI.Model.ViewModels.UFA;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// AppIdSearchBo class
    /// </summary>
	public class AppIdSearchBo : SearchBo
	{
        /// <summary>
        /// AppIdSearchBo Constructor
        /// </summary>
		public AppIdSearchBo()
		{
			RetailerviewName = Model.Lookup.RetailerSearchViewName.AppIdSearch;
		}

		/// <summary>
		/// Invoke
		/// </summary>
		/// <param name="request">RequestDto<CUSPRequestHeader></param>
		/// <returns>ViewModel</returns>
		public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref ApplicationDataModel dataModel)
		{
			AppDataHeader appHeader = SearchHelper.GetModelObject(request);
			SearchApplicationResultsModel model = null;

			model = SearchHelper.GetModelObject(SearchCASApplicationByAppId(appHeader));
			return model;
		}
		/// <summary>
		/// SearchCASApplicationByAppId
		/// </summary>
		/// <param name="appHeader">AppDataHeader</param>
		/// <returns>SearchApplicationResultEntityCollection</returns>
		public SearchApplicationResultEntityCollection SearchCASApplicationByAppId(AppDataHeader appHeader)
		{
			SearchApplicationIDEntity entity = new SearchApplicationIDEntity() { ApplicationID = appHeader.ApplicationId, LineOfBusiness = "M" };
			appHeader.TabDataText = JsonConvert.SerializeObject(entity);
			string jsonText = UAP.UA.Global.BLL.SearchHelper.SearchCASApplicationByAppID(appHeader);
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

		public ViewModel GetViewModel(SearchApplicationResultsModel searchmodel)
		{
			ViewModel model = new ViewModel()
			{

			};

			return model;
		}
	}
}
