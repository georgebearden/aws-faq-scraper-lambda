using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Aws.Faq.Scraper
{
    public class Function
    {
        private readonly IKernel _kernel = new StandardKernel(new ScraperModule());
        private readonly EntryPoint _entryPoint;

        public Function()
        {
            _entryPoint = _kernel.Get<EntryPoint>();
        }
        
        public async Task FunctionHandler(string awsServiceType, ILambdaContext context)
        {
            var service = Enum.Parse<AwsServiceTypes>(awsServiceType);
            await _entryPoint.GetFaq(service);
        }
    }
}
