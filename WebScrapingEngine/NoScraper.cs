
namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    public class NoScraper : ILinkScraper<HtmlDocument>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoScraper"/> class.
        /// </summary>
        public NoScraper()
        {
        }

        /// <summary>
        /// scrapes no links.
        /// </summary>
        /// <param name="html">html.</param>
        /// <returns>no links.</returns>
        public Url[] ScrapeLinks(HtmlDocument html) => new Url[0];
    }
}
