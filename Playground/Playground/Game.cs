using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using Samples.Scenes.Demo1;
using Samples.Scenes.Demos;

namespace Samples
{
    public class MilkShakeSamples : MilkShake
    {
        public MilkShakeSamples() : base(1280, 720) { }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("Demo1", new Demo1Scene());
            SceneManager.AddScene("Demo2", new Demo2Scene());
            SceneManager.AddScene("Demo3", new Demo3Scene());
            SceneManager.AddScene("Demo4", new Demo4Scene());
            SceneManager.AddScene("Demo_BreakOut", new Demo_BreakOut());

            SceneManager.ChangeScreen("Demo4");           
        }
    }
}

