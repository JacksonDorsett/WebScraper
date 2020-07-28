﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingEngine;
namespace TestWebscraper
{
    [TestFixture]
    public class TestSitemap
    {
        [Test]
        public void TestGetUrls()
        {
            Sitemap map = new Sitemap(new Url("https://www.allrecipes.com/sitemap.xml"));
            var urls = map.GetUrls;
            Assert.AreEqual(10, urls.Length);
        }
    }
}
