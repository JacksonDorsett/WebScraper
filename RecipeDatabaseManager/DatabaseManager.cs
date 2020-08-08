using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using WebScrapingEngine.Recipe;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace RecipeDatabaseManager
{
    class DatabaseManager
    {
        public static List<Recipe> LoadAllRecipes()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Recipe>("select * from Recipe", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void saveRecipe(Recipe recipe)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Query($"Select * from Recipe where recipe_name = '{SanatizeString(recipe.Info.RecipeName)}'", recipe).Count() == 0)
                {
                    string s = $"insert into Recipe(recipe_name, author, ingredients, instructions , cook_time, prep_time,cuisine,yeild,dish_type,url)" +
                        $" values('{recipe.Info.RecipeName}', '{recipe.Info.Author}', '{ConcatanateString(recipe.Ingredients)}', '{ConcatinateInstructions(recipe.Instructions)}',{recipe.Info.CookTime}, {recipe.Info.PrepTime}, '{ConcatanateString(recipe.Info.Cuisine)}', '{recipe.Info.Yeild}','{ConcatanateString(recipe.Info.DishType)}','{recipe.Url.FullUrl}')";
                    cnn.Execute($"insert into Recipe(recipe_name, author, ingredients, instructions , cook_time, prep_time,cuisine,yeild,dish_type,url)" +
                        $" values('{SanatizeString(recipe.Info.RecipeName)}', '{SanatizeString(recipe.Info.Author)}', '{ConcatanateString(recipe.Ingredients)}', '{ConcatinateInstructions(recipe.Instructions)}',{recipe.Info.CookTime}, {recipe.Info.PrepTime}, '{ConcatanateString(recipe.Info.Cuisine)}', '{SanatizeString(recipe.Info.Yeild)}','{ConcatanateString(recipe.Info.DishType)}','{recipe.Url.FullUrl}')");
                }
                else
                {

                }


                
                //recipe.Info.
            }


        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private static string ConcatanateString(string[] arr)
        {
            string ret = string.Empty;

            if (arr == null)
            {
                return string.Empty;
            }
            foreach(var s in arr)
            {
                ret += SanatizeString(s) + "\r\n";
            }
            return ret;
        }

        private static string ConcatinateInstructions(InstructionSet[] instructionSet)
        {
            string ret = string.Empty;

            if(instructionSet.Length == 1)
            {
                return ConcatanateString(instructionSet[0].Steps);
            }
            
            return ret;
        }

        private static string SanatizeString(string s)
        {
            return s.Replace("'", "").Replace("\"", "").Replace(":", "").Replace(")", "").Replace("(", "").Replace("?", "").Replace("!", "");
        }
    }
}
