using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public interface Component 
    {
        int Id {get; set; }

        bool IsRun { get; set; }

        void Run();

        void Stop();

    }

}
