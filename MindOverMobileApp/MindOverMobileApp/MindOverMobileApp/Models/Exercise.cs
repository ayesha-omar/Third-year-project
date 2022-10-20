using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
    public class Exercise
    {

        public int Ex_ID { get; set; }
        public string Exercise_Name { get; set; }
        public string Exercise_Description { get; set; }
        public string MediaPath { get; set; }
        public string IsItPrescribed { get; set; }


        //public int Ex_status_or_rank { get; set; }

    }

}
