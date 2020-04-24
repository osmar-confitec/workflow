using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowIdentity.Models
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class AuthEmailMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
