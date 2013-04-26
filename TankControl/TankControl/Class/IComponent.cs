using System;
using System.Linq;


namespace TankControl.Class
{
    public interface IComponent 
    {
        int Id {get; set; }

        bool IsRun { get; set; }

        UInt16 DeviceAddress { get; set; }

        void Run();

        void Stop();

    }

}
