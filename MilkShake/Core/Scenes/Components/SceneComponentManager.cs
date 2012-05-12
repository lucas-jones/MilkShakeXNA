using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Scenes.Components
{
    public class SceneComponentManager
    {
        private List<SceneComponent> mComponents;

        public SceneComponentManager()
        {
            mComponents = new List<SceneComponent>();
        }

        public void AddComponent(SceneComponent aComponent)
        {
            if (!HasComponent(aComponent.GetType()))
            {                
                mComponents.Add(aComponent);
            }
            else
            {
                throw new Exception("Component type already exists");
            }
        }

        public bool HasComponent<T>()
        {
            foreach (SceneComponent sceneComponent in mComponents)
            {
                if (sceneComponent.GetType() == typeof(T)) return true;
            }

            return false;
        }

        public bool HasComponent(Type aComponent)
        {
            foreach (SceneComponent sceneComponent in mComponents)
            {
                if (sceneComponent.GetType() == aComponent) return true;
            }

            return false;
        }

        public T GetComponent<T>()
        {
            foreach (SceneComponent sceneComponent in mComponents)
            {
                if (sceneComponent.GetType() == typeof(T)) return (T)(object)sceneComponent;
            }

            throw new Exception("Component dosn't exists");
        }

    }
}
