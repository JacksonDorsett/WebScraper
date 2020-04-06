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
                this.GetAuthor(wprmContainer),
                this.GetName(wprmContainer),
                this.GetIngredients(wprmContainer),
                this.GetInstructions(wprmContainer));
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

            //var authorNode = node.SelectSingleNode("//span[contains(@class, 'wprm-recipe-details wprm-recipe-author wprm-block-text-normal')]");

            //if (authorNode != null)
            //{
            //    return authorNode.InnerText;
            //}
            //else
            //{
            //    return string.Empty;
            //}
        }

        private string GetName(HtmlNode node)
        {
            var nameNode = node.SelectSingleNode("//h2[contains(@class, 'wprm-recipe-name wprm-block-text-bold')]");

            if (nameNode != null)
            {
                return nameNode.InnerText;
            }
            else
            {
                return string.Empty;
            }
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
                catch
                {
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
    }
}