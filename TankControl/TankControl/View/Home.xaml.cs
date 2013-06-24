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

        private TankControl.View.Dashboard dashboardPage;
        private TankControl.View.RecipeView recipePage;
        private TankControl.View.ReportingView reportingPage;
        private TankControl.View.UserView userPage;

        public Home()
        {
            InitializeComponent();
            int userAuthentication = (int)Application.Current.Properties["userAuthenticationLevel"];
            this.dashboardPage = this.dashboard_view;
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
                this.recipePage = new TankControl.View.RecipeView();
                this.reportingPage = new TankControl.View.ReportingView();
                this.userPage = new TankControl.View.UserView();
            }
            else
            {
                User.IsEnabled = false;
                User.Visibility = System.Windows.Visibility.Hidden;
                Reporting.IsEnabled = false;
                Reporting.Visibility = System.Windows.Visibility.Hidden;
                Recipe.IsEnabled = false;
                Recipe.Visibility = System.Windows.Visibility.Hidden;
            }

            
            
        }

        public void Dashboard_Click(object sender, RoutedEventArgs e)
        {

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(this.dashboardPage);
            }
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(this.recipePage);
            }
        }

        private void Reporting_Click(object sender, RoutedEventArgs e)
        {

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(this.reportingPage);
            }
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {

            if (MainContainer.Children.Count > 0)
            {
                MainContainer.Children.RemoveAt(0);
                MainContainer.Children.Add(this.userPage);
            }
        }

    }
}
