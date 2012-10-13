using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MilkShakeFramework.Core.Game.Animation
{
    public class AnimationFile
    {
        public List<Animation> Animations { get { return mAnimations; } }
        public string ImageURL { get { return mImageURL; } }

        public int SpriteWidth { get { return mSpriteWidth; } }
        public int SpriteHeight { get { return mSpriteHeight; } }
        public int SpriteCount { get { return mSpriteCount; } }

        private List<Animation> mAnimations;
        private string mImageURL;

        private int mSpriteHeight;
        private int mSpriteWidth;
        private int mSpriteCount;

        public AnimationFile(string xmlLocation)
        {
            mAnimations = new List<Animation>();

            ReadJSON(File.ReadAllText(xmlLocation));
        }

        private void ReadJSON(string JSON)
        {
            // Parse raw 
            JToken token = JObject.Parse(JSON);

            mImageURL = (string)token.SelectToken("images")[0];

            JObject animations = (JObject)token["animations"];

            foreach (var item in animations)
            {
                JArray array = new JArray(item.Value);
                string animationName = item.Key;
                int start = 0;
                int end = 0;

                foreach (JToken service in array)
                {
                    start = (int)service.First;
                    end = (int)service.Last;
                }

                mAnimations.Add(new Animation(animationName, start, end));
            }

            JObject frames = (JObject)token["frames"];

            mSpriteWidth = (int)frames["width"];
            mSpriteHeight = (int)frames["height"];
            mSpriteCount = (int)frames["count"];            
        }

        public Animation GetAnimation(string _AnimationName)
        {
            return Animations.Find(A => A.Name == _AnimationName);
        }

    }

    public class Animation
    {
        private string mName;
        private int mStartFrame;
        private int mEndFrame;

        public Animation(string name, int startFrame, int endFrame)
        {
            mName = name;
            mStartFrame = startFrame;
            mEndFrame = endFrame;
        }

        public string Name      { get { return mName; } }
        public int StartFrame   { get { return mStartFrame; } }
        public int EndFrame     { get { return mEndFrame; } }
    }
}
