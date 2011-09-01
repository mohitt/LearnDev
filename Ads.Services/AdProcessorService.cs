using Ads.Helper;
using Ads.Model.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Ads.Services
{
    public class AdProcessorService : IAdProcessorService
    {
        public AdProcessorService(IEnumerable<IAdComponentParser> adComponentParsers)
        {
            this.ComponentParsers = adComponentParsers;
        }

        public IEnumerable<IAdComponentParser> ComponentParsers { get; set; }

        public void Process(Ad ad, string adInString)
        {
            this.ComponentParsers.ForEach(parser => parser.Parse(ad, adInString));
        }
    }
}