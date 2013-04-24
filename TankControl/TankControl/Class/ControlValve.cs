using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class ControlValve : Component
    {
        private ShakeValveLComponent view;
        private int id;
        private bool isRun;
        
        public ShakeValveLComponent View
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

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool IsRun
        {
            get
            {
                return this.isRun;
            }
            set
            {
                this.isRun = value;
            }

        }

        public ControlValve(ShakeValveLComponent shakeValve, int id)
        {
            this.Id = id;
            if (shakeValve != null)
            {
                this.View = shakeValve;
            }
        }

        public void Run()
        {
            if (this.IsRun == false)
            {
                this.View.Open();
                this.IsRun = true;
            }
        }

        public void Stop()
        {
            if (this.IsRun == true)
            {
                this.View.Close();
                this.IsRun = false;
            }
        }

    }
}
