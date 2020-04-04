using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingEngine.WPRM
{
    using HtmlAgilityPack;

    /// <summary>
    /// WPRM page validator.
    /// </summary>
    public class WPRMPageValidator : IPageValidator<HtmlDocument>
    {
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
