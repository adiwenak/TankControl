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
        private TinyTankPumpComponent tPump1;
        private TinyTankPumpComponent tPump2;
        private TinyTankLComponent tt3;
        private TinyTankLComponent tt4;
        private TinyTankRComponent tt5;
        private TinyTankRComponent tt6;
        private TinyTankRComponent tt7;

        public GraphicDisplayArea()
        {
            InitializeComponent();

            GdaMainTank = mainTank;
            sc = shakeComponent;
            tPump1 = tinyTank_1;
            tPump2 = tinyTank_2;
            tt3 = tinyTank_3;
            tt4 = tinyTank_4;
            tt5 = tinyTank_5;
            tt6 = tinyTank_6;
            tt7 = tinyTank_7;

            this.Components = new List<UserControl>();
            this.Components.Add(tPump1);
            this.Components.Add(tPump2);
            this.Components.Add(tt3);
            this.Components.Add(tt4);
            this.Components.Add(tt5);
            this.Components.Add(tt6);
            this.Components.Add(tt7);
            this.Components.Add(sc);
            
            Process.Singleton.AddView(this);


        }

        // PROPERTIES - START
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

        public ShakeComponent GdaMainTankShake
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

        public TinyTankPumpComponent GdaTinyTankPump1
        {
            get
            {
                return this.tPump1;
            }
            set
            {
                this.tPump1 = value;
            }
        }
        public TinyTankPumpComponent GdaTinyTankPump2
        {
            get
            {
                return this.tPump2;
            }
            set
            {
                this.tPump2 = value;
            }
        }
        public TinyTankLComponent GdaTinyTank3
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
        public TinyTankLComponent GdaTinyTank4
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
        public TinyTankRComponent GdaTinyTank5
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
        public TinyTankRComponent GdaTinyTank6
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
        public TinyTankRComponent GdaTinyTank7
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

        public MainTankComponent GdaMainTank
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

        // PROPERTIES - END
        
        public void RunWhole()
        {
            this.GdaMainTank.Run();
            this.GdaTinyTankPump1.Bv.Open();
            this.GdaTinyTankPump1.Sv.Open();
            this.GdaTinyTankPump2.Bv.Open();
            this.GdaTinyTankPump2.Sv.Open();
            this.GdaTinyTank3.Bv.Open();
            this.GdaTinyTank4.Bv.Open();
            this.GdaTinyTank5.Bv.Open();
            this.GdaTinyTank6.Bv.Open();
            this.GdaTinyTank7.Bv.Open();

            this.GdaMainTankShake.slc.Open();
            this.GdaMainTankShake.src.Open();
            this.GdaMainTankShake.oc.Open();
        }

        public void StopWhole()
        {
            this.GdaMainTank.Stop();
            this.GdaTinyTankPump1.Bv.Close();
            this.GdaTinyTankPump1.Sv.Close();
            this.GdaTinyTankPump2.Bv.Close();
            this.GdaTinyTankPump2.Sv.Close();
            this.GdaTinyTank3.Bv.Close();
            this.GdaTinyTank4.Bv.Close();
            this.GdaTinyTank5.Bv.Close();
            this.GdaTinyTank6.Bv.Close();
            this.GdaTinyTank7.Bv.Close();
            this.GdaMainTankShake.slc.Close();
            this.GdaMainTankShake.src.Close();
            this.GdaMainTankShake.oc.Close();
        }

        public void UpdateView()
        {

        }


    }

}
