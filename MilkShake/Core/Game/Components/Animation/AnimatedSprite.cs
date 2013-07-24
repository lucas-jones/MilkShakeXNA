using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;

namespace MilkShakeFramework.Core.Game.Components.Animation
{
    public enum PlayMode { Loop, PlayOnce, PlayAndPause } ;

    public delegate void AnimationEvent(string animationName);

    public class AnimatedSprite : GameEntity
    {
        public event AnimationEvent OnAnimationComplete;

        private SpriteSheetRenderer mSpriteSheet;
        private AnimationFile mAnimationFile;
        private List<Rectangle> mSpriteSource;

        private int curFrame = 1;
        private float curTime = 0;

        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public float Rotation = 0;

        private string curAnimation = "NotSet";

        // Move?
        private bool mIsFlipped = false;

        public AnimatedSprite(string animatedSpriteURL, string animation = null)
        {
            loadAnimationFile(animatedSpriteURL);
            Visible = true;
            Color = Color.White;
            // Default to first animation
            curAnimation = animation ?? mAnimationFile.Animations[0].Name;
        }

        private void loadAnimationFile(string fileURL)
        {
            mAnimationFile = new AnimationFile(fileURL);

            string spriteSheetPath = (fileURL.Split('.')[0]).Replace("Content//", "");
            mSpriteSheet = new SpriteSheetRenderer(spriteSheetPath);
            mSpriteSource = new List<Rectangle>();
        }

        public override void Setup()
        {
            AddNode(mSpriteSheet);
            
            base.Setup();
        }

        public override void FixUp()
        {
            // Calculate tile count in sprite sheet
            int xTiles = mSpriteSheet.Texture.Width / mAnimationFile.SpriteWidth;
            int yTiles = mSpriteSheet.Texture.Height / mAnimationFile.SpriteHeight;

            // Add all frames
            for (int yFrames = 0; yFrames < yTiles; yFrames++)
            for (int xFrames = 0; xFrames < xTiles; xFrames++)
            {
                mSpriteSource.Add(new Rectangle(xFrames * mAnimationFile.SpriteWidth, yFrames * mAnimationFile.SpriteHeight, mAnimationFile.SpriteWidth, mAnimationFile.SpriteHeight));
            }

            base.FixUp();
        }

        public override void Draw()
        {
            if (Visible)
            {
                if (Scene.RenderManager.IsRawDraw)
                {
                    mSpriteSheet.Draw(WorldPosition + Scene.Camera.Transform, mAnimationFile.SpriteWidth, mAnimationFile.SpriteHeight, mSpriteSource[curFrame], mIsFlipped, Color, Rotation, Origin, Scale);
                }
                else
                {
                    mSpriteSheet.Draw(WorldPosition, mAnimationFile.SpriteWidth, mAnimationFile.SpriteHeight, mSpriteSource[curFrame], mIsFlipped, Color, Rotation, Origin, Scale);
                }

                base.Draw();
            }
        }

        private PlayMode playMode = PlayMode.Loop;

        public float frameRate = 35;
        public static float GlobalMultiplier = 1;

        public override void Update(GameTime gameTime)
        {
            float frameTime = (1000 / frameRate) * GlobalMultiplier;

            if (curTime >= frameTime)
            {
                curFrame++;
                curTime -= frameTime;

                if (curFrame >= CurrentAnimation.EndFrame)
                {
                    if (OnAnimationComplete != null) OnAnimationComplete(CurrentAnimation.Name);

                    if (playMode == PlayMode.Loop) curFrame = CurrentAnimation.StartFrame;
                    if (playMode == PlayMode.PlayAndPause) curFrame = CurrentAnimation.EndFrame;
                }
            }

            curTime += gameTime.ElapsedGameTime.Milliseconds;


            base.Update(gameTime);
        }

        public void SetAnimation(string _AnimationName, PlayMode _PlayMode = PlayMode.Loop, int frame = -1)
        {
            playMode = _PlayMode;

            if (_AnimationName != curAnimation)
            {
                curAnimation = _AnimationName;

                if (frame == -1)
                {
                    curFrame = CurrentAnimation.StartFrame;
                }
                else
                {
                    curFrame = frame;
                }
            }
        }

        public bool IsFlipped { get { return mIsFlipped; } set { mIsFlipped = value; } }

        public Color Color { get; set; }

        public Animation CurrentAnimation { get { return mAnimationFile.GetAnimation(curAnimation); } }
        public List<Rectangle> SourceRectangles { get { return mSpriteSource; } }
        public SpriteSheetRenderer SpriteSheet { get { return mSpriteSheet; } }
        public bool Visible { get; set; }
    }
}
