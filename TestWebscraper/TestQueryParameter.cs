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
    public class TestQueryParameter
    {
        [Test]
        public void TestStringConstructor()
        {
            UrlQueryParameter t = new UrlQueryParameter("field=.455234");
            Assert.AreEqual("field", t.Field);
            Assert.AreEqual(".455234", t.Value);

            var e = Assert.Throws<UrlParameterException>(() => new UrlQueryParameter("Hello"));
        }
        [Test]
        public void TestFieldsConstructor()
        {
            UrlQueryParameter t = new UrlQueryParameter("hello", "world");
            Assert.AreEqual("hello", t.Field);
            Assert.AreEqual("world", t.Value);
            Assert.AreEqual("hello=world", t.ToString());
        }
    }
}
