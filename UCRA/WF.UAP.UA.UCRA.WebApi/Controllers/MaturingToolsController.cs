// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaturingToolsController.cs" company="">
//   
// </copyright>
// <summary>
//   The maturing tools controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.WebApi.Controllers
{
    #region

    using System.Web.Http;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    using WF.EAI.BLL.BO.CUSPApps.MaturingTools.Interfaces;
    using WF.EAI.Entities.domain.MaturingTools;

    #endregion

    [RoutePrefix("MaturingTools")]
    public class MaturingToolsController : ApiController
    {
        #region Dependancies
        [Dependency]
        public IMaturingToolsBo MaturingToolsBo
        {
            set;
            get;
        }
        #endregion

        [Route("MaturingToolsDataList")]
        [HttpGet]
        public IHttpActionResult MaturingToolsDataList()
        {
            return Ok<List<MaturingToolsEntity>>(MaturingToolsBo.MaturingToolsDataList());
        }

        [Route("GetMaturingOptionsData")]
        [HttpPost]
        public IHttpActionResult GetMaturingOptionsData(MaturingToolsEntity meEntity)
        {
            return Ok<MaturingToolsEntity>(MaturingToolsBo.GetMaturingToolsData(meEntity.AccountNum, meEntity.MOGUID, meEntity.CreatedBy, meEntity.UserType, meEntity.MNLSRID, meEntity.AgentName));
        }

        [Route("SubmitMaturingToolsData")]
        [HttpPost]
        public IHttpActionResult SubmitMaturingToolsData(MaturingToolsEntity meEntity)
        {
            return Ok<MaturingToolsEntity>(MaturingToolsBo.SubmitMaturingToolsData(meEntity));
        }

        [Route("CancelMaturingToolsData")]
        [HttpPost]
        public IHttpActionResult CancelMaturingToolsData(MaturingToolsEntity meEntity)
        {
            return Ok<MaturingToolsEntity>(MaturingToolsBo.CancelMaturingToolsData(meEntity));
        }

        [Route("BounceBackMaturingToolsData")]
        [HttpPost]
        public IHttpActionResult BounceBackMaturingToolsData(MaturingToolsEntity meEntity)
        {
            return Ok<bool>(MaturingToolsBo.BounceBackMaturingToolsData(meEntity));
        }

        [Route("ClearAssignToData")]
        [HttpPost]
        public IHttpActionResult ClearAssignToData(MaturingToolsEntity meEntity)
        {
            return Ok<bool>(MaturingToolsBo.ClearAssignToData(meEntity));
        }
    }
}