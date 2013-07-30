using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Components.Scripting
{
    public class ScriptComponent : SceneComponent
    {
        public ScriptComponent() { }

        protected override void OnEntityAdded(Entity node)
        {
            if (node is IScript && node is GameEntity) AddScript(node as GameEntity);
        }
        
        private void AddScript(GameEntity gameEntity)
        {
            string scriptURL = (gameEntity as IScript).Url;

            gameEntity.AddNode(new ScriptNode(gameEntity, scriptURL));
        }
    }
}
