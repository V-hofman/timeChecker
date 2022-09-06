using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Models;

namespace timeChecker.Services
{
    internal class CategoryService
    {
        List<Categories> Categories { get; set; }

        public async Task<List<Categories>> GetCategoriesAsync()
        {
            return await App.DatabasePublic.GetAllCategoriesAsync();
        }

        public async Task DeleteProduct(int Id)
        {
            try
            {
                Categories categories = await App.DatabasePublic.GetCategoryByIdAsync(Id);

                _ = App.DatabasePublic.DeleteCategoryAsync(categories);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete category: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
