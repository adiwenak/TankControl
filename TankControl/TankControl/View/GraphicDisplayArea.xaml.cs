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
using TankControl.ComponentGDA;
using TankControl.Class;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for GraphicDisplayArea.xaml
    /// </summary>
    public partial class GraphicDisplayArea : UserControl
    {
        private UCValve valve;
        public GraphicDisplayArea()
        {
            InitializeComponent();

            Process.Singleton.AddView(this);

        }

        public UCValve Valve
        {
            get
            {
                return this.valve;
            }
            set
            {
                this.valve = value;
            }
        }
    }

}
