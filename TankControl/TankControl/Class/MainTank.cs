using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class MainTank : BaseTank
    {
        private MainTankComponent view;
        private IEnumerable<TinyTankL> tinyTanksL;
        private IEnumerable<TinyTankR> tinyTanksR;
        private IEnumerable<TinyTankPump> tinyTanksPump;


        public IEnumerable<TinyTankPump> TinyTanksPump
        {
            get
            {
                return this.tinyTanksPump;
            }
            set
            {
                this.tinyTanksPump = value;
            }
        }

        public IEnumerable<TinyTankR> TinyTanksR
        {
            get
            {
                return this.tinyTanksR;
            }
            set
            {
                this.tinyTanksR = value;
            }
        }

        public IEnumerable<TinyTankL> TinyTanksL
        {
            get
            {
                return this.tinyTanksL;
            }
            set
            {
                this.tinyTanksL = value;
            }
        }

        public MainTankComponent View
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

        public MainTank(MainTankComponent tankView,string tankName)
        {
            this.View = tankView;
            this.ID = new Guid();

            if (tankName != null)
            {
                this.Name = tankName;
            }
        }

        public void InitializeComponent(IList<Component> components)
        {
            if (components.Count() > 0)
            {
                this.AddComponents(components);
            }
        }

        public void InitializeTinyTankL(IList<TinyTankL> tinyTanksL)
        {
            this.TinyTanksL = tinyTanksL;
        }

        public void InitializeTinyTankR(IList<TinyTankR> tinyTanksR)
        {
            this.TinyTanksR = tinyTanksR;
        }

        public void InitializeTinyTankPump(IList<TinyTankPump> tinyTanksPump)
        {
            this.TinyTanksPump = tinyTanksPump;
        }
    }
}
