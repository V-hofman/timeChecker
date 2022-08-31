using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using timeChecker.Models;
using Xamarin.Forms;

namespace timeChecker.Converter
{
    public class UserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace(values[0].ToString()) && String.IsNullOrWhiteSpace(values[1].ToString()))
            {
                Console.WriteLine("oopsie");
                return null;

            }else
            {
                string name = values[0].ToString();
                string secret = values[1].ToString();
                Console.WriteLine("succes");
                return new User(Username: name, Password: secret);
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
