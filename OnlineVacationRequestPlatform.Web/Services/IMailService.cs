using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string content);
    }
}
