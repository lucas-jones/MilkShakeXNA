using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Core.Scenes.Components
{
    public class SceneComponent : GameEntity
    {
        public SceneComponent() { }

        public override void Setup()
        {
            base.Setup();

            Scene.Listener.EntityAdded += new EntityEvent(OnEntityAdded);
            Scene.Listener.EntityRemoved += new EntityEvent(OnEntityRemoved);
        }

        public override void Destroy()
        {
            base.Destroy();

            Scene.Listener.EntityAdded -= new EntityEvent(OnEntityAdded);
            Scene.Listener.EntityRemoved -= new EntityEvent(OnEntityRemoved);
        }

        protected virtual void OnEntityRemoved(Entity node) { }
        protected virtual void OnEntityAdded(Entity node) { }
    }
}
