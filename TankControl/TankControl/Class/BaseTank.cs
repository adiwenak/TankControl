using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.Class;

namespace TankControl
{
    public class BaseTank
    {

        public ICollection<Component> Components
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
