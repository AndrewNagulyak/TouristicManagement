using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
  public  class User:Base<User>
    {

        public User(string username, string email, string role) 
        {
            Username = username;
            Email = email;
            Role = role;
        }
        public User()
        {

        }

        public string Username
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
    }
}