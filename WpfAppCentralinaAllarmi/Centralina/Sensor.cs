using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.Centralina
{
    public class Sensor
    {
        private int id { get; set; }

        public Sensor(int num)
        {
            this.id = num;
        }

        public bool getAlarm()
        {
            return true;
        }
    }
}
