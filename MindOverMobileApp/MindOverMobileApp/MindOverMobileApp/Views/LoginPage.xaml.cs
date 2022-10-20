using MindOverMobileApp.Models;
using MindOverMobileApp.ViewModels;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiUrl = HelperClass.apiurl;
        public LoginPage()
        {
            InitializeComponent();
            //notification section
           /* NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
            //end of notification section

            this.BindingContext = new HomeViewModel();
            //creating session variables for current User
            Application.Current.Properties["StudentNum"] = null;
            Application.Current.Properties["StudentName"] = null;
            HelperClass.hasNewNotification = true; //terminate the while loop
            HelperClass.StudentNum = -00000001111; //reset the student number at logout
            HelperClass.AllowNotificationCheck = false; //disAllow the check for notification feature from running
           // DisplayAlert("WENT BACK TO LOGIN","","ok");
        }


        public string getJson(string myurl)
        {
            WebClient apiclient = new WebClient();
            apiclient.Headers["Content-type"] = "application/json";
            apiclient.Encoding = Encoding.UTF8;

            string json = apiclient.DownloadString(apiUrl + myurl);
            return json;
        }

        public string uploadjson(string myUrl, string inputJson)
        {
                     
             //serialize object and return json
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl+myUrl, inputJson);
            return json;
        }


        protected void loginbtn(Object sender, EventArgs args)
        {
            /*var student = client.loginStudent("williamwilson@student.uj.ac.za","william");
            Console.WriteLine("Number: "+student.Student_num+" Name: "+student.Stu_name);
            */
            //Requesting Json
            login();


        }

        public async void login() {
            /*using (var client = new HttpClient())
            {
                string result = await client.GetStringAsync(HelperClass.apiurl + "Login/loginStudent?useremail=" + username.Text + "&password=" + password.Text);
                if (result != "false")
                {
                    //deserialize json to StudentClass Object
                    StudentClass student = JsonConvert.DeserializeObject<StudentClass>(result);
                    //DisplayAlert("StudentFound", s.Name + " " + s.Surname + " " + s.email, "Ok");
                    Application.Current.Properties["StudentNum"] = student.studentNumber;
                    Application.Current.Properties["StudentName"] = student.Name + " " + student.Surname;
                    this.Navigation.PushAsync(new Home());
                    HelperClass.StudentNum = student.studentNumber;
                    return;
                }
            }*/
            string json = getJson("Login/loginStudent?useremail="+ username.Text + "&password="+ HashPass.Secrecy.HashPassword(password.Text));
            if (json != "false")
            {
                //deserialize json to StudentClass Object
                StudentClass student = JsonConvert.DeserializeObject<StudentClass>(json);
                //DisplayAlert("StudentFound", s.Name + " " + s.Surname + " " + s.email, "Ok");
                Application.Current.Properties["StudentNum"] = student.studentNumber;
                Application.Current.Properties["StudentName"] = student.Name + " " + student.Surname;
                this.Navigation.PushAsync(new Home());
                HelperClass.StudentNum = student.studentNumber;
                return;
            }

            DisplayAlert("Ops", "Username or password in correct", "OK");
        }

        private void openULINK(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://ulink.uj.ac.za/views/reset/Reset1");
        }
        /* private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
{
}*/
    }


}