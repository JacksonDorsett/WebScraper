// <copyright file="InternalLinkScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Recipe
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
        private Url[] startUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLinkScraper"/> class.
        /// </summary>
        /// <param name="history">history.</param>
        /// <param name="startingUrl">base url.</param>
        public InternalLinkScraper(PageHistory history, Url startingUrl)
        {
            this.History = history;
            this.startUrl = new Url[1];
            this.startUrl[0] = startingUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLinkScraper"/> class.
        /// </summary>
        /// <param name="history">history.</param>
        /// <param name="startUrls">urls.</param>
        public InternalLinkScraper(PageHistory history, Url[] startUrls)
        {
            this.History = history;
            Dictionary<string, bool> uniqueUrls = new Dictionary<string, bool>();
            var list = new List<Url>();
            foreach (var s in startUrls)
            {
                if (!uniqueUrls.ContainsKey(s.DomainName))
                {
                    uniqueUrls[s.DomainName] = true;
                    list.Add(s);
                }
            }

            this.startUrl = list.ToArray();
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
                    if (link != null && this.IsInternalDomain(link.Value))
                    {
                        Url url = new Url(link.Value);

                        if (!url.FullUrl.Contains('#') && !url.FullUrl.Contains('?') && !url.FullUrl.Contains(".jpg") && !url.FullUrl.Contains(".pdf") && !url.Contains("wprm_print") && !url.Contains("wp-content") && !url.FullUrl.Contains(".jpeg") && !this.History.CheckUrl(url.FullUrl))
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

        private bool IsInternalDomain(string url)
        {
            var uri = new Url(url);
            foreach (var link in this.startUrl)
            {
                if (link.DomainName == uri.DomainName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
