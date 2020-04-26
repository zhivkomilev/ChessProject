using Chess.Users.Models.EmailModels;
using Chess.Users.Models.SettingsModels;
using Chess.Users.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services
{
    public class EmailService : IEmailService, IDisposable
    {
        private readonly EmailSettings _settings;
        private readonly ISmtpClient _smtpClient;

        public EmailService(EmailSettings settings,
            ISmtpClient smtpClient)
        {
            _settings = settings;
            _smtpClient = smtpClient;
        }

        //TODO -> Finish implementing
        public Task SendAsync(MimeMessage message)
        {
            throw new NotImplementedException();
        }

        public async Task SendChangePasswordEmailAsync(ChangePasswordEmailModel model)
        {
            await ConnectSmtpClientAsync();
            var message = new MimeMessage();
            var sender = new MailboxAddress(model.Sender.Name, model.Sender.Email);
            var reciever = new MailboxAddress(model.Reciever.Name, model.Reciever.Email);
            
            message.Sender = sender;
            message.From.Add(sender);
            message.To.Add(reciever);
            message.Body = new TextPart(TextFormat.RichText)
            {
                Text = model.Body
            };

            //TODO -> Finish implementing
        }

        private async Task ConnectSmtpClientAsync()
        {
            await _smtpClient.ConnectAsync(_settings.Smtp.Host, _settings.Smtp.Port, _settings.Smtp.UseSsl);
            await _smtpClient.AuthenticateAsync(_settings.Smtp.Username, _settings.Smtp.Password);
        }

        #region IDisposable Support
        private bool _disposed = false; 

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

           _smtpClient.Dispose();
           _disposed = true;
        }
        #endregion
    }
}