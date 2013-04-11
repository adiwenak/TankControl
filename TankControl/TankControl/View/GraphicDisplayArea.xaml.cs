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
        private List<UserControl> components;

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

            Mt = mainTank;
            sc = shakeComponent;
            tt1 = tinyTank_1;
            tt2 = tinyTank_2;
            tt3 = tinyTank_3;
            tt4 = tinyTank_4;
            tt5 = tinyTank_5;
            tt6 = tinyTank_6;
            tt7 = tinyTank_7;

            this.Components = new List<UserControl>();
            this.Components.Add(tt1);
            this.Components.Add(tt2);
            this.Components.Add(tt3);
            this.Components.Add(tt4);
            this.Components.Add(tt5);
            this.Components.Add(tt6);
            this.Components.Add(tt7);
            this.Components.Add(sc);
            
            Process.Singleton.AddView(this);


        }


        public List<UserControl> Components
        {
            get
            {
                return this.components;
            }
            set
            {
                this.components = value;
            }
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
        
        public void RunWhole()
        {
            this.Mt.Run();
            this.Tt1.Bv.Run();
            this.Tt1.Sv.Run();
            this.Tt2.Bv.Run();
            this.Tt2.Sv.Run();
            this.Tt3.Bv.Run();
            this.Tt4.Bv.Run();
            this.Tt5.Bv.Run();
            this.Tt6.Bv.Run();
            this.Tt7.Bv.Run();

            this.Sc.slc.Run();
            this.Sc.src.Run();
            this.Sc.oc.Run();
        }

        public void StopWhole()
        {
            this.Mt.Stop();
            this.Tt1.Bv.Stop();
            this.Tt1.Sv.Stop();
            this.Tt2.Bv.Stop();
            this.Tt2.Sv.Stop();
            this.Tt3.Bv.Stop();
            this.Tt4.Bv.Stop();
            this.Tt5.Bv.Stop();
            this.Tt6.Bv.Stop();
            this.Tt7.Bv.Stop();
            this.Sc.slc.Stop();
            this.Sc.src.Stop();
            this.Sc.oc.Stop();
        }

        public void UpdateView()
        {

        }


    }

}
