using System;
using System.Collections.Generic;

using WF.EAI.Entities.domain.MaturingTools;

namespace WF.EAI.BLL.BO.CUSPApps.MaturingTools.Interfaces
{
    public interface IMaturingToolsBo
    {
        MaturingToolsEntity GetMaturingToolsData(string accountNum, Guid? moGuid, string userId, string userType, string mloId, string agentName);

        MaturingToolsEntity SubmitMaturingToolsData(MaturingToolsEntity meEntity);

        MaturingToolsEntity CancelMaturingToolsData(MaturingToolsEntity meEntity);

        bool BounceBackMaturingToolsData(MaturingToolsEntity meEntity);

        List<MaturingToolsEntity> MaturingToolsDataList();

        string SendEmail(MaturingToolsEntity meEntity);

        void ElpsCall(string accountNum);

        bool ClearAssignToData(MaturingToolsEntity meEntity);
    }
}
