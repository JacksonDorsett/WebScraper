// <copyright file="HtmlSelector.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    /// <summary>
    /// Html Selector utility functions.
    /// </summary>
    public class HtmlSelector
    {
        public static HtmlNodeCollection SelectAll(HtmlNode node, string tag, string identifier, string value)
        {
            return node.SelectNodes($"//{tag}[contains(@{identifier}, '{value}')]");
        }

        public static HtmlNode SearchHtmlByInnerText(HtmlNode doc, string keyword)
        {
            return doc.SelectSingleNode("//*[text()[contains(., '" + keyword + "')]]");
        }
    }
}
