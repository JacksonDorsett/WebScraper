// <copyright file="WPRMJsonPageScaper.cs" company="PlaceholderCompany">
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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Scrapes WPRM source data.
    /// </summary>
    internal class WPRMJsonPageScaper
        : IPageScraper<HtmlDocument, Recipe>
    {
        /// <summary>
        /// scrapesJsonPage.
        /// </summary>
        /// <param name="doc">doc.</param>
        /// <returns>scraped recipe.</returns>
        public Recipe ScrapePage(HtmlDocument doc)
        {
            var node = doc.DocumentNode.SelectSingleNode("//script[contains(@type, 'application/ld+json')]");
            string s = node.InnerText;
            JObject json = JObject.Parse(s);

            JObject recipe;
            if ((JArray)json["@graph"] != null)
            {
                recipe = this.FindRecipe((JArray)json["@graph"]);
            }
            else
            {
                recipe = json;
            }
            

            return new Recipe(
                this.GetRecipeInfo(recipe),
                this.GetIngredients(doc.DocumentNode),
                this.GetInstructions(recipe),
                this.GetUrl(recipe));
        }

        private JObject FindRecipe(JArray array)
        {
            foreach (JObject obj in array)
            {
                if (obj.ContainsKey("@type") && obj["@type"].ToString() == "Recipe")
                {
                    return obj;
                }
            }

            return null;
        }

        private Url GetUrl(JObject obj)
        {
            if (obj["@id"] == null)
            {
                return null;
            }

            return new Url(obj["@id"].ToString().Replace("#recipe", string.Empty));
        }

        private RecipeInfo GetRecipeInfo(JObject obj)
        {
            return new RecipeInfo(
                this.GetRecipeName(obj),
                this.GetCookTime(obj),
                this.GetPrepTime(obj),
                this.GetAuthor(obj),
                this.GetRecipeType(obj),
                this.GetCuisine(obj),
                this.GetYeild(obj));
        }

        private string GetYeild(JObject obj)
        {
            if (obj["recipeYield"] != null)
            {
                return obj["recipeYield"].ToString();
            }

            return null;
        }

        private int GetCookTime(JObject obj)
        {
            if (obj["cookTime"] != null)
            {
                return this.ParseTimeStamp(obj["cookTime"].ToString());
            }

            return 0;
        }

        private int GetPrepTime(JObject obj)
        {
            if (obj["prepTime"] != null)
            {
                return this.ParseTimeStamp(obj["prepTime"].ToString());
            }

            return 0;
        }

        private int ParseTimeStamp(string stamp)
        {
            return int.Parse(stamp.Replace("PT", string.Empty).Replace("M", string.Empty));
        }

        private string[] GetCuisine(JObject obj)
        {
            return obj["recipeCuisine"].ToObject<string[]>();
        }

        private string[] GetRecipeType(JObject obj)
        {
            return obj["recipeCategory"].ToObject<string[]>();
        }

        private string GetRecipeName(JObject obj)
        {
            return obj["name"].ToString();
        }

        private string GetAuthor(JObject obj)
        {
            return (obj["author"] as JObject)["name"].ToString();
        }

        private string[] GetInstructions(JObject obj)
        {
            List<string> instructions = new List<string>();
            foreach (JObject instruct in obj["recipeInstructions"])
            {
                instructions.Add(instruct["text"].ToString());
            }

            return instructions.ToArray();
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
    }
}
