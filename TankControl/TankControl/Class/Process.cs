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
using System.Data.Entity.Infrastructure;

namespace TankControl.Class
{
    public class Process
    {
        private static Process singleton;

        // use for duration in mixing the element in main tank
        private static int shakeCounter;
        // use as a rate to calibrate between the actual max weight of tank and height of main tank object view
        private float pixelRate;
        // use to determine if user has already press start process button
        private bool processRun;
        // use to determine the current process step
        private int processStep = 0;

        // a collection of the current running components (valve, pump), it use for stoping the somewhere along the process
        private List<IComponent> runComponents;
        // use to update the check list process in the view
        private ControlArea controlArea;

        private History history;

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

        public GraphicDisplayArea View { get; set; }

        public Recipe Recipe { get; set; }

        public MainTank MainTank { get; set;}

        private float PixelRate
        {
            get
            {
                if (this.pixelRate == 0)
                {
                    this.pixelRate = (float)TankControl.Properties.Settings.Default.Limit / TankControl.Properties.Settings.Default.TankHeight;
                }

                return this.pixelRate;
            }
        }

        private History History { get; set; }
        // PROPERTIES - END


        public bool IsProcessReady()
        {
            bool ready = false;

            if (TankControl.Properties.Settings.Default.SystemTest == 1)
            {
                if (this.Recipe != null)
                {
                    ready = true;
                }
            }
            else
            {
                if (this.Recipe != null)
                {
                    if (this.Recipe.IsValid() == true)
                    {
                        if (WeightScale.Singleton.IsConnected())
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
                }
            }

            return ready;
        }

        #region Process Method

        
        /// <summary>
        /// this is the heart of the process, it responsible to update the state of the process in accordance with the current weight
        /// </summary>
        /// <param name="receiveWeight">The current weight of the tank, suppose to be receive from the weight scale</param>

        public void ProcessFillup(decimal receiveWeight)
        {
            if (receiveWeight >= 0 && receiveWeight < this.MainTank.TPump1.StageLimit)
            {
                if (processStep == 0)
                {
                    this.AddToRunComponent(this.MainTank.TPump1.RunPump());
                    this.AddToRunComponent(this.MainTank.TPump1.RunValveBig());
                    processStep = 1;
                }
            }
            else if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump1.StageLimit2)
            {
                if (processStep == 1)
                {
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TPump1.RunPump());
                    this.AddToRunComponent(this.MainTank.TPump1.RunValveSmall());
                    processStep = 2;
                }
            }
            else if(receiveWeight >= this.MainTank.TPump1.StageLimit2 && receiveWeight < this.MainTank.TPump2.StageLimit)
            {
                if (processStep <= 2)
                {
                    if (this.History != null)
                    {
                        this.History.el1 = (double)receiveWeight;
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TPump2.RunPump());
                    this.AddToRunComponent(this.MainTank.TPump2.RunValveBig());
                    processStep = 3;
                }   
            }
            else if (receiveWeight >= this.MainTank.TPump1.StageLimit && receiveWeight < this.MainTank.TPump2.StageLimit2)
            {
                if (processStep <= 3)
                {
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TPump2.RunPump());
                    this.AddToRunComponent(this.MainTank.TPump2.RunValveSmall());
                    processStep = 4;
                }
            }
            else if (receiveWeight >= this.MainTank.TPump2.StageLimit2 && receiveWeight < this.MainTank.TLeft1.StageLimit)
            {
                if (processStep <= 4)
                {
                    if (this.History != null)
                    {
                        this.History.el2 = (double)receiveWeight - this.History.el1;
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TLeft1.Run());
                    processStep = 5;
                }
            }
            else if (receiveWeight >= this.MainTank.TLeft1.StageLimit && receiveWeight < this.MainTank.TLeft2.StageLimit)
            {
                if (processStep <= 5)
                {
                    if (this.History != null)
                    {
                        this.History.el3 = (double)receiveWeight - (this.History.el1 + this.History.el2);
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TLeft2.Run());
                    processStep = 6;
                }
            }
            else if (receiveWeight >= this.MainTank.TLeft2.StageLimit && receiveWeight < this.MainTank.TRight1.StageLimit)
            {
                if (processStep <= 6)
                {
                    if (this.History != null)
                    {
                        this.History.el4 = (double)receiveWeight - (this.History.el1 + this.History.el2 + this.History.el3);
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TRight1.Run());
                    processStep = 7;
                }
            }
            else if (receiveWeight >= this.MainTank.TRight1.StageLimit && receiveWeight < this.MainTank.TRight2.StageLimit)
            {
                if (processStep <= 7)
                {
                    if (this.History != null)
                    {
                        this.History.el5 = (double)receiveWeight - (this.History.el1 + this.History.el2 + this.History.el3 + this.History.el4);
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TRight2.Run());
                    processStep = 8;
                }
            }
            else if (receiveWeight >= this.MainTank.TRight2.StageLimit && receiveWeight < this.MainTank.TRight3.StageLimit)
            {
                if (processStep <= 8)
                {
                    if (this.History != null)
                    {
                        this.History.el6 = (double)receiveWeight - (this.History.el1 + this.History.el2 + this.History.el3 + this.History.el4 + this.History.el5);
                    }
                    else
                    {

                    }
                    this.StopRunComponent();
                    this.AddToRunComponent(this.MainTank.TRight3.Run());
                    processStep = 9;
                }
            }
            else if (receiveWeight >= this.MainTank.TRight3.StageLimit)
            {
                if (processStep <= 9)
                {
                    if (this.History != null)
                    {
                        this.History.el7 = (double)receiveWeight - (this.History.el1 + this.History.el2 + this.History.el3 + this.History.el4 + this.History.el5 + this.History.el6);
                    }
                    else
                    {

                    }

                    if (TankControl.Properties.Settings.Default.SystemTest == 1)
                    {
                        RunTester.Singleton.StopTimer();
                    }
                    this.controlArea.CheckFillup.IsChecked = true;
                    processStep = 10;
                    this.ProcessShake();
                    
                }
            }
        }

        private void StopRunComponent(List<IComponent> getRuns = null)
        {
            if (this.runComponents.Count > 0)
            {
                foreach (IComponent cmp in this.runComponents)
                {
                    cmp.Stop();
                }
            }

            this.runComponents.Clear();
        }

        private void AddToRunComponent(IComponent cmp)
        {
            if (this.runComponents == null)
            {
                this.runComponents = new List<IComponent>();
            }

            if (!this.runComponents.Contains(cmp))
            {
                this.runComponents.Add(cmp);
            }
        }
        /// <summary>
        /// this process responsible to mix the elements inside the tank by duration specify in recipe
        /// </summary>


        public void ProcessShake()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(UpdateShake);
            timer.Start();
        }

        private void UpdateShake(object sender, EventArgs e)
        {
            shakeCounter++;
            
                if (shakeCounter < this.Recipe.runtime)
                {
                    if (this.processStep <= 10)
                    {
                        this.StopRunComponent();
                        this.AddToRunComponent(this.MainTank.OpenValveControl());
                        this.AddToRunComponent(this.MainTank.OpenValveShake());
                        this.processStep = 11;
                    }
                }
                else
                {
                    this.controlArea.CheckMixing.IsChecked = true;
                    shakeCounter = 0;
                    this.controlArea.EnableStartProcess();
                    this.StopRunComponent();
                    (sender as DispatcherTimer).Stop();
                    this.FillupStop();
                }
        }


        public void SetupFillup()
        {
            if (this.Recipe != null)
            {
                decimal pumpOneA = (decimal)(this.Recipe.el1 * this.Recipe.switch_el1);
                decimal pumpOneB = pumpOneA + (decimal)(this.Recipe.el1 * (1 - this.Recipe.switch_el1));

                decimal pumpTwoA = pumpOneB + (decimal)(this.Recipe.el2 * this.Recipe.switch_el2);
                decimal pumpTwoB = pumpTwoA + (decimal)(this.Recipe.el2 * (1 - this.Recipe.switch_el2));

                decimal pumpThree = pumpTwoB + (decimal)this.Recipe.el3;

                decimal pumpFour = pumpThree + (decimal)this.Recipe.el4;

                decimal pumpFive = pumpFour + (decimal)this.Recipe.el5;

                decimal pumpSix = pumpFive + (decimal)this.Recipe.el6;

                decimal pumpSeven = pumpSix + (decimal)this.Recipe.el7;

                this.MainTank.TPump1.StageLimit = pumpOneA;
                this.MainTank.TPump1.StageLimit2 = pumpOneB;
                this.MainTank.TPump2.StageLimit = pumpTwoA;
                this.MainTank.TPump2.StageLimit2 = pumpTwoB;
                this.MainTank.TLeft1.StageLimit = pumpThree;
                this.MainTank.TLeft2.StageLimit = pumpFour;
                this.MainTank.TRight1.StageLimit = pumpFive;
                this.MainTank.TRight2.StageLimit = pumpSix;
                this.MainTank.TRight3.StageLimit = pumpSeven;

                this.History = new History(this.Recipe.id, this.Recipe.name);
            }
            else
            {

            }
        }

        /// <summary>
        /// Responsible to trigger the fill up process
        /// </summary>
        public void FillupRun()
        {
            this.processRun = true;
            this.SetupFillup();

            if (TankControl.Properties.Settings.Default.SystemTest == 1)
            {
                RunTester.Singleton.RunTimer();
            }

            this.MainTank.Start();
        }

        public void FillupStop()
        {
            this.MainTank.Stop();
            using (TankControlEntities tce = new TankControlEntities())
            {
                if (this.History != null)
                {
                    tce.Histories.Add(this.History);
                    try
                    {
                        tce.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.InnerException.InnerException);
                    }
                    catch (System.Data.EntityException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.InnerException.InnerException);
                    }
                }

                this.History = null;
            }

            this.controlArea.CheckFillup.IsChecked = false;
            this.controlArea.CheckMixing.IsChecked = false;
            this.processStep = 0;
            this.processRun = false;
            this.Reset(true);
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
            this.MainTank.OpenValveControl();
            this.MainTank.OpenValveOutput();
        }

        public void TakeStop()
        {
            this.MainTank.StopValveControl();
            this.MainTank.StopValveOutput();
        }

        /// <summary>
        /// Stops all component.
        /// </summary>
        /// <param name="cleanup">if cleanup true, it will reset all the recipe attributes store in each tank to 0</param>
        public void Reset(bool cleanup)
        {
            this.MainTank.TPump1.End(cleanup);
            this.MainTank.TPump2.End(cleanup);
            this.MainTank.TLeft1.End(cleanup);
            this.MainTank.TLeft2.End(cleanup);
            this.MainTank.TRight1.End(cleanup);
            this.MainTank.TRight2.End(cleanup);
            this.MainTank.TRight3.End(cleanup);
        }

        #endregion

        // ADD VIEW OSERVER
        public void AddView(GraphicDisplayArea processView)
        {
            if (processView != null)
            {
                this.View = processView;
                this.SetupTanks(processView);
            }

        }

        public void AddControlArea(ControlArea controlArea)
        {
            if (controlArea != null)
            {
                this.controlArea = controlArea;
            }
        }

        /// <summary>
        /// This method responsible to tie component with each matching view.
        /// </summary>
        /// <param name="gdaView">takes the view which contain the whole component pump, valve, etc.</param>
        private void SetupTanks(GraphicDisplayArea gdaView)
        {
            if (gdaView.GdaMainTank != null)
            {
                if (this.MainTank == null)
                {
                    List<IComponent> list = new List<IComponent>{
                            new ControlValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO14),gdaView.GdaMainTankShake.Slc,(int)ReferenceEnum.Component.ValveControl),
                            new ShakeValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO12),gdaView.GdaMainTankShake.Src, (int)ReferenceEnum.Component.ValveShake),
                            new OutValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO13),gdaView.GdaMainTankShake.Oc, (int)ReferenceEnum.Component.ValveOutput)
                    };
                    this.MainTank = new MainTank(gdaView, (int)ReferenceEnum.Tank.MainTank, list);
                }
            }

            if (gdaView.GdaTinyTankPump1 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO3),gdaView.GdaTinyTankPump1.Bv, (int)ReferenceEnum.Component.ValveBig),
                        new SmallValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO4),gdaView.GdaTinyTankPump1.Sv, (int)ReferenceEnum.Component.ValveSmall),
                        new Pump(Convert.ToUInt16(ReferenceEnum.MOXA.DO0),gdaView.GdaTinyTankPump1.Pc, (int)ReferenceEnum.Component.PumpTinyTank)
                };

                this.MainTank.TPump1 = new TinyTankPump(gdaView.GdaTinyTankPump1, (int)ReferenceEnum.Tank.TinyTankOne, list);
            }

            if (gdaView.GdaTinyTankPump2 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO5),gdaView.GdaTinyTankPump2.Bv, (int)ReferenceEnum.Component.ValveBig),
                        new SmallValve(Convert.ToUInt16(ReferenceEnum.MOXA.DO6),gdaView.GdaTinyTankPump2.Sv, (int)ReferenceEnum.Component.ValveSmall),
                        new Pump(Convert.ToUInt16(ReferenceEnum.MOXA.DO2),gdaView.GdaTinyTankPump2.Pc, (int)ReferenceEnum.Component.PumpTinyTank)
                };

                this.MainTank.TPump2 = new TinyTankPump(gdaView.GdaTinyTankPump2, (int)ReferenceEnum.Tank.TinyTankTwo, list);
            }

            if (gdaView.GdaTinyTank3 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO7),gdaView.GdaTinyTank3.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TLeft1 = new TinyTankL(gdaView.GdaTinyTank3, (int)ReferenceEnum.Tank.TinyTankThree, list);
            }

            if (gdaView.GdaTinyTank4 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveL(Convert.ToUInt16(ReferenceEnum.MOXA.DO8),gdaView.GdaTinyTank4.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TLeft2 = new TinyTankL(gdaView.GdaTinyTank4, (int)ReferenceEnum.Tank.TinyTankFour, list);
            }

            if (gdaView.GdaTinyTank5 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO9),gdaView.GdaTinyTank5.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight1 = new TinyTankR(gdaView.GdaTinyTank5, (int)ReferenceEnum.Tank.TinyTankFive, list);
            }

            if (gdaView.GdaTinyTank6 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO10),gdaView.GdaTinyTank6.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight2 = new TinyTankR(gdaView.GdaTinyTank6, (int)ReferenceEnum.Tank.TinyTankSix, list);
            }

            if (gdaView.GdaTinyTank7 != null)
            {
                List<IComponent> list = new List<IComponent>{
                        new BigValveR(Convert.ToUInt16(ReferenceEnum.MOXA.DO11),gdaView.GdaTinyTank7.Bv, (int)ReferenceEnum.Component.ValveBig),
                };
                this.MainTank.TRight3 = new TinyTankR(gdaView.GdaTinyTank7, (int)ReferenceEnum.Tank.TinyTankSeven, list);
            }
        }

        // LISTENER WEIGHT SCALE
        public void WeightUpdated(decimal currentWeight, float addweight)
        {
            if (processRun)
            {
                if ((float)currentWeight >= 0)
                {
                    double addPixel = (float)currentWeight / this.PixelRate;

                    this.MainTank.FillupWithLimit(addPixel);
                    this.ProcessFillup(currentWeight);
                }
            }
        }
    }
}
