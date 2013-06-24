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
        private decimal weight = 0;
        private DispatcherTimer timer;
        private static RunTester singleton;
        public decimal AddWeight {get;set;}

        public RunTester()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(WeightUpdated);
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
            this.AddWeight = 0;
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
            this.Reset();
        }

        public void Reset()
        {
            this.weight = 0;
            this.AddWeight = 1;
        }

        private void WeightUpdated(object sender, EventArgs e)
        {
            this.weight += this.AddWeight;
            System.Diagnostics.Debug.WriteLine("weight :" + weight);
            WeightScale.Singleton.WeightScaleUpdated(weight,this.AddWeight);
        }
    }
}
