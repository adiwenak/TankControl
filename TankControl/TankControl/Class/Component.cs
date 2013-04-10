using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TankControl.Class
{
    public interface Component 
    {
        Guid Id {get; set; }

        string Name{ get; set; }

        void Run();

        void Stop();

        bool IsRun();
    }

}
