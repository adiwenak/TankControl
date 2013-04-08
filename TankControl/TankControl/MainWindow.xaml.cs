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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace TankControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Storyboard storyTank;
        private DispatcherTimer delay;
        private DispatcherTimer checkDelay;

        private int iteratorTimeDelay;
		
        public MainWindow()
        {
            delay = new DispatcherTimer();
            delay.Interval = new TimeSpan(0,0,1);
            delay.Tick += new EventHandler(delay_Tick);

            checkDelay = new DispatcherTimer();
            checkDelay.Interval = new TimeSpan(0, 0, 1);
            checkDelay.Tick += new EventHandler(checkDelay_Tick);
            InitializeComponent();
            storyTank = (Storyboard)FindResource("storyTank");
        }

        void checkDelay_Tick(object sender, EventArgs e)
        {
            if (tankObject.StatusBigSource)
            {
                delay.Start();
                storyTank.Pause();
                checkDelay.Stop();
            }
        }

        void delay_Tick(object sender, EventArgs e)
        {
            if(iteratorTimeDelay == 2){
                delay.Stop();
                storyTank.Resume();
            }
            iteratorTimeDelay++;           
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            clean();
            if(storyTank != null)
                storyTank.Begin();
            tankObject.Clean();
            tankObject.Capacity = 5;
            tankObject.FromBigSource = 2;
            tankObject.RunStory();

            checkDelay.Start();
        }

        private void clean() {
            delay = new DispatcherTimer();
            delay.Interval = new TimeSpan(0, 0, 1);
            delay.Tick += new EventHandler(delay_Tick);

            checkDelay = new DispatcherTimer();
            checkDelay.Interval = new TimeSpan(0, 0, 1);
            checkDelay.Tick += new EventHandler(checkDelay_Tick);
            InitializeComponent();
            storyTank = (Storyboard)FindResource("storyTank");
        }


    }
}
