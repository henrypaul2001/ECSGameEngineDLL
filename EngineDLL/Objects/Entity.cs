﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using EngineDLL.Components;

namespace EngineDLL.Objects
{
    public class Entity
    {
        string name;
        List<IComponent> componentList = new List<IComponent>();
        ComponentTypes mask;
 
        public Entity(string name)
        {
            this.name = name;
        }

        /// <summary>Adds a single component</summary>
        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            componentList.Add(component);
            mask |= component.ComponentType;
        }

        public String Name
        {
            get { return name; }
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }

        public List<IComponent> Components
        {
            get { return componentList; }
        }

        public void Close()
        {
            foreach (IComponent c in componentList)
            {
                c.Close();
            }
        }
    }
}
