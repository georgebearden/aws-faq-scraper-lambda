using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.SecurityToken;

namespace Aws.Faq.Scraper
{
    public class DynamoDBFaqPersister : IFaqPersister
    {
        [DynamoDBTable("service-faqs")]
        private class AwsFaq 
        {
            [DynamoDBHashKey("service-name")]
            public string Id {get;set;}

            [DynamoDBProperty]
            public string Content {get;set;} 
        }

        private readonly DynamoDBContext _ddbContext;
        private readonly AmazonDynamoDBClient _ddbClient = new AmazonDynamoDBClient();

        public DynamoDBFaqPersister()
        {
            _ddbContext = new DynamoDBContext(_ddbClient);
        }

        public async Task Persist(string faqName, Dictionary<string, string> faqContent)
        {
            var faqJson = JsonConvert.SerializeObject(faqContent, Formatting.Indented);
            var faq = new AwsFaq { Id = faqName, Content = faqJson };
            await _ddbContext.SaveAsync(faq);
        }
    }
}