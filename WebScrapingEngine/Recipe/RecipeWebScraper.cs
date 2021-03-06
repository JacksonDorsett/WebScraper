﻿// <copyright file="WPRMJsonScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using HtmlAgilityPack;

    /// <summary>
    /// Scrapes WPRM from json source.
    /// </summary>
    public class RecipeWebScraper : WebScraper<HtmlDocument, Recipe>
    {
        private readonly HtmlWeb web;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeWebScraper"/> class.
        /// </summary>
        /// <param name="url">url.</param>
        public RecipeWebScraper(Url url)
            : base(
                  new InternalLinkScraper(new PageHistory(), url),
                  new RecipePageScraper())
        {
            this.UrlQueue = new Queue<Url>();
            this.UrlQueue.Enqueue(url);
            this.web = new HtmlWeb();
        }

        public RecipeWebScraper(Url[] urls, ILinkScraper<HtmlDocument> linkScraper)
            : base(linkScraper, new RecipePageScraper())
        {
            this.web = new HtmlWeb();
            web.UserAgent = "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)";
            this.UrlQueue = new Queue<Url>();
            foreach (Url url in urls)
            {
                this.UrlQueue.Enqueue(url);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeWebScraper"/> class.
        /// </summary>
        /// <param name="url">url list.</param>
        public RecipeWebScraper(Url[] url)
            : base(
                  new InternalLinkScraper(new PageHistory(), url),
                  new RecipePageScraper())
        {
            this.web = new HtmlWeb();
            this.UrlQueue = new Queue<Url>();
            foreach (var u in url)
            {
                this.UrlQueue.Enqueue(u);
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
            if (this.UrlQueue.Count != 0)
            {
                var url = this.UrlQueue.Dequeue();
                try
                {
                    var html = this.web.Load(url.FullUrl);
                    var links = this.LinkScraper.ScrapeLinks(html);
                    foreach (var link in links)
                    {
                        this.UrlQueue.Enqueue(link);
                    }

                    Console.WriteLine($"scraped {links.Length} links from {url.FullUrl}");

                    Recipe r = this.Scraper.ScrapePage(html);
                    r.Url = url;
                    if (r != null)
                    {
                        this.Recipes.Add(r);
                        Console.WriteLine($"Added {r.Info.RecipeName} to list");
                    }

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        /// Continuously Scrapes until queue is empty.
        /// </summary>
        public void ScrapeAll()
        {
            while (this.UrlQueue.Count != 0)
            {
                this.Scrape();
            }
        }
    }
}
