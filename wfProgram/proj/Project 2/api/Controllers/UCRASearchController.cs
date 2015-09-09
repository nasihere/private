using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using xxxxProjec.....xxxxProjec.......Interface;
using ucaCommon = WF.EAI.Entities.domain.uca.Common;


namespace xxxxProjec.....xxxxProjec.....WebApi.Controllers
{
    [RoutePrefix("xxxxProjec....Search")]
    public class xxxxProjec....SearchController : ApiController
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
        public IxxxxProjec....SearchBo xxxxProjec....SearchBo
        {
            set;
            get;
        }
        [Route("SearchCASApplicationByName")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByName(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplicationByName(appHeader));
         }

         [Route("SearchCASApplicationByPhone")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByPhone(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplicationByPhone(appHeader));
         }


         [Route("GetCustomerDetails")]
         [HttpPost]
         public IHttpActionResult GetCustomerDetails(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.GetCustomerDetails(appHeader));
         }

         [Route("SearchCASApplicationByAppID")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByAppID(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplicationByAppID(appHeader));
         }

         [Route("SearchCASApplicationByFDRAccountNumber")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationByFDRAccountNumber(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplicationByFDRAccountNumber(appHeader));
         }

         [Route("SearchCASApplicationBySSNClientCAMS")]
         [HttpPost]
         public IHttpActionResult SearchCASApplicationBySSNClientCAMS(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplicationBySSNClientCAMS(appHeader));
         }

         [Route("SearchCASApplication")]
         [HttpPost]
         public IHttpActionResult SearchCASApplication(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchCASApplication(appHeader));
         }


         [Route("SearchPartyData")]
         [HttpPost]
         public IHttpActionResult SearchPartyData(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchPartyData(appHeader));
         }

         [Route("SearchSavedApplication")]
         [HttpPost]
         public IHttpActionResult SearchSavedApplication(ucaCommon.AppDataHeader appHeader)
         {
             return Ok<string>(xxxxProjec....SearchBo.SearchSavedApplication(appHeader));
         }


         [Route("SearchESignature/{acctNum}/{sessionId}")]
         [HttpGet]
         public WF.EAI.Entities.domain.uca.Common.AppDataHeader SearchESignature(string acctNum, string sessionId)
         {
             return WF.EAI...SearchBO.RetailerEsignatureSearch(acctNum, sessionId);
         }

 
    }
}
