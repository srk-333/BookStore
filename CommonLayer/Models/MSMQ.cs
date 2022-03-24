namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Experimental.System.Messaging;

    /// <summary>
    /// Mail sender Class
    /// </summary>
    public class MSMQ
    {
        /// <summary>
        /// The message queue
        /// </summary>
        private readonly MessageQueue messageQueue = new MessageQueue();

        /// <summary>
        /// Senders the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Sender(string token)
        {
            // system private msmq server path
            this.messageQueue.Path = @".\private$\Tokens";
            try
            {
                // Checking Path Exists or Not
                if (!MessageQueue.Exists(this.messageQueue.Path))
                {
                    // Creating Path
                    MessageQueue.Create(this.messageQueue.Path);
                }

                this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                // Delegate Method Called
                this.messageQueue.ReceiveCompleted += this.MessageQueue_ReceiveCompleted;
                this.messageQueue.Send(token);
                this.messageQueue.BeginReceive();
                this.messageQueue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the ReceiveCompleted event of the MessageQueue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("saurav.kr.192.168.1.1@gmail.com", "jkliop89")
                };
                mailMessage.From = new MailAddress("saurav.kr.192.168.1.1@gmail.com");
                mailMessage.To.Add(new MailAddress("saurav.kr.192.168.1.1@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "Forgot Password Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                this.messageQueue.BeginReceive();
            }
        }
    }
}
