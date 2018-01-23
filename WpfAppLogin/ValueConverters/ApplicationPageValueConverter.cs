using System;
using System.Diagnostics;
using System.Globalization;


namespace WpfAppLogin
{
    // converts the ApplicationPage to an actual viewpage
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage) value)
            {
                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.Map:
                    return new MapPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
