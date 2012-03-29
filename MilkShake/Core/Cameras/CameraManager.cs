using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Cameras
{
    public class CameraManager : SceneComponent
    {
        private Camera mCurrentCamera;

        public CameraManager(Scene scene) : base(scene)
        {
        }

        public Camera CurrentCamera { get { return mCurrentCamera;  } }
    }
}
