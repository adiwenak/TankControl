using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankControl.Model
{
    public class WeightScale
    {
        private Process process;
        public float currentWeight;
        public float setLimit;
        private static WeightScale scale;

        public float CurrentWeight
        {
            get
            {
                return this.currentWeight;
            }
            set
            {
                this.currentWeight = value;
            }
        }

        public float SetLimit
        {
            get
            {
                return this.setLimit;
            }
            set
            {
                this.setLimit = value;
                Thread newThread = new Thread (() => receiveLimit(value));
                newThread.Start();
            }
        }

        public Process Process
        {
            get
            {
                return this.process;
            }
            set
            {
                this.process = value;
            }
        }

        private void receiveLimit(float weight)
        {
            while (currentWeight < weight)
            {
                // update when know how to set currentWeight
            }
            notify();
        }
    
        public void notify()
        {
            process.UpdateState();
        }

        public static WeightScale Scale
        {
            get
            {
                if (scale == null)
                    scale = new WeightScale();

                return scale;
            }
        }
    }
}
