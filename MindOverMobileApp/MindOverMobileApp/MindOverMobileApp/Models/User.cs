using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        
        public string Name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
  
        public string userType { get; set; }


       /* public User(int UserId, string name, string surname, string email, string userType)
        {
            this.UserId = UserId;
            this.Name = name;
            this.surname = surname;
            this.email = email;
            this.userType = userType;
        }*/


    }
}