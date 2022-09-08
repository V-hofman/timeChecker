using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Services;
using Xamarin.Forms;
using timeChecker.Models;
using timeChecker.Utils;
using timeChecker.Views;
using System.ComponentModel;

namespace timeChecker.ViewModels
{
    public class CheckViewModel : ContentPage, INotifyPropertyChanged
    {
        int _CheckCount { get; set; }
        Color _CheckColor { get; set; }
        public int CheckCount { get { return _CheckCount; }
            set { _CheckCount = value; OnPropertyChanged(nameof(CheckCount)); } }
        public Color CheckColor { get { return _CheckColor; }
            set { _CheckColor = value; OnPropertyChanged(nameof(CheckColor)); } } 
        public List<Product> ProductList { get; set; }

        public Command ChangeToProductCommand { get; }

        ProductService productService;
        Utils.NotificationManager notificationManager;

        public CheckViewModel()
        {
            
            productService = new ProductService();
            notificationManager = new Utils.NotificationManager();
            Task.Run(async () => { await GetProductsDueAsync(); }).Wait();
            if(CheckCount > 0)
            {
                notificationManager.CreateNotification(CheckCount);
            }
            ChangeToProductCommand = new Command(ChangePage);
        }

        public async Task GetProductsDueAsync()
        {
           ProductList = await productService.GetProducts();
           var LinqResult = ProductList.Where((x) => DateTime.Parse(x.endDate) <= DateTime.Now);
            CheckCount = LinqResult.Count();
            switch(CheckCount)
            {
                case var _ when CheckCount < 4:
                    CheckColor = Color.Green;
                    break;                   
                case var _  when CheckCount >= 4 && CheckCount < 10:
                    CheckColor = Color.Orange;
                    break;
                default:
                    CheckColor=Color.Red;
                    break;
            }

        }

        public async void ChangePage()
        {
            Console.WriteLine("Tried");
            await Shell.Current.GoToAsync($"//{nameof(ProductDisplayPage)}");
        }
    }
}