using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace IQRecruitmentTool.Controllers
{
    public class AssesSectionController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: AssesSections
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemoGen(int AssesmentID= 1153)
        {
            String UserID = User.Identity.GetUserId();
            String AssessmentName = db.Assessments.Where(x => x.AssessmentID == AssesmentID).SingleOrDefault().AssementName.ToString();
            //String Name = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).SingleOrDefault().Name.ToString();
            //String Surname = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).SingleOrDefault().Surname.ToString();
            var SectionIDs = db.AssesSection.Where(x => x.AssesmentID == AssesmentID).Select(x => x.SectionID);
            //ViewBag.Name = Name;
            //ViewBag.Surname = Surname;
            List<object> MemoGen = new List<object>();
            MemoGen.Add(db.AssesSection.Where(x => x.AssesmentID == AssesmentID).ToList());
            MemoGen.Add(db.AssessQuestionStorage.ToList());
            MemoGen.Add(db.AssessMultipleQuestion.ToList());
            MemoGen.Add(db.AssessAnswers.ToList());


            return View(MemoGen);
        }

        public ActionResult AssessmentEdit(int? SectionID)
        {
            int AssessmentID = db.AssesSection.Where(x => x.SectionID == SectionID).SingleOrDefault().AssesmentID.Value;
            ViewBag.AssessmentID = AssessmentID;
            ViewBag.AssessmentName = db.Assessments.Where(x => x.AssessmentID == AssessmentID).SingleOrDefault().AssementName.ToString();

            return View();
            
        }

        public ActionResult MultipleChoice(int? QuestionID)
        {
            ViewBag.SectionID = db.AssessQuestionStorage.Where(x => x.QuestionID == QuestionID).SingleOrDefault().SectionID.Value;
            ViewBag.QuestionID = QuestionID;
            ViewBag.MultichoiceOptions = db.AssessQuestionStorage.Where(x => x.QuestionID == QuestionID).SingleOrDefault().Question.ToString();
            return View();
        }

        
        public ActionResult GetmultiQuestions(int? QuestionID)
        {
            var QuestionsMultilist = db.AssessMultipleQuestion.Where(x => x.QuestionID == QuestionID).ToList();
            return Json(QuestionsMultilist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuestions(int? SectionsID)
        {
            var Questionslist = db.AssessQuestionStorage.Where(x=> x.SectionID== SectionsID).ToList();
            return Json(Questionslist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSections(int? AssesmentID)
        {
            if (AssesmentID == null)
            {
                var SectionsList = "";
                 return Json(SectionsList, JsonRequestBehavior.AllowGet);
            }
            else
            { 
            var SectionsList = db.AssesSection.Where(x=> x.AssesmentID==AssesmentID).ToList();
                return Json(SectionsList, JsonRequestBehavior.AllowGet);
            }
           
        }

        public PartialViewResult All()
        {

            List<AssesSection> model = db.AssesSection.ToList();
            return PartialView("_SectionAssess", model);
        }
        // GET: AssesSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssesSection assesSection = db.AssesSection.Find(id);
            if (assesSection == null)
            {
                return HttpNotFound();
            }
            return View(assesSection);
        }

        // GET: AssesSections/Create
        public ActionResult Create()
        {

            List<AssesSection> model = db.AssesSection.ToList();
            ViewBag.Sections = model;

            return View();
        }

        // POST: AssesSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(AssesSection model)
        {
            if (ModelState.IsValid)
            {
                db.AssesSection.Add(model);
                db.SaveChanges();
                return PartialView("_SectionAssess", model);
            }

            return PartialView("_SectionAssess",model);
        }
        public ActionResult Questions(int? SectionID)
        {
            String SectionName = db.AssesSection.Where(x => x.SectionID == SectionID).SingleOrDefault().SectionName.ToString();
            ViewBag.QuestionTypeDropDown = new SelectList(db.AssesQuestionType, "QuestionTypeID", "QuestionType");
            ViewBag.SectionID = SectionID;
            ViewBag.SectionName = SectionName;
            return View();

        }
        public ActionResult UpdateAssesmentName( Assessments Assessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Assessment).State = EntityState.Modified;
                db.SaveChanges();
                return Json(Assessment, JsonRequestBehavior.AllowGet);
            }
            return Json(Assessment, JsonRequestBehavior.AllowGet);
        }


        // GET: AssesSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssesSection assesSection = db.AssesSection.Find(id);
            if (assesSection == null)
            {
                return HttpNotFound();
            }
            return View(assesSection);
        }

        // POST: AssesSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,SectionNr,SectionName,JobID,ComapnyID,UserID")] AssesSection assesSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assesSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assesSection);
        }


        
               public ActionResult SaveMultiQuestions(AssessMultipleQuestion AssessMultipleQuestion)
        {

            AssessMultipleQuestion Sect = new AssessMultipleQuestion();

            Sect.QuestionID = AssessMultipleQuestion.QuestionID;
         
            Sect.MultipleChoiceAnswers = AssessMultipleQuestion.MultipleChoiceAnswers;
            db.AssessMultipleQuestion.Add(Sect);
            db.SaveChanges();


            return Json(AssessMultipleQuestion, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveQuestions(AssessQuestionStorage AssessQuestionStorage)
        {

            AssessQuestionStorage Sect = new AssessQuestionStorage();

            Sect.SectionID = AssessQuestionStorage.SectionID;
            Sect.Question = AssessQuestionStorage.Question;
            Sect.QuestionTypeID = AssessQuestionStorage.QuestionTypeID;
            db.AssessQuestionStorage.Add(Sect);
            db.SaveChanges();


            return Json(AssessQuestionStorage, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Savedata(AssesSection AssesSection)
        {

            AssesSection Sect = new AssesSection();
           
            Sect.SectionID = 34;
            Sect.UserID = "ae465423f2hgef2r3721r";         
            Sect.AssesmentID = AssesSection.AssesmentID;
            Sect.SectionName = AssesSection.SectionName;            
            db.AssesSection.Add(Sect);
            db.SaveChanges();
            

            return Json(AssesSection, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateAssesment(String AssementName)
        {

            Assessments Sect = new Assessments();
            Sect.CompanyID = 5;
            Sect.UserID = "ae465423f2hgef2r3721r";
            Sect.AssementName = AssementName;
            Sect.AssessmentID = 0;
            db.Assessments.Add(Sect);            
            db.SaveChanges();
            ViewBag.AssesmentID = Sect.AssessmentID;

            return Json(Sect.AssessmentID.ToString(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Deletedata(AssesSection AssesSection)
        {

            if (db.AssessQuestionStorage.Any(x=> x.SectionID== AssesSection.SectionID))
            {
                int umber = 1;

                return Json(umber.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                AssesSection assesSection = db.AssesSection.Find(AssesSection.SectionID);
                db.AssesSection.Remove(assesSection);
                db.SaveChanges();

                return Json(assesSection, JsonRequestBehavior.AllowGet);

            }
            
         

        }

        
        public ActionResult DeleteMultiQuestion(AssessMultipleQuestion AssessMultipleQuestion)
        {

            AssessMultipleQuestion QuestionAssess = db.AssessMultipleQuestion.Find(AssessMultipleQuestion.MultipleChoiceID);
            db.AssessMultipleQuestion.Remove(QuestionAssess);
            db.SaveChanges();

            return Json(QuestionAssess, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteQuestion(AssessQuestionStorage AssessQuestionStorage)
        {

            if (db.AssessMultipleQuestion.Any(x => x.QuestionID == AssessQuestionStorage.QuestionID))
            {
                int umber = 1;

                return Json(umber.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                AssessQuestionStorage QuestionAssess = db.AssessQuestionStorage.Find(AssessQuestionStorage.QuestionID);
                db.AssessQuestionStorage.Remove(QuestionAssess);
                db.SaveChanges();

                return Json(QuestionAssess, JsonRequestBehavior.AllowGet);
            }


        }


        

        // GET: AssesSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssesSection assesSection = db.AssesSection.Find(id);
            if (assesSection == null)
            {
                return HttpNotFound();
            }
            return View(assesSection);
        }

        // POST: AssesSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssesSection assesSection = db.AssesSection.Find(id);
            db.AssesSection.Remove(assesSection);
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
