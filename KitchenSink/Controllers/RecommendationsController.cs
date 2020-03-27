using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KitchenSink.Models;
using Microsoft.Extensions.Configuration;

namespace KitchenSink.Controllers
{
    public class RecommendationsController : Controller
    {
        private IConfiguration _config;
        Random random = new Random();
        public RecommendationsController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult GetRec(string drinkCategory, string recipeCuisine)
        {
            KitchenSinkDBContext db = new KitchenSinkDBContext();
            Recommendation recom = new Recommendation();
            Drinks drinks = new Drinks();
            Cuisine cuisine = new Cuisine();
            Genres genres = new Genres();
            int drinkId=0;
            int cuisineId=0;
            int genreId=0;
            int movieGenre = 0;

            foreach(Drinks d in db.Drinks)
            {
                if(d.Category == drinkCategory)
                {
                    drinkId = d.Id;
                }
                //for when a drinkCategory is returned that does not match database
                else
                {
                    drinkId = random.Next(1,6);
                }
            }
            foreach(Cuisine c in db.Cuisine)
            {
                if(c.Cuisine1 == recipeCuisine)
                {
                    cuisineId = c.Id;
                }
                //for when a cuisine is returned that does not match database
                else
                {
                    cuisineId = random.Next(1,8);
                }
            }
            foreach(Recommendation rec in db.Recommendation)
            {
                if(rec.DrinkId == drinkId && rec.RecipeId == cuisineId)
                {
                    genreId = rec.GenreId;
                }
            }
            foreach(Genres gen in db.Genres)
            {
                if(gen.Id == genreId)
                {
                    movieGenre = gen.DbgenreId;
                }
            }
            return RedirectToAction("GetRandomMovie", "Movie", movieGenre);
        }
    }
}
