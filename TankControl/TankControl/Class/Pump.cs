using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class Pump : Component
    {
        private PumpComponent view;
        private Guid id;
        private string name;

        public PumpComponent View
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

        public Pump(PumpComponent pumpView, string pumpName)
        {
            View = pumpView;
            Id = new Guid();
            if (pumpName != null)
                Name = pumpName;
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
