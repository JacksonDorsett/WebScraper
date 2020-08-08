using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingEngine.Recipe;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace RecipeDatabaseManager
{
    public class RecipeLoader
    {
        string filePath;

        public RecipeLoader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Recipe> LoadRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            string json = "";
            using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                json = sr.ReadToEnd();
            }

            JArray res = JArray.Parse(json);


            foreach (JObject o in res)
            {
                recipes.Add(JsonConvert.DeserializeObject<Recipe>(o.ToString()));
            }

            
            return recipes;
        }

        
    }
}
