using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eQuiz.Models;
using eQuiz.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using eQuiz.Helpers;

namespace eQuiz.Controllers
{
    public class QuestionsController : Controller
    {
        public eQuizContext db = new eQuizContext();

        // GET: Questions
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        [Authorize]
        public string Details(int? id, QuestionViewModel SolvedQvm)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();
            //if (user.QuizInfo != null && user.QuizInfo.HasCompletedQuiz) return RedirectToAction("Result", "Home");

            var QuizStartTime = DateTime.Parse(db.Settings.SingleOrDefault(s => s.Name == "Quiz Start Time").Value);
            
            string EasternStandardTimeId = "Eastern Standard Time";
            TimeZoneInfo ESTTimeZone = TimeZoneInfo.FindSystemTimeZoneById(EasternStandardTimeId);
            DateTime ESTDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ESTTimeZone);
            var TimeDiff = QuizStartTime.Subtract(ESTDateTime);
            
            return QuizStartTime + " - " + DateTime.Now.ToLocalTime() + " - " + TimeDiff;
            //return local + " - " + universal + " - " + DateTime.UtcNow.ToLocalTime();
            
            //TimeZone.CurrentTimeZone.ToLocalTime()
            //if (TimeDiff.TotalSeconds > 0)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            
            //Question QuestionToSolve = db.Questions.SingleOrDefault(q => q.QuestionId == id);
            //var UserMngr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ////var user = UserMngr.FindById(User.Identity.GetUserId());

            //if (user.QuizInfo == null)
            //{
            //    QuizInfo QuizInfo = new QuizInfo();
            //    QuizInfo.QuizStartDateTime = QuizStartTime;
            //    QuizInfo.ApplicationUser = user;
            //    db.QuizInfo.Add(QuizInfo);
            //    db.SaveChanges();
            //}

            //try
            //{
            //    var QuestionUser = db.QuestionUsers.SingleOrDefault(
            //        qu => qu.QuestionId.Equals(QuestionToSolve.QuestionId) &&
            //            qu.ApplicationUserId.Equals(user.Id));
            //    if (QuestionUser == null)
            //    {
            //        QuestionUser = new QuestionUser();
            //        QuestionUser.ApplicationUserId = user.Id;
            //        QuestionUser.QuestionId = QuestionToSolve.QuestionId;
            //        QuestionUser.StartTime = DateTime.Now;
            //        QuestionUser.EndTime = DateTime.Now;
            //        db.QuestionUsers.Add(QuestionUser);
            //        db.SaveChanges();
            //    } else if (!QuestionUser.IsSolved)
            //    {
            //        QuestionUser.StartTime = DateTime.Now;
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception exc) { }

            //var UnsolvedQuestions = user.GetUnsolvedQuestions();
            //if (UnsolvedQuestions.Count == 0)
            //{
            //    HttpCookie cookie = new HttpCookie("QuizSolved", true.ToString());
            //    Response.Cookies.Add(cookie);
            //    return RedirectToAction("Solved", "Home");
            //}

            //if (user.HasSolvedQuestion(QuestionToSolve))
            //{
            //    // redirect to next unsolved question
            //    var QuestionsInDb = db.Questions.OrderBy(q => q.QuestionId);
            //    foreach (Question Question in QuestionsInDb)
            //    {
            //        if (!user.HasSolvedQuestion(Question))
            //        {
            //            HttpCookie cookie = new HttpCookie("QuestionId", Question.QuestionId.ToString());
            //            Response.Cookies.Add(cookie);
            //            return RedirectToAction("Solved", "Home");
            //        }
            //    }
            //}

            //if (SolvedQvm != null && SolvedQvm.SelectedAnswerId != 0)
            //{
            //    QuestionHelper.CheckQuestion(SolvedQvm, this, user, db);
            //}

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //if (user.IsAheadOfNextUnsolvedQuestion(QuestionToSolve))
            //{
            //    // redirect to next unsolved question
            //    var QuestionsInDb = db.Questions.OrderBy(q => q.QuestionId);
            //    foreach (Question Question in QuestionsInDb)
            //    {
            //        if (!user.HasSolvedQuestion(Question))
            //        {
            //            HttpCookie cookie = new HttpCookie("QuestionId", Question.QuestionId.ToString());
            //            Response.Cookies.Add(cookie);
            //            return RedirectToAction("Solved", "Home");
            //        }
            //    }
            //}

            //List<Answer> Answers = QuestionToSolve.Answers;
            //var qvm = new QuestionViewModel();
            //qvm.Question = QuestionToSolve;
            //qvm.Answers = Answers;
            //var NextQuestion = db.Questions.OrderBy(q => q.QuestionId).FirstOrDefault(q => q.QuestionId > id);
            //if (NextQuestion != null)
            //{
            //    qvm.NextId = NextQuestion.QuestionId;
            //    qvm.IsLastQuestion = false;
            //}
            //else
            //{
            //    qvm.IsLastQuestion = true;
            //}

            //if (QuestionToSolve == null)
            //{
            //    return HttpNotFound();
            //}
            
            //return View(qvm);
        }

        // GET: Questions/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel qvm) //[Bind(Include = "Id,Text,AnswerId")]
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Questions.Add(qvm.Question);
                    db.SaveChanges();

                    foreach (var Answer in qvm.Answers)
                    {
                        if (Answer.AnswerId.Equals(0))
                        {
                            Answer.QuestionId = qvm.Question.QuestionId;
                            db.Answers.Add(Answer);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch(Exception exc) { }
            }

            return View(qvm.Question);
        }

        // GET: Questions/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            List<Answer> Answers = question.Answers;

            var qvm = new QuestionViewModel();
            qvm.Question = question;
            qvm.Answers = Answers;

            if (question == null)
            {
                return HttpNotFound();
            }
            return View(qvm);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(QuestionViewModel qvm) //[Bind(Include = "Id,Text,AnswerId")]
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ModifiedQuestion = qvm.Question;
                    var Question = db.Questions.Find(ModifiedQuestion.QuestionId);
                    Question.Text = ModifiedQuestion.Text;

                    db.Entry(Question).State = EntityState.Modified;
                    db.SaveChanges();

                    foreach (var ModifiedAnswer in qvm.Answers)
                    {
                        var Answer = db.Answers.Find(ModifiedAnswer.AnswerId);
                        Answer.Text = ModifiedAnswer.Text;
                        Answer.IsCorrect = ModifiedAnswer.IsCorrect;

                        db.Entry(Answer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    //db.Entry(qvm.Question).State = EntityState.Modified;
                    //db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception exc) {}
            }
            return View(qvm);
        }

        // GET: Questions/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
