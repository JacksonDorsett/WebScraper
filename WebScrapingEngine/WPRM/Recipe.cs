﻿// <copyright file="Recipe.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.WPRM
{
    /// <summary>
    /// Recipe class.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="author">author.</param>
        /// <param name="name">name of dish.</param>
        /// <param name="ingredients">ingredients.</param>
        /// <param name="instructions">instructions.</param>
        public Recipe(RecipeInfo info, Ingredient[] ingredients, string[] instructions)
        {
            // this.Author = author;
            // this.Name = name;
            this.Info = info;
            this.Ingredients = ingredients;
            this.Instructions = instructions;
        }

        public string RecipeName
        {
            get
            {
                return Info.RecipeName;
            }
        }

        /// <summary>
        /// Gets Recipe info.
        /// </summary>
        public RecipeInfo Info { get; private set; }

        /// <summary>
        /// Gets Ingredients.
        /// </summary>
        public Ingredient[] Ingredients { get; private set; }

        /// <summary>
        /// Gets instructions for recipe.
        /// </summary>
        public string[] Instructions { get; private set; }
    }
}