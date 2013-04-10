using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class SmallValve : Component
    {
        private SmallValveComponent view;
        private Guid id;
        private string name;

        public SmallValveComponent View
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

        public SmallValve(SmallValveComponent smallValve, string nameValve)
        {
            View = smallValve;
            Id = new Guid();
            if (nameValve != null)
                Name = nameValve;
        }

        public void Run()
        {
            View.Run();
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
