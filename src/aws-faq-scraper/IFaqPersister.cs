using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Aws.Faq.Scraper
{
    public interface IFaqPersister
    {
        Task Persist(string faqName, Dictionary<string, string> faqContent);
    }
}