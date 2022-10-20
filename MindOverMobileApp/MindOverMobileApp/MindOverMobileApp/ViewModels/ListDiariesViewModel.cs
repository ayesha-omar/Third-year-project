using MindOverMobileApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MindOverMobileApp.ViewModels
{
    
    public class ListDiariesViewModel : ViewModelBase
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        string apiurl = HelperClass.apiurl;
        public ObservableRangeCollection<Diary> Diary { get; set; }
        public ObservableRangeCollection<Grouping<string, Diary>> Diarygroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public ListDiariesViewModel()
        {
            Title = "List of Diaries";
            Diary = new ObservableRangeCollection<Diary>();
            Diarygroups = new ObservableRangeCollection<Grouping<string, Diary>>();
            Thread thread = new Thread(setDiaryEntries);
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

        private void setDiaryEntries() 
        {

            //retrieve list of diary entries
            string json = getJson("Diary/GetDiaryEntries");
            if (json == "false")
            {
                return;
            }

            List<DiaryClass> diaries = JsonConvert.DeserializeObject<List<DiaryClass>>(json);

            //var diaries = client.GetDiaryEntries();

            //var diaries = client.GetStudentDiaryEntries(HelperClass.StudentNum);

            foreach (var a in diaries)
            {
                if (a.student_Num.Equals(HelperClass.StudentNum)) { 
                string title = a.title + "...";
                string diarydate = a.date.ToString().Substring(0, 10); //including only the date not the time
                string description = diarydate + "| " + a.description.Substring(0, 50) + "...";
                Diary.Add(new Diary { DiaryId = a.Diary_id, DiaryTitle = title, DiaryDescription = description }); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries
            }
            }
        }

        async Task refresh() {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}