using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using System;
using MilkShakeFramework.Core.Game;

namespace Playground
{
    public class Playground : MilkShake
    {
        public Playground() : base(400, 250) 
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("BasicScene", new BasicScene());
            SceneManager.ChangeScreen("BasicScene");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
    }

    public class BasicScene : Scene
    {
        public BasicScene() : base()
        {

           
        }

        public override void Setup()
        {
            AddNode(new Sprite("Img"));
            base.Setup();
        }

    }
}
