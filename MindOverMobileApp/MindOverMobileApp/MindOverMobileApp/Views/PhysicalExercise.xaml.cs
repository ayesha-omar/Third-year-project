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
    public partial class PhysicalExercise : ContentPage
    {
        public PhysicalExercise()
        {
            InitializeComponent();
            //notification section
            /*NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
            //end of notification section

        }
    }
}