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

            // Remove previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;


            if ((bool)e.NewValue)
            {
                // set default value
                HasTextProperty.SetValue(passwordBox);

                // Start listening for password changes
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
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
}
