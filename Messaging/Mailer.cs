using System;
using System.Linq;
using System.Net.Mail;
using Shared.Html.Logging;

namespace Shared.Html.Messaging
{
    public static class Mailer
    {
        private const string ServerName = "";
        private const string SenderAddr = "";
        private const string SenderName = "";
        private static readonly MailAddress DebugSender = new MailAddress("a@b.com", "Some Person");

        public static void SendMessage(string subject,
            string body,
            string toAddress,
            string projectIdentifier,
            bool isHTML = false,
            MailPriority priority = MailPriority.Normal)
        {
            Mailer.SendMessage(subject, body, new MailAddress[1] { new MailAddress(toAddress) }, projectIdentifier, isHTML, priority);
        }

        public static void SendMessage(string subject,
            string body,
            MailAddress toAddress,
            string projectIdentifier,
            bool isHTML = false,
            MailPriority priority = MailPriority.Normal)
        {
            Mailer.SendMessage(subject, body, new MailAddress[1] { toAddress }, projectIdentifier, isHTML, priority);
        }

        public static void SendMessage(
            string subject,
            string body,
            string[] toAddresses,
            string projectIdentifier,
            bool isHTML = false,
            MailPriority priority = MailPriority.Normal)
        {
            var addrBase = toAddresses.Where(p => p != null && p.Contains("@")).ToArray();
            MailAddress[] addresses = addrBase.Select(p => new MailAddress(p)).ToArray();
            Mailer.SendMessage(subject, body, addresses, projectIdentifier, isHTML, priority);
        }

        public static void SendMessage(string subject,
            string body,
            MailAddress[] toAddresses,
            string projectIdentifier,
            bool isHTML = false,
            MailPriority priority = MailPriority.Normal)
        {
            MailMessage msg = null;
            SmtpClient clt = null;
            var sender = projectIdentifier + SenderName;
            try
            {
                msg = new MailMessage()
                {
                    From = new MailAddress(SenderAddr, sender),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHTML,
                    Priority = priority
                };
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    foreach (var address in toAddresses)
                    {
                        msg.To.Add(address);
                    }
                }
                else
                {
                    msg.Subject += " (debug)";

                    msg.To.Add(DebugSender);
                }
                clt = new SmtpClient(ServerName);
                clt.Send(msg);
            }
            catch (Exception ex)
            {
                Log.Event(ex);
            }
            finally
            {
                msg?.Dispose();
                clt?.Dispose();
            }
        }
    }
}