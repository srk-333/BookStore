using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Token
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
