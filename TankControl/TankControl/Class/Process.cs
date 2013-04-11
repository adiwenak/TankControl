using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View;
using TankControl.View.ComponentGDA;
using System.Diagnostics;

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
            WeightScale.Singleton.Process = this;
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

        // PROCESS - START
        public void ProcessFillup(float receiveWeight)
        {
            if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump1.StageLimit2)
            {
                this.MainTank.TPump1.StopValveBig();
                this.MainTank.TPump1.RunValveSmall();
            }
            else if(receiveWeight >= this.MainTank.TPump1.StageLimit2 && receiveWeight < this.MainTank.TPump2.StageLimit)
            {
                this.MainTank.TPump1.StopValveSmall();
                this.MainTank.TPump1.StopPump();
                this.MainTank.TPump2.RunPump();
                this.MainTank.TPump2.RunValveBig();
            }
            else if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump2.StageLimit2)
            {
                this.MainTank.TPump2.RunValveSmall();
                this.MainTank.TPump2.StopValveBig();
            }
            else if (receiveWeight >= this.MainTank.TPump2.StageLimit2 && receiveWeight < this.MainTank.TLeft1.StageLimit)
            {
                this.MainTank.TPump2.StopPump();
                this.MainTank.TPump2.StopValveSmall();
                this.MainTank.TLeft1.Run();
            }
            else if (receiveWeight >= this.MainTank.TLeft1.StageLimit && receiveWeight < this.MainTank.TLeft2.StageLimit)
            {
                this.MainTank.TLeft1.Stop();
                this.MainTank.TLeft2.Run();
            }
            else if (receiveWeight >= this.MainTank.TLeft2.StageLimit && receiveWeight < this.MainTank.TRight1.StageLimit)
            {
                this.MainTank.TLeft2.Stop();
                this.MainTank.TRight1.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight1.StageLimit && receiveWeight < this.MainTank.TRight2.StageLimit)
            {
                this.MainTank.TRight1.Stop();
                this.MainTank.TRight2.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight2.StageLimit && receiveWeight < this.MainTank.TRight3.StageLimit)
            {
                this.MainTank.TRight2.Stop();
                this.MainTank.TRight3.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight3.StageLimit)
            {
                this.MainTank.TRight3.Stop();
                RunTester.Singleton.StopTimer();
                this.ProcessShake();
            }

        }

        public void ProcessShake()
        {
            
        }

        public void ProcessTaking()
        {
            throw new NotImplementedException();
        }

        // PROCESS - END
        
        // CONTROL - START
        public void FillupRun()
        {
            this.MainTank.TPump1.StageLimit = 2;
            this.MainTank.TPump1.StageLimit2 = 4;
            this.MainTank.TPump2.StageLimit = 6;
            this.MainTank.TPump2.StageLimit2 = 8;
            this.MainTank.TLeft1.StageLimit = 10;
            this.MainTank.TLeft2.StageLimit = 12;
            this.MainTank.TRight1.StageLimit = 14;
            this.MainTank.TRight2.StageLimit = 15;
            this.MainTank.TRight3.StageLimit = 16;

            this.MainTank.TPump1.RunPump();
            this.MainTank.TPump1.RunValveBig();

            RunTester.Singleton.RunTimer();

            this.MainTank.Start();
        }

        public void FillupStop()
        {
            this.MainTank.Stop();
            this.MainTank.TPump1.End();
            this.MainTank.TPump2.End();
            this.MainTank.TLeft1.End();
            this.MainTank.TLeft2.End();
            this.MainTank.TRight1.End();
            this.MainTank.TRight2.End();
            this.MainTank.TRight3.End();
            RunTester.Singleton.StopTimer();
        }

        public void FillupPause()
        {
            throw new NotImplementedException();
        }

        public void FillupResume()
        {
            throw new NotImplementedException();
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
        public void WeightUpdated(float receiveWeight)
        {
            if (WeightScale.Singleton.CurrentWeight == receiveWeight)
            {

            }
            else
            {
                this.MainTank.FillupWithLimit(20);
                this.ProcessFillup(receiveWeight);
            }
        }

    }
}
