// <copyright file="IPageScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    /// <summary>
    /// Interface for Scraping webpage.
    /// </summary>
    /// <typeparam name="T">Html doc type.</typeparam>
    /// <typeparam name="U">Object being scraped.</typeparam>
    public interface IPageScraper<T, U>
    {
        /// <summary>
        /// Scrapes page.
        /// </summary>
        /// <param name="doc">html doc to be scraped.</param>
        /// <returns>Type u.</returns>
        U ScrapePage(T doc);
    }
}