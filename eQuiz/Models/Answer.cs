using System.ComponentModel.DataAnnotations;

namespace eQuiz.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        
        [Display(Name="Answer")]
        [Required]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}