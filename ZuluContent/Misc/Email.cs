using System;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Server.Accounting;
using Server.Engines.Help;

namespace Server.Misc
{
    public static class Email
    {
        /// <summary>
        ///     Sends Queue-Page request using Email
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="pageType"></param>
        public static void SendQueueEmail(PageEntry entry, string pageType)
        {
            if (!ZhConfig.Email.Enabled)
            {
                return;
            }

            var sender = entry.Sender;
            var time = Core.Now;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ZhConfig.Email.FromName, ZhConfig.Email.FromAddress));
            message.To.Add(new MailboxAddress(ZhConfig.Email.SpeechLogPageName, ZhConfig.Email.SpeechLogPageAddress));
            message.Subject = "ModernUO Speech Log Page Forwarding";

            using (var writer = new StringWriter())
            {
                writer.WriteLine(
                    @$"
          ModernUO Speech Log Page - {pageType}
          From: '{sender.RawName}', Account: '{(sender.Account is Account accSend ? accSend.Username : " ??? ")}'
          Location: {sender.Location} [{sender.Map}]
          Sent on: {time.Year}/{time.Month:00}/{time.Day:00} {time.Hour}:{time.Minute:00}:{time.Second:00}
          Message:
          '{entry.Message}'
          Speech Log
          ==========
        "
                );

                foreach (var logEntry in entry.SpeechLog)
                {
                    var from = logEntry.From;
                    var fromName = from.RawName;
                    var fromAccount = from.Account is Account accFrom ? accFrom.Username : "???";
                    var created = logEntry.Created;
                    var speech = logEntry.Speech;
                    writer.WriteLine(
                        @$"{created.Hour}:{created.Minute:00}:{created.Second:00} - {fromName} ({fromAccount}): '{speech}'"
                    );
                }

                message.Body = new BodyBuilder
                {
                    TextBody = writer.ToString(),
                    HtmlBody = null
                }.ToMessageBody();
            }

            SendAsync(message);
        }

        /// <summary>
        ///     Sends crash email
        /// </summary>
        /// <param name="filePath"></param>
        public static void SendCrashEmail(string filePath)
        {
            if (ZhConfig.Email.Enabled)
            {
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ZhConfig.Email.FromName, ZhConfig.Email.FromAddress));
            message.To.Add(new MailboxAddress(ZhConfig.Email.CrashName, ZhConfig.Email.CrashAddress));
            message.Subject = "Automated ModernUO Crash Report";
            var builder = new BodyBuilder
            {
                TextBody = "Automated ModernUO Crash Report. See attachment for details.",
                HtmlBody = null
            };
            builder.Attachments.Add(filePath);
            message.Body = builder.ToMessageBody();
        }

        /// <summary>
        ///     Sends emails async
        /// </summary>
        /// <param name="message"></param>
        private static async void SendAsync(MimeMessage message)
        {
            if (!ZhConfig.Email.Enabled)
            {
                return;
            }

            var now = Core.Now;
            var messageID = $"<{now:yyyyMMdd}.{now:HHmmssff}@{ZhConfig.Email.EmailServer}>";
            message.Headers.Add("Message-ID", messageID);
            message.From.Add(new MailboxAddress(ZhConfig.Email.FromName, ZhConfig.Email.FromAddress));


            var delay = ZhConfig.Email.EmailSendRetryDelay;

            for (var i = 0; i < ZhConfig.Email.EmailSendRetryCount; i++)
            {
                try
                {
                    using var client = new SmtpClient();
                    await client.ConnectAsync(ZhConfig.Email.EmailServer, ZhConfig.Email.EmailPort, true).ConfigureAwait(false);
                    await client.AuthenticateAsync(
                        ZhConfig.Email.EmailUsername,
                        ZhConfig.Email.EmailPassword
                    ).ConfigureAwait(false);
                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    return;
                }
                catch (Exception ex)
                {
                    if (i == 0)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }

                    delay *= delay;

                    await Task.Delay(delay * 1000).ConfigureAwait(false);
                }
            }
        }
    }
}