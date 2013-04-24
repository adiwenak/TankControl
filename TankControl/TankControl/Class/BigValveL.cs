using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class BigValveL : Component
    {
        private BigValveLComponent view;
        private int id;
        private bool isRun;

        public BigValveLComponent View
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

        public BigValveL(BigValveLComponent valveView,int id)
        {
            View = valveView;
            Id = id;
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
