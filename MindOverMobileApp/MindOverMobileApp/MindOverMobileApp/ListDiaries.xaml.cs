using MindOverMobileApp.Models;
using MindOverMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListDiaries : ContentPage
    {
        //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
        public ListDiaries()
        {

            InitializeComponent();
            Application.Current.Properties["DiaryEntryID"] = null; //new session variable
           /* int stunum = Convert.ToInt32(Application.Current.Properties["StudentNum"]);
            var students = client.GetDiaryEntries();
            String results = "";
            foreach (var a in students) {
                if (a.Student_num.Equals(stunum)) {
                    results += "Title: " + a.Diary_title + "\r\n" + "date: "+a.Diary_date+"\r\n"+ "Body: " + a.Diary_description;
                }
                results += "\r\n"+"\r\n";
            }*/

        }

        public void btnaddnew(object sender, EventArgs args) {
            //redirect to the Diary entry page

            //set the session variable to null
            Application.Current.Properties["DiaryEntryID"] = null;
            if (Application.Current.Properties["DiaryEntryID"]==null) {
                Navigation.PushAsync(new DiaryEntryPage());
            }
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var diary = ((ListView)sender).SelectedItem as Diary;
            if (diary==null) {
                return;
            }
            //pass the diary id of the clicked diary entry to the next page
            Application.Current.Properties["DiaryEntryID"] = diary.DiaryId;
            //redirect to the next page
            if (Application.Current.Properties["DiaryEntryID"]!=null) {
                Navigation.PushAsync(new DiaryEntryPage());
            }
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; //item tap should not trigger item selection
        }
    }
}