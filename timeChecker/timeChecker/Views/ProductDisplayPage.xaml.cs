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
    public partial class ProductDisplayPage : ContentPage
    {
        public ProductDisplayPage()
        {
            this.BindingContext = new ProductDisplayViewModel();
            InitializeComponent();

        }
    }
}