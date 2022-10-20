using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class AvailableDatesAndTimesClass
    {

        public int ID { get; set; }

        public int coundID { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public int isAvailable { get; set; } 

    }
}