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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TankControl.View.ComponentGDA
{
    /// <summary>
    /// Interaction logic for ShakeValveLComponent.xaml
    /// </summary>
    public partial class ShakeValveLComponent : UserControl
    {
        private Storyboard ShakeValveAnimation;
        public ShakeValveLComponent()
        {
            InitializeComponent();
            ShakeValveAnimation = (Storyboard)FindResource("MovingArrow");
        }

        public void Run()
        {
            if (ShakeValveAnimation != null)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/open.png");
                img.EndInit();
                valveLeft.Source = img;
                ShakeValveAnimation.Begin();
            }
        }
    }
}
