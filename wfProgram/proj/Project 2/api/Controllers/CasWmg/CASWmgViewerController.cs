using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xxxxProjec.....Global..xxxxProjec.....BO;
using WF.UAP.UDB.Repository.Domain.Entities.xxxxProjec.....CasWmg.Viewer.Request;
using WF.UAP.UDB.Repository.Domain.Entities.xxxxProjec.....CasWmg.Viewer.Response;

namespace xxxxProjec.....xxxxProjec.....WebApi.Controllers.CasWmg
{
    [RoutePrefix("Viewer")]
    public class CASWmgViewerController : ApiController
    {
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return "Welcome to WebAPI - Viewer Controller";
        }

        //[Authorize]
        [Route("GetBankerProfiles")]
        [HttpPost]
        public HttpResponseMessage GetBankerProfiles(BankerProfileEntity bankerProfileEntity)
        {
            //// Fetch the records from [CreditApp].[Wre].[RateExceptionApprover]
            var bankerProfiles = RateExViewerBO.GetBankerProfiles(bankerProfileEntity);
            return Response(bankerProfiles);
        }

        [Route("GetWmgLogs")]
        [HttpPost]
        public HttpResponseMessage GetWmgLogs(TransactionLogEntity transactionLogEntity)
        {
            //// Fetch the records from [CreditApp].[Wre].[RateExceptionRequest]
            var transactionLogs = RateExViewerBO.GetWmgLogs(transactionLogEntity);
            return Response(transactionLogs);
        }

        [Route("GetAcapsCwsMessages")]
        [HttpPost]
        public HttpResponseMessage GetAcapsCwsMessages(AppSubmissionLogEntity appSubmissionLogEntity)
        {
            //// Fetch the records from [DotNetViewerLogging].[dbo].[LogUSWAppSubmission]
            var appSubmissionLogs = RateExViewerBO.GetAcapsCwsMessages(appSubmissionLogEntity);
            return Response(appSubmissionLogs);
        }

        // Method to generate response
        private HttpResponseMessage Response(ViewerResponse viewerResponse)
        {
            HttpResponseMessage response;
            if (viewerResponse.ErrorMessages != null)
            {
                var firstOrDefault = viewerResponse.ErrorMessages.FirstOrDefault();
                var errorResponse = firstOrDefault != null
                    ? new HttpError(firstOrDefault.Message)
                    : new HttpError("Uncaught Error");
                response = Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, viewerResponse);
            }

            return response;
        }
    }
}