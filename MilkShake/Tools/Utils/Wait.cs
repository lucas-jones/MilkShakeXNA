using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game.Components.Misc;

namespace MilkShakeFramework.Tools.Utils
{
    public class Wait
    {
        public Wait(float delay, BasicEvent callBack)
        {
            EventTimer eventTimer = new EventTimer(delay);

            eventTimer.AddEvent(100, () =>
            {
                callBack();

                SceneManager.CurrentScene.RemoveNode(eventTimer);
            });

            SceneManager.CurrentScene.AddNode(eventTimer);
        }
    }
}
