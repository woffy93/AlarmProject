using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCentralinaAllarmi.Centralina.Sensors
{
    /// <summary>
    /// Al fine di mockare i sensori come richiesto li abbiamo dotati di un alarm state
    /// con metodi get e set. Quando nell'interfaccia si attiva/disattiva un allarme
    /// si modifica lo stato del sensore
    /// La classe astratta è dotata di un metodo abstract readSensor() che simula la lettura di un sensore
    /// diverso per ogni tipo concreto
    /// </summary>
    public abstract class AbstractSensor
    {

        public int sensorId { get; set; }

        public bool alarmState { get; set; }

        public abstract bool readSensor();
}
    }
}
