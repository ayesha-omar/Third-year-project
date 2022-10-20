using MindOverMobileApp.Models;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        //string apiurl = "http://192.168.56.1/api/";
        public Home()
        {
            InitializeComponent();

            //notification section

            NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            HelperClass.AllowNotificationCheck = true; //whenever the home page is loaded, allpw the nofication checker to be enabled
            CheckNotificationAsync();
            //CheckNotificationAsync();
            //notiFYmessage();
            //end of notification section

            int studentnum = Convert.ToInt32(Application.Current.Properties["StudentNum"].ToString());
            string studentname = Application.Current.Properties["StudentName"].ToString();
            HelperClass.StudentNum = studentnum;
            getCounsellorID();

            //myName.Text = $"{studentname}";

        }



        public string getJson(string myurl)
        {

            //TAKES in the url and returns the found json
            WebClient apiclient = new WebClient();
            apiclient.Headers["Content-type"] = "application/json";
            apiclient.Encoding = Encoding.UTF8;

            string json = apiclient.DownloadString(HelperClass.apiurl + myurl);
            return json;
        }

        public string uploadjson(string myurl, string inputJson)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(HelperClass.apiurl + myurl, inputJson);
            return json;
        }

        //get Default Counsellor ID
        private void getCounsellorID()
        {
            //determine if the Student had a booking before
            int studentnum = Convert.ToInt32(Application.Current.Properties["StudentNum"].ToString());

            string json = getJson("StuCounsLink/getLinkInfoByStudent?stunum=" + studentnum); //get link information for this Student
            if (json != "false")
            {
                StuCounsLinkClass link = JsonConvert.DeserializeObject<StuCounsLinkClass>(json);
                //get the linking information if the student had been linked before
                HelperClass.CounsellorID = Convert.ToInt32(link.couns_id); //return counsellor ID
                HelperClass.Counsellor = client.GetCounsellor(HelperClass.CounsellorID);
                return;

            }

            //if the student is logging in for the 1st time, choose from the database the counsellor with the lowest number of students they are monitoring
            string json2 = getJson("Counsellor/getMinCounsellor");
            if (json2 == "false")
            {
                return;
            }
            CounsellorClass counsellor = JsonConvert.DeserializeObject<CounsellorClass>(json2);
            HelperClass.CounsellorID = counsellor.Couns_id;
            HelperClass.Counsellor = counsellor;
            //<<<<<<<HEAD
            //client.LinkStudentToCounsellor(studentnum,counsid);
            //=======
            if (HelperClass.StudentNum != 0 && HelperClass.CounsellorID != 0)
            {
                StuAndCounsIDPair stuAndCounsIDPair = new StuAndCounsIDPair
                {
                    StudentNum = studentnum,
                    CounsID = HelperClass.Counsellor.Couns_id
                };
                string jsonOUT = JsonConvert.SerializeObject(stuAndCounsIDPair);
                string resultsjson = uploadjson("StuCounsLink/LinkStudentToCounsellor?=", jsonOUT);
                if (resultsjson == "false")
                {
                    DisplayAlert("information", "Could not link student to counsellor", "ok");
                }
                DisplayAlert("information", "You have been paired with Counsellor " + counsellor.Name + " " + counsellor.Surname, "Ok");
                //"StuCounsLink/LinkStudentToCounsellor?=";
                //   client.LinkStudentToCounsellor(HelperClass.StudentNum, HelperClass.CounsellorID); //link student to counsellor
            }
            //>>>>>>> main
            //return counsid;
        }

        protected void ExerciseButton_Clicked(object sender, EventArgs args)
        {

            Navigation.PushAsync(new ExercisesPage());

        }
        public void DiaryButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ListDiaries());
        }

        private void BookingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookingsPage());
        }
        public void MedicationButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MedicationTracker());
        }

        public void MoodButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MoodTracker());
        }

        private void InfoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Information());
        }



        //Notification Bar beginning...

        public void notiFYmessage()
        {
            var mynotification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = HelperClass.NotificationMessage,
                Title = "From MindOverMatter",
                ReturningData = HelperClass.NotificationMessage,
                NotificationId = 1337,
                NotifyTime = DateTime.Now.AddSeconds(2)
            };

            NotificationCenter.Current.Show(mynotification);
            if (HelperClass.AllowNotificationCheck==true) {
                CheckNotificationAsync(); //only loop if the app is in the stage where it is allowed to check for notification (e.g, if the user logs in then logs out, this feature should be disabled)
            }

        }

        private void Current_NotificationTapped(NotificationTappedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() => {
                //redirect to the activities page
                Navigation.PushAsync(new ExercisesPage());
                DisplayAlert("Notification", e.Data, "Ok");
            });

        }

        private void Current_NotificationReceived(NotificationReceivedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert(e.Title, e.Description, "Ok");
            });
            }
        }

        public async void CheckNotificationAsync()
        {
            //this function is terminated in the login page
            //int index = 0;
            HelperClass.hasNewNotification = false;
            while (!HelperClass.hasNewNotification) //if it has no new notification keep checking
            {
               
                await Task.Delay(5000); //wait ten seconds before checking the database
                //check db if it has a new notification
                var notification=client.GetNotification(HelperClass.StudentNum);
                if (notification!=null) //if yes then set boolean to true
                {
                    HelperClass.NotificationMessage = notification.description;
                    HelperClass.hasNewNotification = true;
                }
                //index++;
            }
            notiFYmessage();

        }

        //END OF NOTIFICATION BAR
    }

    /*class StatusChecker
    {
        private int invokeCount;
        private int maxCount;

        public StatusChecker(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Checking status {1,2}.",
                DateTime.Now.ToString("h:mm:ss.fff"),
                (++invokeCount).ToString());

            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }*/
}