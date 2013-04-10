using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class TinyTankR : BaseTank
    {
        private TinyTankRComponent view;
        public TinyTankRComponent View
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
