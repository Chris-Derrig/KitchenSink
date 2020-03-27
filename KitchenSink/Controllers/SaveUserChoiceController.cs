using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenSink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace KitchenSink.Controllers
{
    public class SaveUserChoiceController : Controller
    {
        KitchenSinkDBContext db = new KitchenSinkDBContext();
        UserPreferences preferences = new UserPreferences();
        AspNetUsers users = new AspNetUsers();
        UserItems newItems = new UserItems();
        Cuisine newCuisine = new Cuisine();

        [Authorize]
        public IActionResult ToSaveRecipe(int recipeId, string[] cuisine)
        {
            string recipeCuisine = string.Empty;
            AspNetUsers user;
            
            if (User.Identity.Name != null)
            {
                KitchenSinkDBContext db = new KitchenSinkDBContext();
                user = db.AspNetUsers.Where(user => user.Email == User.Identity.Name.ToString()).FirstOrDefault();
            }
            
            newItems.RecipeId = recipeId.ToString();
            recipeCuisine = SetRecipeData(cuisine);
            newCuisine.Cuisine1 = recipeCuisine;

            return RedirectToAction("Drink", "Drink", newItems);
        }
        public string SetRecipeData(string[] recipeCuisine)
        {
            Random random = new Random();
            Recipes userRecipe = new Recipes();
            List<string> cuisineList = new List<string>();
            string chosenCuisine = string.Empty;

            foreach (var x in recipeCuisine)
            {
                cuisineList.Add(x);
            }
            if (cuisineList.Count > 0)
            {
                chosenCuisine = cuisineList[random.Next(cuisineList.Count)];
            }
            else
            {
                chosenCuisine = "american";
            }
            return (chosenCuisine);
        }
        public IActionResult ToSaveDrink(UserItems Items, string dCategory)
        {
            string recipeCuisine = string.Empty;
            newItems.DrinkId = Items.DrinkId;
            string drinkCategory = dCategory;
            recipeCuisine = newCuisine.Cuisine1;
            return RedirectToAction("GetRec", "Recommendations", drinkCategory, recipeCuisine);
        }
    }
}