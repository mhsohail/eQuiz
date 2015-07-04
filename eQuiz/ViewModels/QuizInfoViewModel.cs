using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.ViewModels
{
    public class QuizInfoViewModel
    {
        public string UserFullName { get; set; }
        public int CorrectAnswers { get; set; }
        public TimeSpan QuizTime { get; set; }
    }
}