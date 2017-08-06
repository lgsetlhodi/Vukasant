using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace IQRecruitmentTool.Controllers
{
    public class LanguagesController : Controller
    {
        // GET: Languages
        public ActionResult Index()
        {
            return View();
        }

        // GET: Languages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Languages/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Languages/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
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
