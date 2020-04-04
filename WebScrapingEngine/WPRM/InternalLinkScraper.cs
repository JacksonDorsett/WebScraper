
namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    /// <summary>
    /// Scapes Links only on the website.
    /// </summary>
    public class InternalLinkScraper : ILinkScraper<HtmlDocument>
    {
        private PageHistory history;
        private Url startUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLinkScraper"/> class.
        /// </summary>
        /// <param name="history">history.</param>
        /// <param name="startingUrl">base url.</param>
        public InternalLinkScraper(PageHistory history, Url startingUrl)
        {
            this.history = history;
            this.startUrl = startingUrl;
        }

        /// <summary>
        /// scrapes all links with same domain.
        /// </summary>
        /// <param name="html">html to scrape.</param>
        /// <returns>list of urls.</returns>
        public Url[] ScrapeLinks(HtmlDocument html)
        {
            List<Url> list = new List<Url>();
            foreach (var node in html.DocumentNode.SelectNodes("//a"))
            {
                
                try
                {
                    Url url = new Url(node.Attributes["href"].Value);
                    if (!this.history.CheckUrl(url.FullUrl) && this.startUrl.DomainName == url.DomainName)
                    {
                        list.Add(url);
                    }
                }
                catch
                {
                }
            }
            return list.ToArray();
        }
    }
}
