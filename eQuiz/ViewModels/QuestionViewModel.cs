﻿using eQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.ViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public int NextId { get; set; }
    }
}