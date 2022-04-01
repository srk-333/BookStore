using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IAdminRL
    {
        public AdminAccount AdminLogin(string email, string password);
    }
}
