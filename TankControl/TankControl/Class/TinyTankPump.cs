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

        public decimal StageLimit2 {get;set;}

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

        public IComponent RunPump()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.PumpTinyTank);
            if (cmp != null)
            {
                cmp.Run();
                this.pump = true;
                this._TempAddWeight();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to run Pump");
            }

            return cmp;
        }

        public IComponent StopPump()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.PumpTinyTank);
            if (cmp != null)
            {
                cmp.Stop();
                this.pump = false;
                this._TempEqualWeight();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to stop Pump");
            }

            return cmp;
        }

        public IComponent RunValveSmall()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveSmall);
            if (cmp != null)
            {
                cmp.Run();
                this.valveSmall = true;
                this._TempAddWeight();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to run valve small");
            }

            return cmp;
        }

        public IComponent StopValveSmall()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveSmall);
            if (cmp != null)
            {
                cmp.Stop();
                this.valveSmall = false;
                this._TempEqualWeight();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to stop valve small");
            }

            return cmp;
        }

        public IComponent RunValveBig()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Run();
                this.valveBig = true;
                this._TempAddWeight();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to run Valve Big");
            }

            return cmp;
        } 

        public IComponent StopValveBig()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Stop();
                this.valveBig = false;
                this._TempEqualWeight(); 
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tiny Tank Pump unable to stop Valve Big");
            }

            return cmp;
        }

        public void SwicthValveSmall()
        {
            StopValveBig();
            RunValveSmall();
        }

        public IComponent GetPump()
        {
            return this.GetComponent((int)ReferenceEnum.Component.PumpTinyTank);
        }

        public IComponent GetBigValve()
        {
            return this.GetComponent((int)ReferenceEnum.Component.ValveBig);
        }

        public IComponent GetSmallValve()
        {
            return this.GetComponent((int)ReferenceEnum.Component.ValveSmall);
        }

        public void End(bool cleanup)
        {
            this.StopValveBig();
            this.StopPump();
            this.StopValveSmall();
            this._TempEqualWeight();
            if (cleanup)
            {
                this.CleanUp();
            }
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
