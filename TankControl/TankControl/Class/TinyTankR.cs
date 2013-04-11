using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;
using System.Windows.Controls;

namespace TankControl.Class
{
    public class TinyTankR : BaseTank
    {
        private TinyTankRComponent view;
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

        // CONTRUCTOR
        public TinyTankR(TinyTankRComponent tankView, string tankName)
        {
            this.View = tankView;
            this.ID = new Guid();
            if (tankName != null)
            {
                this.Name = tankName;
            }

            Component bbl = new BigValveR(tankView.Bv, "asd");
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
            if (this.IsValveOpen == true)
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
