using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

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

        public TinyTankL(TinyTankLComponent tankView, string tankName,List<Component> components)
        {
            this.View = tankView;
            this.ID = new Guid();
            if (tankName != null)
            {
                this.Name = tankName;
            }

            if (components.Count > 0)
            {
                this.AddComponents(components);
            }
            
        }
         
    }
}
