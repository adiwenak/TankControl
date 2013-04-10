using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class BigValveR : Component
    {
        private BigValveRComponent view;
        public BigValveRComponent View
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
