using System;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Ads.Services
{
    public interface IOpenIdService
    {
        IAuthenticationRequest CreateRequest(string id);
        IAuthenticationResponse GetResponse();
    }
}