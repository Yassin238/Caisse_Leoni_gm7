using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Caisse_Leoni_gm7.Services
{
    public class SmsService
    {
        private const string AccountSid = "*****************"; 
        private const string AuthToken = "*****************";
        private const string FromPhoneNumber = "+**************";

        public SmsService()
        {
            TwilioClient.Init(AccountSid, AuthToken);
        }

        public void SendSms(string toPhoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber(FromPhoneNumber),
                Body = message
            };

            var msg = MessageResource.Create(messageOptions);
            Console.WriteLine($"Message sent with SID: {msg.Sid}");
        }
    }
}
