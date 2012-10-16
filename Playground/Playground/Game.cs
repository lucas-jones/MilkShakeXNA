using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Core.Game;
using Playground.Tools;
using MilkShakeFramework.Components.Lighting;
using Krypton;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Components.Lighting.Lights;
using MilkShakeFramework.Core.Scenes.Components;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using MilkShakeFramework.Tools.Physics;
using FarseerPhysics.Controllers;
using Lidgren.Network;
using MilkShakeFramework.Core.Game.Components.Polygon;
using MilkShakeFramework.Core.Game.Components.Polygon.Render;
using MilkShakeFramework.Tools;

namespace Playground
{
    public class Playground : MilkShake
    {
        public Playground() : base(1280, 720) { }

        protected override void Initialize()
        {
            base.Initialize();
            
            SceneManager.AddScene("BasicScene", new BasicScene());
            SceneManager.ChangeScreen("BasicScene");
        }
    }

    public class BasicScene : Scene
    {
        private PointLight pointLight;
        

        private Sprite icecreamTag;

        private VGroup buttonGroup;

        public BasicScene() : base()
        {
            //

            ComponentManager.AddComponent(new LightingComponent(this, LightMapSize.Eighth, 16));
            ComponentManager.AddComponent(new PhysicsComponent(this, new Vector2(0, 60)) { CameraRotationGravity = false, DrawDebug = false });

            // Scene
            
            UILayer uiForground = new UILayer(DrawMode.Post);
            UILayer uiBackground = new UILayer(DrawMode.Pre);

            uiBackground.AddNode(new ScrollingPattern());
            uiForground.AddNode(icecreamTag = new UISprite("icecreamtag") { Position = new Vector2(Globals.ScreenWidth - 112, Globals.ScreenHeight - 46) });
            
            //uiForground.AddNode(new Sprite("bar") { Height = Globals.ScreenHeight });
            
            uiForground.AddNode(buttonGroup = new VGroup() { Position = new Vector2(5, 5) });
            buttonGroup.AddNode(new UISprite("Icons2//Add"));
            buttonGroup.AddNode(new UISprite("Icons2//Remove"));
            buttonGroup.AddNode(new UISprite("Icons2//Gear"));
            buttonGroup.AddNode(new UISprite("Icons2//Blue Ball"));
            buttonGroup.AddNode(new UISprite("Icons2//Desktop"));
            buttonGroup.AddNode(new UISprite("Icons2//Run"));

            buttonGroup.Nodes.ForEach(button => (button as UISprite).onMouseEnter += () => (button as UISprite).Color = Color.White);
            buttonGroup.Nodes.ForEach(button => (button as UISprite).onMouseExit += () => (button as UISprite).Color = Color.Gray);
            
            //uiBackground.Listener.Update += (gameTime) => uiBackground.X = MouseInput.X;

           


            AddNode(uiForground);
            AddNode(uiBackground);

            List<Vector2> quad = new List<Vector2>();
            quad.Add(Vector2.Zero);
            quad.Add(new Vector2(0, 200));
            quad.Add(new Vector2(200, 200));
            quad.Add(new Vector2(200, 0));
            quad.Add(new Vector2(200, 100));
          

            //AddNode(PolygonFactory.Quad(200, 200));
            //Polygon poly = new Polygon(PolygonDataFactory.PolygonFromPoints(quad), new TexturedPolygonRenderer("se_free_dirt_texture")) { Position = new Vector2(100, 100) };
            Polygon poly = new Polygon(PolygonDataFactory.PolygonFromPoints(quad), new BasicPolygonRenderer(Color.White, true)) { Position = new Vector2(100, 100) };
            poly.AddNode(new EditPolygonModifier());
            AddNode(poly);//

            //AddNode(new Polygon(PolygonFactory.PolygonFromPoints(quad), Color.Green));
            //AddNode(playerOne);
            //AddNode(playerTwo);
           //  planet = BodyFactory.CreateCircle(PhysicsComponent.World, ConvertUnits.ToSimUnits(50), 0.1f);
           // planet.Position = ConvertUnits.ToSimUnits(new Vector2(Globals.ScreenWidthCenter, Globals.ScreenHeightCenter));
   
            //planet.BodyType = BodyType.Static;
            
        }
        Body planet;
        private List<Body> shizzle = new List<Body>();

        public override void Update(GameTime gameTime)
        {
            if (KeyboardInput.isKeyPressed(Keys.L))
            {
                Color mRandomColor = new Color(
          (Globals.Random.Next(50, 230)),
          (Globals.Random.Next(50, 230)),
          (Globals.Random.Next(50, 230)));

                pointLight = new PointLight(512) { Color = mRandomColor };
                pointLight.AddNode(new PointLight(512, Krypton.Lights.ShadowType.Illuminated) { Color = mRandomColor, Intensity = 0.7F });
                AddNode(pointLight);
            }

            if (KeyboardInput.isKeyDown(Keys.Space))
            {
                Body mBody = BodyFactory.CreateCircle(PhysicsComponent.World, ConvertUnits.ToSimUnits(5), 0.1f);
                mBody.Position = ConvertUnits.ToSimUnits(MouseInput.Position);
                mBody.BodyType = BodyType.Dynamic;
                mBody.Friction = 0.2f;
                mBody.Restitution = 0.6f;
                
                shizzle.Add(mBody);
            }
           // planet.ApplyForce(
            //shizzle.ForEach(pew => pew.ApplyForce(1, planet.Position));

            if(pointLight != null) pointLight.Position = MouseInput.WorldPosition;
            ControlCamera();
            base.Update(gameTime);
        }

        private void ControlCamera()
        {
            Vector2 movementStash = Vector2.Zero;

            if (KeyboardInput.isKeyDown(Keys.NumPad6)) movementStash.X++;
            if (KeyboardInput.isKeyDown(Keys.NumPad4)) movementStash.X--;
            if (KeyboardInput.isKeyDown(Keys.NumPad8)) movementStash.Y--;
            if (KeyboardInput.isKeyDown(Keys.NumPad2)) movementStash.Y++;

            if (KeyboardInput.isKeyDown(Keys.NumPad1)) Camera.Zoom += 0.01f;
            if (KeyboardInput.isKeyDown(Keys.NumPad3)) Camera.Zoom -= 0.01f;


            if (KeyboardInput.isKeyDown(Keys.NumPad7)) Camera.Rotation += MathHelper.ToRadians(20);
            if (KeyboardInput.isKeyDown(Keys.NumPad9)) Camera.Rotation -= MathHelper.ToRadians(20);

            Camera.Position += movementStash * 5;


        }

        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }
}

