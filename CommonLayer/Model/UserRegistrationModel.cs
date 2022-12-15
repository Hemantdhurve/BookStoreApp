using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Modal
{
    public class UserRegistrationModel
    {
        [Key]
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
    }
}
