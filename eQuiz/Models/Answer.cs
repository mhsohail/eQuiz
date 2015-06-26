﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}