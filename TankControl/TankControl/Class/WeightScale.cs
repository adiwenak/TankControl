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
using System.IO;
using TankControl.Class.Functions;

namespace TankControl.Class
{
    public class WeightScale
    {
        private static WeightScale singleton;
        private SerialPort serialPort;
        private int counterInterval = 1;
        private static int interval = (int)(TankControl.Properties.Settings.Default.WSInterval * 10);

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

        // Use for testing purposes, SystemTest = 1 << in setting
        public void WeightScaleUpdated(decimal weight, decimal addWeight)
        {
            this.CurrentWeight = weight;
            this.Notify(addWeight);
        }

        // METHOD

        public bool IsConnected()
        {
            return this.serialPort.IsOpen;
        }

        public bool Connect()
        {
            bool success = false;
            if (!this.serialPort.IsOpen)
            {
                this.serialPort.PortName = TankControl.Properties.Settings.Default.WSPort;
                this.serialPort.BaudRate = TankControl.Properties.Settings.Default.WSBaudRate;

                try
                {
                    this.serialPort.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    TCFunction.MessageBoxError(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    TCFunction.MessageBoxError(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    TCFunction.MessageBoxError(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    TCFunction.MessageBoxError(ex.Message);
                }
                catch (IOException ex)
                {
                    TCFunction.MessageBoxError(ex.Message);
                }

                if (this.serialPort.IsOpen)
                {
                    success = true;
                    this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
                }
            }
            else
            {
                TCFunction.MessageBoxFail("Port weight scale sudah dibuka");
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

        /// <summary>
        /// Handles the DataReceived event of the serialPort control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.Ports.SerialDataReceivedEventArgs" /> instance containing the event data.</param>
        /// interval use for deciding in what frequency need to update the weight of the system, because by default the weight scale device updated by 10/s
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (counterInterval == interval)
            {
                this.counterInterval = 1;

                if (serialPort.IsOpen)
                {
                    string resp = "";

                    try
                    {
                        resp = serialPort.ReadLine();
                    }
                    catch (IOException ex)
                    {
                        TCFunction.MessageBoxError(ex.InnerException.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        TCFunction.MessageBoxError(ex.InnerException.Message);
                    }
                    catch (TimeoutException ex)
                    {
                        TCFunction.MessageBoxError(ex.InnerException.Message);
                    }

                    if (resp.Length > 7)
                    {
                        int startIndex = resp.IndexOf("0");
                        if (startIndex > 0)
                        {
                            string toParse = resp.Substring(startIndex, 7);
                            decimal weight;
                            if (Decimal.TryParse(toParse, out weight))
                            {
                                decimal addWeight = weight - this.CurrentWeight;
                                this.CurrentWeight = weight;
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                    new Action(() => this.Notify(addWeight)));
                            }
                        }
                    }
                }


            }
            else
            {
                this.counterInterval++;
            }
        }

        // Notify the process as well as updating the weight label in Control Area View
        public void Notify(decimal addWeight)
        {
            if (this.Process != null)
            {
                string value = this.CurrentWeight.ToString() + " kg";
                this.WeightLabel.Content = value;
                this.Process.WeightUpdated(this.CurrentWeight, (float)addWeight);
            }
        }

    }
}
