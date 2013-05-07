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

        // PROPERTIES - START
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

        // PROPERTIES END
    
        // UPDATE OBSERVER
        public void Notify(float weight, float addWeight)
        {
            process.WeightUpdated(weight, addWeight);
        }

        // LISTENER TO WEIGHT SCALE

        public void WeightScaleUpdated(float weight, float addWeight)
        {
            Notify(weight, addWeight);
            this.CurrentWeight = weight;
        }

    }
}
