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


namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    using WF.EAI.BLL.CUSP;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// WorkListSearchBo class
    /// </summary>
    public class WorkListSearchBo : SearchBo
    {
        /// <summary>
        ///     The app data.
        /// </summary>
        protected WF.EAI.Entities.domain.cusp.Core.CUSPApplicationData AppData;

        /// <summary>
        /// WorkListSearchBo Constructor
        /// </summary>
        public WorkListSearchBo()
        {
            RetailerviewName = Model.Lookup.RetailerSearchViewName.WorkListSearch;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<SearchRequestHeader></param>
        /// <returns>dataModel</returns>
        public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
        {
            List<WorkListSearchResultModel> WorkListSearchResultModelLst = new List<Model.ViewModels.CUSPSearch.Retailer.WorkListSearchResultModel>();
            SearchApplicationResultsModel searchApplicationResultModel = new Model.ViewModels.CUSPSearch.SearchApplicationResultsModel();
            searchApplicationResultModel.WorkListSearchResultModel = new List<WorkListSearchResultModel>();

            //TODO : Need to integrate with Retailer application 
            if (request.RequestHeader.WorkList == "true")
            {
                searchApplicationResultModel = GetWorklistData(request);
            }
            else if (request.RequestHeader.Workstatistics == "true")
            {
                searchApplicationResultModel.WorkListSearchResultModel = GetWorkListStatistics(request);
            }

            return searchApplicationResultModel;
    
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">CUSPAppDataHeader<appHeader></param>
        /// <returns>string</returns>
        public string GetACAPSData(CUSPAppDataHeader appHeader)
        {
            return ACAPSAppDataServiceAdapter.GetACAPSData(appHeader);
        }

        private SearchApplicationResultsModel GetWorklistData(RequestDto<SearchRequestHeader> request)
        {

            SearchApplicationResultsModel model = new SearchApplicationResultsModel();

            CUSPAppDataHeader appDataHeader = new CUSPAppDataHeader();
            appDataHeader.UserId = request.RequestHeader.UserId;
            appDataHeader.Password = request.RequestHeader.Password;
            appDataHeader.AcapsFunction = request.RequestHeader.AcapsFunction;
            appDataHeader.SessionId = request.RequestHeader.SessionId;
            appDataHeader.AcapsSessionId = request.RequestHeader.UserSessionId;

            //appDataHeader.ApplicationData = request.RequestHeader.UserId;
            //string response = ACAPSAppDataServiceAdapter.GetUserProfile(appDataHeader);
            //object jsonObject = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);

            //WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity userIDSearchEntity = WF.EAI.Utils.JsonMapper<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>.DeserializeList<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>(Convert.ToString(jsonObject));
            //appDataHeader.WorkListLocation = "030101";
            //appDataHeader.WorkListLstate = "U01U02U04U05";

            //string appDataHeaderResponse = ACAPSAppDataServiceAdapter.GetNext(appDataHeader);

            if (appDataHeader.ApplicationData != null)
            {
                var obj = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);
                var workListSearchEntityLst =
                   JsonMapper<List<WF.EAI.Entities.domain.cusp.Search.WorkAListSearchEntity>>.JsonTextToAppDataHeader<List<WF.EAI.Entities.domain.cusp.Search.WorkAListSearchEntity>>(
                        Convert.ToString(obj));
                if (workListSearchEntityLst.Count > 0)
                {
                    model.ApplicationId = workListSearchEntityLst[0].ApplicationId;

                }
                else
                {
                    model.ApplicationId = "20140782900012";
                }
            }

            else
            {
                model.ApplicationId = "20140782900012";
            }
            return model;
        }

        private List<WorkListSearchResultModel> GetWorkListStatistics(RequestDto<SearchRequestHeader> request)
        {
            List<WorkListSearchEntity> workListSearchEntityList = new List<WorkListSearchEntity>();

            WorkListSearchModel WorkListSearchModel = new WorkListSearchModel();
            WorkListSearchResultModel WorkListSearchResultModel = new WorkListSearchResultModel();
            List<WorkListSearchResultModel> WorkListSearchResultModelLst = new List<Model.ViewModels.CUSPSearch.Retailer.WorkListSearchResultModel>();

            CUSPAppDataHeader appDataHeader = new CUSPAppDataHeader();
            appDataHeader.UserId = request.RequestHeader.UserId;
            appDataHeader.Password = request.RequestHeader.Password;
            appDataHeader.AcapsFunction = request.RequestHeader.AcapsFunction;
            appDataHeader.SessionId = request.RequestHeader.SessionId;
            appDataHeader.AcapsSessionId = request.RequestHeader.UserSessionId;

            appDataHeader.ApplicationData = "";
            string response = ACAPSAppDataServiceAdapter.GetUserProfile(appDataHeader);
            object jsonObject = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);

            WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity userIDSearchEntity = WF.EAI.Utils.JsonMapper<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>.DeserializeList<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>(Convert.ToString(jsonObject));
            appDataHeader.WorkListLocation = "030101";
            appDataHeader.WorkListLstate = "U01U02U04U05";

            string appDataHeaderResponse = ACAPSAppDataServiceAdapter.GetNext(appDataHeader);

            if (appDataHeader.ApplicationData != null)
            {
                var obj = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);
                var workListSearchEntityLst =
                    JsonMapper<WorkListSearchEntity>.JsonTextToAppDataHeader<List<WorkListSearchEntity>>(
                               Convert.ToString(obj));
                if (workListSearchEntityLst != null)
                {
                    for (int i = 0; i < workListSearchEntityLst.Count; i++)
                    {
                        WorkListSearchResultModel.CarryOverCnt_value = workListSearchEntityLst[i].CarryOverCnt;
                        WorkListSearchResultModel.NewEntryCnt_value = workListSearchEntityLst[i].NewEntryCnt;
                        WorkListSearchResultModel.PendingCnt_value = workListSearchEntityLst[i].PendingCnt;
                        WorkListSearchResultModel.StateCode_value = workListSearchEntityLst[i].StateCode;
                        WorkListSearchResultModel.StateName_value = workListSearchEntityLst[i].StateName;
                        WorkListSearchResultModel.TotalListCnt_value = workListSearchEntityLst[i].TotalListCnt;
                        WorkListSearchResultModel.UserId_value = workListSearchEntityLst[i].UserId;
                        WorkListSearchResultModel.UserName_value = workListSearchEntityLst[i].UserName;
                        WorkListSearchResultModelLst.Add(WorkListSearchResultModel);
                    }
                }
            }
            return WorkListSearchResultModelLst;
        }

    }
}
