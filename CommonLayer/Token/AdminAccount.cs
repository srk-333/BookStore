using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AdminAccount 
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }       
        public string Role { get; set; }

        public string Token { get; set; }
    }
}
