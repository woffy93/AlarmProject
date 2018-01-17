using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace WpfAppLogin.Centralina
{
    /// <summary>
    ///  classe che rappresenta la centralina controllo allarmi, connessa ai sensori
    /// </summary>
    public class AlarmController
    {
        private GeoCoordinate mapCoordinates { get; set; }
        private List<Sensor> sensors { get; set; }

        // gestire caso lista nulla
        public List<Sensor> readSensors()
        {
            return sensors.Where(s => s.getAlarm()).ToList();
        }

        public void allarm(string sensorID, string eventType, string note)
        {
            //scrivere allarme su db remoto
            //MOCK
            Console.WriteLine("Allarme scattato");
        }
    }
}