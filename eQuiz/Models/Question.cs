using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eQuiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        [Display(Name="Question")]
        [Required]
        public string Text { get; set; }
        
        public virtual List<Answer> Answers { get; set; }
        public virtual ICollection<ApplicationUser> Candidates { get; set; }
        public virtual ICollection<QuestionUser> QuestionAppUsers { get; set; }
    }
}