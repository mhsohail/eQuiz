﻿using eQuiz.Models;
using eQuiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eQuiz.Helpers
{
    public class QuestionHelper
    {
        public static void CheckQuestion(QuestionViewModel SolvedQvm, Controller context, ApplicationUser user, eQuizContext db)
        {
            var QuestionToCheck = db.Questions.Where(q => q.QuestionId == SolvedQvm.Question.QuestionId).SingleOrDefault();
            var CorrectAnswer = QuestionToCheck.Answers.SingleOrDefault(a => a.IsCorrect);
            if (SolvedQvm.SelectedAnswerId == CorrectAnswer.AnswerId)
            {
                if (context.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TotalScore"))
                {
                    HttpCookie cookie = context.ControllerContext.HttpContext.Request.Cookies["TotalScore"];
                    //cookie.Expires = DateTime.Now.AddDays(-1); // remove the cookie
                    int TotalScore = int.Parse(cookie.Value);
                    TotalScore++;
                    cookie.Value = TotalScore.ToString();
                    context.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("TotalScore");
                    cookie.Value = "1";
                    context.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
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
    }
}