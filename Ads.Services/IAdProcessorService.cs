using System;
using System.Collections.Generic;
using Ads.Model.Entities;

namespace Ads.Services
{
    public interface IAdProcessorService
    {
        IEnumerable<IAdComponentParser> ComponentParsers { get; set; }

        void Process(Ad ad, string adInString);
    }
}