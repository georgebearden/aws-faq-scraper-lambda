using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aws.Faq.Scraper
{
    public interface IFaqScraper
    {
        Task<Dictionary<string, string>> GetFaq(string fileLocation);
    }
}
