using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View;
using TankControl.View.ComponentGDA;
using System.Diagnostics;
using System.Windows.Threading;
using TankControl.Functions;

namespace TankControl.Class
{
    public class Process
    {
        private MainTank mainTank;
        private Recipe recipe;
        private GraphicDisplayArea view;
        private static Process singleton;
        private static int runCounter;

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


        public bool IsProcessReady()
        {
            bool ready = false;

            if (this.Recipe != null)
            {
                if (this.Recipe.IsValid() == true)
                {
                    if (WeightScale.Singleton.CurrentWeight >= -1 && WeightScale.Singleton.CurrentWeight <= 1)
                    {
                        if (Microcontroller.Singleton.IsConnected())
                        {
                            ready = true;
                        }
                    }
                }
            }

            return ready;
        }

        // PROCESS - START
        public void ProcessFillup(float receiveWeight)
        {
            if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump1.StageLimit2)
            {
                this.StopAllComponent(false);
                this.MainTank.TPump1.RunValveSmall();
            }
            else if(receiveWeight >= this.MainTank.TPump1.StageLimit2 && receiveWeight < this.MainTank.TPump2.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TPump2.RunPump();
                this.MainTank.TPump2.RunValveBig();
            }
            else if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump2.StageLimit2)
            {
                this.StopAllComponent(false);
                this.MainTank.TPump2.RunValveSmall();
            }
            else if (receiveWeight >= this.MainTank.TPump2.StageLimit2 && receiveWeight < this.MainTank.TLeft1.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TLeft1.Run();
            }
            else if (receiveWeight >= this.MainTank.TLeft1.StageLimit && receiveWeight < this.MainTank.TLeft2.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TLeft2.Run();
            }
            else if (receiveWeight >= this.MainTank.TLeft2.StageLimit && receiveWeight < this.MainTank.TRight1.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TRight1.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight1.StageLimit && receiveWeight < this.MainTank.TRight2.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TRight2.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight2.StageLimit && receiveWeight < this.MainTank.TRight3.StageLimit)
            {
                this.StopAllComponent(false);
                this.MainTank.TRight3.Run();
            }
            else if (receiveWeight >= this.MainTank.TRight3.StageLimit)
            {
                this.StopAllComponent(false);
                RunTester.Singleton.StopTimer();
                this.ProcessShake();
            }
        }

        public void ProcessShake()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(ShakeRun);
            timer.Start();

        }

        public void ProcessTaking()
        {
            this.MainTank.OpenValveControl();
            this.MainTank.OpenValveOutput();
        }

        public void ProcessTakingStop()
        {
            this.MainTank.StopValveControl();
            this.MainTank.StopValveOutput();
        }

        private void ShakeRun(object sender, EventArgs e)
        {
            runCounter++;
            if (runCounter < this.Recipe.runtime)
            {
                this.MainTank.OpenValveControl();
                this.MainTank.OpenValveShake();
            }
            else
            {
                runCounter = 0;
                this.MainTank.StopValveControl();
                this.MainTank.StopValveShake();
                (sender as DispatcherTimer).Stop();
                this.FillupStop();

            }
        }

        

        // PROCESS - END
        
        // CONTROL - START
        public void FillupRun()
        {
            if (this.Recipe != null)
            {
                float pumpOneA = (float)(this.Recipe.el1 * this.Recipe.switch_el1);
                float pumpOneB = pumpOneA + (float)(this.Recipe.el1 * (1 - this.Recipe.switch_el1));

                float pumpTwoA = pumpOneB + (float)(this.Recipe.el2 * this.Recipe.switch_el2);
                float pumpTwoB = pumpTwoA + (float)(this.Recipe.el2 * (1 - this.Recipe.switch_el2));

                float pumpThree = pumpTwoB + (float)this.Recipe.el3;

                float pumpFour = pumpThree + (float)this.Recipe.el4;

                float pumpFive = pumpFour + (float)this.Recipe.el5;

                float pumpSix = pumpFive + (float)this.Recipe.el6;

                float pumpSeven = pumpSix + (float)this.Recipe.el7;
                     
                this.MainTank.TPump1.StageLimit = pumpOneA;
                this.MainTank.TPump1.StageLimit2 = pumpOneB;
                this.MainTank.TPump2.StageLimit = pumpTwoA;
                this.MainTank.TPump2.StageLimit2 = pumpTwoB;
                this.MainTank.TLeft1.StageLimit = pumpThree;
                this.MainTank.TLeft2.StageLimit = pumpFour;
                this.MainTank.TRight1.StageLimit = pumpFive;
                this.MainTank.TRight2.StageLimit =  pumpSix;
                this.MainTank.TRight3.StageLimit = pumpSeven;
                this.MainTank.TPump1.RunPump();
                this.MainTank.TPump1.RunValveBig();

                RunTester.Singleton.RunTimer();

                this.MainTank.Start();
            }
        }

        public void FillupStop()
        {
            this.MainTank.Stop();
            this.StopAllComponent(true);
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

        public void TakeStart()
        {
            this.ProcessTaking();
        }

        public void TakeStop()
        {
            this.ProcessTakingStop();
        }

        public void StopAllComponent(bool cleanup)
        {
            this.MainTank.TPump1.End(cleanup);
            this.MainTank.TPump2.End(cleanup);
            this.MainTank.TLeft1.End(cleanup);
            this.MainTank.TLeft2.End(cleanup);
            this.MainTank.TRight1.End(cleanup);
            this.MainTank.TRight2.End(cleanup);
            this.MainTank.TRight3.End(cleanup);
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
                    List<IComponent> list = new List<IComponent>{
                            new ControlValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO9),gdaView.GdaMainTankShake.Slc,(int)ReferenceEnum.Component.ValveControl),
                            new ShakeValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO10),gdaView.GdaMainTankShake.Src, (int)ReferenceEnum.Component.ValveShake),
                            new OutValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO11),gdaView.GdaMainTankShake.Oc, (int)ReferenceEnum.Component.ValveOutput)
                    };
                    this.MainTank = new MainTank(gdaView,(int)ReferenceEnum.Tank.MainTank, list);
                }
            }

            if (gdaView.GdaTinyTankPump1 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO0),gdaView.GdaTinyTankPump1.Bv, (int)ReferenceEnum.Component.ValveBig),
                        new SmallValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO1),gdaView.GdaTinyTankPump1.Sv, (int)ReferenceEnum.Component.ValveSmall),
                        new Pump(Convert.ToUInt16(ReferenceEnum.MOXA.DO12),gdaView.GdaTinyTankPump1.Pc, (int)ReferenceEnum.Component.PumpTinyTank)
                };

                this.MainTank.TPump1 = new TinyTankPump(gdaView.GdaTinyTankPump1, (int)ReferenceEnum.Tank.TinyTankOne,list);
            }

            if (gdaView.GdaTinyTankPump2 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO2),gdaView.GdaTinyTankPump2.Bv, (int)ReferenceEnum.Component.ValveBig),
                        new SmallValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO3),gdaView.GdaTinyTankPump2.Sv, (int)ReferenceEnum.Component.ValveSmall),
                        new Pump(Convert.ToUInt16(ReferenceEnum.MOXA.DO13),gdaView.GdaTinyTankPump2.Pc, (int)ReferenceEnum.Component.PumpTinyTank)
                };

                this.MainTank.TPump2 = new TinyTankPump(gdaView.GdaTinyTankPump2, (int)ReferenceEnum.Tank.TinyTankTwo,list);
            }

            if (gdaView.GdaTinyTank3 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO4),gdaView.GdaTinyTank3.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TLeft1 = new TinyTankL(gdaView.GdaTinyTank3, (int)ReferenceEnum.Tank.TinyTankThree,list);
            }

            if (gdaView.GdaTinyTank4 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO5),gdaView.GdaTinyTank4.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TLeft2 = new TinyTankL(gdaView.GdaTinyTank4, (int)ReferenceEnum.Tank.TinyTankFour,list);
            }

            if (gdaView.GdaTinyTank5 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO6),gdaView.GdaTinyTank5.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight1 = new TinyTankR(gdaView.GdaTinyTank5, (int)ReferenceEnum.Tank.TinyTankFive,list);
            }

            if (gdaView.GdaTinyTank6 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO7),gdaView.GdaTinyTank6.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight2 = new TinyTankR(gdaView.GdaTinyTank6, (int)ReferenceEnum.Tank.TinyTankSix,list);
            }

            if (gdaView.GdaTinyTank7 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO8),gdaView.GdaTinyTank7.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight3 = new TinyTankR(gdaView.GdaTinyTank7, (int)ReferenceEnum.Tank.TinyTankSeven,list);
            }

        }

        // NOTIFY VIEW OBSERVER
        public void NotifyView()
        {
            view.UpdateView();
        }

        // LISTENER WEIGHT SCALE
        public void WeightUpdated(float receiveWeight,float addweight)
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
