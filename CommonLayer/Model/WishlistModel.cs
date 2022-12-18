using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class WishlistModel
    {
        [Key]
        public long WishlistId { get; set; }        
        public long BookId { get; set; }
        public long UserId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public long DiscountedPrice { get; set; }
        public long ActualPrice { get; set; }
        public string Image { get; set; }

    }
}
