using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WF.UAP.UA.UAPortal.BLL.BO;
using WF.UAP.UA.UAPortal.WebAPI.CustomFilters;
using WF.UAP.UASF.CrossCutting.ConfigMgmt.Processors;
using WF.UAP.UDB.Repository.Domain.Entities.UAPortal;
using WF.UAP.UDB.Repository.Domain.Entities.UAPortal.Account.Response;


namespace WF.UAP.UA.UAPortal.WebAPI.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return "Welcome to WebAPI";
        }

        [Route("Register")]
        [HttpPost]
        public HttpResponseMessage Register(AccountModel model)
        {
            // Registers a new user and sends an email notificatoin on success to his/her manager's email.
            var acctModel = UAPortalAccountBO.RegisterAccountModel(model);
            return Response(acctModel);
        }



        [Route("Approve/{guid}/{roleids}")]
        [HttpGet]
        public IHttpActionResult Approve(string guid, string roleids)
        {
            var returnResult = "Not Approved";
            if (guid == null) return Ok(returnResult);

            var guidOnly = guid.Substring(guid.LastIndexOf('=') + 1);
            var roleidsOnly = roleids.Substring(roleids.LastIndexOf('=') + 1);
            // Approve: Enable User, and send notification to AppAdmin via email.
            returnResult = UAPortalAccountBO.ApproveAccountModel(guidOnly, roleidsOnly);
            return Ok(returnResult);
        }

        [Authorize]
        [Route("GetAccountModules")]
        [HttpPost]
        public HttpResponseMessage GetAccountModules(AccountModel model)
        {
            var acctModel = UAPortalAccountBO.GetAcccountModel(model);
            return Response(acctModel);
        }

        [Authorize]
        [Route("SaveAccountRoles")]
        [HttpPost]
        public IHttpActionResult SaveAccountRoles(AccountModel model)
        {
            // Saves Account Roles.
            var isSuccess = UAPortalAccountBO.SaveAccountRoles(model);
            var response = (isSuccess) ? new HttpResponseMessage(HttpStatusCode.Created) : new HttpResponseMessage(HttpStatusCode.BadRequest);
            return Ok(response);
        }

        [Authorize]
        [Route("SaveAccount")]
        [HttpPost]
        public IHttpActionResult SaveAccount(AccountModel model)
        {
            // Saves Account.
            var isSuccess = UAPortalAccountBO.SaveAccount(model);
            var response = (isSuccess) ? new HttpResponseMessage(HttpStatusCode.Created) : new HttpResponseMessage(HttpStatusCode.BadRequest);
            return Ok(response);
        }

        [CacheClient(Duration = 120)]
        [Route("GetRoles")]
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            var rolesList = UAPortalAccountBO.GetRoles();
            return Ok(rolesList);
        }

        [Authorize]
        [Route("GetModules")]
        [HttpGet]
        public IHttpActionResult GetModules()
        {
            var rolesList = UAPortalAccountBO.GetRoles();
            return Ok("200");
        }

        [Authorize]
        [Route("UserList")]
        [HttpPost]
        public IHttpActionResult UserList(AccountModel model)
        {
            var usersList = UAPortalAccountBO.GetUsersList(model);
            return Ok(usersList);
        }


        // Method to generate response
        private HttpResponseMessage Response(AccountResponseModel acctModel)
        {
            HttpResponseMessage response;
            if (acctModel.ErrorMessages != null)
            {
                var firstOrDefault = acctModel.ErrorMessages.FirstOrDefault();
                var errorResponse = firstOrDefault != null
                    ? new HttpError(firstOrDefault.Message)
                    : new HttpError("Uncaught Error");
                response = Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, acctModel);
            }

            return response;
        }
    }
}