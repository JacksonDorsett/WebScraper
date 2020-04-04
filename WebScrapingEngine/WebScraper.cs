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
    public abstract class WebScraper<T,U>
    {
        protected PageHistory history;
        private readonly ILinkScraper<T> linkScraper;
        private readonly IPageValidator<T> pageValidator;
        private readonly IPageScraper<T, U> scraper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScraper{T}"/> class.
        /// </summary>
        /// <param name="linkScraper">Link Scraper.</param>
        /// <param name="pageScraper">Page Scraper.</param>
        /// <param name="pageValidator">Page Validator.</param>
        protected WebScraper(ILinkScraper<T> linkScraper, IPageScraper<T, U> pageScraper, IPageValidator<T> pageValidator)
        {
            this.history = new PageHistory();
            this.ScrapedObjects = new List<T>();
            this.linkScraper = linkScraper;
            this.pageValidator = pageValidator;
            this.scraper = pageScraper;
        }

        /// <summary>
        /// Gets list of Objects scraped.
        /// </summary>
        protected List<T> ScrapedObjects { get; private set; }

        /// <summary>
        /// Abstract Scraping Class.
        /// </summary>
        public abstract void Scrape();
    }
}
