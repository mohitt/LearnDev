using System;
using System.Linq;
using Ads.Model.Entities;
using Ads.Model.Repositories;

namespace Ads.Model.EFRepositories
{
    public class CategoryEFRepository : BaseEFRepository<Category,int,int>, ICategoryRepository
    {
        
    }
}