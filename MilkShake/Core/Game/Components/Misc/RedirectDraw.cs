using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class RedirectDraw : GameEntity
    {
        private GameEntity _drawHost;
        private RedirectDrawHook _drawHostHook;

        public RedirectDraw(GameEntity drawHost)
        {
            

            _drawHost = drawHost;

           
        }

        public override void FixUp()
        {
            base.FixUp();

            GameEntity gameEntityParent = (GameEntity)Parent;
            // Prevent parent from drawing
            gameEntityParent.AutoDraw = false;

            _drawHost.AddNode(_drawHostHook = new RedirectDrawHook(gameEntityParent));

            //AutoDraw = false;
        }
    }

    public class RedirectDrawHook : GameEntity
    {
        private GameEntity _requestedNodeDraw;

        public RedirectDrawHook(GameEntity requestedNodeDraw)
        {
            _requestedNodeDraw = requestedNodeDraw;
        }

        public override void Draw()
        {
            _requestedNodeDraw.Draw();
            base.Draw();
        }
    }
}
