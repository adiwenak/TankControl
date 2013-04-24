using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class Pump : Component
    {
        private PumpComponent view;
        private int id;
        private bool isRun;

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

        public Pump(PumpComponent pumpView, int id)
        {
            View = pumpView;
            Id = id;
        }

        public void Run()
        {
            if (this.IsRun == false)
            {
                this.View.Run();
                this.IsRun = true;
            }
        }

        public void Stop()
        {
            if (this.IsRun == true)
            {
                this.View.Stop();
                this.IsRun = false;
            }
        }

    }
}
