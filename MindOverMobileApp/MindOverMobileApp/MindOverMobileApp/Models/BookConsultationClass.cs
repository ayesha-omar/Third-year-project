using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class BookConsultationClass
    {

        public int Book_number { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }

        public int studentNum { get; set; }

        public int Couns_id { get; set; }

    }
}