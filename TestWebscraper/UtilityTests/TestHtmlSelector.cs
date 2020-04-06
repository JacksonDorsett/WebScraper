using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebscraper.UtilityTests
{
    using WebScrapingEngine.Utility;
    using HtmlAgilityPack;
    [TestFixture]
    public class TestHtmlSelector
    {
        [Test]
        public void TestSelectAll()
        {
            HtmlWeb web = new HtmlWeb();
            var html = web.Load("https://panlasangpinoy.com/chicken-and-liver-adobo/");
            var l = HtmlSelector.SelectAll(html.DocumentNode, "li", "class", "wprm-recipe-instruction");
            Assert.NotNull(l);
        }
    }
}
