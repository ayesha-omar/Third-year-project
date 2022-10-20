using MindOverMobileApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace MindOverMobileApp.ViewModels
{
    class MedicineTrackerViewModel : ViewModelBase
    {


         ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        public ObservableRangeCollection<Medicine> Medicine { get; set; }
        public MedicineTrackerViewModel()
        {

            /*DateTime dateTime = DateTime.Now;
            XamForms.Controls.SpecialDate specialDate = new XamForms.Controls.SpecialDate(dateTime);
            ObservableCollection<XamForms.Controls.SpecialDate> specialDatesvar = new ObservableCollection<XamForms.Controls.SpecialDate>();
            specialDatesvar.Add(specialDate);
            Attendances = specialDatesvar;*/

            Title = "List of Bookings";
            Medicine = new ObservableRangeCollection<Medicine>();
            // PreviousBookings = new ObservableRangeCollection<PreviousBookings>();

            //using threads will help with making the app not break while running
            //Thread thread = new Thread(setMedicinesInfo);
            //thread.Start();
            setMedicinesInfo();
            /*diary groups instantiations*/
            //  Diarygroups.Add(new Grouping<string, Diary>("I am happy",new[] { }));

            RefreshCommand = new AsyncCommand(refresh);
        }
        public AsyncCommand RefreshCommand { get; }


        private void setMedicinesInfo()
        {

            int studentnum = Convert.ToInt32(Application.Current.Properties["StudentNum"].ToString());

            List<MedicineTrackerClass> medicines = client.GetMedicines(studentnum); //get medicines

            foreach (var a in medicines)
            {
                if (a.isActive.Equals(1)){
                    Medicine.Add(new Medicine {ID=a.ID, NameOfMedicine = a.NameOfMedicine, DoctorName = a.DoctorName, date = a.date, category = a.category }); //creating a diary model, to be displayed. diary id will be used for displayind individual diaries
                }
            }

        }

        async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }





        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private ObservableCollection<XamForms.Controls.SpecialDate> specialdates;
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get
            {
                return specialdates;
            }
            set
            {
                specialdates = value;
                OnPropertyChanged(nameof(Attendances));
            }
        }

        public MvvmHelpers.Commands.Command DateChosen
        {
            get
            {
                return new MvvmHelpers.Commands.Command((obj) =>
                {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);
                });
            }
        }
    }
}
