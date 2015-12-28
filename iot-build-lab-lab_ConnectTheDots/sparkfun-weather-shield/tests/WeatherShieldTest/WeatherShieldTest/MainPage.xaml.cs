﻿using System;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using Microsoft.Maker.Sparkfun.WeatherShield;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherShieldTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private WeatherShield shield;

        private float altitude;
        private float humidity;
        private float pressure;
        private float temperature;

        public MainPage()
        {
            InitializeComponent();
            shield = new WeatherShield("I2C1", 6, 5);
            ReadySet();
        }

        private async void ReadySet()
        {
            Status.Text = "Initialize Shield Components...";
            await shield.BeginAsync();
            Status.Text += "\nShield Ready!";

            UpdateScreen();
            GoButton.IsEnabled = true;

            if (GpioPinValue.Low == shield.BlueLedPin.Read()) {
                BlueLED.Fill = new SolidColorBrush(Colors.Black);
            }
            else {
                BlueLED.Fill = new SolidColorBrush(Colors.DarkCyan);
            }
            BlueLedButton.IsEnabled = true;

            if (GpioPinValue.Low == shield.GreenLedPin.Read()) {
                GreenLED.Fill = new SolidColorBrush(Colors.Black);
            }
            else {
                GreenLED.Fill = new SolidColorBrush(Colors.DarkSeaGreen);
            }
            GreenLedButton.IsEnabled = true;

        }

        private async void Go(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GoButton.IsEnabled = false;
            altitude = shield.Altitude;
            humidity = shield.Humidity;
            pressure = shield.Pressure;
            temperature = shield.Temperature;

            Status.Text += "\n";
            Status.Text += string.Format("\nAltitude: {0:N2}(m)", altitude.ToString());
            Status.Text += string.Format("\nHumidity: {0:N2}(%RH)", humidity.ToString());
            Status.Text += string.Format("\nPressure: {0:N2}(Pa)", pressure.ToString());
            Status.Text += string.Format("\nTemperature: {0:N2}(C)", temperature.ToString());

            UpdateScreen();

            await Task.Delay(2000);
            GoButton.IsEnabled = true;
        }

        private void ToggleBlueLed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ( GpioPinValue.Low == shield.BlueLedPin.Read() ) { 
                shield.BlueLedPin.Write(GpioPinValue.High);
                BlueLED.Fill = new SolidColorBrush(Colors.DarkCyan);
            }
            else {
                shield.BlueLedPin.Write(GpioPinValue.Low);
                BlueLED.Fill = new SolidColorBrush(Colors.Black);
            }
        }

        private void ToggleGreenLed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (GpioPinValue.Low == shield.GreenLedPin.Read()) {
                shield.GreenLedPin.Write(GpioPinValue.High);
                GreenLED.Fill = new SolidColorBrush(Colors.DarkSeaGreen);
            } else {
                shield.GreenLedPin.Write(GpioPinValue.Low);
                GreenLED.Fill = new SolidColorBrush(Colors.Black);
            }
        }

        private void UpdateScreen()
        {
            Altimeter.Value = altitude;
            Altitude.Text = string.Format("{0:N2}m", Altimeter.Value);

            Hygrometer.Value = humidity;
            Humidty.Text = string.Format("{0:N2}%RH", Hygrometer.Value);

            Barometer.Value = pressure / 1000;
            Pressure.Text = string.Format("{0:N4}kPa", Barometer.Value);

            Thermometer.Value = temperature;
            Temperature.Text = string.Format("{0:N2}C", Thermometer.Value);
        }

        private void Status_TextChanged(object sender, TextChangedEventArgs e)
        {
            var grid = (Grid)VisualTreeHelper.GetChild(Status, 0);
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                object obj = VisualTreeHelper.GetChild(grid, i);
                if (!(obj is ScrollViewer)) continue;
                ((ScrollViewer)obj).ChangeView(0.0f, ((ScrollViewer)obj).ExtentHeight, 1.0f);
                break;
            }
        }
    }
}
