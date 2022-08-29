using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Services;
using timeChecker.Models;
using System.Diagnostics;
using Xamarin.Forms;

namespace timeChecker.ViewModels
{
    public class SettingsViewModel :BaseViewModel
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get {
                return _products; }
            set
            {
                SetProperty(ref _products, value);
                OnPropertyChanged("Products");
            }
        }
        public Command GetProductsCommand { get; }
        public Command<int> DeleteProductCommand { get; }

        ProductService productService;


        public SettingsViewModel()
        {
            Title = "Product Finder";
            this.productService = new ProductService();
            GetProductsCommand = new Command(async () => await getProductAsync());
            DeleteProductCommand = new Command<int>(DeleteProductAsync);

            Products = new ObservableCollection<Product>();
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task getProductAsync()
        {
            if (IsBusy)
                return; 
            IsBusy = true;
            try
            {

                var products = await productService.GetProducts();

                if (_products.Count != 0)
                    _products.Clear();

                products.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.endDate), DateTime.Parse(y.endDate)));

                var dateNow = DateTime.Now.AddDays(3);

                foreach (var product in products)
                {
                    int result = DateTime.Compare( DateTime.Parse(product.endDate), dateNow);
                    switch(result)
                    {
                        case 0:
                            product.textColor = "Red";
                            break;
                        case 1:
                            int result2 = DateTime.Compare(DateTime.Parse(product.endDate), dateNow.AddDays(2));
                            if(result2 == 0 || result2 < 0)
                            {
                                product.textColor = "Orange";
                            }else
                            {
                                product.textColor = "Green";
                            }
                            break;
                        case -1:
                            product.textColor = "DarkRed";
                            break;
                    }
                    _products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get products: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async void DeleteProductAsync(int Id)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert ("Product Verwijderen?", "Weet u het zeker?", "Ja", "Nee");
                if (!answer)
                    return;

                    Product tempProduct = await productService.FindProductById(Id);
                    _ = productService.DeleteProduct(Id);
                    Products.Remove(tempProduct);
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
    }
}
