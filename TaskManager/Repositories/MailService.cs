using System;
using System.Threading.Tasks;
using TaskManager.Interfaces;

namespace TaskManager.Repositories
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            throw new NotImplementedException();
        }
    }
}