using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aws.Faq.Scraper 
{
    public class FaqScraper : IFaqScraper
    {
        private readonly Regex _questionRegex = new Regex(@"<p><b>Q.*?</b></p>", RegexOptions.IgnoreCase);
        private readonly Func<string, string> _removeHtml = input => Regex.Replace(input, "<.*?>", String.Empty);

        private readonly IFaqResolver _faqResolver;

        public FaqScraper(IFaqResolver faqResolver)
        {
            _faqResolver = faqResolver;
        }

        public async Task<Dictionary<string, string>> GetFaq(string faqRequest)
        {
            var fileLocation = await _faqResolver.GetFaqContent(faqRequest);

            Dictionary<string, string> faq = new Dictionary<string, string>();
            string line;

            string question = string.Empty;
            string answer = string.Empty;

            using (StreamReader file = new StreamReader(fileLocation))
            {
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (!string.IsNullOrEmpty(question))
                    {
                        answer = _removeHtml(line);
                        faq.Add(question, answer);
                        question = string.Empty;
                        answer = string.Empty;
                        continue;
                    }

                    question = TryParseQuestion(line);
                }
            }

            return faq;
        }

        private string TryParseQuestion(string line) 
        {
            var foundQuestion = _questionRegex.Match(line);
            if (foundQuestion.Success)
            {
                return _removeHtml(line).TrimStart('Q', '.', ':', ' ');
            }

            return string.Empty;
        }
    }
}