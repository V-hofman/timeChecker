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
        public List<string> categoryList;

        public Settings()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
            this.categoryList = new List<string>();
            Task.Run(async () => { await GatherCategories(); }).Wait();
            FilterPicker.ItemsSource = categoryList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            FilterPicker.SelectedItem = null;
            await (BindingContext as SettingsViewModel).getProductAsync();

        }

        private async void FilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem as string;
            await (BindingContext as SettingsViewModel).filterByCategoryAsync(selectedOption);
        }

        private async Task GatherCategories()
        {
            this.categoryList.Clear();
            var tempList = new List<Categories>();
            tempList = await (BindingContext as SettingsViewModel).GetCategoriesAsync();
            foreach (var category in tempList)
            {
                categoryList.Add(category.CategoryName);
            }


        }
    }

}