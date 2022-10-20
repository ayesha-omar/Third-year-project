using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
   public class Medicine
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public string NameOfMedicine { get; set; }

        public string category { get; set; }

        public string DoctorName { get; set; }

    }
}
