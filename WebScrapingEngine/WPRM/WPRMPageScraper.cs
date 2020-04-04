using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingEngine.WPRM
{
    using HtmlAgilityPack;

    public class WPRMPageScraper : IPageScraper<HtmlDocument, Recipe>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
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
            try
            {
                return node.SelectSingleNode("//span[contains(@class, 'wprm-recipe-details wprm-recipe-author wprm-block-text-normal')]").InnerText;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetName(HtmlNode node)
        {
            try
            {
                return node.SelectSingleNode("//h2[contains(@class, 'wprm-recipe-name wprm-block-text-bold')]").InnerText;
            }
            catch
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

                    amt = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-amount')]").InnerText;
                    unit = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-unit')]").InnerText;
                    name = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'wprm-recipe-ingredient-name')]").InnerText;
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