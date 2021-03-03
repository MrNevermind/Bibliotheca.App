using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bibliotheca.App.Converters
{
    [ValueConversion(typeof(BookStatus), typeof(string))]
    class BookStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BookStatus status = (BookStatus)value;
            string result = "";
            switch (status)
            {
                case BookStatus.Available: result = "Available"; break;
                case BookStatus.Taken: result = "Taken"; break;
                default: result = ""; break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
