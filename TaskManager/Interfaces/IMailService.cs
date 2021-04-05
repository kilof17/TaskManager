using System.Threading.Tasks;

namespace TaskManager.Interfaces
{
    internal interface IMailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string content);
    }
}