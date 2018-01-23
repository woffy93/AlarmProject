using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin
{
    // View Model for each house item
    class HouseItemViewModel :BaseViewModel
    {
        public String Name { get; set; }

        public bool IsSelected { get; set; } // true if clicked, used to have side color
    }
}
