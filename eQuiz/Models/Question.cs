﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Display(Name="Question")]
        public string Text { get; set; }
        
        public virtual List<Answer> Answers { get; set; }
    }
}