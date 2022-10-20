using MindOverMobileApp.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MindOverMobileApp.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        public AsyncCommand RefreshCommand { get; }
        public LoginViewModel() {
            RefreshCommand = new AsyncCommand(refresh);
            Title = "About";
            OpenUlink = new Xamarin.Forms.Command(async () => await Browser.OpenAsync("https://ulink.uj.ac.za/views/reset/Reset1"));
        }
       
        public ICommand OpenUlink { get; }
        async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(2100);
            IsBusy = false;
        }


    }
}
