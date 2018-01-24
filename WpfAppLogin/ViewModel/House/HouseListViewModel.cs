using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin
{
    public class HouseListViewModel
    {
        public static HouseListViewModel Instance => new HouseListViewModel();
        public List<HouseItemViewModel> Houses { get; set; }

        public HouseListViewModel()
        {
            //Houses.Add(new HouseItemViewModel{ Name = "aa" });
            //Houses.Add(new HouseItemViewModel { Name = "bb" });
            Houses = new List<HouseItemViewModel>
            {
                new HouseItemViewModel
                {
                    Name="aa"
                },
                new HouseItemViewModel
                {
                    Name="bb"
                },
                new HouseItemViewModel
                {
                    Name="bb"
                },
                new HouseItemViewModel
                {
                    Name="bb"
                },

            };
        }

    }
}
