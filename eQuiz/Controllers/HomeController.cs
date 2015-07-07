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
            DbInitializer.Initialize();    
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
            var QSTime = DateTime.Parse(db.Settings.SingleOrDefault(s => s.Name == "Quiz Start Time").Value);
            var TimeDiff = QSTime.Subtract(DateTime.Now);
            if (TimeDiff.TotalSeconds > 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ResultViewModel rvm = new ResultViewModel();

            try
            {
                var UserId = User.Identity.GetUserId();
                user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();

                if (SolvedQvm.Question != null)
                {
                    SolvedQvm.IsLastQuestion = true;
                    QuestionHelper.CheckQuestion(SolvedQvm, this, user, db);
                }
                var CorrectAnswersCount = db.QuestionUsers.Where(qu => qu.ApplicationUserId.Equals(UserId) && qu.IsCorrect.Equals(true)).Count();

                var QuizStartTime = db.QuestionUsers.OrderBy(qu => qu.StartTime).FirstOrDefault().StartTime;
                var QuizEndTime = db.QuestionUsers.OrderByDescending(qu => qu.EndTime).FirstOrDefault().EndTime;

                TimeSpan TimeSpan = QuizEndTime.Subtract(QuizStartTime);

                rvm.QuizTime = TimeSpan;
                rvm.CorrectAnswersCount = CorrectAnswersCount;
                rvm.TotalQuestions = db.Questions.ToList().Count;
                rvm.TotalScore = GetScoreFromCookie();
            }
            catch (NullReferenceException exc)
            {
                rvm.TotalScore = GetScoreFromCookie();
            }
            catch (Exception exc)
            {
                rvm.TotalScore = GetScoreFromCookie();
            }

            return View(rvm);
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
            var QSTime = DateTime.Parse(db.Settings.SingleOrDefault(s => s.Name == "Quiz Start Time").Value);
            var TimeDiff = QSTime.Subtract(DateTime.Now);
            if (TimeDiff.TotalSeconds > 0)
            {
                return RedirectToAction("Index", "Home");
            }

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
            var AppUsers = db.Users.Where(u => u.QuestionUsers.Count > 0).ToList();
            var QuizInfos = new List<QuizInfoViewModel>();

            foreach (var AppUser in AppUsers)
            {
                var QuestionUsers = AppUser.QuestionUsers.ToList();
                QuizInfoViewModel qifv = new QuizInfoViewModel();

                if (QuestionUsers.Count > 0)
                {
                    var CorrectAnswersCount = QuestionUsers.Where(
                        qu => qu.ApplicationUserId.Equals(AppUser.Id) &&
                        qu.IsCorrect).Count();

                    var QuizStartTime = QuestionUsers.OrderBy(qu => qu.StartTime).FirstOrDefault().StartTime;
                    var QuizEndTime = QuestionUsers.OrderByDescending(qu => qu.EndTime).FirstOrDefault().EndTime;
                    qifv.QuizTime = QuizEndTime.Subtract(QuizStartTime);
                    qifv.UserFullName = AppUser.FirstName + " " + AppUser.LastName;
                    qifv.CorrectAnswersCount = CorrectAnswersCount;
                    QuizInfos.Add(qifv);
                }
            }

            var QuizInfosOrdered = QuizInfos.OrderByDescending(qi => qi.CorrectAnswersCount).ThenBy(qi => qi.QuizTime).ToList();
            return View(QuizInfosOrdered);
        }
    }
}