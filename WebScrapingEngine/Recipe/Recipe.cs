// <copyright file="Recipe.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebScrapingEngine.Recipe
{
    /// <summary>
    /// Recipe class.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="info">name of dish.</param>
        /// <param name="ingredients">ingredients.</param>
        /// <param name="instructions">instructions.</param>
        /// <param name="url">url.</param>
        public Recipe(RecipeInfo info, string[] ingredients, InstructionSet[] instructions, Url url)
        {
            // this.Author = author;
            // this.Name = name;
            this.Info = info;
            this.Ingredients = ingredients;
            this.Instructions = instructions;
            this.Url = url;
        }

        /// <summary>
        /// Gets Recipe Name.
        /// </summary>
        public string RecipeName
        {
            get
            {
                return this.Info.RecipeName;
            }
        }

        /// <summary>
        /// Gets Recipe info.
        /// </summary>
        public RecipeInfo Info { get; private set; }

        /// <summary>
        /// Gets Ingredients.
        /// </summary>
        public string[] Ingredients { get; private set; }

        /// <summary>
        /// Gets instructions for recipe.
        /// </summary>
        public InstructionSet[] Instructions { get; private set; }

        /// <summary>
        /// Gets recipe url.
        /// </summary>
        public Url Url { get; internal set; }
    }
}