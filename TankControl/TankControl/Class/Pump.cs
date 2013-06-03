using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class Pump : IComponent
    {
        private PumpComponent view;
        private int id;
        private bool isRun;

        private UInt16 deviceAddress;

        public UInt16 DeviceAddress
        {
            get
            {
                return this.deviceAddress;
            }
            set
            {
                this.deviceAddress = value;
            }
        }

        public bool IsRun
        {
            get
            {
                return this.isRun;
            }
            set
            {
                this.isRun = value;
            }

        }
        public PumpComponent View
        {
            get
            {
                return this.view;
            }
            set
            {
                this.view = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public Pump(UInt16 pumpAddress, PumpComponent pumpView, int id)
        {
            DeviceAddress = pumpAddress;
            View = pumpView;
            Id = id;
        }

        public void Run()
        {
            if (this.IsRun == false)
            {
                if (Microcontroller.Singleton.OnDigitalOutput(DeviceAddress) || TankControl.Properties.Settings.Default.SystemTest == 1)
                {
                    this.View.Run();
                }
                this.IsRun = true;
            }
        }

        public void Stop()
        {
            if (this.IsRun == true)
            {
                if (Microcontroller.Singleton.OffDigitalOutput(DeviceAddress) || TankControl.Properties.Settings.Default.SystemTest == 1)
                {
                    this.View.Stop();
                }
                this.IsRun = false;
            }
        }

    }
}
