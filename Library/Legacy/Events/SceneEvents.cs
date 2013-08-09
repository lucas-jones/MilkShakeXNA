using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Events
{
    public class SceneEvents
    {
        public const string PRE_DRAW = "PRE_DRAW";
        public const string POST_DRAW = "POST_DRAW";

        public const string PRE_SCENE_DRAW = "PRE_SCENE_DRAW";
        public const string POST_SCENE_DRAW = "POST_SCENE_DRAW";
        
    }

    public class GameEntityEvents
    {
        public const string UPDATE = "UPDATE";

        public const string SETUP = "SETUP";
        public const string LOAD = "LOAD";
        public const string FIXUP = "FIXUP";
    }

    
}
