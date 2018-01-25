using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppCentralinaAllarmi;

namespace WpfAppLogin
{
    // View Model for each house item
    public class HouseItemViewModel :BaseViewModel
    {
        public static HouseItemViewModel Istance => new HouseItemViewModel();

        public String Name { get; set; }

        public String Id { get; set; }

        public ICommand isClicked { get; set; }

        public bool IsSelected { get; set; } // true if clicked, used to have side color

       public HouseItemViewModel()
        {
            isClicked = new RelayCommand(onClick);
        }
        public void onClick()
        {
            //MessageBox.Show("Clicked "+this.Name+this.Id);
            Window centralina = new WpfAppCentralinaAllarmi.MainWindow(Int32.Parse(this.Id));
            centralina.Show();
            
            
        }
    }
}
