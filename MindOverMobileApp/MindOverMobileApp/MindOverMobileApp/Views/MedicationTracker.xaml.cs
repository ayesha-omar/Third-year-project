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
    public partial class MedicationTracker : ContentPage
    {
        public MedicationTracker()
        {
            InitializeComponent();
        
        }

        private void MedYes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListMedications());
        }

        private void MedNo_Clicked(object sender, EventArgs e)
        {
            MedNoLabel.IsVisible = true;
            lblDescription.IsVisible = false;
            lblQuestion.IsVisible = false;
            btnNo.IsVisible = false;
            btnYes.IsVisible = false;
        }
    }
}