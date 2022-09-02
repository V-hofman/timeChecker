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
            if (await CheckLogin(user))
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
            }
            
        }

        private async Task<bool> CheckLogin(User user)
        {
            var linqList = await userService.GetUsers();
            if(linqList == null || linqList.Count <= 0)
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Geen account bestaat!", "Wilt u deze account toevoegen?", "Ja", "Nee");
                if (!answer)
                    return false;

                await userService.SaveUser(user);
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
