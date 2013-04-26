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
    /// Interaction logic for BigValveR.xaml
    /// </summary>
    public partial class BigValveRComponent : UserControl
    {
        private Storyboard bigValveAnimation;
        public BigValveRComponent()
        {
            InitializeComponent();
            bigValveAnimation = (Storyboard)FindResource("MovingArrow");
        }

        private void ValveClose()
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/close.png");
            img.EndInit();
            valveBig.Source = img;
        }

        private void ValveOpen()
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/open.png");
            img.EndInit();
            valveBig.Source = img;
        }

        public void Open()
        {
            if (bigValveAnimation != null)
            {
                ValveOpen();
                bigValveAnimation.Begin();
            }
        }

        public void Close()
        {
            if (bigValveAnimation != null)
            {
                ValveClose();
                bigValveAnimation.Stop();
            }
        }

        public void Resume()
        {
            if (bigValveAnimation != null)
            {
                ValveOpen();
                bigValveAnimation.Resume();
            }
        }

        public void Pause()
        {
            if (bigValveAnimation != null)
            {
                ValveClose();
                bigValveAnimation.Pause();
            }
        }
    }
}
