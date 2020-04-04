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
    public abstract class WebScraper<T>
    {
        /// <summary>
        /// Objects scraped.
        /// </summary>
        protected List<T> ScrapedObjects { get; private set; }

        protected readonly ILinkScraper linkScraper;
        protected readonly IPageValidator pageValidator;
        protected readonly IPageScraper<T> scraper;
        protected PageHistory history;


        protected WebScraper(ILinkScraper linkScraper, IPageScraper<T> pageScraper, IPageValidator pageValidator)
        {
            this.history = new PageHistory();
            this.ScrapedObjects = new List<T>();
        }

        /// <summary>
        /// Abstract Scraping Class.
        /// </summary>
        public abstract void Scrape();


    }
}
