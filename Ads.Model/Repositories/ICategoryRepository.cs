using System.Linq;
using Ads.Model.Entities;

namespace Ads.Model.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int, int>
    {
    }
}