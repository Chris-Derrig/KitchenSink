using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenSink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace KitchenSink.Controllers
{
    public class RecipesController : Controller
    {
        private IConfiguration _config;

        List<Recipes> mikeRecipe = new List<Recipes>();
        Random random = new Random();
        KitchenSinkDBContext db = new KitchenSinkDBContext();

        public RecipesController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult GetStarted()
        {
            return View();
        }
        public async Task<IActionResult> GetRandomRecipe(string protein = null, string starch = null, string veggie=null, string spice=null, string aromatic=null)
        {
            var SpoonApiKey = _config["SpoonacularApiKey"];
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync
                    ($"https://api.spoonacular.com/recipes/complexSearch?includeIngredients={protein},+{starch},+{veggie},+{spice},+{aromatic}&instructionsRequired=true&addRecipeInformation=true&apiKey={SpoonApiKey}");
                var stringResponse = await response.Content.ReadAsStringAsync();

                JsonDocument jdoc = JsonDocument.Parse(stringResponse);
                var jsonList = jdoc.RootElement.GetProperty("results");
                var list = jsonList.GetArrayLength();

                if (list == 0)
                {
                    return View("GetStarted");
                }
                else
                {
                    foreach (var item in jsonList.EnumerateArray())
                    {
                        var recipes = JsonSerializer.Deserialize<Recipes>(item.ToString());

                        mikeRecipe.Add(recipes);
                    }

                    Recipes chosenRecipe = mikeRecipe[random.Next(mikeRecipe.Count)];
                    var chosenId = chosenRecipe.Id;

                    using var response2 = await httpClient.GetAsync
                    ($"https://api.spoonacular.com/recipes/{chosenId}/information?includeNutrition=false&apiKey={SpoonApiKey}");

                    var stringResponse2 = await response2.Content.ReadAsStringAsync();
                    var recipes2 = JsonSerializer.Deserialize<Recipes>(stringResponse2.ToString());
                    return View(recipes2);
                }
            }
        }
    }
}