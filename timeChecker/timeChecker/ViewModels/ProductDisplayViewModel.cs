using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Models;
using timeChecker.Services;
using timeChecker.Views;
using Xamarin.Forms;

namespace timeChecker.ViewModels
{
    internal class ProductDisplayViewModel : BaseViewModel
    {
        public Command<DateTime> completeCheck { get; }
        public List<Product> Products { get; set; }
        public string ProductName { get; set; }

        ProductService productService;

        public string Minimaldate { get; set; }
        public string Maximaldate { get; set; }

        public ProductDisplayViewModel()
        {
            this.Products = new List<Product>();
            this.productService = new ProductService();
            this.Minimaldate = DateTime.Now.ToString();
            this.Maximaldate = DateTime.Now.AddYears(1).ToString();

            completeCheck = new Command<DateTime>(async x => await CompleteCheck(x));

            Task.Run( async () => await getProductAsync()).Wait();
            Task.Run( async () => await UpdateView()).Wait();

    }

        public async Task getProductAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var productsTemp = await productService.GetProducts();

                if(Products.Count > 0)
                {
                    return;
                }

                var productsResult = productsTemp.Where(x => DateTime.Parse(x.endDate) <= DateTime.Now.AddDays(1));

                foreach(var temp in productsResult)
                {
                    this.Products.Add(temp);
                }
                    
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete products: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task UpdateView()
        {
            if(Products.Count == 0 | Products == null)
            {
                await Shell.Current.GoToAsync($"//{nameof(CheckPage)}");
                return;
            }
            Console.WriteLine(this.ProductName);
            this.ProductName = this.Products.First().productName;
            Console.WriteLine(this.ProductName);

        }

        public async Task CompleteCheck(DateTime newTime)
        {
            await this.productService.DeleteProduct(this.Products.First().Id);
            this.Products.First().endDate = newTime.ToString("dd/MM/yyyy");
            await App.DatabasePublic.SaveProductAsync(this.Products.First());
            this.Products.RemoveAt(0);
            await UpdateView();
        }
    }
}
