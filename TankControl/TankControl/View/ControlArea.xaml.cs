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
using TankControl.Class;

namespace TankControl.View
{
    /// <summary>
    /// Interaction logic for ControlArea.xaml
    /// </summary>
    public partial class ControlArea : UserControl
    {
        public ControlArea()
        {
            InitializeComponent();
            ListReceipe();

            StopProcess.IsEnabled = false;
        }

        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.Stop();
            StartProcess.IsEnabled = true;
            StopProcess.IsEnabled = false;

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Process.Singleton.Run();
            StartProcess.IsEnabled = false;
            StopProcess.IsEnabled = true;
        }

        private void ListReceipe() {
            for (int i = 1; i < 5; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = "Receipe "+i;
                tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                tb.Margin = new Thickness(5, 3, 0, 3);

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(tb);

                ListBoxItem lb = new ListBoxItem();
                lb.Content = sp;
                Recipe.Items.Add(lb);
            }
        }
    }
}
