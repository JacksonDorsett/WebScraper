// <copyright file="WPRMWebscraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    /// <summary>
    /// Scrapes All recipes from website that uses WPRM.
    /// </summary>
    public class WPRMWebscraper
        : WebScraper<HtmlAgilityPack.HtmlDocument, Recipe>
    {

        private Queue<Url> urlQueue;
        private HtmlWeb web;

        /// <summary>
        /// Initializes a new instance of the <see cref="WPRMWebscraper"/> class.
        /// </summary>
        /// <param name="baseUrl">starting seed url.</param>
        public WPRMWebscraper(string baseUrl)
            : base(
                  new InternalLinkScraper(
                      new PageHistory(),
                      new Url(baseUrl)),
                  new WPRMPageScraper(),
                  new WPRMPageValidator())
        {
            this.Recipes = new List<Recipe>();
            this.web = new HtmlWeb();
            this.urlQueue = new Queue<Url>();
            this.urlQueue.Enqueue( new Url(baseUrl));
        }

        /// <summary>
        /// Gets List of scraped recipes.
        /// </summary>
        public List<Recipe> Recipes { get; private set; }

        /// <summary>
        /// Runs Scraping algorithm.
        /// </summary>
        public override void Scrape()
        {
            while (this.urlQueue.Count != 0 && this.Recipes.Count < 100)
            {
                var url = this.urlQueue.Dequeue();
                var html = this.web.Load(url.FullUrl);

                var links = this.LinkScraper.ScrapeLinks(html);
                foreach (var link in links)
                {
                    this.urlQueue.Enqueue(link);
                }

                Console.WriteLine($"scraped {links.Length} links from {url.FullUrl}");
                if (this.PageValidator.ValidatePage(html))
                {
                    this.Recipes.Add(this.Scraper.ScrapePage(html));
                    Console.WriteLine($"Added {this.Recipes.Last().Name} to list");
                }
                else
                {
                    Console.WriteLine($"{url.FullUrl} did not contain a recipe.");
                }
                Console.WriteLine();
            }
        }
    }
}
