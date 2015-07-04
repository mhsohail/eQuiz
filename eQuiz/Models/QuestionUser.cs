using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class QuestionUser
    {
        [Key, Column(Order = 0)]
        public int QuestionId { get; set; }
        [Key, Column(Order = 1)]
        public string ApplicationUserId { get; set; }
        [Column(Order = 2)]
        public DateTime StartTime { get; set; }
        [Column(Order = 3)]
        public DateTime EndTime { get; set; }
        [Column(Order = 4)]
        public bool IsCorrect { get; set; }
        public bool IsSolved { get; set; }
        
        public virtual Question Question { get; set; }
        public virtual ApplicationUser Candidate { get; set; }
    }
}