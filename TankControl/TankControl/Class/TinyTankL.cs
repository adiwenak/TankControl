using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public class TinyTankL : BaseTank
    {
        private TinyTankLComponent view;
        public TinyTankLComponent View
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
