﻿using System;
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
    /// Interaction logic for OutValveComponent.xaml
    /// </summary>
    public partial class OutValveComponent : UserControl
    {
        private Storyboard OutValveAnimation;
        public OutValveComponent()
        {
            InitializeComponent();
            OutValveAnimation = (Storyboard)FindResource("MovingArrow");
        }

        public void Run()
        {
            if (OutValveAnimation != null)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"pack://application:,,,/TankControl;component/Images/valve/openRotate.png");
                img.EndInit();
                valveOut.Source = img;
                OutValveAnimation.Begin();
            }
        }
    }
}