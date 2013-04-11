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
        private bool isValveOpen;

        public bool IsValveOpen
        {
            get
            {
                return this.isValveOpen;
            }
            set
            {
                this.isValveOpen = value;
            }
        }

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

        public TinyTankL(TinyTankLComponent tankView, string tankName)
        {
            this.View = tankView;
            this.ID = new Guid();
            if (tankName != null)
            {
                this.Name = tankName;
            }


            Component bbl = new BigValveL(tankView.Bv, "asd");
            this.AddComponent(bbl);
        }

        public void Run()
        {
            if (this.IsValveOpen == false)
            {
                this.View.Bv.Run();
                this.IsValveOpen = true;
                this._TempAddWeight();
            }
        }

        public void Stop()
        {
            if (this.isValveOpen == true)
            {
                this.View.Bv.Stop();
                this.IsValveOpen = false;
                this._TempEqualWeight();
            }
        }

        public void End()
        {
            this.Stop();
            this.CleanUp();
        }
         
    }
}
