using System;
using System.Net.Mail;


namespace CyAwareWebApi.Utils
{
    public class SMTPClient
    {
        private static SMTPClient instance;
        SmtpClient smtpClient;

        private SMTPClient()
        {
            smtpClient = new SmtpClient(Configurator.Instance.getValue("SMTPHost"), short.Parse(Configurator.Instance.getValue("SMTPPort")));
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(Configurator.Instance.getValue("SMTPUsername"), Configurator.Instance.getValue("SMTPPassword"));
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
        }

        public static SMTPClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SMTPClient();
                }
                return instance;
            }
        }

        public int send(string body, string toAddress)
        {
            int successCode = 1;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Configurator.Instance.getValue("SMTPFrommail"), Configurator.Instance.getValue("SMTPFrommail"));
                mail.To.Add(new MailAddress(toAddress));
                mail.Body = body;
                //mail.CC.Add(new MailAddress("ealparslan@gmail.com"));

                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                successCode = -1;
                throw;
            }
            return successCode;
        }
    }
}