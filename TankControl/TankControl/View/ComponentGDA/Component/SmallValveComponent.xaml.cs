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
    /// Interaction logic for SmallValve.xaml
    /// </summary>
    public partial class SmallValveComponent : UserControl
    {

        private Storyboard smallValveAnimation;

        public SmallValveComponent()
        {
            InitializeComponent();
            smallValveAnimation = (Storyboard)FindResource("MovingArrow");
        }

        public void Run()
        {
            if (smallValveAnimation != null)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/open.png");
                img.EndInit();
                valveBig.Source = img;
                smallValveAnimation.Begin();
            }
        }
    }
}
