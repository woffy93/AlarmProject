using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppLogin
{



    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            //passwordBox.PasswordChanged -=

            if((bool)e.NewValue) //if caller sets MonitorPassword to  true start listening out for password changes
            {

                HasTextProperty.SetValue(passwordBox); // set default value

                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    // has text attached property for passwordbox
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        public static void SetValue(DependencyObject sender)
        {
            HasTextProperty.SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
 
}
