using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class ExerciseClass
    {

        public int Ex_id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string mediaPath { get; set; }
        public char Status { get; set; }
        public char isActive { get; set; }


    }
}