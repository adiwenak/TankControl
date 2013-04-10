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
using TankControl.Class;
using System.Windows.Media.Animation;
using TankControl.View.ComponentGDA;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for GraphicDisplayArea.xaml
    /// </summary>
    public partial class GraphicDisplayArea : UserControl
    {
        private MainTankComponent mt;
        private ShakeComponent sc;
        private TinyTankPumpComponent tt1;
        private TinyTankPumpComponent tt2;
        private TinyTankLComponent tt3;
        private TinyTankLComponent tt4;
        private TinyTankRComponent tt5;
        private TinyTankRComponent tt6;
        private TinyTankRComponent tt7;

        public GraphicDisplayArea()
        {
            InitializeComponent();

            Process.Singleton.AddView(this);

            mt = mainTank;
            sc = shakeComponent;
            tt1 = tinyTank_1;
            tt2 = tinyTank_2;
            tt3 = tinyTank_3;
            tt4 = tinyTank_4;
            tt5 = tinyTank_5;
            tt6 = tinyTank_6;
            tt7 = tinyTank_7;
        }


        public ShakeComponent Sc
        {
            get
            {
                return this.sc;
            }
            set
            {
                this.sc = value;
            }
        }

        public TinyTankPumpComponent Tt1
        {
            get
            {
                return this.tt1;
            }
            set
            {
                this.tt1 = value;
            }
        }
        public TinyTankPumpComponent Tt2
        {
            get
            {
                return this.tt2;
            }
            set
            {
                this.tt2 = value;
            }
        }
        public TinyTankLComponent Tt3
        {
            get
            {
                return this.tt3;
            }
            set
            {
                this.tt3 = value;
            }
        }
        public TinyTankLComponent Tt4
        {
            get
            {
                return this.tt4;
            }
            set
            {
                this.tt4 = value;
            }
        }
        public TinyTankRComponent Tt5
        {
            get
            {
                return this.tt5;
            }
            set
            {
                this.tt5 = value;
            }
        }
        public TinyTankRComponent Tt7
        {
            get
            {
                return this.tt7;
            }
            set
            {
                this.tt7 = value;
            }
        }
        public TinyTankRComponent Tt6
        {
            get
            {
                return this.tt6;
            }
            set
            {
                this.tt6 = value;
            }
        }
        public MainTankComponent Mt
        {
            get
            {
                return this.mt;
            }
            set
            {
                this.mt = value;
            }
        }

        public void UpdateView()
        {

        }

    }

}
