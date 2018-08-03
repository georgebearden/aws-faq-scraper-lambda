using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using Moq;

namespace Aws.Faq.Scraper.Tests
{
    public class FaqScraperTests
    {

        [Fact]
        public async Task CanGetFaq()
        {
            Mock<IFaqResolver> faqResolver = new Mock<IFaqResolver>();
            faqResolver.Setup(f => f.GetFaqContent(It.IsAny<string>())).Returns(Task.FromResult(@"polly-faq.html"));

            var faqScraper = new FaqScraper(faqResolver.Object);
            var faq = await faqScraper.GetFaq("doesnt matter, since its getting mocked");
        
            Assert.NotEmpty(faq);
        }
    }
}
