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
                    Microcontroller.Singleton.InitConnection();
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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(ShakeRun);
            timer.Start();

        }

        private void ShakeRun(object sender, EventArgs e)
        {
            runCounter++;
            if (runCounter < 5)
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
                    List<IComponent> list = new List<IComponent>{
                            new ControlValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO9),gdaView.GdaMainTankShake.Slc,(int)ReferenceEnum.Component.ValveControl),
                            new ShakeValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO10),gdaView.GdaMainTankShake.Src, (int)ReferenceEnum.Component.ValveShake),
                            new OutValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO11),gdaView.GdaMainTankShake.Oc, (int)ReferenceEnum.Component.PumpMainTank)
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
