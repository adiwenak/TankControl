﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class MainTank : BaseTank
    {
        private MainTankComponent view;

        public MainTankComponent View
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
    }
}
