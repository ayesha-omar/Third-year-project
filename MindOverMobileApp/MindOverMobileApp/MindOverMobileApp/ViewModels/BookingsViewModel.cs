using MindOverMobileApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MindOverMobileApp.ViewModels
{
    class BookingsViewModel : ViewModelBase
    {
        // ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiurl = HelperClass.apiurl;
        public BookingsViewModel() {

            /*DateTime dateTime = DateTime.Now;
            XamForms.Controls.SpecialDate specialDate = new XamForms.Controls.SpecialDate(dateTime);
            ObservableCollection<XamForms.Controls.SpecialDate> specialDatesvar = new ObservableCollection<XamForms.Controls.SpecialDate>();
            specialDatesvar.Add(specialDate);
            Attendances = specialDatesvar;*/

            Title = "List of Bookings";
            BookedSession = new ObservableRangeCollection<BookedSession>();
            PreviousBookings = new ObservableRangeCollection<PreviousBookings>();
            
            //using threads will help with making the app not break while running
            Thread thread = new Thread(setDiaryEntriesAsync);
            thread.Start();
            /*diary groups instantiations*/
            //  Diarygroups.Add(new Grouping<string, Diary>("I am happy",new[] { }));
            RefreshCommand = new AsyncCommand(refresh);
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
            string json = client.UploadString(apiurl + myurl, inputJson);
            return json;
        }

        public ObservableRangeCollection<BookedSession> BookedSession { get; set; }
        public ObservableRangeCollection<PreviousBookings> PreviousBookings { get; set; }
        
        public AsyncCommand RefreshCommand { get; }
        

        private void setDiaryEntriesAsync()
        {
           
            int studentnum = Convert.ToInt32(Application.Current.Properties["StudentNum"].ToString());
           // var books = client.getStudentBookings(studentnum);
            //get student bookings
            string json = getJson("Consultation/getBookingsByStudent?studentid=" + HelperClass.StudentNum);
            if (json == "false")
            {
                return;
            }

            List<BookConsultationClass> books = JsonConvert.DeserializeObject<List<BookConsultationClass>>(json);

            foreach (var a in books)
            {
                int counsid = Convert.ToInt32(a.Couns_id); //get counsellor ID

                //get counsellor for this booking
                string json1 = getJson("Counsellor/GetCounsellor?Couns_id=" + HelperClass.CounsellorID);
                if (json1 == "false")
                {
                    return; //return? or maybe continue?
                }

                CounsellorClass couns = JsonConvert.DeserializeObject<CounsellorClass>(json1);

               // var couns =client.GetCounsellor(counsid);
                string time = a.time;
                DateTime dateTime = (DateTime)a.date;

                if (dateTime.Date >= DateTime.Today)
                {
                    //upcoming Consultationss
                    string diarydate = dateTime.Date.ToString().Substring(0, 10); //including only the date not the time
                    string description = "" + couns.Name + " " + couns.Surname;
                    BookedSession.Add(new BookedSession { timeslot = time, date = diarydate, description = description }); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries

                }
                else {
                    //previous bookings
                    //upcoming Consultationss
                    string diarydate = dateTime.Date.ToString().Substring(0, 10); //including only the date not the time
                    string description = "" + couns.Name + " " + couns.Surname;
                    PreviousBookings.Add(new PreviousBookings { timeslot = time, date = diarydate, description = description }); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries

                }


            }
            //BookedSession.Add(new BookedSession { timeslot = "00:00", description = "+" });

        }

        async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }





        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private ObservableCollection<XamForms.Controls.SpecialDate> specialdates;
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get
            {
                return specialdates;
            }
            set
            {
                specialdates = value;
                OnPropertyChanged(nameof(Attendances));
            }
        }

        public MvvmHelpers.Commands.Command DateChosen
        {
            get
            {
                return new MvvmHelpers.Commands.Command((obj) =>
                {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);
                });
            }
        }
    }
}
