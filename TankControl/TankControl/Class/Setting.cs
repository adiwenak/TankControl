using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;

namespace TankControl.Class
{
    public static class Setting
    {
        public static double Limit
        {
            get
            {
                double limit = Properties.Settings.Default.Limit;
                return limit;
            }

        }
    }
}
