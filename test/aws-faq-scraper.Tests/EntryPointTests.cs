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
    public class EntryPointTests
    {
        [Fact]
        public async Task CanGetFaq()
        {
            var scraper = new Mock<IFaqScraper>();
            var resolver = new Mock<IFaqResolver>();
            var persister = new Mock<IFaqPersister>();

            scraper.Setup(s => s.GetFaq(It.IsAny<string>())).Returns(Task.FromResult(new Dictionary<string, string>(){ { "Question 1", "Answer 1"} }));

            var entryPoint = new EntryPoint(scraper.Object, resolver.Object, persister.Object);
            await entryPoint.GetFaq(AwsServiceTypes.Polly);

           persister.Verify(p => p.Persist("Polly", It.IsAny<Dictionary<string, string>>()), Times.Once());
        }
    }
}
