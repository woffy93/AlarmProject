﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCentralinaAllarmi.Centralina.Sensors
{
    class SensorFactory
    {
        public AbstractSensor getSensor(string sensorType, bool activation, int id)
        {
            switch (sensorType)
            {
                case "Incendio":
                    FireSensor f = new FireSensor();
                    f.sensorId = id;
                    f.abilitato = activation;
                    f.alarmState = false;
                    return f;
                case "Fuoriuscita di gas":
                    GasSensor g = new GasSensor();
                    g.sensorId = id;
                    g.abilitato = activation;
                    g.alarmState = false;
                    return g;
                case "Intrusione":
                    IntrusionSensor i= new IntrusionSensor();
                    i.sensorId = id;
                    i.abilitato = activation;
                    i.alarmState = false;
                    return i;
                default:
                    return null;
            }
        }
    }
}
