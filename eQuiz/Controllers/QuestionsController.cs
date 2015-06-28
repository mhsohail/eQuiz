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

namespace eQuiz.Controllers
{
    public class QuestionsController : Controller
    {
        private eQuizContext db = new eQuizContext();

        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id, QuestionViewModel SolvedQvm)
        {
            if (SolvedQvm != null && SolvedQvm.SelectedAnswerId != 0)
            {
                var QuestionToCheck = db.Questions.Find(SolvedQvm.Question.Id);
                var CorrectAnswer = QuestionToCheck.Answers.SingleOrDefault(a => a.IsCorrect);
                if (SolvedQvm.SelectedAnswerId == CorrectAnswer.Id)
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
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            List<Answer> Answers = question.Answers;
            
            var qvm = new QuestionViewModel();
            qvm.Question = question;
            qvm.Answers = Answers;
            var NextQuestion = db.Questions.OrderBy(q => q.Id).FirstOrDefault(q => q.Id > id);
            if (NextQuestion != null)
            {
                qvm.NextId = NextQuestion.Id;
                qvm.IsLastQuestion = false;
            }
            else
            {
                qvm.IsLastQuestion = true;
            }

            if (question == null)
            {
                return HttpNotFound();
            }
            
            return View(qvm);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                        if (Answer.Id.Equals(0))
                        {
                            Answer.QuestionId = qvm.Question.Id;
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
        public ActionResult Edit(QuestionViewModel qvm) //[Bind(Include = "Id,Text,AnswerId")]
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ModifiedQuestion = qvm.Question;
                    var Question = db.Questions.Find(ModifiedQuestion.Id);
                    Question.Text = ModifiedQuestion.Text;

                    db.Entry(Question).State = EntityState.Modified;
                    db.SaveChanges();

                    foreach (var ModifiedAnswer in qvm.Answers)
                    {
                        var Answer = db.Answers.Find(ModifiedAnswer.Id);
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
