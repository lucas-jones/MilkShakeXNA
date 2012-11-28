using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using LuaInterface;
using MilkShakeFramework.Core.Content;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.Scripting
{
    public class ScriptComponent : SceneComponent
    {
        public ScriptComponent(Scene aScene) : base(aScene)
        {
            aScene.Listener.EntityAdded += new EntityEvent(EntityAdded);
        }

        private void EntityAdded(Entity node)
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
