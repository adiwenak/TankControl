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
    /// Interaction logic for Pump.xaml
    /// </summary>
    public partial class PumpComponent : UserControl
    {
        private Storyboard pumpAnimation;

        public PumpComponent()
        {
            InitializeComponent();
            pumpAnimation = (Storyboard)FindResource("animate");
        }

        public void Run() {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/pumpOpen.png");
            img.EndInit();
            Pump.Source = img;
            pumpAnimation.Begin();
          
        }
		
		public void Stop() {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/pumpClose.png");
            img.EndInit();
            Pump.Source = img;
            pumpAnimation.Stop();
        }
    }
}
