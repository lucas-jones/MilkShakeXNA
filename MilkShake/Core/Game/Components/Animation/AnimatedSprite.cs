using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Animation
{
    public enum PlayMode { Loop, PlayOnce, PlayAndPause } ;

    public class AnimatedSprite : GameEntity
    {
        private SpriteSheet mSpriteSheet;
        private AnimationFile mAnimationFile;
        private List<Rectangle> mSpriteSource;

        private int curFrame = 1;
        private float curTime = 0;

        private string curAnimation = "Idle";

        private bool mIsFlipped = false;

        public AnimatedSprite(string animatedSpriteURL)
        {
            loadAnimationFile(animatedSpriteURL);
        }

        private void loadAnimationFile(string fileURL)
        {
            mAnimationFile = new AnimationFile(fileURL);
            mSpriteSheet = new SpriteSheet(mAnimationFile.ImageURL.Split('.')[0]);
            mSpriteSource = new List<Rectangle>();
        }

        public override void Setup()
        {
            AddNode(mSpriteSheet);
            base.Setup();
        }

        public override void FixUp()
        {
            base.FixUp();

            int spriteWidth = mSpriteSheet.Texture.Width / mAnimationFile.SpriteCount;

            for (int frames = 0; frames < mAnimationFile.SpriteCount; frames++)
            {
                mSpriteSource.Add(new Rectangle(frames * spriteWidth, 0, spriteWidth, mSpriteSheet.Texture.Height));
            }
        }


        public override void Draw()
        {
            mSpriteSheet.Draw(WorldPosition, 100, 100, mSpriteSource[curFrame], mIsFlipped);

            base.Draw();
        }

        private PlayMode playMode = PlayMode.Loop;

        public override void Update(GameTime gameTime)
        {

            if (curTime >= 100)
            {
                curFrame++;
                curTime = 0;

                if (curFrame > CurrentAnimation.EndFrame - 1)
                {
                    curFrame = CurrentAnimation.StartFrame + 1;
                }
            }

            curTime += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        public void SetAnimation(string _AnimationName)
        {
            if (_AnimationName != curAnimation)
            {
                curAnimation = _AnimationName;

                curFrame = CurrentAnimation.StartFrame + 1;
            }
        }


        public bool IsFlipped { get { return mIsFlipped; } set { mIsFlipped = value; } }
        public Animation CurrentAnimation { get { return mAnimationFile.GetAnimation(curAnimation); } }
        public List<Rectangle> SourceRectangles { get { return mSpriteSource; } }
        public SpriteSheet SpriteSheet { get { return mSpriteSheet; } }
    }
}
