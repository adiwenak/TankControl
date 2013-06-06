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

namespace TankControl.View.ComponentGDA
{
    /// <summary>
    /// Interaction logic for MainTank.xaml
    /// </summary>
    public partial class MainTankComponent : UserControl
    {
        private double tankHeight;
        private Storyboard mainTankAnimation;
        private double incrementHeight; //increment tangki
        private double currentHeight;
        
        private DispatcherTimer fillTimer;

        public MainTankComponent()
        {
            InitializeComponent();
            mainTankAnimation = (Storyboard)FindResource("storyTank");
            currentHeight = tankHeight = tankiFill.Height;
            fillTimer = new DispatcherTimer();
            fillTimer.Tick += new EventHandler(fillTimer_Tick);
            fillTimer.Interval = TimeSpan.FromMilliseconds(1);
        }

        void fillTimer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("tanki Height :" + tankiFill.Height);
            System.Diagnostics.Debug.WriteLine("after render height:" + currentHeight);
            System.Diagnostics.Debug.WriteLine("increment :" + incrementHeight);
            if (tankiFill.Height <= (tankHeight - incrementHeight))
            {               
                currentHeight = tankiFill.Height;
                mainTankAnimation.Pause();
                fillTimer.Stop();
            }

        }

        public Storyboard MainTankAnimation
        {
            get
            {
                return this.mainTankAnimation;
            }
            set
            {
                this.mainTankAnimation = value;
            }
        }

        public void Run()
        {
            if (mainTankAnimation != null)
            {
                mainTankAnimation.Begin();
            }
        }

        public void RunWithLimit(double incrementHeight)
        {
            if (mainTankAnimation != null)
            {
                mainTankAnimation.Begin();
                this.incrementHeight = incrementHeight;
                fillTimer.Start();
            }
        }

        public void Stop()
        {
            if (mainTankAnimation != null)
            {
                Clean();
                mainTankAnimation.Stop();
                fillTimer.Stop();
                System.Diagnostics.Debug.WriteLine("current stop:" + currentHeight);

            }
        }

        public void Pause()
        {
            if (mainTankAnimation != null)
            {
                mainTankAnimation.Pause();
                fillTimer.Stop();
            }
        }

        public void Resume()
        {
            if (mainTankAnimation != null)
            {
                mainTankAnimation.Resume();
            }
        }

        public void Add(double incrementHeight){
            System.Diagnostics.Debug.WriteLine("increment top :" + incrementHeight);
            this.incrementHeight = incrementHeight;
            if(currentHeight  > (tankHeight - this.incrementHeight)){
                fillTimer.Start();
                mainTankAnimation.Resume();
            }
            
        }

        public void Clean() {
            currentHeight = tankHeight;
        }
    }
}
