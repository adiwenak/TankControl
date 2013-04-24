using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Functions
{
    class ReferenceEnum
    {
        public enum Component
        {
            ValveSmall = 1,
            ValveBig,
            ValveControl,
            ValveShake,
            ValveOutput,
            PumpTinyTank,
            PumpMainTank
        }

        public enum Tank
        {
            MainTank = 1,
            TinyTankOne,
            TinyTankTwo,
            TinyTankThree,
            TinyTankFour,
            TinyTankFive,
            TinyTankSix,
            TinyTankSeven
        }
    }
}
