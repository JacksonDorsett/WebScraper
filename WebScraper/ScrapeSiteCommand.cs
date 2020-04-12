using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using WebScrapingEngine;
using WebScrapingEngine.WPRM;
using Newtonsoft.Json;
using System.IO;

namespace WebScraper
{
    class ScrapeSiteCommand
    {
        Url url;
        int index;

        public ScrapeSiteCommand(Url url, int index)
        {
            this.url = url;
            this.index = index;
        }
        public void Execute()
        {
            Console.WriteLine($"Started Scraping {url.DomainName}");
            Stopwatch clock = new Stopwatch();

            
            WPRMJsonScraper scraper = new WPRMJsonScraper(url);

            clock.Start();
            scraper.Scrape();
            clock.Stop();
            string output = JsonConvert.SerializeObject(scraper.Recipes, Formatting.Indented);
            Console.WriteLine(output);
            Console.WriteLine($"recipes scraped {scraper.Recipes.Count}\ntime elapsed: {clock.Elapsed.Minutes}");
            StreamWriter fs = new StreamWriter(GetFileName(url));
            fs.Write(output);
            fs.Close();
            //Console.ReadKey();
        }

        string GetFileName(Url url)
        {
            string s = url.DomainName;
            s = s.Replace('.', '_');
            s += ".json";
            return s;
        }
    }
}
