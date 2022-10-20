using MediaManager;
using MindOverMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BreathingControl : ContentPage
    {
        private const string videoLink = "https://www.youtube.com/watch?v=8vkYJf8DOsc&t=13s.mp4";
        string apiurl = HelperClass.apiurl;
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        public BreathingControl()
        {
            InitializeComponent();
            /* NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
             NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
             notimessage();*/

        }

        /*private async void tbnControl_Clicked(object sender, EventArgs e)
        {
            switch (tbnControl.Text) {
                case "Play":
                    
                    await CrossMediaManager.Current.Play(videoLink);
                    tbnControl.Text = "Pause";
                    tbnControl.BackgroundColor = Color.Crimson;
                    videoStatus.Text = "Playing...";
                    break;
                case "Pause":
                    await CrossMediaManager.Current.Pause();
                    tbnControl.Text = "Play";
                    tbnControl.BackgroundColor = Color.LawnGreen;
                    videoStatus.Text = "Paused";
                    break;
            }
        }*/

        private void btnDone(object sender, EventArgs e)
        {
            //take ex ID from helper class,
            videoview.Stop();
            //  Mark as Completed exercise by passing the exercise ID
            //bool var = client.exerciseCompleted(HelperClass.CounsellorID, HelperClass.exerciseID,HelperClass.StudentNum);

            PrescribeExerciseClass prescribed = new PrescribeExerciseClass { Couns_id = HelperClass.CounsellorID, Ex_id = HelperClass.exerciseID, Student_Num = HelperClass.StudentNum };
            string inputJson = JsonConvert.SerializeObject(prescribed); //serialize object and return json
            string resultjson = uploadjson("Exercise/exerciseCompleted?prescribeExercise=", inputJson);
            if (resultjson != "false")
            {
                //if the exercise status has been successfully logged
                this.Navigation.PopAsync();//go back to home page
                DisplayAlert("progress saved", "Congradulations on finishing your prescribed exercise", "Thanks!");
                return;
            }

            this.Navigation.PopAsync();//go back to home page
            DisplayAlert("Confirmation", "Congradulations on finishing your exercise", "Thanks!");



            /*BookConsultationClass consultationClass = new BookConsultationClass { date=datetime.Date,time=timecliked,studentNum= HelperClass.StudentNum,Couns_id= HelperClass.CounsellorID };
                 string json = getJson("Consultation/book_Consultation?book=" + consultationClass);
                 HelperClass.removetimeSlot(dateclicked, timecliked);
                 if (json != "false")
                 {

                     BookConsultationClass booked = JsonConvert.DeserializeObject<BookConsultationClass>(json);
                     HelperClass.removetimeSlot(dateclicked, timecliked);

                     await DisplayAlert("Booking Confirmation", "You have been Booked with: " + Environment.NewLine + "Counsellor " + counsellorname + " " + counsellorSurname + Environment.NewLine + "Email: " + counselloremail, "OK");

                 }*/

        }
        public string getJson(string myurl)
        {

            //TAKES in the url and returns the json for the requested JSON
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
    }
}