﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TankControl.Class;
using System.Collections.ObjectModel;
using System.Windows.Media;
using TankControl.Class.Functions;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for ControlArea.xaml
    /// </summary>
    /// 

    public partial class ControlArea : UserControl
    {
        private ObservableCollection<TankControl.Recipe> recipelist;

        public ControlArea()
        {
            InitializeComponent();
            WeightScale.Singleton.WeightLabel = this.CurrentWeightLabel;
            Process.Singleton.AddControlArea(this);
            this.EnableConnection();
            if (TankControl.Properties.Settings.Default.SystemTest == 1)
            {
                this.EnableStartProcess();
            }
        }

        // Event Handler
        public void StopProcess_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.FillupStop();
            this.CurrentWeightLabel.Content = "0 kg";
            this.EnableStartProcess();
        }

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            if (Process.Singleton.IsProcessReady() || TankControl.Properties.Settings.Default.SystemTest == 1)
            {
                Process.Singleton.FillupRun();
                this.DisableStartProcess();
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (Microcontroller.Singleton.Disconnect())
            {
                if (WeightScale.Singleton.Disconnect())
                {
                    this.StartProcessButton.IsEnabled = false;
                    this.EnableConnection();
                }
            }
            else
            {
                MessageBox.Show("Fail to disconnet moxa, contact technician", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (WeightScale.Singleton.Connect())
            {
                if (Microcontroller.Singleton.Connect())
                {
                    if (this.CheckProcess() == true)
                    {
                       
                    }
                    this.DisableConnection();
                }
                else
                {
                    TCFunction.MessageBoxFail("Fail to connect weight scale");
                }
            }
            else
            {
                TCFunction.MessageBoxFail("Fail to connect moxa");
            }

        }

        private void StartTake_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.DrainStart();
            this.StartDrainButton.IsEnabled = false;
            this.StopDrainButton.IsEnabled = true;
        }

        private void StopTake_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.DrainStop();
            this.StopDrainButton.IsEnabled = false;
            this.StartDrainButton.IsEnabled = true;
        }

        private void DropdownRecipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownRecipe.SelectedValue != null)
            {
                int recipeId = (int)DropdownRecipe.SelectedValue;

                using (TankControlEntities tce = new TankControlEntities())
                {
                    Recipe selectedRecipe = tce.Recipes.Where(x => x.id == recipeId).FirstOrDefault();

                    if (selectedRecipe != null)
                    {
                        Process.Singleton.Recipe = selectedRecipe;

                        if (this.CheckProcess() == true)
                        {
                            this.EnableStartProcess();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to get recipe","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (TankControlEntities tce = new TankControlEntities())
            {
                try
                {

                    this.recipelist = new ObservableCollection<Recipe>();

                    var query = tce.Recipes.Select(x => new { x.id, x.name }).ToList();

                    if (query.Count > 0)
                    {
                        foreach (var recipe in query)
                        {
                            this.recipelist.Add(new TankControl.Recipe() { id = recipe.id, name = recipe.name });
                        }
                    }

                }
                catch (System.Data.EntityException exception)
                {
                    MessageBox.Show("An error occured while generating recipe data from database. Please contact technician {" + exception.Message + "}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("An unknown error has occurred. Please contact technician {" + exception.Message + "}" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }

            }

            if (Microcontroller.Singleton.IsConnected())
            {
                this.EnableStartProcess();
                this.DisableConnection();
            }
            else
            {
                this.EnableConnection();
            }

            this.DropdownRecipe.ItemsSource = this.recipelist;
        }

        // Event Handler Stop

        # region HELPER METHOD

        private bool CheckProcess()
        {
            bool ready = false;

            if (Process.Singleton.IsProcessReady() == true)
            {
                this.CheckReady.IsChecked = true;
                this.EnableStartProcess();
            }

            return ready;
        }

        public void EnableStartProcess()
        {
            this.StartProcessButton.IsEnabled = true;
            this.StartDrainButton.IsEnabled = true;

            this.StopProcessButton.IsEnabled = false;
            this.StopDrainButton.IsEnabled = true;
        }

        public void DisableStartProcess()
        {
            this.StartProcessButton.IsEnabled = false;
            this.StopProcessButton.IsEnabled = true;
        }

        private void EnableConnection()
        {
            this.ConnectButton.IsEnabled = true;
            this.DisconnectButton.IsEnabled = false;

            this.MoxaStatusLabel.Foreground = Brushes.Red;
            this.MoxaStatusLabel.Content = "Disconnected";

            this.ScaleStatusLabel.Foreground = Brushes.Red;
            this.ScaleStatusLabel.Content = "Disconnected";
        }

        private void DisableConnection()
        {
            this.DisconnectButton.IsEnabled = true;
            this.ConnectButton.IsEnabled = false;

            this.MoxaStatusLabel.Foreground = Brushes.Green;
            this.MoxaStatusLabel.Content = "Connected";

            this.ScaleStatusLabel.Foreground = Brushes.Green;
            this.ScaleStatusLabel.Content = "Connected";
        }

        #endregion
    }
}
