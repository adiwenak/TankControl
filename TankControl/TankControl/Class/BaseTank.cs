using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public class BaseTank
    {
        private IList<Component> listComponents;
        private Guid id;
        private string name;
        private float stageLimit;

        public float StageLimit
        {
            get
            {
                return this.stageLimit;
            }
            set
            {
                this.stageLimit = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Guid ID
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

        public IList<Component> ListComponents
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

        public void AddComponents(IList<Component> components)
        {
            if (this.ListComponents == null)
            {
                this.ListComponents = new List<Component>();
            }

            if (components.Count() > 0)
            {
                foreach (Component cmp in components)
                {
                    ListComponents.Add(cmp);
                }
            }
        }

        public void AddComponent(Component component)
        {
            if (this.ListComponents == null)
            {
                this.ListComponents = new List<Component>();
            }

            ListComponents.Add(component);
        }

        public IList<Component> GetAllComponent()
        {
            return ListComponents;
        }

        public Component GetComponent(Component component)
        {
            Component cmp = ListComponents.FirstOrDefault(x => x == component);
            return cmp;
        }

        public Component GetComponent(Guid id)
        {
            Component cmp = ListComponents.FirstOrDefault(x => x.Id == id);
            return cmp;
        }

        public Component GetComponent(string name)
        {
            Component cmp = ListComponents.FirstOrDefault(x => x.Name == name);
            return cmp;
        }

        public bool RemoveAllComponent()
        {
            bool success = false;
            int totalComponent = ListComponents.Count();

            if (totalComponent > 0)
            {
                foreach (Component cmp in listComponents)
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

        public bool RemoveComponent(Component component)
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


    }
}
