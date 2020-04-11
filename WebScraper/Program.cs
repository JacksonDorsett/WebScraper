using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WebScrapingEngine;
using WebScrapingEngine.WPRM;
using Newtonsoft.Json;
using System.IO;
namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch clock = new Stopwatch();
            
            Url url = new Url("https://www.theseasonedmom.com/strawberry-bread/");
            StreamWriter fs = new StreamWriter(GetFileName(url));
            WPRMJsonScraper scraper = new WPRMJsonScraper(url);

            clock.Start();
            scraper.Scrape();
            clock.Stop();
            string output = JsonConvert.SerializeObject(scraper.Recipes, Formatting.Indented);
            Console.WriteLine(output);
            Console.WriteLine($"recipes scraped {scraper.Recipes.Count}\ntime elapsed: {clock.Elapsed.Minutes}");
            fs.Write(output);
            fs.Close();
            Console.ReadKey();
        }
        static string GetFileName(Url url)
        {
            string s = url.DomainName.Substring(0, url.DomainName.LastIndexOf('.'));
            s = s.Replace('.', '_');
            s += ".json";
            return s;
        }
    }
}
