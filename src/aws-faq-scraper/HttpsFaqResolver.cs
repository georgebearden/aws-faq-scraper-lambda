using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;

namespace Aws.Faq.Scraper
{
    public class HttpsFaqResolver : IFaqResolver 
    {
        public async Task<string> GetFaqContent(string location)
        {
            WebRequest request = WebRequest.Create(location);
            using (WebResponse response = await request.GetResponseAsync())
            {
                Stream dataStream = response.GetResponseStream();  
                StreamReader reader = new StreamReader(dataStream);  
                string fileContents = reader.ReadToEnd();  
                string faqFileName = Path.GetTempFileName();
                File.WriteAllText(faqFileName, fileContents);
                return faqFileName;
            }
        }
    }
}