using System;
using timeChecker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using timeChecker.Models;
using System.IO;

namespace timeChecker
{
    public partial class App : Application
    {
        private static Database database;

        internal static Database DatabasePublic
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimeChecker3.db"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
