using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankControl.Class
{
    public class WeightScale
    {
        private Process process;
        private float currentWeight;
        private float limit;
        private static WeightScale singleton;

        public static WeightScale Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new WeightScale();
                }

                return singleton;
            }
        }

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

        public float Limit
        {
            get
            {
                return this.limit;
            }
            set
            {
                this.limit = value;
                Thread newThread = new Thread(() => StartCalculating(value));
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

        private void StartCalculating(float limit)
        {
            while (currentWeight < limit)
            {
                // update when know how to set currentWeight
            }
            notify();
        }
    
        public void notify()
        {
            process.WeightUpdated();
        }

    }
}
