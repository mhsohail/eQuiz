using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eQuiz.Models
{
    public class QuestionUser
    {
        [Key, Column(Order = 0)]
        public int QuestionId { get; set; }
        [Key, Column(Order = 1)]
        public string ApplicationUserId { get; set; }
        [Column(Order = 2)]

        // the time when questions is displayed to user
        public DateTime StartTime { get; set; }
        [Column(Order = 3)]
        
        // the time when user submits question after choosing answer
        public DateTime EndTime { get; set; }
        [Column(Order = 4)]
        public bool IsCorrect { get; set; }
        public bool IsSolved { get; set; }
        
        public virtual Question Question { get; set; }
        public virtual ApplicationUser Candidate { get; set; }
    }
}