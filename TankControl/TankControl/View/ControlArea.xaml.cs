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

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for ControlArea.xaml
    /// </summary>
    public partial class ControlArea : UserControl
    {
        private bool animation;

        public ControlArea()
        {
            animation = false;
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (!(parent is Dashboard))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (!animation)
            {              
                (parent as Dashboard).gda.Mt.MainTankAnimation.Begin();
                (parent as Dashboard).gda.Tt1.Bv.Run();
                (parent as Dashboard).gda.Tt1.Sv.Run();
                (parent as Dashboard).gda.Tt2.Bv.Run();
                (parent as Dashboard).gda.Tt2.Sv.Run();
                (parent as Dashboard).gda.Tt3.Bv.Run();
                (parent as Dashboard).gda.Tt4.Bv.Run();
                (parent as Dashboard).gda.Tt5.Bv.Run();
                (parent as Dashboard).gda.Tt6.Bv.Run();
                (parent as Dashboard).gda.Tt7.Bv.Run();

                (parent as Dashboard).gda.Sc.slc.Run();
                (parent as Dashboard).gda.Sc.src.Run();
                (parent as Dashboard).gda.Sc.oc.Run();

                animation = true;
            }
        }
    }
}
