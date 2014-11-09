using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snake2
{
    public abstract class BaseLevelType
    {
        public SpriteBatch spriteBatch;
        public Color ambientLight = Color.White;

        public BaseLevelType()
        {
            spriteBatch = new SpriteBatch(GameMain.Current.GraphicsDevice);
        }  

        public static List<BaseLevelType> levelTypes = new List<BaseLevelType>()
        {
            new LevelTypePlain(), 
            new LevelTypeForest()
        };

        public abstract void Draw(GameTime gameTime);
    }
}
