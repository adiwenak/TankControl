using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TankControl.Class;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for ControlArea.xaml
    /// </summary>
    /// 

    public partial class ControlArea : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;

        // use to determine if process can be started
        private int counterProcess;

        public ControlArea()
        {
            InitializeComponent();
        }

        // Event Handler
        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.FillupStop();
            StartProcess.IsEnabled = true;
            StopProcess.IsEnabled = false;

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.FillupRun();
            StartProcess.IsEnabled = false;
            StopProcess.IsEnabled = true;
        }

        private void DisconnectMoxa_Click(object sender, RoutedEventArgs e)
        {
            if (Microcontroller.Singleton.Disconnect())
            {
                StartTake.IsEnabled = false;
                StartProcess.IsEnabled = false;
                this.DisableConnectMoxa();
            }
            else
            {
                MessageBox.Show("Fail to disconnet moxa, contact technician", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConnectMoxa_Click(object sender, RoutedEventArgs e)
        {
            if (Microcontroller.Singleton.Connect())
            {
                this.EnableStartProcess();
                this.EnableConnectMoxa();
                StartTake.IsEnabled = true;
                
            }
            else
            {
                MessageBox.Show("Fail to connect moxa, check setting or contact technician", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void StartTake_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.TakeStart();
            StartTake.IsEnabled = false;
            StopTake.IsEnabled = true;
        }

        private void StopTake_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.TakeStop();
            StartTake.IsEnabled = true;
            StopTake.IsEnabled = false;
        }

        private void DropdownRecipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int recipeId = (int)DropdownRecipe.SelectedValue;
            using (TankControlEntities tce = new TankControlEntities())
            {
                Recipe selectedRecipe = tce.Recipes.Where(x => x.id == recipeId).FirstOrDefault();

                if (selectedRecipe != null)
                {
                    Process.Singleton.Recipe = selectedRecipe;
                    if (Process.Singleton.IsProcessReady() == true)
                    {
                        this.EnableStartProcess();
                    }
                }
                else
                {

                }
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {
                    if (this.recipelist == null)
                    {
                        this.recipelist = new ObservableCollection<Recipe>();
                    }

                    if (this.recipelist.Count > 0)
                    {
                        this.recipelist.Clear();
                    }

                    var query = tce.Recipes.Select(x => new { x.id, x.name }).ToList();

                    if (query.Count > 0)
                    {
                        foreach (var recipe in query)
                        {
                            this.recipelist.Add(new TankControl.Recipe() { id = recipe.id, name = recipe.name });
                        }
                    }

                }
                catch (System.Data.EntityException)
                {
                    MessageBox.Show("An error occured while generating recipe data from database. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("An unknown error has occurred. Please contact technician", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }

            }

            if (Microcontroller.Singleton.IsConnected())
            {
                this.StartTake.IsEnabled = true;
                this.StopTake.IsEnabled = false;
                this.EnableStartProcess();

                this.EnableConnectMoxa();
            }
            else
            {
                this.StartTake.IsEnabled = false;
                this.StopTake.IsEnabled = false;

                this.DisableConnectMoxa();
            }

            this.DropdownRecipe.ItemsSource = this.recipelist;
        }

        // Event Handler Stop

        // Helper Method
        private void EnableStartProcess()
        {
            if (Process.Singleton.IsProcessReady())
            {
                this.StartProcess.IsEnabled = true;
                this.StopProcess.IsEnabled = false;
            }
        }

        private void DisableStartProcess()
        {
            this.StartProcess.IsEnabled = false;
            this.StopProcess.IsEnabled = true;
        }

        private void EnableConnectMoxa()
        {
            this.DisconnectMoxa.IsEnabled = true;
            this.ConnectMoxa.IsEnabled = false;

            this.MoxaStatusLabel.Foreground = Brushes.Green;
            this.MoxaStatusLabel.Content = "Connected";
        }

        private void DisableConnectMoxa()
        {
            this.ConnectMoxa.IsEnabled = true;
            this.DisconnectMoxa.IsEnabled = false;

            this.MoxaStatusLabel.Foreground = Brushes.Red;
            this.MoxaStatusLabel.Content = "Disconnected";
        }

        // Helper Method Stop
    }
}
