using Ads.Model.Entities;
using System.Linq;
using Ads.Model.Repositories;

namespace Ads.Model.EFRepositories
{
    public class UserEFRepository : BaseEFRepository<User,int,int>, IUserRepository
    {
        public User FindByOpenId(string openId)
        {
            return All.Where(user => user.OpenId == openId).FirstOrDefault();
        }
    }
}