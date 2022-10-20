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
    public partial class AddMedication : ContentPage
    {
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        public AddMedication()
        {
            InitializeComponent();

            //check if it is an edit condition
            if (HelperClass.ID_MedicineToEdit > 0)
            {
                var medicine = client.GetMedicine(HelperClass.ID_MedicineToEdit);
                medName.Text = medicine.NameOfMedicine;

                pickCategory.SelectedItem = medicine.category;
                medDoc.Text = medicine.DoctorName;
            }


        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListMedications());
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            if (HelperClass.ID_MedicineToEdit > 0)
            {
                var edited = client.EditMedicine(HelperClass.ID_MedicineToEdit, DateTime.Now, medName.Text, pickCategory.SelectedItem.ToString(), HelperClass.StudentNum, medDoc.Text);
                if (edited==true) {
                    Navigation.PopAsync();
                }
            }
            else{ 

            if (pickCategory.SelectedItem == null || medName.Text == null || medDoc.Text == null)
            {
                DisplayAlert("information", "Make sure to fill in required all fields", "Ok");
                return;
            }

            var logged = client.LogMedicine(DateTime.Now, medName.Text, pickCategory.SelectedItem.ToString(), HelperClass.StudentNum, medDoc.Text);
            if (logged == true)
            {
                DisplayAlert("confirmation", "Medicine information saved", "Ok");
            }
            Navigation.PushAsync(new ListMedications());
        }
        }
    }
}