using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.ComponentGDA;

namespace TankControl.Class
{
    public class Valve : Component
    {
        private UCValve view;

        public UCValve View
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

        public void Run()
        {
            view.valveRun();
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public bool IsRun()
        {
            throw new NotImplementedException();
        }

    }
}
