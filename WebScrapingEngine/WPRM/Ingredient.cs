// <copyright file="Ingredient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Encapsulates an ingredient.
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="amount">amount of ingredient.</param>
        /// <param name="itemName">item name.</param>
        public Ingredient(string amount, string itemName)
        {
            this.Amount = amount;
            this.ItemName = itemName;
        }

        /// <summary>
        /// Gets or sets amount of ingredient.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets ingredient name.
        /// </summary>
        public string ItemName { get; set; }
    }
}
