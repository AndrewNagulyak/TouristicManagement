using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class InternalUserData:Base
    {
        public InternalUserData(string username,string hashedPassword, string role)
        {
            Username = username;
           
            HashedPassword = hashedPassword;
            Role = role;
        }
        public InternalUserData()
        {

        }
        public string Username
        {
            get;
            set;
        }
    
     
        public string HashedPassword
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
    }
}