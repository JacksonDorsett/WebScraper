// <copyright file="WPRMPageScraper.cs" company="PlaceholderCompany">
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
    /// Recipe page scraper.
    /// </summary>
    public class WPRMPageScraper : IPageScraper<HtmlDocument, Recipe>
    {
        /// <summary>
        /// Scrapes page for WPRM recipe.
        /// </summary>
        /// <param name="doc">document being scraped.</param>
        /// <returns>returns recipe.</returns>
        public Recipe ScrapePage(HtmlDocument doc)
        {
            var wprmContainer = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'wprm-recipe-container')]");
            return new Recipe(
                this.GetRecipeInfo(wprmContainer),
                this.GetIngredients(wprmContainer),
                null,
                new Url(string.Empty));
        }

        private string GetAuthor(HtmlNode node)
        {
            var curNode = Utility.HtmlSelector.SearchHtmlByInnerText(node, "Author");
            if (curNode != null)
            {
                curNode = curNode.NextSibling;
                if (curNode != null)
                {
                    return curNode.InnerText;
                }
            }

            return string.Empty;
        }

        private string GetName(HtmlNode node)
        {
            HtmlNode nameNode;
            for (int i = 1; i <= 4; ++i)
            {
                nameNode = node.SelectSingleNode("//h" + i);
                if (nameNode != null)
                {
                    return nameNode.InnerText;
                }
            }

            return string.Empty;
        }

        private Ingredient[] GetIngredients(HtmlNode node)
        {
            List<Ingredient> list = new List<Ingredient>();
            foreach (var item in node.SelectNodes("//li[contains(@class, 'wprm-recipe-ingredient')]"))
            {
                try
                {
                    string amt = string.Empty;
                    string unit = string.Empty;
                    string name = string.Empty;
                    string note = string.Empty;
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(item.InnerHtml);

                    var amountNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-amount')]");
                    if (amountNode != null)
                    {
                        amt = amountNode.InnerText;
                    }

                    var unitNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-unit')]");
                    if (unitNode != null)
                    {
                        unit = unitNode.InnerText;
                    }

                    var nameNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-name')]");
                    if (nameNode != null)
                    {
                        name = nameNode.InnerText;
                    }

                    list.Add(new Ingredient(amt + ' ' + unit, name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return list.ToArray();
        }

        private string[] GetInstructions(HtmlNode node)
        {
            List<string> list = new List<string>();
            foreach (var instruction in node.SelectNodes("//div[contains(@class, 'wprm-recipe-instruction-text')]"))
            {
                list.Add(instruction.InnerText);
            }

            return list.ToArray();
        }

        private RecipeInfo GetRecipeInfo(HtmlNode node)
        {
            return new RecipeInfo(
                this.GetName(node),
                this.GetCookTime(node),
                this.GetPrepTime(node),
                this.GetAuthor(node),
                null,
                null,
                string.Empty);
        }

        private int GetPrepTime(HtmlNode node)
        {
            int total = 0;
            node = Utility.HtmlSelector.SearchHtmlByInnerText(node, "Prep Time");
            total += this.GetMinutes(node) + (this.GetHours(node) * 60);
            return total;
        }

        /// <summary>
        /// Gets cook time.
        /// </summary>
        /// <param name="node">cook time.</param>
        /// <returns>time cooking.</returns>
        private int GetCookTime(HtmlNode node)
        {
            return 0;
        }

        private int GetMinutes(HtmlNode node)
        {
            // doc.SelectSingleNode("//*[text()[contains(., 'minutes')]]");
            int min = 0;

            if (node != null)
            {
                if (node.ParentNode != null)
                {
                    node = node.ParentNode;
                    int.TryParse(node.PreviousSibling.InnerText, out min);
                }
            }

            return min;
        }

        private int GetHours(HtmlNode node)
        {
            int hr = 0;
            if (Utility.HtmlSelector.SearchHtmlByInnerText(node, "hour") != null)
            {
                node = Utility.HtmlSelector.SearchHtmlByInnerText(node, "hour");
            }
            else if (Utility.HtmlSelector.SearchHtmlByInnerText(node, "hours") != null)
            {
                node = Utility.HtmlSelector.SearchHtmlByInnerText(node, "hours");
            }
            else
            {
                return 0;
            }

            if (node.PreviousSibling != null)
            {
                int.TryParse(node.PreviousSibling.InnerText, out hr);
            }

            return hr;
        }
    }
}