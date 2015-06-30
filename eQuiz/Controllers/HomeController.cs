using eQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eQuiz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FirstQuestionId = new eQuizContext().Questions.OrderBy(q => q.QuestionId).FirstOrDefault().QuestionId;
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

        public ActionResult Result()
        {
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

        public ActionResult Solved()
        {
            return View();
        }
    }
}