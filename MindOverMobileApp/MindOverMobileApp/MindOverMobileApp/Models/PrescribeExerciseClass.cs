using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class PrescribeExerciseClass
    {

        public int ID { get; set; }
        public int Couns_id { get; set; }
        public int Ex_id { get; set; }
        public int Student_Num { get; set; }

        public DateTime DatePrescribed { get; set; }

        public int IsLatestCompleted { get; set; }

        public int numCompletion { get; set; }

        public int isPrescribed { get; set; }

    }
}