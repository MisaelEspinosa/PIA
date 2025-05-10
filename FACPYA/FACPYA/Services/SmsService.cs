using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace API.FACPYA.Services
{
    public class TwilioService
    {
        private readonly string accountSid = "ACb093215d29687b0c98c8fb2de830ea51";
        private readonly string authToken = "b83c8cf875900213520680ebfb6e5249";
        private readonly string twilioPhoneNumber = "+15075333102";

        public TwilioService()
        {
            TwilioClient.Init(accountSid, authToken);
        }

        public void EnviarSms(string numeroDestino, string mensaje)
        {
            var message = MessageResource.Create(
                body: mensaje,
                from: new PhoneNumber(twilioPhoneNumber),
                to: new PhoneNumber(numeroDestino)
            );
        }
    }
}
