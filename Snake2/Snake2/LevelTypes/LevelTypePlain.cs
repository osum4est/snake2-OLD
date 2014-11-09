using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class LevelTypePlain : BaseLevelType
    {
        public LevelTypePlain()
        {
            ambientLight = new Color(50, 0, 0, 25);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, GameMain.Current.camera.transform);
            spriteBatch.DrawString(GameMain.Current.fontMain, "Plain", new Vector2(200, 200), Color.AntiqueWhite);
            spriteBatch.End();
        }
    }
}
