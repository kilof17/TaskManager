using System.Threading.Tasks;

namespace TaskManager.Interfaces
{
    public interface IMailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string content);
    }
}