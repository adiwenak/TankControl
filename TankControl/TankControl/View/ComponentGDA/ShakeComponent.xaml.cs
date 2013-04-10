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
    /// Interaction logic for ShakeComponent.xaml
    /// </summary>
    public partial class ShakeComponent : UserControl
    {
        public ShakeValveLComponent slc;
        public ShakeValveRComponent src;
        public OutValveComponent oc;

        public ShakeComponent()
        {
            InitializeComponent();
            slc = ShakeValveL;
            src = ShakeValveR;
            oc = OutValve;
        }
        public OutValveComponent Oc
        {
            get
            {
                return this.oc;
            }
            set
            {
                this.oc = value;
            }
        }

        public ShakeValveRComponent Src
        {
            get
            {
                return this.src;
            }
            set
            {
                this.src = value;
            }
        }

        public ShakeValveLComponent Slc
        {
            get
            {
                return this.slc;
            }
            set
            {
                this.slc = value;
            }
        }
    }
}
