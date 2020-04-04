

namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class WPRMWebscraper
        : WebScraper<HtmlAgilityPack.HtmlDocument, Recipe>
    {
        public WPRMWebscraper(string BaseUrl)
            : base(new InternalLinkScraper(new PageHistory(), new Url(BaseUrl)),
                  new WPRMPageScraper(), new WPRMPageValidator())
        {

        }
        public override void Scrape()
        {
            throw new NotImplementedException();
        }
    }
}
