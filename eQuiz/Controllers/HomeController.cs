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
            DateTime QuizStartTime = DateTime.Parse("2020/01/02 02:34:45 AM");
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
            var UserId = User.Identity.GetUserId();
            user = db.Users.Where(u => u.Id == UserId).SingleOrDefault();
            QuestionHelper.CheckQuestion(SolvedQvm, this, user, db);
            
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TotalScore"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["TotalScore"];
                //cookie.Expires = DateTime.Now.AddDays(-1); // remove the cookie
                ViewBag.TotalScore = int.Parse(cookie.Value);
            }

            return View();
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
    }
}