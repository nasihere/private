using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DieChart.Entity;
namespace DieChart.Controllers

{
    public class DieChartController : Controller
    {
        // GET: DieChart
        private DataAccessLayer Adonet = new DataAccessLayer();
        public ActionResult Index()
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            var obj =  Adonet.GetDieEntityADONET();
            return View(obj);
            
        }
        [HttpGet]
        public ActionResult Sync()
        {
            return Json(new { foo = "bar", baz = "Blech" });
            //return Adonet.GetDieEntityADONET();
            

        }
        public ActionResult Search(FormCollection form)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            var obj = Adonet.SearchDieEntity(form["search"]);
            return View("Index", obj);

        }
        // GET: DieChart/Details/5
        public ActionResult Details(int id)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            var obj = Adonet.EditDieEntity(id);
            return View(obj);
        }

        // GET: DieChart/Create
        public ActionResult Create()
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        // POST: DieChart/Create
        [HttpPost]
        public ActionResult Create(DieEntity model, FormCollection collection)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            try
            {
                // TODO: Add insert logic here
                Adonet.PutDieEntity(model);
                return RedirectToAction("Details", new { id = model.id });
            }
            catch
            {
                return View();
            }
        }

        // GET: DieChart/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            var obj = Adonet.EditDieEntity(id);
            return View(obj);
        }

        // POST: DieChart/Edit/5
        [HttpPost]
        public ActionResult Edit(DieEntity model, FormCollection collection)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            try
            {
                // TODO: Add update logic here
                var obj = Adonet.SaveDieEntity(model);
                return RedirectToAction("Details", new { id = model.id });
            }
            catch
            {
                return View();
            }
        }

        // GET: DieChart/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            Adonet.DeleteDieEnity(id);
            return RedirectToAction("Index");
        }

        // POST: DieChart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["LoginValid"] == null) return RedirectToAction("Login", "Account");
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
