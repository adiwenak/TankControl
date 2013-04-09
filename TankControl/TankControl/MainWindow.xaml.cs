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
<<<<<<< HEAD
=======
using TankControl.Model;
>>>>>>> origin/master-adi

namespace TankControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
<<<<<<< HEAD
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
=======
    public partial class MainWindow : NavigationWindow
    {
        private Process process;
        public MainWindow()
        {
            InitializeComponent();
            process = new Process();
            process.AddView(this);
        }

        public void Update()
        {

>>>>>>> origin/master-adi
        }
    }
}
