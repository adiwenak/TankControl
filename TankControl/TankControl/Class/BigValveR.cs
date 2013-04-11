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
        private Guid id;
        private string name;

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

        public Guid Id
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


        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public BigValveR(BigValveRComponent valveView,string nameValve)
        {
            View = valveView;
            Id = new Guid();
            if (nameValve != null)
                Name = nameValve;
        }

        public void Run()
        {
            this.View.Run();
        }

        public void Stop()
        {
            this.View.Stop();
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
