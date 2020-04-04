using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebscraper.WPRM
{
    using HtmlAgilityPack;
    using WebScrapingEngine.WPRM;
    [TestFixture]
    public class TestPageValidator
    {
        [Test]
        public void TestValidatorForTrue()
        {
            HtmlWeb web = new HtmlWeb();

            var v = web.Load("https://panlasangpinoy.com/chicken-and-liver-adobo/");

            WPRMPageValidator validator = new WPRMPageValidator();
            Assert.That(validator.ValidatePage(v));
        }
    }
}
