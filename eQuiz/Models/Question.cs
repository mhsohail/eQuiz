using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AnswerId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}