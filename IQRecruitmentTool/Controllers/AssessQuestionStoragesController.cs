using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;

namespace IQRecruitmentTool.Controllers
{
    public class AssessQuestionStoragesController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: AssessQuestionStorages
        public ActionResult Index(String SectionName , int? SectionID)

        {
            ViewBag.QuestionTypeDropDown = new SelectList(db.AssesQuestionType, "QuestionTypeID", "QuestionType");
            ViewBag.SectionID = SectionID;
            ViewBag.SectioName = SectionName;
            return View();
        }

        // GET: AssessQuestionStorages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessQuestionStorage assessQuestionStorage = db.AssessQuestionStorage.Find(id);
            if (assessQuestionStorage == null)
            {
                return HttpNotFound();
            }
            return View(assessQuestionStorage);
        }

        // GET: AssessQuestionStorages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssessQuestionStorages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,SectionID,QuestionTypeID,Question")] AssessQuestionStorage assessQuestionStorage)
        {
            if (ModelState.IsValid)
            {
                db.AssessQuestionStorage.Add(assessQuestionStorage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assessQuestionStorage);
        }

        // GET: AssessQuestionStorages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessQuestionStorage assessQuestionStorage = db.AssessQuestionStorage.Find(id);
            if (assessQuestionStorage == null)
            {
                return HttpNotFound();
            }
            return View(assessQuestionStorage);
        }

        // POST: AssessQuestionStorages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,SectionID,QuestionTypeID,Question")] AssessQuestionStorage assessQuestionStorage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessQuestionStorage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessQuestionStorage);
        }

        // GET: AssessQuestionStorages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessQuestionStorage assessQuestionStorage = db.AssessQuestionStorage.Find(id);
            if (assessQuestionStorage == null)
            {
                return HttpNotFound();
            }
            return View(assessQuestionStorage);
        }

        // POST: AssessQuestionStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssessQuestionStorage assessQuestionStorage = db.AssessQuestionStorage.Find(id);
            db.AssessQuestionStorage.Remove(assessQuestionStorage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
