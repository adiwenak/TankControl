using System;
using System.Collections.Generic;
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
	/// Interaction logic for Tank.xaml
	/// </summary>
	public partial class Tank : UserControl
	{
		private Storyboard bigSource,smallSource;
        private float capacity;
        private float fromBigSource;
        private Boolean statusBigSource;
        private bool done;

        private DispatcherTimer delay;
        private int iteratorTimeDelay;
		
        //it must be coming from scale
        private DispatcherTimer scale;
        public int i;

		public Tank()
		{         
            scale = new DispatcherTimer();
            scale.Tick += new EventHandler(Scale_Tick);
            scale.Interval = new TimeSpan(0, 0, 1);
            i = 0;

            done = false; statusBigSource = false;
            int iteratorTimeDelay = 0;
            delay = new DispatcherTimer();
            delay.Tick += new EventHandler(Delay_Tick);
            delay.Interval = new TimeSpan(0, 0, 1);

			this.InitializeComponent();
            bigSource = (Storyboard)bigPath.FindResource("MovingArrow");
            bigSource.CurrentTimeInvalidated += new EventHandler(StoryboardBig_CurrentTimeInvalidated);

            smallSource = (Storyboard)smallPath.FindResource("MovingArrow");
            bigSource.CurrentTimeInvalidated += new EventHandler(StoryboardSmall_CurrentTimeInvalidated);
		}

        public Boolean StatusBigSource
        {
            get
            {
                return this.statusBigSource;
            }
            set
            {
                this.statusBigSource = value;
            }
        }

        public Storyboard SmallSource
        {
            get
            {
                return this.smallSource;
            }
            set
            {
                this.smallSource = value;
            }
        }

        public Storyboard BigSource
        {
            get
            {
                return this.bigSource;
            }
            set
            {
                this.bigSource = value;
            }
        }

        public bool Done
        {
            get
            {
                return this.done;
            }
            set
            {
                this.done = value;
            }
        }

        public float FromBigSource
        {
            get
            {
                return this.fromBigSource;
            }
            set
            {
                this.fromBigSource = value;
            }
        }

        public float Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                this.capacity = value;
            }
        }

        void Delay_Tick(object sender, EventArgs e)
        {
            DispatcherTimer temp = (DispatcherTimer)sender;
            if (iteratorTimeDelay == 2)
            {
                smallSource.Begin();
                delay.Stop();
            }
            iteratorTimeDelay++;
        }

        void Scale_Tick(object sender, EventArgs e)
        {
            i++;
        }

        

        void StoryboardBig_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            if (i > fromBigSource) {
                if(bigSource.GetCurrentState() == ClockState.Active){
                    bigSource.Stop();
                    statusBigSource = true;
                    delay.Start();
                }
            }
        }

        void StoryboardSmall_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            if (i > capacity)
            {
                if (smallSource.GetCurrentState() == ClockState.Active)
                {
                    smallSource.Stop();
                    scale.Stop();
                    done = true;
                }
            }
        }

        public void RunStory()
        {
            if (bigSource != null)
            {
                bigSource.Begin();
                scale.Start();
            }
            
        }

        public void Clean()
        {
            scale = new DispatcherTimer();
            scale.Tick += new EventHandler(Scale_Tick);
            scale.Interval = new TimeSpan(0, 0, 1);
            i = 0;

            done = false;
            int iteratorTimeDelay = 0;
            delay = new DispatcherTimer();
            delay.Tick += new EventHandler(Delay_Tick);
            delay.Interval = new TimeSpan(0, 0, 1);

            this.InitializeComponent();
            bigSource = (Storyboard)bigPath.FindResource("MovingArrow");
            bigSource.CurrentTimeInvalidated += new EventHandler(StoryboardBig_CurrentTimeInvalidated);

            smallSource = (Storyboard)smallPath.FindResource("MovingArrow");
            bigSource.CurrentTimeInvalidated += new EventHandler(StoryboardSmall_CurrentTimeInvalidated);

        }

	}
}