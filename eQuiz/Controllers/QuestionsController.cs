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
        public ActionResult Details(int? id, QuestionViewModel SolvedQvm)
        {
            Question QuestionToSolve = db.Questions.SingleOrDefault(q => q.QuestionId == id);
            var UserMngr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var user = UserMngr.FindById(User.Identity.GetUserId());
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();

            if (user.HasSolvedQuestion(QuestionToSolve))
            {
                // redirect to next unsolved question
                var QuestionsInDb = db.Questions.OrderBy(q => q.QuestionId);
                foreach (Question Question in QuestionsInDb)
                {
                    if (!user.HasSolvedQuestion(Question))
                    {
                        HttpCookie cookie = new HttpCookie("QuestionId", Question.QuestionId.ToString());
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Solved", "Home");
                    }
                }
            }

            if (SolvedQvm != null && SolvedQvm.SelectedAnswerId != 0)
            {
                var QuestionToCheck = db.Questions.Where(q => q.QuestionId == SolvedQvm.Question.QuestionId).SingleOrDefault();
                var CorrectAnswer = QuestionToCheck.Answers.SingleOrDefault(a => a.IsCorrect);
                if (SolvedQvm.SelectedAnswerId == CorrectAnswer.AnswerId)
                {
                    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TotalScore"))
                    {
                        HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["TotalScore"];
                        //cookie.Expires = DateTime.Now.AddDays(-1); // remove the cookie
                        int TotalScore = int.Parse(cookie.Value);
                        TotalScore++;
                        cookie.Value = TotalScore.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("TotalScore");
                        cookie.Value = "1";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                }
                else
                { 
                    // answer is incorrect
                }

                try
                {
                    user.SolvedQuestions.Add(QuestionToCheck);
                    db.Questions.Attach(QuestionToCheck);
                    db.SaveChanges();
                    
                }
                catch (Exception exc) { }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (user.IsAheadOfNextUnsolvedQuestion(QuestionToSolve))
            {
                // redirect to next unsolved question
                var QuestionsInDb = db.Questions.OrderBy(q => q.QuestionId);
                foreach (Question Question in QuestionsInDb)
                {
                    if (!user.HasSolvedQuestion(Question))
                    {
                        HttpCookie cookie = new HttpCookie("QuestionId", Question.QuestionId.ToString());
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Solved", "Home");
                    }
                }
            }

            List<Answer> Answers = QuestionToSolve.Answers;
            var qvm = new QuestionViewModel();
            qvm.Question = QuestionToSolve;
            qvm.Answers = Answers;
            var NextQuestion = db.Questions.OrderBy(q => q.QuestionId).FirstOrDefault(q => q.QuestionId > id);
            if (NextQuestion != null)
            {
                qvm.NextId = NextQuestion.QuestionId;
                qvm.IsLastQuestion = false;
            }
            else
            {
                qvm.IsLastQuestion = true;
            }

            if (QuestionToSolve == null)
            {
                return HttpNotFound();
            }
            
            return View(qvm);
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
