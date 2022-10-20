using MindOverMobileApp.Models;
using Newtonsoft.Json;
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
    public partial class DiaryEntryPage : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string studentname = "";
        string apiurl = HelperClass.apiurl;
        public DiaryEntryPage()
        {
            InitializeComponent();
            //notification section
           /* NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
            //end of notification section
            studentname = Application.Current.Properties["StudentName"].ToString();
            from.Text = "From: "+Environment.NewLine+studentname;
            if (Application.Current.Properties["DiaryEntryID"] != null) {
                //display the Diary entry

                //Thread thread = new Thread(displayDiaryEntry);
                //thread.Start();
                displayDiaryEntry();
            }
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

        public void displayDiaryEntry() {
            int Entryid = Convert.ToInt32(Application.Current.Properties["DiaryEntryID"]);
            string json = getJson("Diary/getDiaryEntry?id="+Entryid);
            if (json!="false") {
                DiaryClass diary = JsonConvert.DeserializeObject<DiaryClass>(json);
                body.Text = diary.description; //display
                return;
            }
            DisplayAlert("Information","Diary Not Found","Ok!");
            /*var diary = client.getDiaryEntry(Entryid);
            body.Text = diary.Diary_description; //display*/
        }
        protected async void Save(Object sender, EventArgs args)
        {

            //save the diary entry
            
            string diarybody = body.Text;
            string diarytitle = "";

            if (diarybody == null || diarybody.Length <= 50)
            {
                //diary should be long enough to be saved
                //lbllogged.Text = "Diary Not Saved!(entry should be above atleast 15 words";
                await DisplayAlert("Dear " + studentname, "I failed to save your entry(entry should have atleast 20 words)", "Ok!");

                
            }
            else
            {
                string sometext = diarybody.ToString();
                diarytitle = sometext.Substring(0, 25) + "..."; //diary title taken from the diary body

                //if the Student is editing an existing Diary Entry
                if (Application.Current.Properties["DiaryEntryID"] != null)
                {
                    int entryid =Convert.ToInt32(Application.Current.Properties["DiaryEntryID"]);

                    //edit the diary entry
                    DiaryClass diary = new DiaryClass { Diary_id= entryid,title= diarytitle,description= diarybody };
                    string inputjson= JsonConvert.SerializeObject(diary);
                    string resultsjson = uploadjson("Diary/EditDiaryEntry?diaryClass=", inputjson);

                    if (resultsjson != "false")
                    {
                        await DisplayAlert("Dear " + studentname, "I have saved your entry", "Ok!");

                    }
                    else
                    {
                        await DisplayAlert("Dear " + studentname, "I failed to save your entry", "Ok!");
                    }
                }
                else
                { 
                //if the Student is adding a new Diary entry
                int stunum = Convert.ToInt32(Application.Current.Properties["StudentNum"].ToString()); //retrieve the student number from the session variable
                DateTime todaydate = DateTime.Now; //use today's date when saving the diary entry
                    DiaryClass diary = new DiaryClass {student_Num=stunum,date=todaydate, title = diarytitle, description = diarybody };
                    string inputjson = JsonConvert.SerializeObject(diary);
                    string resultsjson = uploadjson("Diary/logDiaryEntry?diaryClass=", inputjson);

                    if (resultsjson != "false")
                    { //display successful message if entry is saved
                        await DisplayAlert("Dear " + studentname, "I have saved your entry", "thank you!");
                        //lbllogged.Text = "Diary entry Saved!";
                        //title.Text = " ";
                        body.Text = " ";
                    }
                    else
                    {
                        await DisplayAlert("Dear " + studentname, "I failed to save your entry", "Ok!");
                    }
             }
            }
        }

        protected void Cancel(Object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }


        /*protected void listentrybtn(Object sender, EventArgs args)
        {
            Navigation.PushAsync(new ListDiaries());
        }*/
    }
}