using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Aws.Faq.Scraper
{
    public class Function
    {
        private readonly IFaqScraper _scraper;
        private readonly IFaqResolver _resolver;
        private readonly IFaqPersister _persister;
        private readonly EntryPoint _entryPoint;

        public Function()
        {
            _resolver = new HttpsFaqResolver();
            _scraper = new FaqScraper(_resolver);
            _persister = new DynamoDBFaqPersister();
            _entryPoint = new EntryPoint(_scraper, _resolver, _persister);

        }
        
        public async Task<String> FunctionHandler(string input, ILambdaContext context)
        {
            await _entryPoint.GetFaq(AwsServiceTypes.EC2);

            return "success";
        }
    }
}
