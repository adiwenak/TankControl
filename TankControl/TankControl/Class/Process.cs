using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View;
using TankControl.View.ComponentGDA;

namespace TankControl.Class
{
    public class Process
    {
        private MainTank mainTank;
        private Recipe recipe;
        private GraphicDisplayArea view;
        private static Process singleton;


        // SINGLETON
        public static Process Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Process();
                }
                return singleton;
            }
        }

        // CONSTRUCTOR
        public Process()
        {
            
        }

        // PROPERTIES - START
        public GraphicDisplayArea View
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

        public Recipe Recipe
        {
            get
            {
                return this.recipe;
            }
            set
            {
                this.recipe = value;
            }
        }

        public MainTank MainTank
        {
            get
            {
                return this.mainTank;
            }
            set
            {
                this.mainTank = value;
            }
        }

        // PROPERTIES - END

        // CONTROL - START
        public void Run()
        {
            MainTank.Run();
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
        }

        public void Stop()
        {
            MainTank.Stop();
        }

        // CONTROL - END

        // ADD VIEW OSERVER
        public void AddView(GraphicDisplayArea processView)
        {
            if (processView != null)
            {
                View = processView;
                if (processView.Mt != null)
                {
                    MainTank = new MainTank(processView.Mt,"Main Tank");

                    foreach (UserControl uc in processView.Components)
                    {
                        if (uc is TinyTankLComponent)
                        {
                            TinyTankLComponent temp = (uc as TinyTankLComponent);

                            MainTank.TinyTanksL.Add(new TinyTankL(temp, "tinyT"));
                        }
                        else if (uc is TinyTankRComponent)
                        {
                            TinyTankRComponent temp = (uc as TinyTankRComponent);

                            MainTank.TinyTanksR.Add(new TinyTankR(temp, "tinyT"));
                        }
                        else if (uc is TinyTankPumpComponent)
                        {
                            TinyTankPumpComponent temp = (uc as TinyTankPumpComponent);

                            MainTank.TinyTanksPump.Add(new TinyTankPump(temp, "tinyT"));
                        }
                        else if (uc is ShakeComponent)
                        {
                        }
                    }
                }
            }

        }

        // NOTIFY VIEW OBSERVER
        public void NotifyView()
        {
            view.UpdateView();
        }

        // LISTENER WEIGHT SCALE
        public void WeightUpdated(float weightScale)
        {

        }

    }
}
