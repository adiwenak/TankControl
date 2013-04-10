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
using System.Windows.Media.Animation;

namespace TankControl.View.ComponentGDA
{
    /// <summary>
    /// Interaction logic for MainTank.xaml
    /// </summary>
    public partial class MainTank : UserControl
    {
        private Storyboard mainTankAnimation;
        public MainTank()
        {
            InitializeComponent();
            mainTankAnimation = (Storyboard)FindResource("storyTank");
        }
        public Storyboard MainTankAnimation
        {
            get
            {
                return this.mainTankAnimation;
            }
            set
            {
                this.mainTankAnimation = value;
            }
        }

        public void Clean() { 
            InitializeComponent();
            mainTankAnimation = (Storyboard)FindResource("storyTank");
        }
    }
}
