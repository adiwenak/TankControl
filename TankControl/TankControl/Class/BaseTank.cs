using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankControl.Class
{
    public class BaseTank
    {
        private ICollection<Component> listComponents;

        public void AddComponents(ICollection<Component> components)
        {
            if (components.Count() > 0)
            {
                foreach (Component cmp in components)
                {
                    listComponents.Add(cmp);
                }
            }
        }

        public void AddComponent(Component component)
        {
            listComponents.Add(component);
        }

        public ICollection<Component> GetAllComponent()
        {
            return listComponents;
        }

        public Component GetComponent(Component component)
        {
            Component cmp = listComponents.FirstOrDefault(x => x == component);
            return cmp;
        }

        public Component GetComponent(Guid id)
        {
            Component cmp = listComponents.FirstOrDefault(x => x.Id == id);
            return cmp;
        }

        public Component GetComponent(string name)
        {
            Component cmp = listComponents.FirstOrDefault(x => x.Name == name);
            return cmp;
        }

        public bool RemoveAllComponent()
        {
            bool success = false;
            int totalComponent = listComponents.Count();

            if (totalComponent > 0)
            {
                foreach (Component cmp in listComponents)
                {
                    if (listComponents.Remove(cmp))
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
                if (listComponents.Remove(component))
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
