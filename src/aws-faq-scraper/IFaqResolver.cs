using System.Threading.Tasks;

namespace Aws.Faq.Scraper
{
    public interface IFaqResolver
    {
        Task<string> GetFaqContent(string location);
    }
}