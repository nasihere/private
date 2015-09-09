using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDatabase;
using WebsiteShifa.Models;

namespace WebsiteShifa.Controllers
{
    public class RepertoryController : Controller
    {
        //
        // GET: /Repertory/
        private static RepertoryServiceController ApiController = new RepertoryServiceController();
        public ActionResult Kent()
        {
            return View(ApiController.Get());
        }
        
        public ActionResult Repertory()
        {
            string SearchTerm = (Request.QueryString["Search"] == null) ? Request.QueryString["Rubric"] : Request.QueryString["Search"];
            if (String.IsNullOrWhiteSpace(SearchTerm)) 
                return RedirectToAction("Kent");
            return View("Kent",ApiController.Post(SearchTerm));
        }
        public ActionResult Rubric()
        {
            string SearchTerm = (Request.QueryString["Search"] == null) ? Request.QueryString["Rubric"] : Request.QueryString["Search"];
            if (String.IsNullOrWhiteSpace(SearchTerm))
                return RedirectToAction("Kent");
            return View("Kent", ApiController.Rubric(SearchTerm));
        }

    }
}
