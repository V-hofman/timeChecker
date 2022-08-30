using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using timeChecker.ViewModels;

namespace timeChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        public Settings()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
            var categoryList = new List<string>();
            categoryList.Add("Frisdrank");
            categoryList.Add("Vers");
            categoryList.Add("Waterval");
            categoryList.Add("Kleine koeling");

            FilterPicker.ItemsSource = categoryList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as SettingsViewModel).getProductAsync();
        }

        private async void FilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem as string;
            await (BindingContext as SettingsViewModel).filterByCategoryAsync(selectedOption);
            


        }
    }

}