// <copyright file="RecipePageScraper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Recipe
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
    internal class RecipePageScraper
        : IPageScraper<HtmlDocument, Recipe>
    {
        /// <summary>
        /// scrapesJsonPage.
        /// </summary>
        /// <param name="doc">doc.</param>
        /// <returns>scraped recipe.</returns>
        public Recipe ScrapePage(HtmlDocument doc)
        {
            var recipe = this.FindRecipeSchema(doc);

            if (recipe != null)
            {
                return new Recipe(
                this.GetRecipeInfo(recipe),
                this.GetIngredients(recipe),
                this.GetInstructions(recipe),
                this.GetUrl(recipe));
            }

            return null;
        }

        private JObject FindRecipeSchema(HtmlDocument doc)
        {
            foreach (var node in doc.DocumentNode.SelectNodes("//script[contains(@type, 'application/ld+json')]"))
            {
                string s = node.InnerText;
                JToken json = JToken.Parse(s);
                JObject recipe;

                if (json.GetType() == typeof(JArray))
                {
                    recipe = this.FindRecipe((JArray)json);

                    if (recipe != null)
                    {
                        return recipe;
                    }
                }

                if (json.GetType() == typeof(JObject))
                {
                    if ((JArray)json["@graph"] != null)
                    {
                        recipe = this.FindRecipe((JArray)json["@graph"]);
                    }
                    else
                    {
                        recipe = (JObject)json;
                    }
                    return recipe;
                }
            }

            return null;
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
            if (obj["cookTime"] != null && obj["cookTime"].ToString() != string.Empty)
            {
                return new TimeCodeParser().Parse((string)obj["cookTime"]);
            }

            return 0;
        }

        private int GetPrepTime(JObject obj)
        {
            if (obj["prepTime"] != null && obj["prepTime"].ToString() != string.Empty)
            {
                return new TimeCodeParser().Parse((string)obj["prepTime"]);
            }

            return 0;
        }

        private string[] GetCuisine(JObject obj)
        {
            if (obj["recipeCuisine"] != null)
            {
                return obj["recipeCuisine"].ToObject<string[]>();
            }

            return null;
        }

        private string[] GetRecipeType(JObject obj)
        {
            if (obj["recipeCategory"] != null)
            {
                return obj["recipeCategory"].ToObject<string>().Split(',');
            }

            return null;
        }

        private string GetRecipeName(JObject obj)
        {
            if (obj["name"] == null)
            {
                return null;
            }

            return obj["name"].ToString();
        }

        private string GetAuthor(JObject obj)
        {

            if (obj["author"] != null)
            {
                if (obj["author"].HasValues)
                {
                    return (obj["author"] as JObject)["name"].ToString();
                }

                return obj["author"].ToString();
            }

            return null;
            //if ((obj["author"] as JObject) == null)
            //{
            //    return null;
            //}
            
            //return (obj["author"] as JObject)["name"].ToString();
        }

        private InstructionSet[] GetInstructions(JObject obj)
        {
            List<InstructionSet> list = new List<InstructionSet>();

            if (!obj["recipeInstructions"].ToString().Contains("{") && !obj["recipeInstructions"].ToString().Contains("["))
            {
                var steps = new List<string>();
                steps.Add(obj["recipeInstructions"].ToString());
                list.Add(new InstructionSet(null, steps.ToArray()));
                return list.ToArray();
            }

            string prevType = obj["recipeInstructions"][0]["@type"].ToString();

            List<string> instructions = new List<string>();
            foreach (var section in obj["recipeInstructions"])
            {
                if (prevType == section["@type"].ToString())
                {
                    if (section["@type"].ToString() == "HowToSection")
                    {
                        List<string> steps = new List<string>();
                        var name = section["name"].ToString();

                        foreach (JObject step in section["itemListElement"])
                        {
                            steps.Add(step["text"].ToString());
                        }

                        list.Add(new InstructionSet(name, steps.ToArray()));
                    }

                    if (section["@type"].ToString() == "HowToStep")
                    {
                        instructions.Add(section["text"].ToString());
                    }
                }
                else
                {
                    if (prevType == "HowToStep" && section["@type"].ToString() == "HowToSection")
                    {
                        list.Add(new InstructionSet(null, instructions.ToArray()));
                        instructions.Clear();

                        List<string> steps = new List<string>();
                        var name = section["name"].ToString();

                        foreach (JObject step in section["itemListElement"])
                        {
                            steps.Add(step["text"].ToString());
                        }

                        list.Add(new InstructionSet(name, steps.ToArray()));
                    }

                    if (prevType == "HowToSection" && section["@type"].ToString() == "HowToStep")
                    {
                        instructions.Add(section["text"].ToString());
                    }
                }

                prevType = section["@type"].ToString();
            }

            if (instructions.Count != 0)
            {
                list.Add(new InstructionSet(null, instructions.ToArray()));
            }

            return list.ToArray();
        }

        private string[] GetIngredients(JObject obj)
        {
            List<string> list = new List<string>();
            foreach (string ing in obj["recipeIngredient"])
            {
                list.Add(ing.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("   "," "));
            }

            return list.ToArray();
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
