using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeChecker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace timeChecker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckPage : ContentPage
    {
        public CheckPage()
        {
            InitializeComponent();
            this.BindingContext = new CheckViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as CheckViewModel).GetProductsDueAsync();
        }
    }
}