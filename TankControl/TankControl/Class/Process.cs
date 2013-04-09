using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public class Process
    {
        private Recipe recipe;

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
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }


        public void UpdateState()
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

        public void AddView(MainWindow window)
        {
            

        }

        public void UpdateView()
        {
           
        }

    }
}
