﻿using eQuiz.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eQuiz.ViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public int NextId { get; set; }
        public bool IsLastQuestion { get; set; }
        [Required(ErrorMessage="Please choose an answer.")]
        public int SelectedAnswerId { get; set; }
    }
}