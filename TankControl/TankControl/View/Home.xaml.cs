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
        public string userAuthenticationLevel;
        public Home()
        {
            InitializeComponent();
            int userAuthentication = (int)Application.Current.Properties["userAuthenticationLevel"];
            if (userAuthentication == 2)
            {
                User.IsEnabled = false;
                User.Visibility = System.Windows.Visibility.Hidden;
                Reporting.IsEnabled = false;
                Reporting.Visibility = System.Windows.Visibility.Hidden;
                Recipe.IsEnabled = false;
                Recipe.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (userAuthentication == 1)
            {
                //Do Nothing
            }
        }

        public void Dashboad_Click(object sender, RoutedEventArgs e)
        {
            TankControl.View.Dashboard openwindow = new TankControl.View.Dashboard();

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(openwindow);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TankControl.View.Recipe openwindow = new TankControl.View.Recipe();

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(openwindow);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            TankControl.View.Reporting openwindow = new TankControl.View.Reporting();

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(openwindow);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            TankControl.View.User openwindow = new TankControl.View.User();

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(openwindow);
            }
        }

    }
}
