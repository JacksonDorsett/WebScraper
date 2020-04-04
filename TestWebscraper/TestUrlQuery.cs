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
    public class TestUrlQuery
    {
        [Test]
        public void TestClenser()
        {
            UrlQuery q = new UrlQuery("search?q=c%23+find+first+occurrence+of+string+in+string&oq=c%23+find+first+&aqs=chrome.2.69i57j0l6j69i58.5824j0j4&sourceid=chrome&ie=UTF-8");
            Assert.AreEqual(q.Query, "?q=c%23+find+first+occurrence+of+string+in+string&oq=c%23+find+first+&aqs=chrome.2.69i57j0l6j69i58.5824j0j4&sourceid=chrome&ie=UTF-8");

        }
    }
}
