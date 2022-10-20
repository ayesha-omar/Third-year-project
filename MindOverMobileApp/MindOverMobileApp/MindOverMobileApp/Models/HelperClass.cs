using System;
using System.Collections.Generic;
using System.Text;

namespace MindOverMobileApp.Models
{
    public class HelperClass
    {
        public static ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        public static List<AvailableDatesAndTimes> datesandtimes { get; set; } = new List<AvailableDatesAndTimes>(); //this object will store all the List of Counsellor UNavailable dates and times pairs
        public static int CounsellorID { get; set; }
        public static CounsellorClass Counsellor { get; set; } = new CounsellorClass();
        public static int StudentNum { get; set; }

        public static int exerciseID { get; set; }

        public static List<PrescribedExercise> exercises { get; set; } = new List<PrescribedExercise>();

        public static string apiurl { get; set; } = "http://192.168.56.1/api/";

        //public static string apiurl { get; set; } = "https://localhost:44358/api/";

        public static string stringaddedtolist { get; set; }
        //function 
        public static string NotificationMessage { get; set; }

        public static bool hasNewNotification { get; set; }

        public static bool AllowNotificationCheck { get; set; }
        public static int ID_MedicineToEdit { get; set; }  //when the edit menu item is cliked, then set the medicine ID 
        public static void addslot(string date, string time) {
            bool found = false;
          
            foreach (AvailableDatesAndTimes a in datesandtimes) {
                if (a.date.Equals(date)) 
                {

                    //making sure that the time is not repeated
                    List<String> sometimes = new List<string>(a.times);
                    bool timeslotFound=false;
                    foreach (string t in sometimes) {
                        if (t.ToString().Equals(time))
                        {
                            timeslotFound = true; //if the time is found on the list

                        }
                    }
                    if (timeslotFound==false) 
                    {
                        a.times.Add(time); //add this time to the list of available times on this day
                    }
                   
                    found = true;
                    
                    break;
                }
            }
            //the date does not exist in the list
            if (found==false) {
                //create a new slot with new date
                List<String> mytimes = new List<string>();
                mytimes.Add(time);
                AvailableDatesAndTimes dtimes = new AvailableDatesAndTimes { date=date, times=mytimes };
               
                datesandtimes.Add(dtimes); //add the new date slot to the list of datesandtimes

            }

        }

        public static AvailableDatesAndTimes getSlot(string date) {
            foreach (AvailableDatesAndTimes a in datesandtimes) {

                if (a.date.Equals(date)) {
                    return a;
                }
            }
            return null;
        
        }


        public static int getPrescribedExerciseID(string title) {
            
            foreach (var a in exercises)
            {
                if (title.Equals(a.exerciseName) ){
                    return a.exerciseID;
                }

            }
            return -1;
        }
        public static bool removetimeSlot(string date,string time) {

            foreach (var a in datesandtimes) {
                if (a.date.Equals(date)) {
                    foreach (var t in a.times) {
                        if (t.Equals(time)) {
                            return true;
                        }
                    }
                
                }
            }

            return false;
        }

        public static List<String> filterToAvailableTimes(string date) {
            AvailableDatesAndTimes datesandtimespair = getSlot(date.Substring(0,10)); //get slot for unavailabletimes for the given date
            if (datesandtimespair==null) {
                //if the date does not have any unavailable times, return the whole day
                return client.getTimesPerDay();
            }
            List<String> availabletimes = new List<string>(); //will store availabel times for this given date
            List<String> timesPerDay = client.getTimesPerDay();
            foreach (String a in timesPerDay) //iterate through the available times and filter out the UnAvailable times
            {
                bool unavailfound = false;
                //iterate through the timesperDay
                foreach (String b in datesandtimespair.times) {
                    if (a.Equals(b)) {
                        //IF THE TIME IS UNAVAILABLE
                        unavailfound = true;
                    }
                }
                if (unavailfound==false) //if the time is not found
                {
                    //if this time is  not found in the list of UNavailable times, means it is available
                    availabletimes.Add(a);
                }
            }
            return availabletimes;
        
        }

    }


}
