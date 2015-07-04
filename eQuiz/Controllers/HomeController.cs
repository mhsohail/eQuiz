using eQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using eQuiz.ViewModels;
using eQuiz.Helpers;

namespace eQuiz.Controllers
{
    public class HomeController : Controller
    {
        eQuizContext db = new eQuizContext();
        ApplicationUser user = null;

        public HomeController()
        {
            
        }

        [Authorize]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();
            
            var UnsolvedQuestions = user.GetUnsolvedQuestions();
            if (UnsolvedQuestions.Count == 0)
            {
                ViewBag.FirstQuestionId = 0;
            }
            else
            {
                ViewBag.FirstQuestionId = UnsolvedQuestions.OrderBy(q => q.QuestionId).FirstOrDefault().QuestionId;
            }
            //ViewBag.FirstQuestionId = new eQuizContext().Questions.OrderBy(q => q.QuestionId).FirstOrDefault().QuestionId;
            
            var QuizStartTime = DateTime.Parse(db.Settings.SingleOrDefault(s => s.Name == "Quiz Start Time").Value);
            ViewBag.QuizStartTime = db.Settings.SingleOrDefault(s => s.Name == "Quiz Start Time").Value;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Result(QuestionViewModel SolvedQvm)
        {
            try
            {
                var UserId = User.Identity.GetUserId();
                user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();

                if (SolvedQvm.Question != null)
                {
                    SolvedQvm.IsLastQuestion = true;
                    QuestionHelper.CheckQuestion(SolvedQvm, this, user, db);
                }
                var CorrectAnswers = db.QuestionUsers.Where(qu => qu.ApplicationUserId.Equals(UserId) && qu.IsCorrect.Equals(true)).Count();

                var QuizStartTime = db.QuestionUsers.OrderBy(qu => qu.StartTime).FirstOrDefault().StartTime;
                var QuizEndTime = db.QuestionUsers.OrderByDescending(qu => qu.EndTime).FirstOrDefault().EndTime;

                TimeSpan TimeSpan = QuizEndTime.Subtract(QuizStartTime);
                Console.WriteLine("Time Difference (seconds): " + TimeSpan.Seconds);
                Console.WriteLine("Time Difference (minutes): " + TimeSpan.Minutes);
                Console.WriteLine("Time Difference (hours): " + TimeSpan.Hours);
                Console.WriteLine("Time Difference (days): " + TimeSpan.Days);

                ViewBag.TimeSpan = TimeSpan;
                ViewBag.CorrectAnswers = CorrectAnswers;
                ViewBag.TotalQuestions = db.Questions.ToList().Count;
                ViewBag.TotalScore = GetScoreFromCookie();
            }
            catch (NullReferenceException exc)
            {
                ViewBag.TotalScore = GetScoreFromCookie();
            }
            catch (Exception exc)
            {
                ViewBag.TotalScore = GetScoreFromCookie();
            }

            return View();
        }

        private int GetScoreFromCookie()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TotalScore"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["TotalScore"];
                //cookie.Expires = DateTime.Now.AddDays(-1); // remove the cookie
                return int.Parse(cookie.Value);
            }
            return 0;
        }

        public ActionResult Counter()
        {
            return View();
        }

        [Authorize]
        public ActionResult Solved()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Settings()
        {
            var Settings = db.Settings.ToList();
            return View(Settings);
        }

        [Authorize(Roles="Administrator")]
        [HttpPost]
        public ActionResult CreateSettings(List<Setting> Settings)
        {
            foreach (var Setting in Settings)
            {
                var SettingDb = db.Settings.SingleOrDefault(s => s.SettingId == Setting.SettingId);
                if (SettingDb != null)
                {
                    SettingDb.Value = Setting.Value;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Settings");
        }

        public ActionResult DateTimePicker()
        {
            return View();
        }

        [Authorize(Roles="Administrator")]
        public ActionResult QuizInfo()
        {
            var UserId = User.Identity.GetUserId();
            user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();

            return View();
        }
    }
}