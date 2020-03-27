using System;
using System.Collections.Generic;

namespace KitchenSink.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Recommendation = new HashSet<Recommendation>();
        }
        public int Id { get; set; }
        public string Genre { get; set; }
        public int DbgenreId { get; set; }
        public virtual ICollection<Recommendation> Recommendation { get; set; }
    }
}
