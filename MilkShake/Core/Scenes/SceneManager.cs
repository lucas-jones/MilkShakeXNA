using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Scenes
{
    public static class SceneManager
    {
        private static Dictionary<string, Scene> mScenes;
        private static string mCurrentScreenKey;

        public static void Setup()
        {
            mScenes = new Dictionary<string, Scene>();

            AddScene("Default", new Scene());
            ChangeScreen("Default");
        }

        public static void AddScene(string key, Scene scene)
        {
            if (mScenes.ContainsKey(key)) throw new Exception("Screen name already exists.");
            
            mScenes.Add(key, scene);
        }

        public static void AddScene(string key, Scene scene, LoadingScene loadingScene)
        {

        }

        public static void RemoveScene(string key)
        {
            if (!mScenes.ContainsKey(key)) throw new Exception("Screen name dosn't excist.");

            mScenes.Remove(key);
        }

        public static void ChangeScreen(string key)
        {
            if (!mScenes.ContainsKey(key)) throw new Exception("Screen name dosn't exists.");

            mCurrentScreenKey = key;

            if (!CurrentScene.ContentManager.IsLoaded)
            {
                CurrentScene.Setup();
                CurrentScene.LoadScene();
                CurrentScene.FixUp();
            }
        }

        // [Pulbic]
        public static Dictionary<string, Scene> Scenes { get { return mScenes; } }
        public static Scene CurrentScene { get { return mScenes[mCurrentScreenKey]; } }
    }


    // [Temp]
    public class LoadingScene { }
    public class Transition { }
}
