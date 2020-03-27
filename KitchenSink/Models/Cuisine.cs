using System;
using System.Collections.Generic;

namespace KitchenSink.Models
{
    public partial class Cuisine
    {
        public Cuisine()
        {
            Recommendation = new HashSet<Recommendation>();
        }
        public int Id { get; set; }
        public string Cuisine1 { get; set; }

        public virtual ICollection<Recommendation> Recommendation { get; set; }
    }
}
