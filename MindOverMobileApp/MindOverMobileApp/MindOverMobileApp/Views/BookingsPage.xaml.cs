using MindOverMobileApp.Models;
using MindOverMobileApp.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace MindOverMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingsPage : ContentPage
    {
        public List<DateTime> specialdates;
        public BookingsPage()
        {
            InitializeComponent();
            BookConsultationViewModel bookConsultationViewModel = new BookConsultationViewModel(); //setting up the available and unavailable times
            List<DateTime> dateTimes = new List<DateTime>();

            DateTime date1 = DateTime.Now.AddDays(10); //equiv to 25 july
            /*NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
            notimessage();*/
           // mycalender.SelectedDates.Add(date1);
            /*for (int i=0;i<5;) {
                           
                mycalender.SelectedDates.Add(date1); //add multiple dates
                date1.AddDays(2);
            }*/
           
            //mycalender.SelectedBackgroundColor = Color.Pink;
        }
        protected void Goback(Object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }



        private void Calendar_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            var cal =((Calendar)sender).SelectedDate;
            if (cal==null) {
                return;
            }
            DisplayAlert("Date Chosen","Date chosen is"+Convert.ToString(cal),"ok!");
        }

        private void weekview_Toggled(object sender, ToggledEventArgs e)
        {
            if (sender != null)
            {

            
            Boolean istoggled = ((Switch)sender).IsToggled;

            if (istoggled == false)
            {
               // schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.MonthView;
                return;
            }
            //if the switch is ON
            //display the weekly view
          //  schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WeekView;
                
            }
        }

        public void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var bookedsession = ((ListView)sender).SelectedItem as BookedSession;
            if (bookedsession==null) 
            {
                return;
            }
            DisplayAlert("Booking Information","Date of Booking "+ bookedsession.date + Environment.NewLine+"Time: "+ bookedsession.timeslot+ Environment.NewLine+"Counsellor name: "+ bookedsession.description+ Environment.NewLine+"Venue:","OK!");
        }

        public void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        public void AddNewBooking(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookConsultation());
        }

        //button when the booking is edited
        private void EditBooking_Clicked(object sender, EventArgs e)
        {
            var booking = ((MenuItem)sender).BindingContext as BookedSession;

            if (booking==null) {
                return;           
            }
            //create a session variable, that has the BookedSession ID
            //redirect to the BookConsultaion page,
        }

        //switch between previous bookings and current bookings
        private void SwitchView(object sender, EventArgs e)
        {
            if (previousBookings.IsVisible == false)
            {
                //switching to previous bookings
                previousBookings.IsVisible = true;
                currentbookings.IsVisible = false;
                switchviewbutton.Text = "             View Upcoming Sessions";
            }
            else {
                previousBookings.IsVisible = false;
                currentbookings.IsVisible = true;
                switchviewbutton.Text = "             View Previous Bookings";
            }
        }
    }
}