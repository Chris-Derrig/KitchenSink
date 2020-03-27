using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KitchenSink.Models
{
    public class Drink
    {
        public Drink(string idDrink, string strDrink, string strCategory)
        {
            Id = idDrink;
            Name = strDrink;
            Category = strCategory;
        }
        public Drink() { }
        [JsonPropertyName("idDrink")]
        public string Id { get; set; }
        [JsonPropertyName("strDrink")]
        public string Name { get; set; }
        [JsonPropertyName("strDrinkThumb")]
        public string Image { get; set; }
        [JsonPropertyName("strCategory")]
        public string Category { get; set; }
        [JsonPropertyName("strInstructions")]
        public string Instructions { get; set; }
        [JsonPropertyName("strAlcoholic")]
        public string Alcoholic { get; set; }
        
        //ingredients
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        
        //measures
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }
    }
    public class DrinkArray
    {
        [JsonPropertyName("drinks")]
        public Drink[] Drinks { get; set; }
    }
    public class Rootobject
    {
        public Drink[] drinks { get; set; }
    }
}
