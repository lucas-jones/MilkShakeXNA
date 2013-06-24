using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game.Components.Misc;

namespace MilkShakeFramework.Tools.Utils
{
    public class Wait
    {
        public Wait(float delay, BasicEvent callBack)
        {
            EventTimer timeLine = new EventTimer(delay);
            timeLine.AddEvent(100, () =>
            {
                callBack();
                SceneManager.CurrentScene.RemoveNode(timeLine);
            });

            SceneManager.CurrentScene.AddNode(timeLine);
        }
    }
}
