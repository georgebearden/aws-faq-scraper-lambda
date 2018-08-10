using System;
using Ninject.Modules;

namespace Aws.Faq.Scraper
{
    public class ScraperModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IFaqPersister>().To<DynamoDBFaqPersister>().InSingletonScope();
            this.Bind<IFaqResolver>().To<HttpsFaqResolver>().InSingletonScope();
            this.Bind<IFaqScraper>().To<FaqScraper>().InSingletonScope();
            this.Bind<EntryPoint>().ToSelf().InSingletonScope();
        }
    }
}

