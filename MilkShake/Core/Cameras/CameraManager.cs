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
        public const string DEFAULT_CAMERA_NAME = "Default";

        public string CurrentCameraKey { get; private set; }
        public Camera CurrentCamera { get { return Cameras[CurrentCameraKey]; } }

        public Dictionary<string, Camera> Cameras { get; private set; }

        public CameraManager()
        {
            Cameras = new Dictionary<string, Camera>();

            AddCamera(DEFAULT_CAMERA_NAME, new Camera());
            SetCamera(DEFAULT_CAMERA_NAME);
        }

        public void Update(GameTime gameTime)
        {
            CurrentCamera.Update(gameTime);
        }

        public void AddCamera(string Name, Camera Camera)
        {
            Cameras.Add(Name, Camera);
        }

        public void SetCamera(string Name)
        {
            if (!Cameras.ContainsKey(Name)) throw new Exception("Camera not found!");

            CurrentCameraKey = Name;
        }
    }
}
