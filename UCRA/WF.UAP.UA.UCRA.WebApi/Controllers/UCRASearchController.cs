using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using WF.UAP.UA.UCRA.BLL.CUSP.Interface;
using ucaCommon = WF.EAI.Entities.domain.uca.Common;


namespace WF.UAP.UA.UCRA.WebApi.Controllers
{
    [RoutePrefix("UCRASearch")]
    public class UCRASearchController : ApiController
    {

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Get()
        {
            return "Welcome to WebAPI";
        }

         [Dependency]
        public IUCRASearchBo UCRASearchBo
        {
            set;
            get;
        }
        [Route("SearchCASApplicationByName")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByName(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplicationByName(appHeader));
         }

         [Route("SearchCASApplicationByPhone")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByPhone(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplicationByPhone(appHeader));
         }


         [Route("GetCustomerDetails")]
         [HttpPost]
         public IHttpActionResult GetCustomerDetails(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.GetCustomerDetails(appHeader));
         }

         [Route("SearchCASApplicationByAppID")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByAppID(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplicationByAppID(appHeader));
         }

         [Route("SearchCASApplicationByFDRAccountNumber")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByFDRAccountNumber(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplicationByFDRAccountNumber(appHeader));
         }

         [Route("SearchCASApplicationBySSNClientCAMS")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationBySSNClientCAMS(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplicationBySSNClientCAMS(appHeader));
         }

         [Route("SearchCASApplication")]
         [HttpPost]
         public IHttpActionResult SearchCASApplication(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchCASApplication(appHeader));
         }


         [Route("SearchPartyData")]
         [HttpPost]
         public IHttpActionResult SearchPartyData(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchPartyData(appHeader));
         }

         [Route("SearchSavedApplication")]
         [HttpPost]
         public IHttpActionResult SearchSavedApplication(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(UCRASearchBo.SearchSavedApplication(appHeader));
         }


         [Route("SearchESignature/{acctNum}/{sessionId}")]
         [HttpGet]
         public WF.EAI.Entities.domain.uca.Common.AppDataHeader SearchESignature(string acctNum, string sessionId)
         {
             return WF.EAI.BLL.CUSP.SearchBO.RetailerEsignatureSearch(acctNum, sessionId);
         }

 
    }
}
