using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCentralinaAllarmi.Centralina.Sensors
{
    class IntrusionSensor : AbstractSensor
    {
        public override bool readSensor()
        {
            return true;
        }
    }
}
