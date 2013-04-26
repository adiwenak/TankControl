using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;
using TankControl.Functions;

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

        public TinyTankL(TinyTankLComponent tankView, int id)
        {
            this.View = tankView;
            this.ID = id;

            Component bigValve = new BigValveL(tankView.Bv, (int)ReferenceEnum.Component.ValveBig);
            this.AddComponent(bigValve);
        }

        public void Run()
        {
            Component cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);

            if (cmp != null)
            {
                cmp.Run();
                this._TempAddWeight();
            }
        }

        public void Stop()
        {
            Component cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Stop();
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
