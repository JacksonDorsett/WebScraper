namespace WebScrapingEngine
{
    /// <summary>
    /// Scrapes webpage.
    /// </summary>
    /// <typeparam name="T">Object returned from scraping page</typeparam>
    public interface IPageScraper<T>
    {
        /// <summary>
        /// Scrapes page.
        /// </summary>
        /// <returns>Object containing data from scraped page.</returns>
        T ScrapePage();
    }
}