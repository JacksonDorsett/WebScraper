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
        public RecipeInfo(string recipeName, string author)
        {
            this.RecipeName = recipeName;
            this.Author = author;
        }

        /// <summary>
        /// Gets Name of recipe.
        /// </summary>
        public string RecipeName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets cook time.
        /// </summary>
        public int CookTime { get; private set; }

        /// <summary>
        /// Gets prep time.
        /// </summary>
        public int PrepTime { get; private set; }

        /// <summary>
        /// Gets author.
        /// </summary>
        public string Author
        {
            get;
            private set;
        }



    }
}
