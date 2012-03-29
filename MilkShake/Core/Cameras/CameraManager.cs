using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Cameras
{
    public class CameraManager
    {
        private Camera mCurrentCamera;

        public Camera CurrentCamera { get { return mCurrentCamera;  } }
    }
}
