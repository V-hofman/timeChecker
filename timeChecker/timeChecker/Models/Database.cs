using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace timeChecker.Models
{
    internal class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>();
        }
        /// <summary>
        /// Used to get all the products from the database file
        /// </summary>
        /// <returns>a list of the product objects</returns>
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }
        /// <summary>
        /// Used to save a product to the database file
        /// </summary>
        /// <param name="product">The product object to be saved</param>
        /// <returns>The number of rows that is added to the database</returns>
        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }
        /// <summary>
        /// Used to delete a product from the database file
        /// </summary>
        /// <param name="product">The product to be removed</param>
        /// <returns>The amount of rows deleted</returns>
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        /// <summary>
        /// !!!WARNING!!! Deletes all the products from the database
        /// </summary>
        /// <returns>The number of objects deleted</returns>
        public Task DeleteAllProductsAsync()
        {
            return _database.DeleteAllAsync<Product>();
        }
        /// <summary>
        /// Grab a product object using an ID
        /// </summary>
        /// <param name="Id">The ID of the object to be gathered</param>
        /// <returns>The first element in the database that matches the ID</returns>
        public Task<Product> GetProductById(int Id)
        {
            var product = _database.Table<Product>().FirstOrDefaultAsync(p => p.Id == Id);
            return product;
        }
    }
}
