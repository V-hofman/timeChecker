using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace timeChecker.Models
{
    public class Categories : INotifyPropertyChanged
    {
        public Categories()
        {
            //Leave empty is needed
        }
        public Categories(string categoryName)
        {
            CategoryName = categoryName;
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
