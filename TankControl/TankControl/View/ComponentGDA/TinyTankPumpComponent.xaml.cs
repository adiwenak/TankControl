using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TankControl.View.ComponentGDA
{
    /// <summary>
    /// Interaction logic for TinyTankPump.xaml
    /// </summary>
    public partial class TinyTankPumpComponent : UserControl
    {
        public BigValveLComponent Bv;
        public SmallValveComponent Sv;
        public PumpComponent Pc;

        public TinyTankPumpComponent()
        {
            InitializeComponent();
            Bv = BigValve;
            Sv = SmallValve;
            Pc = Pump;
        }
        public PumpComponent PPc
        {
            get
            {
                return this.Pc;
            }
            set
            {
                this.Pc = value;
            }
        }

        public SmallValveComponent PSv
        {
            get
            {
                return this.Sv;
            }
            set
            {
                this.Sv = value;
            }
        }

        public BigValveLComponent PBv
        {
            get
            {
                return this.Bv;
            }
            set
            {
                this.Bv = value;
            }
        }
    }
}
