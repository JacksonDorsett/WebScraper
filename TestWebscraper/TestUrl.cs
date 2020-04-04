// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using WebScrapingEngine;
namespace TestWebscraper
{
    [TestFixture]
    public class TestUrl
    {
        [Test]
        public void TestDomainProperty()
        {
            var url = new Url("https://panlasangpinoy.com/easy-kung-pao-shrimp-recipe-2/");
            Assert.AreEqual("panlasangpinoy.com", url.DomainName);
        }
        [Test]
        public void TestFilePathProperty()
        {
            var url = new Url("https://panlasangpinoy.com/easy-kung-pao-shrimp-recipe-2/");
            Assert.AreEqual(2, url.FilePath.Length);
            Assert.AreEqual("easy-kung-pao-shrimp-recipe-2", url.FilePath[0]);
            Assert.AreEqual(string.Empty, url.FilePath[1]);
        }
        [Test]
        public void TestFilePathPropertyWithQuery()
        {
            var url = new Url("https://panlasangpinoy.com/easy-kung-pao-shrimp-recipe-2/search?field=void&hello=world");
            Assert.AreEqual("easy-kung-pao-shrimp-recipe-2", url.FilePath[0]);
            Assert.AreEqual("search", url.FilePath[1]);
            var v = url.Query;
            Assert.AreEqual(2, url.Query.Parameters.Length);

        }
    }
}
