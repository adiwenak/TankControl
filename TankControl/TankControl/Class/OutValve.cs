using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class OutValve : IComponent
    {
        private OutValveComponent view;
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

        public OutValveComponent View
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

        public OutValve(UInt16 valveAddress, OutValveComponent shakeValve, int id)
        {
            DeviceAddress = valveAddress;
            this.Id = id;
            this.View = shakeValve;
        }

        public void Run()
        {
            if (this.IsRun == false)
            {
                if (Microcontroller.Singleton.OnDigitalOutput(DeviceAddress) || TankControl.Properties.Settings.Default.SystemTest == 1)
                {
                    this.View.Open();
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
                    this.View.Close();
                }
                this.IsRun = false;
            }
        }

    }
}
