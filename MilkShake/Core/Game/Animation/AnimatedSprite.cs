using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Animation
{
    public class AnimatedSprite : GameEntity
    {
        private SpriteSheet mSpriteSheet;
        private AnimationFile mAnimationFile;
        private List<Rectangle> mSpriteSource;

        private int curFrame = 0;
        private float curTime = 0;

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
            mSpriteSheet.Draw(WorldPosition, 100, 100, mSpriteSource[curFrame]);

            base.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            curTime += gameTime.ElapsedGameTime.Milliseconds;
             
            if (curTime > 100)
            {
                curFrame++;
                curTime -= 100;
            }           

            if (curFrame == mAnimationFile.Animations[0].EndFrame + 1)
            {
                curFrame = mAnimationFile.Animations[0].StartFrame + 1;
            }

            base.Update(gameTime);
        }
    }
}
