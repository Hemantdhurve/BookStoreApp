using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
        [Key]
        public long BookId { get; set; }
        public string BookTitle { get; set; }        
        public string Author { get; set; }
        public float Rating { get; set; }
        public long RatedCount { get; set; }       
        public long DiscountedPrice { get; set; }
        public long ActualPrice { get; set; }
        public string Description { get; set; }        
        public long BookQuantity { get; set; }
        public string Image { get; set; }
    }
}
