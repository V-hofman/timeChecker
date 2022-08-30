using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Services;
using timeChecker.Models;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;

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
        public Command DeleteAllProductsCommand { get; }
        ProductService productService;


        public SettingsViewModel()
        {
            Title = "Product Finder";
            this.productService = new ProductService();
            GetProductsCommand = new Command(async () => await getProductAsync());
            DeleteProductCommand = new Command<int>(DeleteProductAsync);
            DeleteAllProductsCommand = new Command(DeleteAllProductsAsync);
            Products = new ObservableCollection<Product>();
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Used to gather all products from the database file
        /// </summary>
        /// <returns>Task</returns>
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

                sortByDate(products);

                var dateNow = DateTime.Now.AddDays(3);

                //Check the text color for the products based on their due date
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
        /// <summary>
        /// Delete a product from the database using its ID
        /// </summary>
        /// <param name="Id">ID of the product</param>
        public async void DeleteProductAsync(int Id)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //Confirmation dialogue
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
        /// <summary>
        /// Deletes every product from the database, with a pop-up to confirm
        /// </summary>
        public async void DeleteAllProductsAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //Confirmation dialogue to prevent accidently removing everything
                string answer = await App.Current.MainPage.DisplayPromptAsync("Weet u het zeker?", "type \"DELETE\" om te verwijderen");
                if (!answer.Equals("DELETE"))
                    return;
                _ = productService.DeleteAllProducts();
                Products.Clear();
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
        /// <summary>
        /// Sort by date, starting with the date that is the earliest
        /// </summary>
        /// <param name="products">List of products to be sorted by date</param>
        public void sortByDate(List<Product> products)
        {
            products.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.endDate), DateTime.Parse(y.endDate)));
        }


        /// <summary>
        /// Filter the product list in the database by the category variable
        /// </summary>
        /// <param name="category">This is the string for the category name</param>
        /// <returns>Task</returns>
        public async Task filterByCategoryAsync(string category)
        {
            Console.WriteLine(category);
            var linqList = await productService.GetProducts();
            var linqResult =linqList.Where(x => x.category.Equals(category)).ToList();
            _products.Clear();

            foreach(var product in linqResult)
            {
                _products.Add(product);
            }
            return; 
        }
    }
}
