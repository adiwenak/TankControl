using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;
using System.Windows.Controls;
using TankControl.Functions;

namespace TankControl.Class
{
    public class TinyTankR : BaseTank
    {
        private TinyTankRComponent view;
        private bool isValveOpen;
        private static string valveOne = "v1";

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
        public TinyTankR(TinyTankRComponent tankView, int id)
        {
            this.View = tankView;
            this.ID = id;

            Component bigValve = new BigValveR(tankView.Bv, (int)ReferenceEnum.Component.ValveBig);
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
