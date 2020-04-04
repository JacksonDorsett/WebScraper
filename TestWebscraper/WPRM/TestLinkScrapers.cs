using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebScrapingEngine.WPRM;
using WebScrapingEngine;

namespace TestWebscraper.WPRM
{
    [TestFixture]
    public class TestInteranlLinkScraper
    {
        [Test]
        public void TestNormalHtml()
        {
            PageHistory ph = new PageHistory();
            HtmlWeb web = new HtmlWeb();
            Url sUrl = new Url("https://www.allrecipes.com/recipe/35151/traditional-filipino-lumpia/");
            var html = web.Load("https://www.allrecipes.com/recipe/35151/traditional-filipino-lumpia/");
            var linkscraper = new InternalLinkScraper(ph, sUrl);
            var v = linkscraper.ScrapeLinks(html);
            foreach (var link in v)
            {
                Assert.AreEqual(sUrl.DomainName, link.DomainName);
            }
            
        }
    }
}
