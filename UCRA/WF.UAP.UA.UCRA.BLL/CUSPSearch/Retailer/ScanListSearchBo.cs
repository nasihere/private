using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using WF.EAI.Entities.domain.c2c.UI;
using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch.Retailer;
using WF.EAI.Utils;
using WF.EAI.Model;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    using WF.EAI.BLL.CUSP;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// ScanListSearchBo class
    /// </summary>
    public class ScanListSearchBo : SearchBo
    {
        /// <summary>
        /// ScanListSearchBo Constructor
        /// </summary>
        public ScanListSearchBo()
        {
            RetailerviewName = Model.Lookup.RetailerSearchViewName.ScanListSearch;
        }

         /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<SearchRequestHeader></param>
        /// <returns>dataModel</returns>
        public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
        {
            List<ScanListSearchResultsModel> ScanListSearchResultsModelList = new List<Model.ViewModels.CUSPSearch.Retailer.ScanListSearchResultsModel>();
            ScanListSearchResultsModel ScanListSearchResultsModel = new Model.ViewModels.CUSPSearch.Retailer.ScanListSearchResultsModel();
            SearchApplicationResultsModel searchApplicationResultModel = new Model.ViewModels.CUSPSearch.SearchApplicationResultsModel();
            searchApplicationResultModel.ScanListSearchResultsModel = new List<ScanListSearchResultsModel>();

            CUSPAppDataHeader appDataHeader = new CUSPAppDataHeader();
            appDataHeader.UserId = request.RequestHeader.UserId;
            appDataHeader.Password = request.RequestHeader.Password;
            appDataHeader.AcapsFunction = request.RequestHeader.AcapsFunction;
            appDataHeader.SessionId = request.RequestHeader.SessionId;
            appDataHeader.AcapsSessionId = request.RequestHeader.UserSessionId;
            appDataHeader.ApplicationData = request.RequestHeader.UserId;
            string response = ACAPSAppDataServiceAdapter.GetUserProfile(appDataHeader);
            object jsonObject = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);

            WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity userIDSearchEntity = WF.EAI.Utils.JsonMapper<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>.DeserializeList<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>(Convert.ToString(jsonObject));
            appDataHeader.WorkListLocation = "030101";
            appDataHeader.WorkListLstate = "U01U02U04U05";
            appDataHeader.WorklistUserid = request.RequestHeader.UserId;

            string appDataHeaderResponse  = ACAPSAppDataServiceAdapter.GetScanList(appDataHeader);
            var obj = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);
            var scanListSearchEntityList =
                JsonMapper<List<ScanListSearchEntity>>.JsonTextToAppDataHeader<List<ScanListSearchEntity>>(
                    Convert.ToString(obj));
            if (scanListSearchEntityList != null)
            {
                for (int i = 0; i < scanListSearchEntityList.Count; i++)
                {
                    ScanListSearchResultsModel.Amount_value = scanListSearchEntityList[i].Amount;
                    ScanListSearchResultsModel.ApplicationId_value = scanListSearchEntityList[i].ApplicationId;
                    ScanListSearchResultsModel.BankerNoteIndicator_value = scanListSearchEntityList[i].BankerNoteIndicator;
                    ScanListSearchResultsModel.CustomerInStore_value = scanListSearchEntityList[i].CustomerInStore;
                    ScanListSearchResultsModel.HoldDate_value = scanListSearchEntityList[i].HoldDate;
                    ScanListSearchResultsModel.LastName_value = scanListSearchEntityList[i].LastName;
                    ScanListSearchResultsModel.LocationCode_value = scanListSearchEntityList[i].LocationCode;
                    ScanListSearchResultsModel.Product_value = scanListSearchEntityList[i].Product;
                    ScanListSearchResultsModel.StateEntryDate_value = scanListSearchEntityList[i].StateEntryDate;
                    ScanListSearchResultsModel.SystemEntryDate_value = scanListSearchEntityList[i].SystemEntryDate;
                    ScanListSearchResultsModel.TimeSubmitted_value = scanListSearchEntityList[i].TimeSubmitted;
                    ScanListSearchResultsModelList.Add(ScanListSearchResultsModel);
                }
            }
            searchApplicationResultModel.ScanListSearchResultsModel = ScanListSearchResultsModelList;
            var errorMessage = JsonConvert.DeserializeObject<List<ErrorMessage>>(appDataHeader.AcapsErrorMessages);
            searchApplicationResultModel.errorMessages = errorMessage;
            return searchApplicationResultModel;
        }
    }
}
