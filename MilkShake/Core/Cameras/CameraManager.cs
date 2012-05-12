using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;

namespace MilkShakeFramework.Core.Cameras
{
    public class CameraManager : SceneComponent
    {
        private Camera mCurrentCamera;

        public CameraManager(Scene scene) : base(scene)
        {
            mCurrentCamera = new Camera();
        }

        public void Update(GameTime gameTime)
        {
            mCurrentCamera.Update(gameTime);
        }

        public Camera CurrentCamera { get { return mCurrentCamera;  } }
    }
}
