﻿// <copyright file="WPRMJsonScraper.cs" company="PlaceholderCompany">
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
    /// Scrapes WPRM from json source.
    /// </summary>
    public class WPRMJsonScraper : WebScraper<HtmlDocument, Recipe>
    {
        HtmlWeb web;

        /// <summary>
        /// Initializes a new instance of the <see cref="WPRMJsonScraper"/> class.
        /// </summary>
        /// <param name="url">url.</param>
        public WPRMJsonScraper(Url url)
            : base(
                  new InternalLinkScraper(new PageHistory(), url),
                  new WPRMJsonPageScaper(),
                  new WPRMPageValidator())
        {
            this.urlQueue = new Queue<Url>();
            this.urlQueue.Enqueue(url);
            this.web = new HtmlWeb();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WPRMJsonScraper"/> class.
        /// </summary>
        /// <param name="url">url list.</param>
        public WPRMJsonScraper(Url[] url)
            : base(
                  new InternalLinkScraper(new PageHistory(), url),
                  new WPRMJsonPageScaper(),
                  new WPRMPageValidator())
        {
            this.web = new HtmlWeb();
            this.urlQueue = new Queue<Url>();
            foreach (var u in url)
            {
                this.urlQueue.Enqueue(u);
            }
        }

        /// <summary>
        /// Gets List of recipes.
        /// </summary>
        public List<Recipe> Recipes { get => this.ScrapedObjects; }

        /// <summary>
        /// Scrapes the links.
        /// </summary>
        public override void Scrape()
        {
            // this.Diagnostics.Start();

            if (this.urlQueue.Count != 0)
            {
                var url = this.urlQueue.Dequeue();
                try
                {
                    var html = this.web.Load(url.FullUrl);
                    var links = this.LinkScraper.ScrapeLinks(html);
                    foreach (var link in links)
                    {
                        this.urlQueue.Enqueue(link);
                    }

                    Console.WriteLine($"scraped {links.Length} links from {url.FullUrl}");

                    Recipe r = this.Scraper.ScrapePage(html);
                    if (r != null)
                    {
                        this.Recipes.Add(r);
                        Console.WriteLine($"Added {r.Info.RecipeName} to list");
                    }

                    
                    //this.Diagnostics.Update();
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //this.Diagnostics.Stop();
        }

        /// <summary>
        /// Continuously Scrapes until queue is empty.
        /// </summary>
        public void ScrapeAll()
        {
            while (this.urlQueue.Count != 0)
            {
                this.Scrape();
            }
        }
    }
}
