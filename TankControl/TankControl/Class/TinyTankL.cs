using System;
using System.Linq;
using TankControl.View.ComponentGDA;
using TankControl.Functions;
using System.Collections.Generic;

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

        public TinyTankL(TinyTankLComponent tankView, int id,List<IComponent> list)
        {
            this.View = tankView;
            this.ID = id;
            this.AddComponents(list);
        }

        public void Run()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);

            if (cmp != null)
            {
                cmp.Run();
                this._TempAddWeight();
            }
        }

        public void Stop()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveBig);
            if (cmp != null)
            {
                cmp.Stop();
                this._TempEqualWeight();
            }
        }

        public void End(bool cleanup)
        {
            this.Stop();
            if (cleanup)
            {
                this.CleanUp();
            }
        }
         
    }
}
