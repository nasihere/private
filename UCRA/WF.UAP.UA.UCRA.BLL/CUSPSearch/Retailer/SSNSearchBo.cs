using Newtonsoft.Json;
using System;
using WF.EAI.BLL.BO.CUSPSearch.Helper;
using WF.EAI.BLL.uca;
using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Utils;
using WF.UAP.UASF.App.Host.WebApi.Contract;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    /// <summary>
    /// SSNSearchBo class
    /// </summary>
    public class SSNSearchBo : SearchBo
	{
        /// <summary>
        /// SSNSearchBo Constructor
        /// </summary>
		public SSNSearchBo()
		{
			RetailerviewName = Model.Lookup.RetailerSearchViewName.SSNSearch;
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

			model = SearchHelper.GetModelObject(SearchCASApplicationBySSNNo(appHeader, request));
			return model;
		}
		/// <summary>
		/// SearchCASApplicationBySSNNo
		/// </summary>
		/// <param name="appHeader"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public SearchApplicationResultEntityCollection SearchCASApplicationBySSNNo(AppDataHeader appHeader, RequestDto<SearchRequestHeader> request)
		{
			SearchApplicationSSNCamsEntity entity = new SearchApplicationSSNCamsEntity() {Location = request.RequestHeader.Location, ClientNumber = request.RequestHeader.ClientNumber ,  SSN = request.RequestHeader.SSNNo,  CAMSApplicationID = request.RequestHeader.ApplicationId,LineOfBusiness = "M" };
			appHeader.TabDataText = JsonConvert.SerializeObject(entity);
			string jsonText = CommonBo.SearchCASApplicationBySSNClientCAMS(appHeader);
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
