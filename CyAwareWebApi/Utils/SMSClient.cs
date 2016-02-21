
using System;
using Twilio;

namespace CyAwareWebApi.Utils
{
    public class SMSClient
    {

        private static SMSClient instance;
        TwilioRestClient twilio;

        private SMSClient()
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account
            string AccountSid = Configurator.Instance.getValue("twilioSID");
            string AuthToken = Configurator.Instance.getValue("twilioToken");
            try
            {
                twilio = new TwilioRestClient(AccountSid, AuthToken);

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static SMSClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SMSClient();
                }
                return instance;
            }
        }

        public int send(string body)
        {
            var message = twilio.SendMessage(Configurator.Instance.getValue("twilioFrom"), Configurator.Instance.getValue("twilioTo"), body);
            if (message.RestException != null)
            {
                var error = message.RestException.Message;
                // handle the error ...
            }
            return 0;
        }
    }
}