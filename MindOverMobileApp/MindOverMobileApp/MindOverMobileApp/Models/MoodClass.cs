using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
   public class MoodClass
    {

        public int Mood_id { get; set; }
        public string time { get; set; }
        public DateTime date { get; set; }

        //we can have 1=angry 2=sad 3=neutral 4=happy 5=amazing
        public string emotion { get; set; }

        public int student_Num { get; set; }
    }
}
