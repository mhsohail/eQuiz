using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }
        
        [Display(Name="Answer")]
        [Required]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<ApplicationUser> Candidates { get; set; }
    }
}