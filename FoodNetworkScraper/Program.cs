using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingEngine;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace FoodNetworkScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Sitemap[] sitemaps = { new Sitemap(new Url("https://www.food.com/sitemap-1.xml.gz")), new Sitemap(new Url("https://www.food.com/sitemap-2.xml.gz")), new Sitemap(new Url("https://www.food.com/sitemap-3.xml.gz")), new Sitemap(new Url("https://www.food.com/sitemap-4.xml.gz")),new Sitemap(new Url("https://www.food.com/sitemap-5.xml.gz")), new Sitemap(new Url("https://www.food.com/sitemap-6.xml.gz")), new Sitemap(new Url("https://www.food.com/sitemap-7.xml.gz")) };
            ScrapeSitemap(sitemaps);
            Console.ReadKey();
        }

        static void ScrapeSitemap(Sitemap[] sitemaps)
        {
            Stopwatch clock = new Stopwatch();
            string siteName = sitemaps[0].Urls[0].DomainName;
            StreamWriter fs = new StreamWriter(siteName.Substring(0, siteName.LastIndexOf('.')) + ".json");
            SitemapRecipeScraper scraper = new SitemapRecipeScraper(sitemaps);

            clock.Start();
            scraper.Scrape();
            clock.Stop();
            string output = JsonConvert.SerializeObject(scraper.Recipes, Formatting.Indented);
            Console.WriteLine(output);
            Console.WriteLine($"recipes scraped {scraper.Recipes.Length}\ntime elapsed: {clock.Elapsed.Minutes}");
            fs.Write(output);
            fs.Close();
            //Console.ReadKey();

        }
    }
}
