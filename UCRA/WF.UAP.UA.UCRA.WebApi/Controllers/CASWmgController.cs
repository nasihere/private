using System.Collections.Generic;
using System.Web.Http;
using WF.UAP.UA.Global.BLL.UCRA;
using WF.UAP.UDB.Repository.Domain.Entities.UCRA.cas;

namespace WF.UAP.UA.UCRA.WebApi.Controllers
{
    [RoutePrefix("CasRateException")]
    public class CASWmgController : ApiController
    {
        public string Get()
        {
            return "Welcome to WebAPI";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wmgReEntity"></param>
        /// <returns></returns>
        [Route("GetWmgRateException")]
        [HttpPost]
        public IHttpActionResult GetWmgRateException(WmgRateExceptionEntity wmgReEntity)
        {
            return Ok<WmgRateExceptionEntity>(RateExDbHelper.GetWmgRateException(wmgReEntity.ApplicationID, wmgReEntity.IsCurrent));
        }

        [Route("GetWmgRateExceptionByReqId/{reReqId}")]
        [HttpGet]
        public IHttpActionResult GetWmgRateExceptionByReqId(string reReqId)
        {
            return Ok<WmgRateExceptionEntity>(RateExDbHelper.GetWmgRateExceptionByReqId(reReqId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approverId"></param>
        /// <returns></returns>

        [Route("GetWmgRateExceptionListByApprover/{approverId}")]
        [HttpGet]
        public IHttpActionResult GetWmgRateExceptionListByApprover(int approverId)
        {
            return Ok<List<WmgRateExceptionEntity>>(RateExDbHelper.GetWmgRateExceptionListByApprover(approverId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approverId"></param>
        /// <returns></returns>

        [Route("GetWmgRateExceptionListByApprover")]
        [HttpPost]
        public IHttpActionResult GetWmgRateExceptionListByApprover(ApproverParm reqParam)
        {
            return Ok<List<WmgRateExceptionEntity>>(RateExDbHelper.GetWmgRateExceptionListByApprover(reqParam.AdEntId));
        }

        [Route("GetWmgReReason")]
        [HttpPost]
        public IHttpActionResult GetWmgReReason(WmgReReasonEntity wmgReReason)
        {
            return Ok<WmgReReasonEntity>(RateExDbHelper.GetWmgReReason(wmgReReason.ID.ToString()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wmgRestatus"></param>
        /// <returns></returns>
        [Route("GetWmgReStatus")]
        [HttpPost]
        public IHttpActionResult GetWmgReStatus(WmgReStatusEntity wmgRestatus)
        {
            return Ok<WmgReStatusEntity>(RateExDbHelper.GetWmgReStatus(wmgRestatus.ID.ToString()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wmgApr"></param>
        /// <returns></returns>
        [Route("GetWmgApproverInfo")]
        [HttpPost]
        public IHttpActionResult GetWmgApproverInfo(WmgApproverCriteriaEntity wmgApr)
        {
            return Ok<List<WmgApproverEntity>>(RateExDbHelper.GetWmgApproverInfo(
                                                            string.Join(",",wmgApr.AuthLevels)
                ));
        }
        [Route("GetWmgAprCcNoticeInfo")]
        [HttpPost]
        public IHttpActionResult GetWmgAprCcNoticeInfo(WmgApproverEntity wmgApr)
        {
            return Ok<List<WmgApproverEntity>>(RateExDbHelper.GetWmgCcNoticeInfo(wmgApr));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meEntity"></param>
        /// <returns></returns>
        [Route("SubmitReException")]
        [HttpPost]
        public IHttpActionResult SubmitReException(WmgRateExceptionEntity reEntity)
        {
            return Ok<WmgRateExceptionEntity>(RateExDbHelper.SubmitReException(reEntity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meEntity"></param>
        /// <returns></returns>
        [Route("UpdateReRequest")]
        [HttpPost]
        public IHttpActionResult UpdateReRequest(WmgRateExceptionEntity reEntity)
        {
            return Ok<WmgRateExceptionEntity>(RateExDbHelper.UpdateDeleteReRequest(reEntity));
        }
    }


}