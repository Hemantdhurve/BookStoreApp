using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class Addressmodel
    {
        [Key]
        public long AddressId { get; set; }
        public long UserId { get; set; }
        public long TypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
