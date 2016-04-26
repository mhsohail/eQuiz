using System.ComponentModel.DataAnnotations;

namespace eQuiz.Models
{
    public class Test
    {
        public int Id { get; set; }
        [Required]
        public int Text { get; set; }
    }
}