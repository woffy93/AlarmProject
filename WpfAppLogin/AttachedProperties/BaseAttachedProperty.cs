using System;
using System.ComponentModel;
using System.Windows;

namespace WpfAppLogin
{
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        // Public Events
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        // singleton istance of parent class
        public static Parent Instance { get; private set; } = new Parent();

        // Attached Property Definitions


        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new UIPropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));


        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent function
            Instance.OnValueChanged(d, e);

            // call event listeners
            Instance.ValueChanged(d, e);
        }


        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);


        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        // Event Methods
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }


    }



}
