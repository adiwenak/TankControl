using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class TinyTankPump : BaseTank
    {
        private TinyTankPumpComponent view;
        public TinyTankPumpComponent View
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
    }
}
