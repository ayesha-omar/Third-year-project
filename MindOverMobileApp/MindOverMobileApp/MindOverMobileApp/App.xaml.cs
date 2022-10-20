using MindOverMobileApp.Services;
using MindOverMobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MindOverMobileApp
{
    public partial class App : Application
    {

        public App()
        {
           // DevExpress.XamarinForms.Scheduler.Initializer.Init();
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
