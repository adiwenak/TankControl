using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.View;
using System.Windows.Controls;

namespace TankControl.Class
{
    public class Valve : Component
    {
        private UserControl view;

        public UserControl View
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
