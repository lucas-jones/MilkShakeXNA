using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Maths;
using System;

namespace MilkShakeFramework.Core.Game
{
    public class DisplayObject : GameEntity
    {        
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public bool Visible { get; set; }

        public DisplayObject()
        {
            Origin = Vector2.Zero;
            Rotation = 0;
            Scale = Vector2.One;
            Visible = true;
        }

        public override Matrix GenerateMatrix()
        {
            Matrix transform = Matrix.Identity;
            transform *= Matrix.CreateTranslation(-new Vector3(Origin, 0));
            transform *= Matrix.CreateRotationZ(Rotation);
            transform *= Matrix.CreateScale(Scale.X, Scale.Y, 0);
            
            return transform * base.GenerateMatrix();
        }
    }

    public class Sprite : DisplayObject
    {
        public ImageRenderer Image { get; protected set; }
        public Color Color { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        
        public bool AutoCenter { get; set; }

        public Sprite(string url, bool fromStream = false)
        {
            Image = new ImageRenderer(url, (fromStream) ? ImageRendererLoadMode.Steam : ImageRendererLoadMode.Content);
        }

        public Sprite(Texture2D texture)
        {
            Image = new ImageRenderer(texture);
        }

        public override void Setup()
        {
            Color = Color.White;
            Scale = new Vector2(1, 1);
            //AutoCenter = false;
            Visible = true;

            AddNode(Image);

            base.Setup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            if (Image.Texture == null)
            {
                Image.SetParent(this);
                Image.SetScene(Scene);
                Image.Load(null);
                Image.FixUp();
            }

            Width = SetValueOrDefault(Width, Image.Texture.Width);
            Height = SetValueOrDefault(Height, Image.Texture.Height);
        }

        public override void FixUp()
        {
            base.FixUp();

            if(AutoCenter)
            {
                Origin = new Vector2(Width / 2, Height / 2);
            }
        }

        public override void Draw()
        {
            if (Visible)
            {
                base.Draw();
                
                Image.Draw(Vector2.Zero, Width, Height, 0, Vector2.Zero, Color, 1, 1);
            }
        }



        public override RotatedRectangle BoundingBox
        {
            get
            {
                return new RotatedRectangle(new Rectangle((int)WorldPosition.X - (int)Origin.X, (int)WorldPosition.Y - (int)Origin.Y, (int)(Width * Scale.X), (int)(Height * Scale.Y)), (Rotation));
            }
        }
    }
}
