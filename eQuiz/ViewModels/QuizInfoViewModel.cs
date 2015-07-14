using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.ViewModels
{
    public class QuizInfoViewModel
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public int CorrectAnswersCount { get; set; }
        public TimeSpan QuizTime { get; set; }
        public string UserIpAddress { get; set; }
        public string UserMacAddress { get; set; }
    }
}