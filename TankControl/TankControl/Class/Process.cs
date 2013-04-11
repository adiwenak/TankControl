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
            this.MainTank.Fillup();
            this.MainTank.TPump1.RunPump();
            this.MainTank.TPump1.RunValveBig();
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
                this.View = processView;
                this.SetupTanks(processView);
            }

        }

        private void SetupTanks(GraphicDisplayArea gdaView)
        {
            if (gdaView.GdaMainTank != null)
            {
                if (this.MainTank == null)
                {
                    this.MainTank = new MainTank(gdaView.GdaMainTank, "Main Tank");
                }
            }

            if (gdaView.GdaTinyTankPump1 != null)
            {
                this.MainTank.TPump1 = new TinyTankPump(gdaView.GdaTinyTankPump1, "ttPump1");
            }

            if (gdaView.GdaTinyTankPump2 != null)
            {
                this.MainTank.TPump2 = new TinyTankPump(gdaView.GdaTinyTankPump2, "ttPump2");
            }

            if (gdaView.GdaTinyTank3 != null)
            {
                this.MainTank.TLeft1 = new TinyTankL(gdaView.GdaTinyTank3, "ttPump1");
            }

            if (gdaView.GdaTinyTank4 != null)
            {
                this.MainTank.TLeft2 = new TinyTankL(gdaView.GdaTinyTank4, "ttPump1");
            }

            if (gdaView.GdaTinyTank5 != null)
            {
                this.MainTank.TRight1 = new TinyTankR(gdaView.GdaTinyTank5, "ttPump1");
            }

            if (gdaView.GdaTinyTank6 != null)
            {
                this.MainTank.TRight2 = new TinyTankR(gdaView.GdaTinyTank6, "ttPump1");
            }

            if (gdaView.GdaTinyTank7 != null)
            {
                this.MainTank.TRight3 = new TinyTankR(gdaView.GdaTinyTank7, "ttPump1");
            }

            if (gdaView.GdaMainTankShake != null)
            {
                ShakeValveL shakeValveL = new ShakeValveL(gdaView.GdaMainTankShake.Slc, "ControlValve");

                ShakeValveR shakeValveR = new ShakeValveR(gdaView.GdaMainTankShake.Src, "ShakeValve");

                OutValve outValve = new OutValve(gdaView.GdaMainTankShake.Oc, "OutputValve");

                this.MainTank.AddComponent(shakeValveL);
                this.MainTank.AddComponent(shakeValveR);
                this.MainTank.AddComponent(outValve);
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
