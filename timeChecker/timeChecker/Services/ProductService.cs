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
        /// <summary>
        /// The database object to gather data
        /// </summary>
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
        /// <summary>
        /// List of the products
        /// </summary>
        List<Product> productList = new List<Product>();
        /// <summary>
        /// Used to gather a list of products from the database
        /// </summary>
        /// <returns>A list of product objects</returns>
        public async Task<List<Product>> GetProducts()
        {
            productList = await DatabasePublic.GetProductsAsync();
            return productList;
        }
        /// <summary>
        /// Delete a product using a product ID
        /// </summary>
        /// <param name="Id">The ID of the product</param>
        /// <returns>Task</returns>
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
        /// <summary>
        /// !!!WARNING!!! Deletes all the products from the database file
        /// </summary>
        /// <returns>Task</returns>
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
        /// <summary>
        /// Grab the product information using an ID
        /// </summary>
        /// <param name="Id">The ID of the product you wish to grab</param>
        /// <returns>The product object</returns>
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
