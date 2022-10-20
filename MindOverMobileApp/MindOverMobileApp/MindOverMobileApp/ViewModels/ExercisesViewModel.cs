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

namespace MindOverMobileApp.ViewModels
{
   
    public class ExercisesViewModel : ViewModelBase
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiurl = HelperClass.apiurl;

        public ObservableRangeCollection<Exercise> Exercise { get; set; }
        public ObservableRangeCollection<Grouping<string, Exercise>> Diarygroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public ExercisesViewModel()
        {
            Title = "List of Exercises";
            Exercise = new ObservableRangeCollection<Exercise>();
            Diarygroups = new ObservableRangeCollection<Grouping<string, Exercise>>();
            Thread thread = new Thread(setExercises);
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

        private void setExercises()
        {
            string json = getJson("Exercise/GetExercises");
            if (json == "false")
            {
                return;
            }

            List<ExerciseClass> exercises = JsonConvert.DeserializeObject<List<ExerciseClass>>(json);

           // var exercises = client.GetExercises();
           
            //code for getting the student's prescribed exercises
            string jsonpresribedex = getJson("Exercise/getStudentPrescribedExercises?studentNum=" + HelperClass.StudentNum);
            if (jsonpresribedex == "false")
            {
                return;
            }

            List<PrescribeExerciseClass> prescribedexercises = JsonConvert.DeserializeObject<List<PrescribeExerciseClass>>(jsonpresribedex);

            //var prescribedexercises = client.getPrescribedExercises(HelperClass.StudentNum);
            foreach (var a in exercises)
            {
               string isprescribed = " ";
                foreach (var b in prescribedexercises) {
                    if (a.Ex_id.Equals(b.Ex_id) && b.isPrescribed.Equals(1)) {
                        isprescribed = "prescribed"; //Mark Exercise as "prescribed"
                    }
                }
                Exercise.Add(new Exercise { Ex_ID = a.Ex_id, Exercise_Name =a.Name, Exercise_Description = a.description,MediaPath=a.mediaPath,IsItPrescribed=isprescribed}); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries
            }
            
            //Exercise.Add(new Exercise { Ex_ID = -1, Exercise_Name = "Information", Exercise_Description ="More Information", MediaPath = "info", IsItPrescribed = "" }); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries

        }

        async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }

}
