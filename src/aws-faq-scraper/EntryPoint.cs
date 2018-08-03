using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

using Amazon.Lambda.Core;

namespace Aws.Faq.Scraper
{
    public class EntryPoint
    {
        private const string AwsServiceMappings = @"aws-service-mappings.json";

        private readonly IFaqResolver _resolver;
        private readonly IFaqScraper _scraper;
        private readonly IFaqPersister _persister;

        private readonly Dictionary<AwsServiceTypes, string> _awsServices;

        public EntryPoint(IFaqScraper scraper, IFaqResolver resolver, IFaqPersister persister)
        {
            _scraper = scraper;
            _resolver = resolver;
            _persister = persister;

            _awsServices = JsonConvert.DeserializeObject<Dictionary<AwsServiceTypes, string>>(File.ReadAllText(AwsServiceMappings));
        }

        public async Task GetFaq(AwsServiceTypes service)
        {
            var faq = await _scraper.GetFaq(_awsServices[service]);
            await _persister.Persist(service.ToString(), faq);
        }
    }
}