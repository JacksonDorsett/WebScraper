namespace WebScrapingEngine.WPRM
{
    public class Recipe
    {
        public Recipe(string author, string name, Ingredient[] ingredients, string[] instructions)
        {
            this.Author = author;
            this.Name = name;
            this.Ingredients = ingredients;
            this.Instructions = instructions;
        }

        public string Author { get; private set; }

        public string Name { get; private set; }

        public Ingredient[] Ingredients { get; private set; }

        public string[] Instructions { get; private set; }
    }
}