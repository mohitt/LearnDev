using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ads.Helper
{

    using System;
    [AttributeUsage(AttributeTargets.Class)]
    public class LinkOrderAttribute : System.Attribute
    {
        public readonly string Key;

        public LinkOrderAttribute(string key)  
        {
            this.Key = key;
        }

      
    }
    
}
