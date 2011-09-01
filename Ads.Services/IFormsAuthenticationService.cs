using DotNetOpenAuth.OpenId;

namespace Ads.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
        void SetAuthCookie(string claimedIdentifier, bool b);
    }
}