using Chess.Users.Models.SettingsModels;
using Chess.Users.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
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

        public async Task SendAsync(MimeMessage message)
        {
            await ConnectSmtpClientAsync();

            await _smtpClient.SendAsync(message);

            await _smtpClient.DisconnectAsync(true);
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