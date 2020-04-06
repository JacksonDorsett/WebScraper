﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebScrapingEngine;

namespace TestWebscraper.WPRM
{
    [TestFixture]
    public class TestWPRMPageScraper
    {
        [Test]
        public void TestGetAuthor()
        {
            HtmlWeb web = new HtmlWeb();
            var html = web.Load("https://panlasangpinoy.com/chicken-and-liver-adobo/");
            var scraper = new WebScrapingEngine.WPRM.WPRMPageScraper();
            var r = scraper.ScrapePage(html);
            Assert.AreEqual("Vanjo Merano", r.Author);
        }
    }
}