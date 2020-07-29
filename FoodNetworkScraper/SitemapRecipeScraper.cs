using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WebScrapingEngine;
using WebScrapingEngine.Recipe;
using HtmlAgilityPack;
namespace FoodNetworkScraper
{
    class SitemapRecipeScraper
    {
        RecipeWebScraper webScraper;
        public SitemapRecipeScraper(Sitemap[] sitemaps)
        {
            webScraper = new RecipeWebScraper(GetUrls(sitemaps), new NoScraper());
        }
        
        public Recipe[] Recipes { get => webScraper.Recipes.ToArray(); }

        public void Scrape()
        {
            while(this.webScraper.IsScrapeable)
            {
                this.webScraper.Scrape();
                
            }
        }

        private Url[] GetUrls(Sitemap[] sitemaps)
        {
            List<Url> urls = new List<Url>();
            foreach (var map in sitemaps)
            {
                foreach (var link in map.Urls)
                {
                    urls.Add(link);
                }
            }
            return urls.ToArray();
        }

    }
}
