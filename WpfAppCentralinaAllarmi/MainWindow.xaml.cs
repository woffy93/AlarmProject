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
        private string[] sensorLabels = { "Antincendio", "Gas", "Antintrusione" };
        private Centralina.AlarmController controller;
        private Ellipse[] lights;
        private SolidColorBrush brush;


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
            });
            //inizializzo array luci
            lights = new Ellipse[] { Light_fire, Light_Gas, Light_Intrusion };
            //inizializzo i pennelli
            brush = new SolidColorBrush();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            control();
        }

        private void Btn_Fire_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], true);
        }

        private void Btn_Fire_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], false);
        }

        private void Btn_Gas_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], true);
        }

        private void Btn_Gas_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], false);
        }

        private void Btn_Intrusion_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], true);
        }

        private void Btn_Intrusion_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], false);
        }

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

        private void setLightColor(Ellipse light, bool state, string name)
        {
            if (name == sensorLabels[0])
            {
                Light_fire.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else if (name == sensorLabels[1])
            {
                return lights[1];
            }
            else
            {
                return lights[2];
            }

            if (state == true)
            {
                this.brush.Color = Color.FromRgb(255, 0, 0);
                light.Fill = brush;
            }
            else
            {
                this.brush.Color = Color.FromRgb(0, 255, 0);
                light.Fill = brush;
            }
        }

        private async void control()
        {
            while (true)
            {
                Dictionary<string, bool> dict = await checkSensors();

                /*dict.ToList()
                    .ForEach(t => setLightColor(findLight(t.Key), t.Value)); */

                foreach(KeyValuePair<string, bool> entry in dict)
                {
                    if(entry.Value == true)
                    {
                        setLightColor(findLight(entry.Key), true);
                    }
                    else
                    {
                        setLightColor(findLight(entry.Key), false);
                    }
                }
            }                    
        }
    }
}
