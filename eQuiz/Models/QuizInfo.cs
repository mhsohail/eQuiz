using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class QuizInfo
    {
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public DateTime QuizStartDateTime { get; set; }
        public bool HasCompletedQuiz { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}