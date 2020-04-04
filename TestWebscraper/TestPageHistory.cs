using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebscraper
{
    using WebScrapingEngine;

    [TestFixture]
    public class TestPageHistory
    {
        [Test]
        public void TestStringInput()
        {
            PageHistory ph = new PageHistory();
            ph.Add("https://google.com");
            ph.Add("https://amazon.com");
            Assert.That(ph.CheckUrl("https://google.com"));
            Assert.That(ph.CheckUrl("https://amazon.com"));
            Assert.That(!ph.CheckUrl("https://yelp.com"));
        }
        [Test]
        public void TestUrlInput()
        {
            PageHistory ph = new PageHistory();
            ph.Add(new Url("https://google.com"));
            ph.Add(new Url("https://amazon.com"));
            Assert.That(ph.CheckUrl("https://google.com"));
            Assert.That(ph.CheckUrl("https://amazon.com"));
            Assert.That(!ph.CheckUrl("https://yelp.com"));
        }
    }
}
