using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace timeChecker.Models
{
    public class Product : INotifyPropertyChanged
    {
        /// <summary>
        /// Auto incremental primary key of the product
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// The date the product is due
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// The amount of the product that is due
        /// </summary>
        public int amount {  get; set; }
        /// <summary>
        /// Barcode of the product
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// Which category does the product fit in
        /// </summary>
        public string category { get; set;}
        /// <summary>
        /// The color of the text that represents the due date
        /// </summary>
        public string textColor { get; set; }  

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
