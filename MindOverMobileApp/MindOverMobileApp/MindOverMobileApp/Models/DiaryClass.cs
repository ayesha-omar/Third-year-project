using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindOverMobileApp.Models
{
    public class DiaryClass
    {
        public int Diary_id {get;set;}
        public string title {get;set;}
        public DateTime date {get;set;}
        public string diaryFlaggedWords { get; set; }
        public string description {get;set;}
        public int student_Num {get;set;}

    }
}