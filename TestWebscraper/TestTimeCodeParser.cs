using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingEngine.Recipe;
namespace TestWebscraper
{
    [TestFixture]
    public class TestTimeCodeParser
    {
        [TestCase("PT35M",35)]
        [TestCase("PT1H", 60)]
        [TestCase("PT1H30M", 90)]
        [TestCase("PT0S", 0)]
        [TestCase("AB0S", -1)]
        public void TestParse(string timecode, int timeInMinutes)
        {
            TimeCodeParser tcp = new TimeCodeParser();
            Assert.That(tcp.Parse(timecode) == timeInMinutes);
        }
    }
}
