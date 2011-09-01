using Ads.Model.Entities;

namespace Ads.Services
{
    public interface IAdComponentParser
    {
        void Parse(Ad ad, string adInString);
    }
}