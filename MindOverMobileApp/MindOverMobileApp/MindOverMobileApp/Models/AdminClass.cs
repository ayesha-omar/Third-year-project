using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class AdminClass
    {

        public int admin_id { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }

        public string email { get; set; }

        public string password { get; set; }

    }
}