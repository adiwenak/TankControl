using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl;

namespace TankControl.Class
{
    public class MainTank : BaseTank
    {

        public ICollection<TinyTank> TinyTank
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
