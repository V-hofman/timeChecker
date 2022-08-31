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
            var options = new SQLiteConnectionString(dbPath, true,key: "secure",
                postKeyAction: c=>
                {
                    c.Execute("PRAGMA cipher_compatibility = 3");
                });
            _database = new SQLiteAsyncConnection(options);
            _database.CreateTableAsync<Product>();
            _database.CreateTableAsync<User>();
        }

        #region Products
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

        #endregion
        #region User
        /// <summary>
        /// Grabs a list of all users. SHOULD ALWAYS BE A SINGLE USER
        /// </summary>
        /// <returns>List of the user object</returns>
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }
        /// <summary>
        /// Save user to the database
        /// </summary>
        /// <param name="user">the userobject to be saved</param>
        /// <returns>The amount of rows added</returns>
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }
        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The number of rows deleted</returns>
        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }


        #endregion
    }
}
