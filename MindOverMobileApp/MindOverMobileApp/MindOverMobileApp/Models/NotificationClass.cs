using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
  public  class NotificationClass
    {

        public int ID { get; set; }
        public DateTime DateOfNotification { get; set; }
        public string description { get; set; }
        public char isActive { get; set; }
		        
        public int StudentNum { get; set; }

        public int CounsID { get; set; }
    }
}
