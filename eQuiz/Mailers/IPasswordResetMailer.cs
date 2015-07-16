using Mvc.Mailer;
using eQuiz.Mailers.Models;

namespace eQuiz.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}