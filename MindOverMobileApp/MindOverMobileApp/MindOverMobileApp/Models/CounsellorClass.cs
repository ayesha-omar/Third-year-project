using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class CounsellorClass
    {

        public int Couns_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string email { get; set; }

        public string password { get; set; }

        public int NumStudentsLinked { get; set; }

        public int Admin_id { get; set; }

    }
}