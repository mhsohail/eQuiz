using System;

namespace eQuiz.ViewModels
{
    public class ResultViewModel
    {
        public TimeSpan QuizTime { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalScore { get; set; }
    }
}