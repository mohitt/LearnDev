using Ads.Model.Entities;
using Ads.Model.Repositories;

namespace Ads.Model.EFRepositories
{
    public class AdEFRepository : BaseEFRepository<Ad,int,int>,IAdRepository
    {
        
    }
}