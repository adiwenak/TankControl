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
        private IList<TinyTankL> tinyTanksL;
        private IList<TinyTankR> tinyTanksR;
        private IList<TinyTankPump> tinyTanksPump;


        public IList<TinyTankPump> TinyTanksPump
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

        public IList<TinyTankR> TinyTanksR
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

        public IList<TinyTankL> TinyTanksL
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
            this.ID = new Guid();
            this.View = tankView;
            this.TinyTanksR = new List<TinyTankR>();
            this.TinyTanksL = new List<TinyTankL>();
            this.TinyTanksPump = new List<TinyTankPump>();
            if (tankName != null)
            {
                this.Name = tankName;
            }
        }

        public void Run()
        {
            foreach (TinyTankL ttL in this.tinyTanksL)
            {
                ttL.Run();
            }

            foreach (TinyTankR ttL in this.tinyTanksR)
            {
                ttL.Run();
            }

            foreach (TinyTankPump ttL in this.tinyTanksPump)
            {
                ttL.Run();
            }

            View.Run();

        }

        public void Stop()
        {
            View.Stop();
        }
    }
}
