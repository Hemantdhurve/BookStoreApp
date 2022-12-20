using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderModel
    {
        [Key]
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public long CartId { get; set; }
        public long AddressId { get; set; }
        public long OrderQuantity { get; set; }
        public long TotalPrice { get; set; }
        public long TotalDiscountedPrice { get; set; }
        public DateTime OrderPlacedDate { get; set; }
    }
}
