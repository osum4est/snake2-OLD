using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    class LevelTypeForest : BaseLevelType
    {
        public LevelTypeForest()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(GameMain.Current.fontMain, "Forest", new Vector2(200 * Adventure.Current.currentLevel.X, 200 * Adventure.Current.currentLevel.Y), Color.Green);
            spriteBatch.End();
        }
    }
}
