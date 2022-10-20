using MindOverMobileApp.Models;
using MindOverMobileApp.ViewModels;
using MvvmHelpers;
using Nancy.Json;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace MindOverMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookConsultation : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        String dateclicked = "";
        String timecliked = "";
        bool booked = false;
        String counsellorname="";
        String counselloremail="";
        String counsellorSurname="";
        List<BookConsultationClass> studentbookings = new List<BookConsultationClass>();
        string apiurl = HelperClass.apiurl;
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        public BookConsultation()
        {


            InitializeComponent();
            // ((BookConsultationViewModel)this.BindingContext).AvailableTimesOnly = new ObservableRangeCollection<AvailableTimesOnly>(); //clean the list after every tap
            //var couns = client.GetCounsellor(HelperClass.CounsellorID);
            string json = getJson("Counsellor/GetCounsellor?Couns_id="+HelperClass.CounsellorID);

            if (json != "false")
            {

                CounsellorClass couns = JsonConvert.DeserializeObject<CounsellorClass>(json);
                counsellorname = couns.Name;
                counsellorSurname = couns.Surname;
                counselloremail = couns.email;
                makeanewbooking.Text = "Make a new Booking with Consellor " + counsellorname + " " + counsellorSurname + Environment.NewLine + Environment.NewLine;

            }
            //get the student bookings
            studentbookings= client.getStudentBookings(HelperClass.StudentNum);
            foreach (BookConsultationClass a in studentbookings) {
                DateTime date = a.date;
                bookcalendar.SelectedDates.Add(date); //add the Student's booked dates
            }

            
           
        }



        public string getJson(string myurl)
        {

            //TAKES in the url and returns the found json
            WebClient apiclient = new WebClient();
            apiclient.Headers["Content-type"] = "application/json";
            apiclient.Encoding = Encoding.UTF8;

            string json = apiclient.DownloadString(apiurl + myurl);
            return json;
        }

        public string uploadjson(string myurl, string inputJson)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiurl+myurl, inputJson);
            return json;
        }


        /*private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //clear the listview
            if (((BookConsultationViewModel)this.BindingContext).AvailableTimesOnly.Count>0) {
                ((BookConsultationViewModel)this.BindingContext).AvailableTimesOnly = new ObservableRangeCollection<AvailableTimesOnly>(); //clean the list after every tap

            }
            var availableDates = ((ListView)sender).SelectedItem as AvailableDatesAndTimes;
          
            if (availableDates == null)
            {
                return;
            }
            dateclicked = availableDates.date; //set cliked date
            foreach (var a in availableDates.times)
            {
                
                // BookConsultationViewModel.AvailableTimesOnly.Add(new AvailableTimesOnly { time =a });

                ((BookConsultationViewModel)this.BindingContext).AvailableTimesOnly.Add(new AvailableTimesOnly { time = a });
            
            }

            promptTimeOfDayAsync(availableDates.times);
           
        }*/

        private async Task promptTimeOfDayAsync(List<string> times) {
            string[] buttons = new string[times.Count];
            for (int i=0;i<times.Count;i++) {
                buttons[i] = times[i];
            }
            string action = await DisplayActionSheet("Choose Time", "Cancel", null, buttons);
            Debug.WriteLine("Action: " + action);
            if (action!=null && !(action.Equals("Cancel"))) {
                timecliked = action;

                bool answer = await DisplayAlert("Verify Booking details:", "Date: " + dateclicked + Environment.NewLine + "Time: " + timecliked, "Book", "Cancel");
                Debug.WriteLine("Answer: " + answer);



                if (answer.Equals(false))
                {

                    return;
                }
               
                DateTime datetime = Convert.ToDateTime(dateclicked);
                BookConsultationClass consultationClass = new BookConsultationClass { date=datetime.Date,time=timecliked,studentNum= HelperClass.StudentNum,Couns_id= HelperClass.CounsellorID };
                string inputJson = JsonConvert.SerializeObject(consultationClass); //serialize object and return json
                string resultjson = uploadjson("Consultation/book_Consultation?book=",inputJson);
                 //HelperClass.removetimeSlot(dateclicked, timecliked);
                if (resultjson == "true")
                {
                    HelperClass.addslot(dateclicked, timecliked); //add this current booking date and time to the list of unavailable times

                    // HelperClass.removetimeSlot(dateclicked, timecliked);

                    await DisplayAlert("Booking Confirmation", "You have been Booked with: " + Environment.NewLine + "Counsellor " + HelperClass.Counsellor.Name + " " + HelperClass.Counsellor.Surname + Environment.NewLine + "Email: " + HelperClass.Counsellor.email, "OK");

                }


            }
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //DisplayAlert("Dates and times added", HelperClass.stringaddedtolist, "ok!");
            ((ListView)sender).SelectedItem = null; //item tap should not trigger item selection
        }

        /*private void timechosen_selected(object sender, ItemTappedEventArgs e)
        {
            var time = ((ListView)sender).SelectedItem as AvailableTimesOnly;
            if (time==null) {
                return;
            }
            timecliked = time.time;
            promptAsync();
        }*/

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void mycalender_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            
            var date = e.DateTime.Date;
            dateclicked =date.ToString(); //set date clicked

            //check if the date has been booked
            BookConsultationClass booking = getBooking(date.ToString().Substring(0, 10));
            if (booking!=null) {  //if there is booking for the chosen date
                DisplayAlert("Booking Information", "Date of Booking " + booking.date.ToString().Substring(0,10) + Environment.NewLine + "Time: " + booking.time + Environment.NewLine + "Counsellor name: "+HelperClass.Counsellor.Name+" "+HelperClass.Counsellor.Surname + Environment.NewLine+"Email: "+HelperClass.Counsellor.email+ Environment.NewLine + "Venue:", "OK!");
                return;
            }
            if (date.DayOfWeek.Equals(System.DayOfWeek.Saturday) || date.DayOfWeek.Equals(System.DayOfWeek.Sunday)) {
                DisplayAlert("No available times","no bookings allowed for Saturday and Sunday.","Ok");
                return;
            }

            List<String> availableTimes = HelperClass.filterToAvailableTimes(date.ToString()); //get available times
            promptTimeOfDayAsync(availableTimes);
            bookcalendar.SelectedDate = null;
            //bookcalendar.SelectedDate.Value;
            //DisplayAlert("Date chosen",""+date,"OK");

            //Navigation.PushAsync(new BookConsultation());
            
        }

        public BookConsultationClass getBooking(string date) {
           
            foreach(BookConsultationClass a in studentbookings) {
                //returns booking for the given date
                if ((a.date).ToString().Substring(0,10).Equals(date)) {
                    return a;
                }
            }
            return null;
        }

        public void displaySelectedDates() { 
            
        }




        /*private void timechosen_tapped(object sender, ItemTappedEventArgs e)
        {

            ((ListView)sender).SelectedItem = null; //item tap should not trigger item selection

        }*/
    }
}