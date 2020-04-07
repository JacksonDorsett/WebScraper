// <copyright file="InternalLinkScraper.cs" company="PlaceholderCompany">
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
    /// Scapes Links only on the website.
    /// </summary>
    public class InternalLinkScraper : ILinkScraper<HtmlDocument>
    {
        private PageHistory history;
        private Url startUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLinkScraper"/> class.
        /// </summary>
        /// <param name="history">history.</param>
        /// <param name="startingUrl">base url.</param>
        public InternalLinkScraper(PageHistory history, Url startingUrl)
        {
            this.History = history;
            this.startUrl = startingUrl;
        }

        /// <summary>
        /// Gets or sets history.
        /// </summary>
        internal PageHistory History { get => this.history; set => this.history = value; }

        /// <summary>
        /// scrapes all links with same domain.
        /// </summary>
        /// <param name="html">html to scrape.</param>
        /// <returns>list of urls.</returns>
        public Url[] ScrapeLinks(HtmlDocument html)
        {
            List<Url> list = new List<Url>();

            var nodes = html.DocumentNode.SelectNodes("//a");
            if (nodes == null)
            {
                return list.ToArray();
            }

            foreach (var node in html.DocumentNode.SelectNodes("//a"))
            {
                try
                {
                    var link = node.Attributes["href"];
                    if (link != null && link.Value.Contains(this.startUrl.DomainName))
                    {
                        Url url = new Url(link.Value);

                        if (!url.FullUrl.Contains('#') && !url.FullUrl.Contains('?') && !this.History.CheckUrl(url.FullUrl) && this.startUrl.DomainName == url.DomainName)
                        {
                            list.Add(url);
                            this.History.Add(url.FullUrl);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return list.ToArray();
        }
    }
}
