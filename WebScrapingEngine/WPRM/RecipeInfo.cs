// <copyright file="RecipeInfo.cs" company="PlaceholderCompany">
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
    /// Information for recipe.
    /// </summary>
    public class RecipeInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeInfo"/> class.
        /// </summary>
        /// <param name="recipeName">name of recipe.</param>
        /// <param name="author">author of recipe.</param>
        /// <param name="cookTime">cook time of recipe.</param>
        /// <param name="prepTime">prep time of recipe.</param>
        public RecipeInfo(string recipeName, int cookTime, int prepTime, string author)
        {
            this.RecipeName = recipeName;
            this.Author = author;
            this.PrepTime = prepTime;
            this.CookTime = cookTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeInfo"/> class.
        /// </summary>
        public RecipeInfo()
        {
        }

        /// <summary>
        /// Gets or sets Name of recipe.
        /// </summary>
        public string RecipeName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets cook time.
        /// </summary>
        public int CookTime { get; set; }

        /// <summary>
        /// Gets or sets prep time.
        /// </summary>
        public int PrepTime { get; set; }

        /// <summary>
        /// Gets or sets author.
        /// </summary>
        public string Author
        {
            get;
            set;
        }
    }
}
