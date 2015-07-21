using Newtonsoft.Json;
using System;
using WF.EAI.BLL.BO.CUSPSearch.Helper;
using WF.EAI.BLL.uca;
using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Model;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Utils;
using WF.UAP.UASF.App.Host.WebApi.Contract;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    /// <summary>
    /// PhoneNoSearchBo class
    /// </summary>
	public class PhoneNoSearchBo : SearchBo
	{
        /// <summary>
        /// PhoneNoSearchBo Constructor
        /// </summary>
		public PhoneNoSearchBo()
		{
			RetailerviewName = Model.Lookup.RetailerSearchViewName.PhoneNoSearch;
		}

		/// <summary>
		/// Invoke
		/// </summary>
		/// <param name="request">RequestDto<SearchRequestHeader></param>
		/// <returns>dataModel</returns>
		public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
		{
			AppDataHeader appHeader = SearchHelper.GetModelObject(request);
			SearchApplicationResultsModel model = null;

			model = SearchHelper.GetModelObject(SearchCASApplicationByPhoneNo(appHeader, request));
			return model;
		}
		/// <summary>
		/// SearchCASApplicationByPhoneNo
		/// </summary>
		/// <param name="appHeader"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public SearchApplicationResultEntityCollection SearchCASApplicationByPhoneNo(AppDataHeader appHeader, RequestDto<SearchRequestHeader> request)
		{
			SearchApplicationPhoneEntity entity = new SearchApplicationPhoneEntity() { ApplicantPhone = request.RequestHeader.ApplicantPhoneNo, LineOfBusiness = "M" };
			appHeader.TabDataText = JsonConvert.SerializeObject(entity);
			string jsonText = CommonBo.SearchCASApplicationByPhone(appHeader);
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

		public WF.UAP.UASF.App.Host.WebApi.Contract.ViewModel GetViewModel(SearchApplicationResultsModel searchmodel)
		{
			WF.UAP.UASF.App.Host.WebApi.Contract.ViewModel model = new WF.UAP.UASF.App.Host.WebApi.Contract.ViewModel()
			{

			};

			return model;
		}

	}
}
