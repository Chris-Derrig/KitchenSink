using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KitchenSink.Models
{
    public class Genre
    {
        public Genre() { }
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    public class GenreArray
    {
        [JsonPropertyName("genres")]
        public Genre[] Genres { get; set; }
    }
}
