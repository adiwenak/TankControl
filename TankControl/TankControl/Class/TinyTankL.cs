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
            //if (components.Count > 0)
            //{
            //    this.AddComponents(components);
            //}
            
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
