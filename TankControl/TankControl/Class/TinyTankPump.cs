using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;
using TankControl.Functions;

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

        public TinyTankPump(TinyTankPumpComponent tankView, int id,List<IComponent> list)
        {
            this.View = tankView;
            this.ID = id;
            this.AddComponents(list);
        }

        public void RunPump()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.PumpTinyTank);
            if (cmp != null)
            {
                cmp.Run();
                this._TempAddWeight();
                pump = true;
            }
        }

        public void StopPump()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.PumpTinyTank);
            if (cmp != null)
            {
                cmp.Stop();
                this._TempEqualWeight();
                pump = false;
            }
        }

        public void RunValveSmall()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveSmall);
            if (cmp != null)
            {
                cmp.Run();
                this._TempAddWeight();
                valveSmall = true;
            }
        }

        public void StopValveSmall()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveSmall);
            if (cmp != null)
            {
                cmp.Stop();
                this._TempEqualWeight();
                valveSmall = false;
            }
        }

        public void RunValveBig()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Run();
                this._TempAddWeight();
                valveBig = true;
            }
        } 

        public void StopValveBig()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Stop();
                this._TempEqualWeight();
                valveBig = false;
            }
        }

        public void End()
        {
            this.StopValveBig();
            this.StopPump();
            this.StopValveSmall();
            this._TempEqualWeight();
            this.CleanUp();
        }

        public void CleanUp()
        {
            this.StageLimit = 0;
            this.StageLimit2 = 0;
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
