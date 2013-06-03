using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankControl;
using TankControl.View.ComponentGDA;
using TankControl.View;
using TankControl.Functions;

namespace TankControl.Class
{
    public class MainTank : BaseTank
    {
        private MainTankComponent view;
        private TinyTankPump tPump1;
        private TinyTankPump tPump2;
        private TinyTankL tLeft1;
        private TinyTankL tLeft2;
        private TinyTankR tRight1;
        private TinyTankR tRight2;
        private TinyTankR tRight3;
        private bool isFillup = false;
        private bool isPause = false;

        // PROPERTIES - START

        public bool IsPause
        {
            get
            {
                return this.isPause;
            }
            set
            {
                this.isPause = value;
            }
        }

        public bool IsFillup
        {
            get
            {
                return this.isFillup;
            }
            set
            {
                this.isFillup = value;
            }
        }

        public TinyTankPump TPump1
        {
            get
            {
                return this.tPump1;
            }
            set
            {
                this.tPump1 = value;
            }
        }

        public TinyTankPump TPump2
        {
            get
            {
                return this.tPump2;
            }
            set
            {
                this.tPump2 = value;
            }
        }

        public TinyTankR TRight3
        {
            get
            {
                return this.tRight3;
            }
            set
            {
                this.tRight3 = value;
            }
        }

        public TinyTankR TRight2
        {
            get
            {
                return this.tRight2;
            }
            set
            {
                this.tRight2 = value;
            }
        }

        public TinyTankR TRight1
        {
            get
            {
                return this.tRight1;
            }
            set
            {
                this.tRight1 = value;
            }
        }

        public TinyTankL TLeft2
        {
            get
            {
                return this.tLeft2;
            }
            set
            {
                this.tLeft2 = value;
            }
        }

        public TinyTankL TLeft1
        {
            get
            {
                return this.tLeft1;
            }
            set
            {
                this.tLeft1 = value;
            }
        }

        public MainTankComponent View
        {
            get
            {
                return this.view;
            }
            set
            {
                this.view = value;
            }
        }

        // PROPERTIES - END

        public MainTank(GraphicDisplayArea gdaView, int id,List<IComponent> list)
        {
            this.ID = id;
            this.View = gdaView.GdaMainTank;
            this.AddComponents(list);
        }


        // CONTROL - START
        public void Start()
        {
            this.View.RunWithLimit(1);
            this.IsFillup = true;
            
        }
        
        public void Stop()
        {
            if (this.IsFillup == true)
            {
                this.View.Stop();
                this.IsFillup = false;
            }
        }

        public void Pause()
        {
            if (this.IsPause == false)
            {
                this.View.Pause();
                this.IsPause = true;
            }
        }

        public void FillupWithLimit(double height)
        {
            this.View.Add(height);
            this.IsPause = false;
        }

        public IComponent OpenValveControl()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveControl);
            if (cmp != null)
            {
                cmp.Run();
            }

            return cmp;
        }

        public IComponent StopValveControl()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveControl);
            if (cmp != null)
            {
                cmp.Stop();
            }

            return cmp;
        }

        public IComponent OpenValveShake()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveShake);
            if (cmp != null)
            {
                cmp.Run();
            }

            return cmp;
        }

        public IComponent StopValveShake()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveShake);
            if (cmp != null)
            {
                cmp.Stop();
            }

            return cmp;
        }

        public IComponent OpenValveOutput()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveOutput);
            if (cmp != null)
            {
                cmp.Run();
            }

            return cmp;
        }

        public IComponent StopValveOutput()
        {
            IComponent cmp = this.GetComponent((int)ReferenceEnum.Component.ValveOutput);

            if (cmp != null)
            {
                cmp.Stop();
            }

            return cmp;
        }

        public void RunPump()
        {
        }

        public void StopPump()
        {
        }
        // CONTROL - END

        public void Setup()
        {
        }
    }
}
