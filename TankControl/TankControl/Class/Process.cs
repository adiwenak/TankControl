using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using TankControl.View;

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

        public void Initialize()
        {
            
        }

        public void WeightUpdated()
        {

        }

        public void Run()
        {

        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Stop()
        {
        }

        public void AddView(GraphicDisplayArea processView)
        {
            if (view != null)
            {
                View = processView;
                foreach (Component cmp in MainTank.Components)
                {
                    if (cmp is Pump)
                    {
                    
                    }
                    else if (cmp is Valve)
                    {
                        cmp.ComponentUC = processView.Valve;
                        cmp.IsRun();
                    }
                }
            }

        }

        public void RemoveView()
        {
            View = null;
        }

        public void NotifyView()
        {
            view.UpdateView();
        }

    }
}
