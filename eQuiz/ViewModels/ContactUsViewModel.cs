using System.ComponentModel.DataAnnotations;

namespace eQuiz.ViewModels
{
    public class ContactUsViewModel
    {
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}