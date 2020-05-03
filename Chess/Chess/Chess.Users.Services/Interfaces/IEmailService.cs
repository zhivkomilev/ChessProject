using Chess.Users.Models.EmailModels;
using MimeKit;
using System.Threading.Tasks;

namespace Chess.Users.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(MimeMessage model);
    }
}