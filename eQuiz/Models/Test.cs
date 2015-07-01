using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Test
    {
        public int Id { get; set; }
        [Required]
        public int Text { get; set; }
    }
}