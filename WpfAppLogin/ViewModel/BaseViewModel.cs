using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PropertyChanged;
using System.ComponentModel;

namespace WpfAppLogin.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged(String name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
