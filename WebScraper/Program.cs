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
namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("WPRMSites.csv");
            List<Url> taskList = new List<Url>();
            ReusableThreadPool pool = new ReusableThreadPool(4);

            int index = 1;
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
            index = 0;
            ScrapeSite(new Url("https://www.organicfacts.net/recipe/smoked-salmon-spread.html"), 0);
            foreach (var task in taskList)
            {
                ScrapeSite(task, 0);
            }
                //if(pool.IsThreadAvailable())
                //{
                //    var task = taskQueue.Dequeue();
                //    pool.StartThread(task.Execute);
                //    index++;
                //}
                
                //ScrapeSite(new Url("https://apriljharris.com/vegetable-tagine-vegan-and-gluten-free/"), index++);
            
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
            scraper.Scrape();
            clock.Stop();
            string output = JsonConvert.SerializeObject(scraper.Recipes, Formatting.Indented);
            Console.WriteLine(output);
            Console.WriteLine($"recipes scraped {scraper.Recipes.Count}\ntime elapsed: {clock.Elapsed.Minutes}");
            fs.Write(output);
            fs.Close();
            //Console.ReadKey();
        }
        
    }
}
