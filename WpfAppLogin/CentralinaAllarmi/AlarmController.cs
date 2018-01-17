using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace WpfAppLogin.CentralinaAllarmi
{
    /// <summary>
    ///  classe che rappresenta la centralina controllo allarmi, connessa ai sensori
    /// </summary>
    public class AlarmController
    {
        private GeoCoordinate mapCoordinates {get; set;}
        private List<SensorInterface> sensors { get; set; }

        // gestire caso lista nulla
        public List<SensorInterface> readSensors()
        {
            return sensors.Where(s => s.getAlarm()).ToList();
        }

        public void allarm(string sensorID, string eventType, string note)
        {
            //scrivere allarme su db remoto
            //MOCK
            System.println("Allarme scattato")
        }
    }
}
