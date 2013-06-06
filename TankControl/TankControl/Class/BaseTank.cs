using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public class BaseTank
    {
        private IList<IComponent> listComponents;
        private int id;

        public decimal StageLimit {get;set;}

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public IList<IComponent> ListComponents
        {
            get
            {
                return this.listComponents;
            }
            set
            {
                this.listComponents = value;
            }
        }

        public void AddComponents(IList<IComponent> components)
        {
            if (this.ListComponents == null)
            {
                this.ListComponents = new List<IComponent>();
            }

            if (components.Count() > 0)
            {
                foreach (IComponent cmp in components)
                {
                    ListComponents.Add(cmp);
                }
            }
        }

        public void AddComponent(IComponent component)
        {
            if (this.ListComponents == null)
            {
                this.ListComponents = new List<IComponent>();
            }

            ListComponents.Add(component);
        }

        public IList<IComponent> GetAllComponent()
        {
            return ListComponents;
        }

        public IComponent GetComponent(IComponent component)
        {
            IComponent cmp = ListComponents.FirstOrDefault(x => x == component);
            return cmp;
        }

        public IComponent GetComponent(int id)
        {
            IComponent cmp = ListComponents.FirstOrDefault(x => x.Id == id);
            return cmp;
        }

        public bool RemoveAllComponent()
        {
            bool success = false;
            int totalComponent = ListComponents.Count();

            if (totalComponent > 0)
            {
                foreach (IComponent cmp in listComponents)
                {
                    if (ListComponents.Remove(cmp))
                    {
                        totalComponent--;
                    }
                }
            }

            if (totalComponent == 0)
            {
                success = true;
            }

            return success;
        }

        public bool RemoveComponent(IComponent component)
        {
            bool success = false;

            if (component != null)
            {
                if (ListComponents.Remove(component))
                {
                    success = true;
                }
            }

            return success;
        }

        public void RemoveComponent(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveComponent(string name)
        {
            throw new NotImplementedException();
        }

        public void CleanUp()
        {
            this.StageLimit = 0;
        }

        public void _TempAddWeight(decimal weight = 1)
        {
            RunTester.Singleton.AddWeight = weight;
        }

        public void _TempEqualWeight()
        {
            RunTester.Singleton.AddWeight = 0;
        }

        

    }
}
