using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public interface Component
    {
        void Run();
        void Stop();
        bool IsRun();
    }

}
