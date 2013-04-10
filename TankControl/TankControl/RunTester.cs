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
        private WeightScale scale;
        private float weight = 0;
        private DispatcherTimer timer;

        public RunTester()
        {
            scale = new WeightScale();
            timer = new DispatcherTimer();
        }

        public void RunTimer()
        {
            timer.Tick += new EventHandler(UpdateWeightScale);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        public void StopTimer()
        {
            weight = 0;
            timer.Stop();
        }

        private void UpdateWeightScale(object sender, EventArgs e)
        {
            weight++;
            System.Diagnostics.Debug.WriteLine(weight);
            scale.WeightScaleUpdated(weight);
            if (weight == 10)
            {
                StopTimer();
            }
        }
    }
}
