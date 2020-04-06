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

        [Test]
        public void TestDuplicateLinks()
        {
            PageHistory ph = new PageHistory();
            HtmlDocument doc = new HtmlDocument();
            string html = "<!DOCTYPE html><html><a href=\"https://google.com/help/\"></a>" +
                "<a href=\"https://google.com/privacy/\"></a></html>";
            doc.LoadHtml(html);
            var linkscraper = new InternalLinkScraper(ph, new Url("https://google.com/"));
            var list1 = linkscraper.ScrapeLinks(doc);
            Assert.AreEqual(2, list1.Length);

            Assert.AreEqual("https://google.com/help/", list1[0].FullUrl);
            Assert.AreEqual("https://google.com/privacy/", list1[1].FullUrl);
            var L2 = linkscraper.ScrapeLinks(doc);
            Assert.AreEqual(0, L2.Length);
        }
    }
}
