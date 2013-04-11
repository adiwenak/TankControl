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
        private Component valve;

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
            this.View.Bv.Run();
        }

        public void Stop()
        {
            this.View.Bv.Stop();
        }

        public void Pause()
        {
            this.View.Bv.Pause();
        }

        public void Resume()
        {
            this.View.Bv.Resume();
        }
    }
}
