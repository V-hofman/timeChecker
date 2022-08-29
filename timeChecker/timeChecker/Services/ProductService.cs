using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Models;

namespace timeChecker.Services
{
    internal class ProductService
    {
        private static Database database;

        internal static Database DatabasePublic
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Product.db3"));
                }
                return database;
            }
        }

        List<Product> productList = new List<Product>();

        public async Task<List<Product>> GetProducts()
        {
            productList = await DatabasePublic.GetProductsAsync();
            return productList;
        }

        public async Task DeleteProduct(int Id)
        {
            try
            {
                Product product = await DatabasePublic.GetProductById(Id);
                
                _ = DatabasePublic.DeleteProductAsync(product);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete products: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            
        }

        public async Task DeleteAllProducts()
        {
            try
            {
                await DatabasePublic.DeleteAllProductsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete products: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        public async Task<Product> FindProductById(int Id)
        {
            try
            {
                 Product product = await DatabasePublic.GetProductById(Id);
                 return product;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to find product: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
           return null;
        }
    }
}
