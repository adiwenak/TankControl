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

        public enum MOXA
        {
            DO0 = 0,
            DO1,
            DO2,
            DO3,
            DO4,
            DO5,
            DO6,
            DO7,
            DO8,
            DO9,
            DO10,
            DO11,
            DO12,
            DO13,
            DO14,
            DO15
        }
    }
}
