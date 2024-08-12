using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Caisse_Leoni_gm7.Services
{
    public class SmsService
    {
        private const string AccountSid = "AC24553201de37245d6fbb73e6e9e4e98c"; 
        private const string AuthToken = "edb25c62e66b3e1134cf8617adc66766";
        private const string FromPhoneNumber = "+21695068570";

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
