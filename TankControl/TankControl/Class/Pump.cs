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

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public bool IsRun()
        {
            throw new NotImplementedException();
        }
    }
}
