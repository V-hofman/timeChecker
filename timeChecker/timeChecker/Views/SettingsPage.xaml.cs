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
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as SettingsViewModel).getProductAsync();
        }

    }

}