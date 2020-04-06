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

    /// <summary>
    /// Scrapes All recipes from website that uses WPRM.
    /// </summary>
    public class WPRMWebscraper
        : WebScraper<HtmlAgilityPack.HtmlDocument, Recipe>
    {
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
        }

        /// <summary>
        /// Runs Scraping algorithm.
        /// </summary>
        public override void Scrape()
        {
            throw new NotImplementedException();
        }
    }
}
