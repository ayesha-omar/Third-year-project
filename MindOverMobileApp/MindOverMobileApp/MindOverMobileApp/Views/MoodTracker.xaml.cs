using MindOverMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodTracker : ContentPage
    {
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        string moodtype = "";
        public MoodTracker()
        {
            InitializeComponent();
        }

        public async void GreatButton_Clicked(object sender, EventArgs args)
        {
            string action = await DisplayActionSheet("Choose Time", "Cancel", null, "YES", "NO");
            if (action.Equals("YES")) {
                moodtype = "GREAT";
            }

        }

        public async void GoodButton_Clicked(object sender, EventArgs args)
        {

            string action = await DisplayActionSheet("Choose Time", "Cancel", null, "YES", "NO");
            if (action.Equals("YES"))
            {
                moodtype = "GREAT";
            }
        }

        public async void NeutralButton_Clicked(object sender, EventArgs args)
        {

            string action = await DisplayActionSheet("Choose Time", "Cancel", null, "YES", "NO");
            if (action.Equals("YES"))
            {
                moodtype = "GREAT";
            }
        }

        public async void BadButton_Clicked(object sender, EventArgs args)
        {

            string action = await DisplayActionSheet("Choose Time", "Cancel", null, "YES", "NO");
            if (action.Equals("YES"))
            {
                moodtype = "GREAT";
            }
        }

        public async void AwfulButton_Clicked(object sender, EventArgs args)
        {

            string action = await DisplayActionSheet("Choose Time", "Cancel", null, "YES", "NO");
            if (action.Equals("YES"))
            {
                moodtype = "GREAT";
            }
        }

        public void Done_Clicked(object sender, EventArgs args)
        {
            if (moodtype!="") {
                var logged=client.logMood(moodtype, HelperClass.StudentNum); //SAVE THE MOOD 
                if (logged==true) 
                {
                    DisplayAlert("Information","Mood saved","Ok");
                }
            }

        }
    }
}