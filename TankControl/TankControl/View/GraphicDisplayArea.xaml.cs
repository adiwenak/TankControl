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
        private MainTank mt;     
        private TinyTank tt3;
        private TinyTank tt4;
        private TinyTankR tt5;
        private TinyTankR tt6;
        private TinyTankR tt7;

        public GraphicDisplayArea()
        {
            InitializeComponent();

            Process.Singleton.AddView(this);

            mt = mainTank;
            tt3 = tinyTank_3;
            tt4 = tinyTank_4;
            tt5 = tinyTank_5;
            tt6 = tinyTank_6;
            tt7 = tinyTank_7;
        }

        

        public TinyTank Tt3
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

        public TinyTank Tt4
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

        public TinyTankR Tt5
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

        public TinyTankR Tt7
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

        public TinyTankR Tt6
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

        public MainTank Mt
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
