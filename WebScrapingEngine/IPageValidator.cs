namespace WebScrapingEngine
{
    /// <summary>
    /// Validates page.
    /// </summary>
    public interface IPageValidator
    {
        /// <summary>
        /// ChecksWhether page can be scraped.
        /// </summary>
        /// <returns>if page can be scraped.</returns>
        bool ValidatePage();
    }
}