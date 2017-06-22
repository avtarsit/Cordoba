using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces_Layout;
using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Mail;
using CordobaServices.Interfaces;


namespace CordobaServices.Services
{
    public class CommonService : ICommonServices
    {
        //Email Sending for order History
        private GenericRepository<PlaceOrderEntity> placeOrderRepository = new GenericRepository<PlaceOrderEntity>();

        public EmailNotification GetEmailSettings()
        {
            var objemailsetting = new EmailNotification();

            objemailsetting.EmailUsername = ConfigurationManager.AppSettings["emailusername"];
            objemailsetting.EmailPassword = ConfigurationManager.AppSettings["emailpassword"];
            objemailsetting.EmailHostName = ConfigurationManager.AppSettings["emailhostname"];
            objemailsetting.EmailEnableSsl = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["emailenablessl"]));
            objemailsetting.EmailPort = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
            objemailsetting.FromName = ConfigurationManager.AppSettings["emailusername"];
            objemailsetting.FromEmail = ConfigurationManager.AppSettings["emailusername"];

            return objemailsetting;
        }



        //Send mail
        public static bool SendMailMessage(string recipient, string bcc, string cc, string subject, string body, EmailNotification emailSetting, string attachment)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                return true;
            }

            // Instantiate a new instance of MailMessage 
            MailMessage mailMessage = new MailMessage();

            // Set the sender address of the mail message 
            mailMessage.From = new MailAddress(emailSetting.FromEmail, emailSetting.FromName);

            // Set the recipient address of the mail message 
            // mailMessage.To.Add(new MailAddress(recipient));
            if (!string.IsNullOrEmpty(recipient))
            {
                string[] strRecipient = recipient.Replace(";", ",").TrimEnd(',').Split(new char[] { ',' });

                // Set the Bcc address of the mail message 
                for (int intCount = 0; intCount < strRecipient.Length; intCount++)
                {
                    mailMessage.To.Add(new MailAddress(strRecipient[intCount]));
                }
            }

            // Check if the bcc value is nothing or an empty string 
            if (!string.IsNullOrEmpty(bcc))
            {
                string[] strBCC = bcc.Split(new char[] { ',' });

                // Set the Bcc address of the mail message 
                for (int intCount = 0; intCount < strBCC.Length; intCount++)
                {
                    mailMessage.Bcc.Add(new MailAddress(strBCC[intCount]));
                }
            }

            // Check if the cc value is nothing or an empty value 
            if (!string.IsNullOrEmpty(cc))
            {
                // Set the CC address of the mail message 
                string[] strCC = cc.Split(new char[] { ',' });
                for (int intCount = 0; intCount < strCC.Length; intCount++)
                {
                    mailMessage.CC.Add(new MailAddress(strCC[intCount]));
                }
            }

            // Set the subject of the mail message 
            mailMessage.Subject = subject;

            // Set the body of the mail message 
            mailMessage.Body = body;

            // Set the format of the mail message body as HTML 
            mailMessage.IsBodyHtml = true;

            // Set the priority of the mail message to normal 
            mailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient 
            var smtpClient = new SmtpClient();

            if (attachment != null && attachment != "")
                mailMessage.Attachments.Add(new Attachment(attachment));
            try
            {
                smtpClient.EnableSsl = emailSetting.EmailEnableSsl;
                smtpClient.Host = emailSetting.EmailHostName;
                smtpClient.Port = emailSetting.EmailPort;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailSetting.EmailUsername, emailSetting.EmailPassword);

                // Send the mail message 
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Security.DisposeOf(mailMessage);
                Security.DisposeOf(smtpClient);
            }
        }



    }
}
