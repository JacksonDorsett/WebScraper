

namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Ingredient
    {
        public Ingredient(string amount, string itemName)
        {
            this.Amount = amount;
            this.ItemName = itemName;
        }

        string Amount { get; set; }
        string ItemName { get; set; }
    }
}
