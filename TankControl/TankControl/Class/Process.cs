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

        public void SetupMainTank()
        {
        }

        // PROPERTIES - END

        // CONTROL - START
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

        // CONTROL - END

        // ADD VIEW OSERVER
        public void AddView(GraphicDisplayArea processView)
        {
            if (view != null)
            {
                View = processView;

            }

        }

        // NOTIFY VIEW OBSERVER
        public void NotifyView()
        {
            view.UpdateView();
        }

        // LISTENER WEIGHT SCALE
        public void WeightUpdated()
        {

        }

    }
}
