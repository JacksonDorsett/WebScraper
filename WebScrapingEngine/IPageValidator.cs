// <copyright file="IPageValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine
{
    /// <summary>
    /// Validate page.
    /// </summary>
    /// <typeparam name="T">Html document.</typeparam>
    public interface IPageValidator<T>
    {
        /// <summary>
        /// Validate Page.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <returns>if page can be extracted.</returns>
        bool ValidatePage(T page);
    }
}