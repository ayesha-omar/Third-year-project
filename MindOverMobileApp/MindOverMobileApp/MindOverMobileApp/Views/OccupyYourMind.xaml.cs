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
    public partial class OccupyYourMind : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiurl = HelperClass.apiurl;
        public OccupyYourMind()
        {
            InitializeComponent();
            //notification section
            /*NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
            //end of notification section

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

        private void Done_clicked(object sender, EventArgs e)
        {

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


        }
    }
}