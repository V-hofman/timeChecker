using timeChecker.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using timeChecker.Models;
using timeChecker.Services;
using System.Linq;
using System.Threading.Tasks;

namespace timeChecker.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        UserService userService;

        public LoginViewModel()
        {
            this.userService = new UserService();
            LoginCommand = new Command<User>((x) => OnLoginClicked(x));
        }

        private async void OnLoginClicked(User user)
        {
            if(user == null)
            {
                return;
            }
            Console.WriteLine(user.UserName);
            if (await CheckLogin(user))
            {
                Console.WriteLine("Succes");
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                Console.WriteLine("Failed");
            }
            
        }

        private async Task<bool> CheckLogin(User user)
        {
            Console.Write("!!!!!====We reached this====!!!!");
            var linqList = await userService.GetUsers();
            if(linqList == null || linqList.Count <= 0)
            {
                return false;
            }
            if (linqList.Any(x => x.UserName == user.UserName && x.Password == user.Password))
            {
                return true;
            }else
            {
                return false;
            }

        }
    }
}
