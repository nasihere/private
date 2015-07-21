using System;
using System.Collections.Generic;
using System.Linq;

using WF.EAI.Entities.domain.cusp.UI;
using WF.EAI.Utils;

using System.Data.Objects;

using WF.EAI.Entities.domain.cusp.Search;

namespace WF.EAI.BLL.CUSP
{
    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.UAP.UDB.Domain.ORM.dal;
    using WF.UAP.UDB.Repository.Transform.dal.UPortal.CUSP;
    //using Common.Logging;

    public class PipelineViewerBO
    {

        public string GetPipelineViewer()
        {
            string jsonText = string.Empty;

            PipelineViewerEntityCollection pipeLineUserList = new PipelineViewerEntityCollection();
            try
            {
                var bankLoanUsersBo = new BankLoanBookingFetchDetailsBo();
                pipeLineUserList = GetPipeLineViewerCount();

                // Map Initial data  to entities                
                jsonText = JsonMapper<string>.ObjectToJsonText(pipeLineUserList);
            }
            catch (Exception ex)
            {
                ////Logger.Instance.Error("Bank Loan Work Flow Utility: ", ex);
                throw new Exception();
            }

            return jsonText;
        }



        public PipelineViewerEntityCollection GetPipeLineViewerCount()
        {
            try
            {
                using (var db = ContextClass.CreateCUSPObjectContext())
                {
                    PipelineViewerEntityCollection pipeLineCount;

                    ObjectResult<GetPipeLineUserCount_Result> result = db.GetPipeLineUserCount();

                    pipeLineCount = new PipelineViewerEntityCollection();
                    pipeLineCount.GetPipeLineViewerUserCount = new List<PipeLineEntity>();

                    var enumBlbUsers = result.AsEnumerable();
                    foreach (GetPipeLineUserCount_Result userCount in enumBlbUsers)
                    {
                        var temp = new PipeLineEntity();
                        temp.TotalUserCnt = userCount.UserCount;
                        temp.QueueName = userCount.Queue;
                        temp.PriorityCount = Convert.ToInt32(userCount.PriorityCount);
                        pipeLineCount.GetPipeLineViewerUserCount.Add(temp);
                    }

                    //Logger.Instance.Info(
                    // "No. of users count with queue  = " + pipeLineCount.GetPipeLineViewerUserCount.Count);
                    return pipeLineCount;
                }
            }
            catch (Exception ex)
            {
                ////Logger.Instance.Error("Pipeline viewer utility: ", ex);
                throw new Exception();
            }
        }
    }
}
