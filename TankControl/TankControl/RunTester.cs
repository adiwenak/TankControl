using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl.Class;
using System.Windows.Threading;

namespace TankControl
{
    class RunTester
    {
        private float weight = 0;
        private float addWeight = 1;
        private DispatcherTimer timer;
        private static RunTester singleton;

        public RunTester()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(WeightUpdated);
        }

        public float AddWeight
        {
            get
            {
                return this.addWeight;
            }
            set
            {
                this.addWeight = value;
            }
        }

        public static RunTester Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new RunTester();
                }

                return singleton;
            }
        }

        public void RunTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            weight = 0;
            timer.Stop();
        }

        private void WeightUpdated(object sender, EventArgs e)
        {
            weight += this.addWeight;
            System.Diagnostics.Debug.WriteLine(weight);
            WeightScale.Singleton.WeightScaleUpdated(weight);
        }
    }
}
