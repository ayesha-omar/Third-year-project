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
    public partial class ListMedications : ContentPage
    {
        ServiceReference.MoMServiceClient client = new ServiceReference.MoMServiceClient();
        int MenuItemID = -1;
        public ListMedications()
        {
            InitializeComponent();
            HelperClass.ID_MedicineToEdit = -1; //initialize 
        }

        private void AddMed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddMedication());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var medicine = ((ListView)sender).SelectedItem as Medicine;
            if (medicine == null)
            {
                return;
            }
            MenuItemID = medicine.ID; //setting the tapped Menu Item
            ((ListView)sender).SelectedItem = null; //item tap should not trigger item selection
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var medicine = ((ListView)sender).SelectedItem as Medicine;
            if (medicine==null) {
                return;
            }
            MenuItemID = medicine.ID; //setting the tapped Menu Item
        }

        private void MenuItem_edit_Clicked(object sender, EventArgs e)
        {
            //redirect to the addMedicine page to edit
            HelperClass.ID_MedicineToEdit = MenuItemID;
            if (HelperClass.ID_MedicineToEdit>0)
            {
                Navigation.PushAsync(new AddMedication());
            }
        }

        private async void MenuItem_delete_Clicked(object sender, EventArgs e)
        {

            string action = await DisplayActionSheet("Do You want to delete this", "Cancel", null, "YES","NO");
            
            if (MenuItemID==-1)
            {
                //3await DisplayAlert("NotFound","try again","Ok");
                return;
            }
            if (action.Equals("YES")) 
            {
                var isremoved = client.RemoveMedicine(MenuItemID); //deactivate this medicine
                if (isremoved==true) {
                   await DisplayAlert("Confirmation","entry deleted","Ok");
                    
                }
            }
            MenuItemID = -1;//reset the menuID
        }
    }
}