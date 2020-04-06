// <copyright file="WPRMPageValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    /// <summary>
    /// WPRM page validator.
    /// </summary>
    public class WPRMPageValidator : IPageValidator<HtmlDocument>
    {
        /// <summary>
        /// Validates Page.
        /// </summary>
        /// <param name="page">page being scraped.</param>
        /// <returns>if page can be scraped.</returns>
        public bool ValidatePage(HtmlDocument page)
        {
            foreach (var item in page.DocumentNode.SelectNodes("//div[contains(@class, 'wprm-recipe-container')]"))
            {
                return true;
            }

            return false;
        }
    }
}
