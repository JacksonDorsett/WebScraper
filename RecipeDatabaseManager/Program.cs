using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingEngine.Recipe;
namespace RecipeDatabaseManager
{
    class Program
    {
        static void Main(string[] args)
        {

            RecipeLoader rl = new RecipeLoader(@"C:\Users\JacksonThe\Desktop\Coding\Project\WebScraper\FoodNetworkScraper\bin\Debug\food batch 1.json");
            var recipes = rl.LoadRecipes();


            foreach (var r in recipes)
            {
                DatabaseManager.saveRecipe(r);
                Console.WriteLine($"{r.Info.RecipeName} added to database");
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
            
        }
    }
}
