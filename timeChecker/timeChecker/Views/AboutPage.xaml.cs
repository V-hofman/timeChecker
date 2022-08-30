using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using timeChecker.Models;
using System.IO;
using System.Linq;

namespace timeChecker.Views
{

    public partial class AboutPage : ContentPage
    {

        /// <summary>
        /// Database object
        /// </summary>
        private static Database database;

        internal static Database DatabasePublic
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Product.db3"));
                }
                return database;
            }
        }
        /// <summary>
        /// The barcode scanner
        /// </summary>
        ZXingScannerPage scanPage;

        public AboutPage()
        {
            InitializeComponent();

            var categoryList = new List<string>();
            categoryList.Add("Frisdrank");
            categoryList.Add("Vers");
            categoryList.Add("Waterval");
            categoryList.Add("Kleine koeling");

            Category.ItemsSource = categoryList;
        }
        /// <summary>
        /// Opens the scanner page
        /// </summary>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Start a scannerpage
            scanPage = new ZXingScannerPage();

            //When we get a result
            scanPage.OnScanResult += (result) =>
            {
                //Turn the scanner off
                scanPage.IsScanning = false;

                //Go on main thread, since the scanning is done on a background thread
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Change the text value to the barcode
                    Navigation.PopAsync();
                    
                    resultScan.Text = result.Text;
                    resultScan.IsReadOnly = true;
                    resultScan.TextColor = Color.Gray;
                    resultScan.FontAttributes = FontAttributes.Italic;
                });
            };

            await Navigation.PushModalAsync(scanPage);
        }
        /// <summary>
        /// used to convert plain text to date format
        /// </summary>
        private void dueDate_Completed(object sender, EventArgs e)
        {
            var temp = dueDate.Text;
            var matches = Regex.Matches(temp, "-{1,}");

            if (matches.Count < 2)
            {
                if (temp.Length > 2)
                {
                    if(temp.IndexOf('-', 2) == -1)
                    {
                        temp = temp.Insert(2, "-");
                    }   
                }
                if (temp.Length > 5)
                {
                    if(temp.IndexOf('-', 5) == -1)
                    {

                        temp = temp.Insert(5, "-");
                    }
                }
            }
            DateTime result;
            if(DateTime.TryParse(temp, out result))
            {
                dueDate.Text = temp;
            }
            else
            {
                dueDate.Text = "Wrong Date";
            } 
        }
        /// <summary>
        /// Used to add a product to the database
        /// </summary>
        private async void add_Product(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(resultScan.Text))
            {
                int amountProduct = 0;
                int.TryParse(amountDue.Text, out amountProduct);

                if (String.IsNullOrEmpty(productName.Text))
                    {
                    return;
                    }
                if(String.IsNullOrEmpty(dueDate.Text))
                {
                    var dateAndTime = DateTime.Now;
                    var date = dateAndTime.Date;
                    dueDate.Text = date.ToString();
                }
                await DatabasePublic.SaveProductAsync(new Product
                {
                    barcode = resultScan.Text,
                    productName = productName.Text,
                    endDate = dueDate.Text,
                    amount = amountProduct,
                    category = Category.SelectedItem.ToString()

                });
                ResetInputs();

            }
        }
        /// <summary>
        /// Used to reset the input fields
        /// </summary>
        private void ResetInputs()
        {
            resultScan.Text = null;
            resultScan.IsReadOnly = false;
            resultScan.TextColor = Color.Black;
            resultScan.FontAttributes = FontAttributes.None;

            productName.Text = null;
            dueDate.Text = null;
            amountDue.Text = null;
            Category.SelectedItem = null;
        }
    }
}