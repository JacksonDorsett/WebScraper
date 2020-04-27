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
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net;
namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestAsyncWebclient();

            StreamReader sr = new StreamReader("WPRMSites.csv");
            List<Url> taskList = new List<Url>();

            foreach (string s in sr.ReadToEnd().Split('\n'))
            {
                try
                {
                    taskList.Add(new Url(s.Substring(0, s.IndexOf(';'))));
                }
                catch
                {

                }

            }

            foreach (var task in taskList)
            {
                ScrapeSite(task, 0);
            }


            Console.ReadKey();
        }
        static string GetFileName(Url url)
        {
            string s = url.DomainName;
            s = s.Replace('.', '_');
            s += ".json";
            return s;
        }

        static void ScrapeSite(Url url, int index)
        {
            Stopwatch clock = new Stopwatch();

            StreamWriter fs = new StreamWriter(index + GetFileName(url));
            WPRMJsonScraper scraper = new WPRMJsonScraper(url);

            clock.Start();
            scraper.ScrapeAll();
            clock.Stop();
            string output = JsonConvert.SerializeObject(scraper.Recipes, Formatting.Indented);
            Console.WriteLine(output);
            Console.WriteLine($"recipes scraped {scraper.Recipes.Count}\ntime elapsed: {clock.Elapsed.Minutes}");
            fs.Write(output);
            fs.Close();
            //Console.ReadKey();
        }

        static void RunWithDiagnostics(Url url)
        {
            WPRMJsonScraper scraper = new WPRMJsonScraper(url);
            ScraperDiagnostics<HtmlDocument, Recipe> diagnostics = new ScraperDiagnostics<HtmlDocument, Recipe>(scraper);
        }

    }
}
