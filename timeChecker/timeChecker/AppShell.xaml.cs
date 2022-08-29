using System;
using System.Collections.Generic;
using timeChecker.ViewModels;
using timeChecker.Views;
using Xamarin.Forms;

namespace timeChecker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
