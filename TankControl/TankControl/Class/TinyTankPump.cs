using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class TinyTankPump : BaseTank
    {
        private TinyTankPumpComponent view;
        public TinyTankPumpComponent View
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

        public TinyTankPump(TinyTankPumpComponent tankView, string tankName)
        {
            this.View = tankView;
            this.ID = new Guid();

            if (tankName != null)
            {
                this.Name = tankName;
            }

            Component bbl = new BigValveL(tankView.Bv, "asd");
            this.AddComponent(bbl);
            Component smv = new SmallValve(tankView.Sv, "asd");
            this.AddComponent(bbl);
            Component pump = new Pump(tankView.Pc, "asd");
            this.AddComponent(bbl);
            this.AddComponent(smv);
            this.AddComponent(pump);
        }

        public void RunPump()
        {
            this.View.Pc.Open();
        }

        public void RunValveSmall()
        {
            this.View.Sv.Run();
        }

        public void RunValveBig()
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
