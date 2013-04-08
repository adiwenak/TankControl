using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Model
{
    public interface Component
    {
        void run();
        void stop();
        bool isRun();
    }

}
