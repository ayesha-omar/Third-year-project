using MindOverMobileApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MindOverMobileApp.ViewModels
{
    class BookConsultationViewModel : ViewModelBase
    {

        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiurl = HelperClass.apiurl;
        public ObservableRangeCollection<AvailableDatesAndTimes> AvailableDatesAndTimes { get; set; }
        public  ObservableRangeCollection<AvailableTimesOnly> AvailableTimesOnly { get; set; }
        public DateTime TodaysDate { get; set; } = DateTime.Today;
        public BookConsultationViewModel()
        {

            /*DateTime dateTime = DateTime.Now;
            XamForms.Controls.SpecialDate specialDate = new XamForms.Controls.SpecialDate(dateTime);
            ObservableCollection<XamForms.Controls.SpecialDate> specialDatesvar = new ObservableCollection<XamForms.Controls.SpecialDate>();
            specialDatesvar.Add(specialDate);
            Attendances = specialDatesvar;*/

            Title = "List of available booking times";
            AvailableDatesAndTimes = new ObservableRangeCollection<AvailableDatesAndTimes>();
            AvailableTimesOnly = new ObservableRangeCollection<AvailableTimesOnly>();


            Thread thread = new Thread(AvailableSlots);
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



        public AsyncCommand RefreshCommand { get; }


        private void AvailableSlots()
        {
            int counsid = HelperClass.CounsellorID;
            HelperClass.CounsellorID = counsid;

            //var availableslots = client.getAvailableSlots(counsid); //get counsellor available slots
            string json = getJson("DatesTimes/getUnAvailableSlots?counsID=" + HelperClass.CounsellorID);
            if (json == "false")
            {
                return;
            }

            //get the unAvailable datetimes
            List<AvailableDatesAndTimesClass> Unavailableslots = JsonConvert.DeserializeObject<List<AvailableDatesAndTimesClass>>(json);
           

            string displaar = " ";
            foreach (var a in Unavailableslots)
            {

                /*string time = a.Book_time;
                string diarydate = a.Book_date.ToString().Substring(0, 9); //including only the date not the time
                string description = "Counsellor Name: Dr " + couns.Couns_name + " " + couns.Couns_Surname;
                //availabledatesandtimes.Add(); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries*/
                //Making sure no previous Dates are displayed
                if (((DateTime)a.Date>=DateTime.Today))
                {
                    HelperClass.addslot(((DateTime)a.Date).Date.ToString().Substring(0, 10), a.Time); //
                                                                                                      //  availabledatesandtimes.Add(new AvailableDatesAndTimes { date = "10/20/20", time = "20:20:20" });
                    displaar += "date: " + ((DateTime)a.Date).Date.ToString().Substring(0, 10) + " time" + a.Time;
                }
                
            }
            HelperClass.stringaddedtolist = displaar;
            List<AvailableDatesAndTimes> list = new List<AvailableDatesAndTimes>(HelperClass.datesandtimes);
            for (int i=0;i<list.Count;i++)
            {
                AvailableDatesAndTimes.Add(new AvailableDatesAndTimes { date=list[i].date,times=list[i].times});
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
