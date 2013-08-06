using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Core.Scenes.Components
{
    public class SceneComponentManager : GameEntity
    {
        public List<SceneComponent> Components { get { return Nodes.OfType<SceneComponent>().ToList(); } }

        public SceneComponentManager() { }

        public void AddComponent(SceneComponent component)
        {
            if (!HasComponent(component.GetType()))
            {
                AddNode(component);
            }
            else
            {
                throw new Exception("Component type already exists");
            }
        }

        public void RemoveComponent(SceneComponent component)
        {
            if (HasComponent(component.GetType()))
            {
                Components.Remove(component);
            }
            else
            {
                throw new Exception("Component type dosn't exists");
            }
        }

        public bool HasComponent<T>()
        {
            return HasComponent(typeof(T));
        }

        public bool HasComponent(Type component)
        {
            //return Components.FindAll(c => c.GetType() == component).Count > 0;
            return false;
        }

        public T GetComponent<T>()
        {
            T component = (T)(object)Components.First(c => c.GetType() == typeof(T));

            if(component == null) throw new Exception("Component " + typeof(T).Name + " dosn't exists");

            return component;
        }
    }
}
