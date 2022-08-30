using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace timeChecker.Models
{
    public class Product : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string productName { get; set; }
        public string endDate { get; set; }
        public int amount {  get; set; }
        public string barcode { get; set; }
        public string category { get; set;}

        public string textColor { get; set; }  

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
