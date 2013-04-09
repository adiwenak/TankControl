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

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            GraphicDisplayArea gda = new GraphicDisplayArea();
            MainContainer.Children.Clear();
            MainContainer.Children.Insert(0, gda);
        }

        public void Dashboad_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            MainContainer.Children.Clear();
            MainContainer.Children.Insert(0, dashboard);
        }
    }
}
