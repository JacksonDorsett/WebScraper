using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingEngine
{
    /// <summary>
    /// Webscraper object.
    /// </summary>
    /// <typeparam name="T">Object being scraped.</typeparam>
    public class WebScraper<T>
    {
        protected readonly ILinkScraper linkScraper;
        protected readonly IPageValidator pageValidator;
        protected readonly IPageScraper<T> scraper;

        protected PageHistory history;
    }
}
