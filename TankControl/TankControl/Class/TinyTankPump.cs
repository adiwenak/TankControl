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
        private bool valveBig;
        private bool valveSmall;
        private bool pump;
        private float stageLimit2;

        public float StageLimit2
        {
            get
            {
                return this.stageLimit2;
            }
            set
            {
                this.stageLimit2 = value;
            }
        }

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
            if (this.pump == false)
            {
                this.View.Pc.Open();
                this.pump = true;
                this._TempAddWeight();
            }
        }

        public void RunValveSmall()
        {
            if (this.valveSmall == false)
            {
                this.View.Sv.Run();
                this.valveSmall = true;
                this._TempAddWeight();
            }
        }

        public void RunValveBig()
        {
            if (this.valveBig == false)
            {
                this.View.Bv.Run();
                this.valveBig = true;
                this._TempAddWeight();
            }
        }

        public void StopValveSmall()
        {
            if (this.valveSmall == true)
            {
                this.View.Sv.Stop();
                this.valveSmall = false;
                this._TempEqualWeight();
            }
        }

        public void StopPump()
        {
            if (this.pump == true)
            {
                this.View.Pc.Close();
                this.pump = false;
                this._TempEqualWeight();
            }
        }

        public void StopValveBig()
        {
            if (this.valveBig == true)
            {
                this.View.Bv.Stop();
                this.valveBig = false;
                this._TempEqualWeight();
            }
        }

        public void StopAll()
        {
            this.StopValveBig();
            this.StopPump();
            this.StopValveSmall();
            this._TempEqualWeight();
        }

        public void _TempAddWeight()
        {
            if (this.pump == true)
            {
                if (this.valveBig == true || this.valveSmall == true)
                {
                    RunTester.Singleton.AddWeight = 1;
                }
            }
        }

        public void _TempEqualWeight()
        {
            if (this.pump == false)
            {
                RunTester.Singleton.AddWeight = 0;
            }
        }
    }
}
