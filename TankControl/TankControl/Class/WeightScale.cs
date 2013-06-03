using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Windows.Controls;
using TankControl.View;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;

namespace TankControl.Class
{
    public class WeightScale
    {
        private static WeightScale singleton;
        private SerialPort serialPort;
        private int counterInterval = 1;
        private static int interval = TankControl.Properties.Settings.Default.WSInterval * 10;

        public static WeightScale Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new WeightScale();
                }

                return singleton;
            }
        }

        public WeightScale()
        {
            this.serialPort = new SerialPort();
        }

        // PROPERTIES - START
        public decimal CurrentWeight { get; set; }
        public float Limit { get; set; }
        public Process Process { get; set; }
        public Label WeightLabel { get; set; }
        public ControlArea control { get; set; }

        // PROPERTIES END

        // UPDATE OBSERVER
        public void Notify(decimal addWeight)
        {
            if (this.Process != null)
            {
                this.Process.WeightUpdated(this.CurrentWeight, (float)addWeight);
            }
        }

        // LISTENER TO WEIGHT SCALE
        public void WeightScaleUpdated(decimal weight, decimal addWeight)
        {
            this.CurrentWeight = weight;
            this.Notify(addWeight);
            this.DisplayText(weight);
        }

        // METHOD

        public bool IsConnected()
        {
            return this.serialPort.IsOpen;
        }

        public bool Connect()
        {
            bool success = false;
            this.serialPort.PortName = TankControl.Properties.Settings.Default.WSPort;
            this.serialPort.BaudRate = TankControl.Properties.Settings.Default.WSBaudRate;

            this.serialPort.Open();

            if (this.serialPort.IsOpen)
            {
                success = true;
                this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            }

            return success;
        }

        public bool Disconnect()
        {
            bool success = false;

            this.serialPort.Close();

            if (!this.serialPort.IsOpen)
            {
                success = true;
            }

            return success;
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            if (counterInterval == interval)
            {
                this.counterInterval = 1;
                string response = serialPort.ReadExisting();
                string twoString = response.Substring(0, 2);
                if (twoString.Contains("W") && twoString.Contains("N"))
                {
                    decimal weight = decimal.Parse(response.Substring(3, 6));
                    decimal addWeight = weight - this.CurrentWeight;
                    this.CurrentWeight = weight;
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => this.Notify(addWeight)));
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.WeightLabel.Content = (weight.ToString() + " kg")));
                }


            }
            else
            {
                this.counterInterval++;
            }

        }

        private void DisplayText(decimal weight)
        {
            if (this.WeightLabel != null)
            {
                string value = weight.ToString() + " kg";
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.WeightLabel.Content = value));
            }
            else
            {

            }
        }

    }
}
