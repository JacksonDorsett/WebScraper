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

    class WPRMJsonPageScaper
        : IPageScraper<HtmlDocument, Recipe>
    {

        /// <summary>
        /// scrapesJsonPage
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public Recipe ScrapePage(HtmlDocument doc)
        {
            var node = doc.DocumentNode.SelectSingleNode("//script[contains(@type, 'application/ld+json')]");
            string s = node.InnerText;
            JObject json = JObject.Parse(s);

            JObject recipe = this.FindRecipe((JArray)json["@graph"]);

            return new Recipe(GetRecipeInfo(recipe), this.GetIngredients(doc.DocumentNode), this.GetInstructions(recipe));
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

        private RecipeInfo GetRecipeInfo(JObject obj)
        {
            return new RecipeInfo(GetRecipeName(obj),0,0,GetAuthor(obj));
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
