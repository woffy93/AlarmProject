using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCentralinaAllarmi
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// WindowLoaded fa partire il loop che controlla i sensori per
    /// aggiornale l'UI a seconda di quali siano attivati.
    /// Le luci sono aggiornate dal loop e non dalla pressione del tasto perché abbiamo
    /// voluto simulare il ciclo di vita di una reale centralina che legge i sensori
    /// ed invia segnali in base al loroo stato
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] sensorLabels = { "Incendio", "Fuoriuscita di gas", "Intrusione" };
        private Centralina.AlarmController controller;
        private Ellipse[] lights;
        private int Id = 2;


        /*TODO:
         * Controllo su db remoto per vedere quali sensori sono da controllare
         */

        public MainWindow()
        {
            InitializeComponent();
            //inizializzo centralina sensori
            controller = new Centralina.AlarmController(
                new Dictionary<string, Centralina.Sensors.AbstractSensor>
            {
                {sensorLabels[0],  new Centralina.Sensors.FireSensor()},
                {sensorLabels[1],  new Centralina.Sensors.GasSensor()},
                {sensorLabels[2],  new Centralina.Sensors.IntrusionSensor()}
            },
                Id);
            //inizializzo array luci
            lights = new Ellipse[] { Light_fire, Light_Gas, Light_Intrusion };
            //inizializzo i pennelli
            
        }

        //metodi per controllo UI

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            control();
        }

        //i bottoni cambiano lo stato del sensore e chiamano il metodo per scrivere sul db
        //l'attivazione/disattivazione dell'allarme

        private void Btn_Fire_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], true);
            controller.allarm(
                controller.getSensorId(sensorLabels[0]),
                sensorLabels[0],
                DateTime.Now);
        }

        private void Btn_Fire_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], false);
        }

        private void Btn_Gas_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], true);
            controller.allarm(
                controller.getSensorId(sensorLabels[1]),
                sensorLabels[1],
                DateTime.Now);
        }

        private void Btn_Gas_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], false);
 
        }

        private void Btn_Intrusion_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], true);
            controller.allarm(
                controller.getSensorId(sensorLabels[2]),
                sensorLabels[2],
                DateTime.Now);
        }

        private void Btn_Intrusion_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], false);
        }

        //Metodi helper

        private async Task<Dictionary<string, bool>> checkSensors()
        {
            await Task.Delay(500);
            return this.controller.getAlarms();
        }

        private Ellipse findLight(string name)
        {
            if(name == sensorLabels[0])
            {
                return lights[0];
            }else if( name == sensorLabels[1])
            {
                return lights[1];
            }
            else
            {
                return lights[2];
            }
        }
        //metodo per le luci, a seconda dello stato le colora in modo diverso
        private void setLightColor(string name, bool state)
        {
            if (name == sensorLabels[0])
            {
                Light_fire.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else if (name == sensorLabels[1])
            {
                Light_Gas.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                Light_Intrusion.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
        }

        //metodo asincrono per controllare i sensori e colorare l'UI senza bloccare tutto
        private async void control()
        {
            while (true)
            {
                Dictionary<string, bool> dict = await checkSensors();
                dict.ToList()
                    .ForEach(t => setLightColor(t.Key, t.Value)); 
            }                    
        }
  
        /// <summary>
        /// metodo per controllare sul db quali sensori leggere  
        /// </summary>
        private void checkbForSensors()
        {


            Console.WriteLine("querydb");
        }
    }
}