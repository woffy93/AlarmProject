using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfAppLogin
{
    // Base ValueConverter that allows direct XAML usage
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static T mConverter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
           return mConverter ?? (mConverter = new T());
        }


        // method to convert one value to another
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        // method to reconvert a value back to its original source type
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
