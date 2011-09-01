using Ads.Model.Entities;

namespace Ads.Model.Repositories
{
    public interface IUserRepository : IRepository<User, int,int>
    {
        User FindByOpenId(string openId);
    }
}