// <copyright file="ILinkScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Link scraper interface.
    /// </summary>
    /// <typeparam name="T">Html Parser used.</typeparam>
    public interface ILinkScraper<T>
    {
        /// <summary>
        /// Collects the Page Links.
        /// </summary>
        /// <param name="htmlDoc">Html page to be parsed.</param>
        /// <returns>list of page links.</returns>
        Url[] ScrapeLinks(T htmlDoc);
    }
}
