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

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task DeleteAllProductsAsync()
        {
            return _database.DeleteAllAsync<Product>();
        }

        public Task<Product> GetProductById(int Id)
        {
            var product = _database.Table<Product>().FirstOrDefaultAsync(p => p.Id == Id);
            return product;
        }
    }
}
