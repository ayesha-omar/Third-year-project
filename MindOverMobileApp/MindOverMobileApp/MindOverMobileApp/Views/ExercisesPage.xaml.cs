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
    public partial class ExercisesPage : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        string apiurl = HelperClass.apiurl;
     
        public ExercisesPage()
        {
            InitializeComponent();
            //notification section
            /*NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
            //end of notification section
            // HelperClass.exercises=client.g
            callprescribeexercises();
           // thread = new Thread(callprescribeexercises);
            //thread.Start();
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

        public void callprescribeexercises() {

            string json = getJson("Exercise/getStudentPrescribedExercises?studentNum=" + HelperClass.StudentNum);
            if (json == "false")
            {
                return;
            }
            List<PrescribeExerciseClass> exercises = JsonConvert.DeserializeObject<List<PrescribeExerciseClass>>(json);
            //var exercises = client.getPrescribedExercises(HelperClass.StudentNum);
            int counter = 0;
            foreach (var a in exercises)
            {
                if (a.isPrescribed.Equals(1)) {

                    string json2 = getJson("Exercise/GetExercise?exerciseid=" + a.Ex_id);
                    ExerciseClass exercise = JsonConvert.DeserializeObject<ExerciseClass>(json2);
                    // var exercise = client.GetExercise(a.Ex_id);
                    counter++;
                    HelperClass.exercises.Add(new PrescribedExercise { exerciseID = a.Ex_id, exerciseName = exercise.Name });
                }
            }
            DisplayAlert("Information","You have "+counter+" Prescribed Exercises","Ok");
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
         {
             var exercise = ((ListView)sender).SelectedItem as Exercise;
             if (exercise == null)
             {
                 return;
             }

             HelperClass.exerciseID = exercise.Ex_ID;
             //redirect to the next appropriate page
             switch (exercise.Exercise_Name) {

                 case "BreathingControl":

                     Navigation.PushAsync(new BreathingControl());
                     break;
                 case "PhysicalExercises":

                     Navigation.PushAsync(new PhysicalExercise());
                     break;
                 case "OccupyYourMind":

                     Navigation.PushAsync(new OccupyYourMind());
                     break;

                 case "Stop, Think, Do":

                     Navigation.PushAsync(new Stop());
                     break;
                 case "SituationalAwareness":

                     Navigation.PushAsync(new SituationalAwareness());
                     break;


             }
         }

         private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
         {
             ((ListView)sender).SelectedItem = null; //item tap should not trigger item selection
         }

        /* private void BreathingButton_Clicked(object sender, EventArgs e)
         {
             DisplayAlert("Exercise ID before", "ID: " + HelperClass.exerciseID, "ok");
             HelperClass.exerciseID = HelperClass.getPrescribedExerciseID(breathingControl.Text);
             DisplayAlert("Exercise ID","ID: "+HelperClass.exerciseID,"ok");
             Navigation.PushAsync(new BreathingControl());
         }



         private void PhysicalButton_Clicked(object sender, EventArgs e)
         {
             HelperClass.exerciseID = HelperClass.getPrescribedExerciseID(physicalExercise.Text);
             Navigation.PushAsync(new PhysicalExercise());
         }
         private void AwarenessButton_Clicked(object sender, EventArgs e)
         {
             HelperClass.exerciseID = HelperClass.getPrescribedExerciseID(AwarenessButton.Text);
             Navigation.PushAsync(new SituationalAwareness());
         }
         private void OccupyMindButton_Clicked(object sender, EventArgs e)
         {
             HelperClass.exerciseID = HelperClass.getPrescribedExerciseID(occupyMind.Text);
             Navigation.PushAsync(new OccupyYourMind());
         }
         private void StopButton_Clicked(object sender, EventArgs e)
         {
             HelperClass.exerciseID = HelperClass.getPrescribedExerciseID(StopButton.Text);
             Navigation.PushAsync(new Stop());
         }*/

       /* protected void BreathingButton_Clicked(object sender, EventArgs args)
        {

            Navigation.PushAsync(new BreathingControl());

        }
        public void PhysicalButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PhysicalExercise());
        }

        private void OccupyButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OccupyYourMind());
        }
        public void StopButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Stop());
        }

        public void SituationalButton_Clicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new SituationalAwareness());
        }

        private void MoodButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoodTracker());
        }*/

    }
}