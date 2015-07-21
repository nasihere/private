using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Utils;

namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    using WF.EAI.BLL.CUSP;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// GetNextBo class
    /// </summary>
    public class GetNextBo : SearchBo
    {
        /// <summary>
        /// GetNextBo constructor
        /// </summary>
        public GetNextBo()
        {
            ViewName = Model.Lookup.CUSPSearchViewName.GetNext;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<SearchRequestHeader></param>
        /// <returns>dataModel</returns>
        public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
        {
            SearchApplicationResultsModel model = new SearchApplicationResultsModel();
            model = GetWorklistData(request);
            return model;
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

            appDataHeader.ApplicationData = "";
            string response = ACAPSAppDataServiceAdapter.GetUserProfile(appDataHeader);
            object jsonObject = JsonConvert.DeserializeObject(appDataHeader.ApplicationData);

            WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity userIDSearchEntity = WF.EAI.Utils.JsonMapper<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>.DeserializeList<WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity>(Convert.ToString(jsonObject));
            appDataHeader.WorkListLstate = QueuePriority(userIDSearchEntity);
            appDataHeader.AcapsFunction = QueuePriority(userIDSearchEntity);
            appDataHeader.WorkListLocation = "030101";
            appDataHeader.WorkListLstate = "U01U02U04U05";

            string appDataHeaderResponse = ACAPSAppDataServiceAdapter.GetNext(appDataHeader);

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
            }

            else
            {
                model.ApplicationId = "";
            }
            return model;
        }

        private string QueuePriority(WF.EAI.Entities.domain.c2c.UI.UserIDSearchEntity userIDSearchEntity)
        {
            string queueStates = string.Empty;
            for (int count = 1; count <= 5; count++)
            {
                if (userIDSearchEntity != null)
                {
                    if (userIDSearchEntity.OrderQueue1 == count)
                    {
                        queueStates = string.Concat(queueStates, userIDSearchEntity.Queue1);
                    }
                    else if (userIDSearchEntity.OrderQueue2 == count)
                    {
                        queueStates = string.Concat(queueStates, userIDSearchEntity.Queue2);
                    }
                    else if (userIDSearchEntity.OrderQueue3 == count)
                    {
                        queueStates = string.Concat(queueStates, userIDSearchEntity.Queue3);
                    }
                    else if (userIDSearchEntity.OrderQueue4 == count)
                    {
                        queueStates = string.Concat(queueStates, userIDSearchEntity.Queue4);
                    }
                    else if (userIDSearchEntity.OrderQueue5 == count)
                    {
                        queueStates = string.Concat(queueStates, userIDSearchEntity.Queue5);
                    }
                }
            }

            return queueStates;
        }
    }
}
