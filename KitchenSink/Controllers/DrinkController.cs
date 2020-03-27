using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenSink.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitchenSink.Controllers
{
    public class DrinkController : Controller
    {
        private JsonDocument jDoc;
        Random random = new Random();
        UserItems newItems = new UserItems();
        DrinkArray drinks = new DrinkArray();
        public IActionResult Drink(UserItems Items)
        {
            newItems = Items;
            return View();
        }
        public async Task<IActionResult> GetDrink(string alcohol)
        {
            List<Drink> drinkList = new List<Drink>();
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?i={alcohol}"))
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();

                        jDoc = JsonDocument.Parse(stringResponse);
                        var jsonList = jDoc.RootElement.GetProperty("drinks");
                        for (int i = 0; i < jsonList.GetArrayLength(); i++)
                        {
                            drinkList.Add(new Drink()
                            {
                                Id = jsonList[i].GetProperty("idDrink").GetString(),
                                Name = jsonList[i].GetProperty("strDrink").GetString(),
                                Image = jsonList[i].GetProperty("strDrinkThumb").GetString()
                            });
                        }
                    }
                }
            }
            catch(Exception)
            {
                return View("Drink");
            }
            
            Drink drink = new Drink();
            var chosenDrink = drinkList[random.Next(0, drinkList.Count)];
            string drinkID = chosenDrink.Id;

            using (var httpClient1 = new HttpClient())
            {
                using (var response2 = await httpClient1.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkID}"))
                {
                    var stringResponse2 = await response2.Content.ReadAsStringAsync();
                    JsonDocument jDoc2 = JsonDocument.Parse(stringResponse2);
                    var jsonList2 = jDoc2.RootElement.GetProperty("drinks");

                    foreach (var item in jsonList2.EnumerateArray())
                    {
                        drink = JsonSerializer.Deserialize<Drink>(item.ToString());

                    }
                }
            }
            return View(drink);
        }
        public async Task<IActionResult> RndNonAlc()
        {
            List<Drink> drinkList = new List<Drink>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);
                    var jsonList = jDoc.RootElement.GetProperty("drinks");
                    for (int i = 0; i < jsonList.GetArrayLength(); i++)
                    {
                        drinkList.Add(new Drink()
                        {
                            Id = jsonList[i].GetProperty("idDrink").GetString(),
                            Name = jsonList[i].GetProperty("strDrink").GetString(),
                            Image = jsonList[i].GetProperty("strDrinkThumb").GetString()
                        });
                    }
                }
            }
            
            Drink drink = new Drink();
            var chosenDrink = drinkList[random.Next(0, drinkList.Count)];
            string drinkID = chosenDrink.Id;

            using (var httpClient1 = new HttpClient())
            {
                using (var response2 = await httpClient1.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkID}"))
                {
                    var stringResponse2 = await response2.Content.ReadAsStringAsync();
                    JsonDocument jDoc2 = JsonDocument.Parse(stringResponse2);
                    var jsonList2 = jDoc2.RootElement.GetProperty("drinks");

                    foreach (var item in jsonList2.EnumerateArray())
                    {
                        drink = JsonSerializer.Deserialize<Drink>(item.ToString());
                    }
                }
            }
            return View("GetDrink",drink);
        }
        public async Task<IActionResult> RndAlc()
        {
            List<Drink> drinkList = new List<Drink>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Alcoholic"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    jDoc = JsonDocument.Parse(stringResponse);
                    var jsonList = jDoc.RootElement.GetProperty("drinks");
                    for (int i = 0; i < jsonList.GetArrayLength(); i++)
                    {
                        drinkList.Add(new Drink()
                        {
                            Id = jsonList[i].GetProperty("idDrink").GetString(),
                            Name = jsonList[i].GetProperty("strDrink").GetString(),
                            Image = jsonList[i].GetProperty("strDrinkThumb").GetString()
                        });
                    }
                }
            }
            Drink drink = new Drink();
            var chosenDrink = drinkList[random.Next(0, drinkList.Count)];
            string drinkID = chosenDrink.Id;

            using (var httpClient1 = new HttpClient())
            {
                using (var response2 = await httpClient1.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkID}"))
                {
                    var stringResponse2 = await response2.Content.ReadAsStringAsync();
                    JsonDocument jDoc2 = JsonDocument.Parse(stringResponse2);
                    var jsonList2 = jDoc2.RootElement.GetProperty("drinks");

                    foreach (var item in jsonList2.EnumerateArray())
                    {
                        drink = JsonSerializer.Deserialize<Drink>(item.ToString());
                    }
                }
            }
            return View("GetDrink", drink);
        }
        public async Task<IActionResult> RndDrink()
        {
            Drink drink = new Drink();
            using (var httpClient1 = new HttpClient())
            {
                using (var response2 = await httpClient1.GetAsync($"https://www.thecocktaildb.com/api/json/v1/1/random.php"))
                {
                    var stringResponse2 = await response2.Content.ReadAsStringAsync();
                    JsonDocument jDoc2 = JsonDocument.Parse(stringResponse2);
                    var jsonList2 = jDoc2.RootElement.GetProperty("drinks");

                    foreach (var item in jsonList2.EnumerateArray())
                    {
                        drink = JsonSerializer.Deserialize<Drink>(item.ToString());
                    }
                }
            }
            return View("GetDrink", drink);
        }
        public IActionResult ToSaveDrink(string drinkID, string Category)
        {
            newItems.DrinkId = drinkID;
            string drinkCategory = Category;
            return RedirectToAction("ToSaveDrink", "SaveUserChoice", newItems, drinkCategory);
        }
    }
}
