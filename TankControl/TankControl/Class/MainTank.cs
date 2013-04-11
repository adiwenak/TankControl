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
        private TinyTankPump tPump1;
        private TinyTankPump tPump2;
        private TinyTankL tLeft1;
        private TinyTankL tLeft2;
        private TinyTankR tRight1;
        private TinyTankR tRight2;
        private TinyTankR tRight3;

        // PROPERTIES - START

        public TinyTankPump TPump1
        {
            get
            {
                return this.tPump1;
            }
            set
            {
                this.tPump1 = value;
            }
        }

        public TinyTankPump TPump2
        {
            get
            {
                return this.tPump2;
            }
            set
            {
                this.tPump2 = value;
            }
        }

        public TinyTankR TRight3
        {
            get
            {
                return this.tRight3;
            }
            set
            {
                this.tRight3 = value;
            }
        }

        public TinyTankR TRight2
        {
            get
            {
                return this.tRight2;
            }
            set
            {
                this.tRight2 = value;
            }
        }

        public TinyTankR TRight1
        {
            get
            {
                return this.tRight1;
            }
            set
            {
                this.tRight1 = value;
            }
        }

        public TinyTankL TLeft2
        {
            get
            {
                return this.tLeft2;
            }
            set
            {
                this.tLeft2 = value;
            }
        }

        public TinyTankL TLeft1
        {
            get
            {
                return this.tLeft1;
            }
            set
            {
                this.tLeft1 = value;
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

        // PROPERTIES - END

        public MainTank(MainTankComponent tankView,string tankName)
        {
            this.ID = new Guid();
            this.View = tankView;
            if (tankName != null)
            {
                this.Name = tankName;
            }
        }

        public void Fillup()
        {
            this.View.Run();
        }

        public void RunTTankU1()
        {

        }

        public void RunTTankU2()
        {
        }

        public void RunTTankB1()
        {
        }

        public void RunTTankB2()
        {
        }

        public void RunTTankR1()
        {
        }

        public void RunTTankR2()
        {
        }

        public void RunTTankR3()
        {
        }
        
        public void Stop()
        {
            View.Stop();
        }
    }
}
