using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TankControl.Class
{
    public interface Component
    {
        void Run();

        void Stop();

        bool IsRun();
    }

}
