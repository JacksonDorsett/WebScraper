
namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    public class WPRMJsonScraper : WebScraper<HtmlDocument, Recipe>
    {
        Queue<Url> urlQueue;

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
        }

        public WPRMJsonScraper(Url[] url)
            : base(
                  new InternalLinkScraper(new PageHistory(), url),
                  new WPRMJsonPageScaper(),
                  new WPRMPageValidator())
        {
            this.urlQueue = new Queue<Url>();
            foreach (var u in url)
            {
                this.urlQueue.Enqueue(u);
            }
        }

        public List<Recipe> Recipes { get => this.ScrapedObjects; }

        public override void Scrape()
        {
            HtmlWeb web = new HtmlWeb();
            while (this.urlQueue.Count != 0 &&Recipes.Count < 10)
            {
                var url = this.urlQueue.Dequeue();
                try
                {
                    var html = web.Load(url.FullUrl);
                    

                    var links = this.LinkScraper.ScrapeLinks(html);
                    foreach (var link in links)
                    {
                        this.urlQueue.Enqueue(link);
                    }

                    Console.WriteLine($"scraped {links.Length} links from {url.FullUrl}");
                    if (this.PageValidator.ValidatePage(html))
                    {
                        Recipe r = this.Scraper.ScrapePage(html);
                        this.Recipes.Add(r);
                        Console.WriteLine($"Added {r.Info.RecipeName} to list");
                    }
                    else
                    {
                        Console.WriteLine($"{url.FullUrl} did not contain a recipe.");
                    }

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
