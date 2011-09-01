using System;

namespace Ads.Model.Entities
{
    public class BaseEntity<TId,TUserId>  where TId:struct where TUserId :struct 
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
            LastUpdatedOn = DateTime.Now;
        }
        public TId Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public TUserId CreatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public TUserId LastUpdatedBy { get; set; }
    }
}