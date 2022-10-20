using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
   public class MedicineTrackerClass
    {

        public int ID { get; set; }
        public DateTime date { get; set; }
        public string NameOfMedicine { get; set; }

        public string category { get; set; }

        public int StudentNum { get; set; }

        public string DoctorName { get; set; }
        public int isActive { get; set; }
    }
}
