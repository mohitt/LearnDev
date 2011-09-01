using DotNetOpenAuth.OpenId.RelyingParty;

namespace Ads.Services
{
    public class OpenIdService : IOpenIdService
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

        public IAuthenticationRequest CreateRequest(string openIdIdentifier)
        {
            return openid.CreateRequest(openIdIdentifier);
        }

        public IAuthenticationResponse GetResponse()
        {
            return openid.GetResponse();
        }
    }
}