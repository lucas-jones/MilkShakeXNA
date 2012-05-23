using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Components.Effects.Presets
{
    public class ReflectiveTileEffect : Effect
    {
        private Tile.Tile mTile;
        private float mOpacity;

        public ReflectiveTileEffect(Tile.Tile tile, float maxAlpha = 0.2f)
        {
            mTile = tile;
            mOpacity = maxAlpha;
        }

        public override void PostSceneRender()
        {
            Scene.RenderManager.Begin();

            for (int i = 1; i < 64; i++)
            {
                Vector2 tilePosition = mTile.WorldPosition - Scene.Camera.Position - new Vector2(0, i);
                Vector2 drawPosition = mTile.WorldPosition - Scene.Camera.Position;

                float alpha = mOpacity * (1 - ((float)i / 64));

                Scene.RenderManager.SpriteBatch.Draw(RenderTarget, drawPosition + new Vector2(0, i), new Rectangle((int)tilePosition.X, (int)tilePosition.Y, 64, 1), new Color(1f, 1f, 1f, alpha), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }


            Scene.RenderManager.End();
        }


    }
}
