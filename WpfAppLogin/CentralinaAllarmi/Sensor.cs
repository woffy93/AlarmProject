using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.CentralinaAllarmi
{
    public class Sensor
    {
        private int sensorId {get; set;}

        public Sensor (int num){
            this.id = num;
        }

        bool getAlarm(){
            return true;
        }
    }
}
