// <copyright file="WebScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Webscraper base class.
    /// </summary>
    /// <typeparam name="T">Html document class.</typeparam>
    /// <typeparam name="U">Object type being scraped.</typeparam>
    public abstract class WebScraper<T, U>
    {
        /// <summary>
        /// link scraping behavior.
        /// </summary>
        private readonly ILinkScraper<T> linkScraper;

        /// <summary>
        /// Page scraping behavior.
        /// </summary>
        private readonly IPageScraper<T, U> scraper;

        private Queue<Url> urlQueue;

        private PageHistory history;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScraper{T, U}"/> class.
        /// </summary>
        /// <param name="linkScraper">Link Scraper.</param>
        /// <param name="pageScraper">Page Scraper.</param>
        /// <param name="pageValidator">Page Validator.</param>
        protected WebScraper(ILinkScraper<T> linkScraper, IPageScraper<T, U> pageScraper)
        {
            this.History = new PageHistory();
            this.ScrapedObjects = new List<U>();
            this.linkScraper = linkScraper;
            this.scraper = pageScraper;
        }

        /// <summary>
        /// Gets a value indicating whether there is urls to scrape.
        /// </summary>
        public bool IsScrapeable { get => this.UrlQueue.Count != 0; }

        /// <summary>
        /// Gets list of Objects scraped.
        /// </summary>
        internal List<U> ScrapedObjects { get; private set; }

        /// <summary>
        /// Gets or sets UrlQueue.
        /// </summary>
        internal Queue<Url> UrlQueue { get => this.urlQueue; set => this.urlQueue = value; }

        /// <summary>
        /// Gets link scraping behavior.
        /// </summary>
        protected IPageScraper<T, U> Scraper => this.scraper;

        /// <summary>
        /// Gets Link scraping behavior.
        /// </summary>
        protected ILinkScraper<T> LinkScraper => this.linkScraper;

        /// <summary>
        /// Gets or sets History of the urls visited.
        /// </summary>
        protected PageHistory History { get => this.history; set => this.history = value; }

        /// <summary>
        /// Abstract Scraping Class.
        /// </summary>
        public abstract void Scrape();
    }
}
