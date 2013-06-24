using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading;
using System.ComponentModel;

namespace MilkShakeFramework.Core.Scenes
{
    public delegate void SceneChangeEvent(Scene scene);

    public static class SceneManager
    {
        public static SceneChangeEvent OnSceneChange;
        public static event SceneChangeEvent OnSceneLoadStart;
        public static event SceneChangeEvent OnSceneLoadComplete;
        public static event SceneChangeEvent OnSceneLoadEnd;
        
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

            if (mCurrentScreenKey != null && mScenes.ContainsKey(mCurrentScreenKey)) CurrentScene.TearDown();

            mCurrentScreenKey = key;

            if (!CurrentScene.ContentManager.IsLoaded)
            {
                CurrentScene.Setup();
                CurrentScene.LoadScene();
            }

            CurrentScene.FixUp();

            if (OnSceneChange != null) OnSceneChange(CurrentScene);
            
        }

        public static void LoadScreenTheaded(string key, BasicEvent onComplete)
        {
            Scene sceneToLoad = Scenes[key];

            if (!sceneToLoad.ContentManager.IsLoaded)
            {
                BackgroundWorker backgroundLoad = new BackgroundWorker();

                backgroundLoad.DoWork += (sender, args) =>
                {
                    if (SceneManager.OnSceneLoadStart != null) SceneManager.OnSceneLoadStart(sceneToLoad);

                    sceneToLoad.Setup();
                    sceneToLoad.LoadScene();
                };

                backgroundLoad.RunWorkerCompleted += (sender, args) =>
                {
                    if (SceneManager.OnSceneLoadEnd != null) SceneManager.OnSceneLoadEnd(sceneToLoad);

                    onComplete();
                };

                backgroundLoad.RunWorkerAsync();
            }
            else
            {
                onComplete();
            }
        }

        public static void Draw()
        {
            CurrentScene.Draw();
        }

        public static void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        // [Pulbic]
        public static Dictionary<string, Scene> Scenes { get { return mScenes; } }
        public static Scene CurrentScene { get { return mScenes[mCurrentScreenKey]; } }
    }


    // [Temp]
    public class LoadingScene { }
    public class Transition { }
}
