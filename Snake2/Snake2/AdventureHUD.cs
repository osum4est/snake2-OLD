using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class AdventureHUD : DrawableGameComponent
    {
        Adventure adventure;
        GameMain gm;
        SpriteBatch spriteBatch;
        AdventureSnake snake;

        string strSword;
        Vector2 vSword;
        Color clrSword;
        Rectangle rectSword;
        Texture2D sptSword;

        string strCoin;
        Vector2 vCoin;
        Color clrCoin;
        Rectangle rectCoin;
        Texture2D sptCoin;

        SpriteFont font;

        public AdventureHUD(Adventure adventure, GameMain gm) : base (gm)
        {
            this.gm = gm;
            this.adventure = adventure;
            snake = adventure.snake;
            spriteBatch = new SpriteBatch(gm.GraphicsDevice);
            font = gm.fontMain;

            strSword = "Swords: ";
            vSword = new Vector2(16, 4);
            clrSword = Color.DarkSlateGray;
            sptSword = Sprites.sword.texture;
            rectSword = new Rectangle((int)vSword.X + (int)font.MeasureString(strSword).X, 4 - ((sptSword.Height - (int)font.MeasureString(strSword).Y)) / 2, sptSword.Width, sptSword.Height);

            strCoin = "Coins: ";
            vCoin = new Vector2(16, 40);
            clrCoin = Color.Goldenrod;
            sptCoin = Sprites.coin.texture;
            rectCoin = new Rectangle((int)vCoin.X + (int)font.MeasureString(strCoin).X, (int)vCoin.Y + (int)font.MeasureString(strCoin).Y - (((int)font.MeasureString(strCoin).Y) - sptCoin.Height) / 2 - sptCoin.Height - 2, sptCoin.Width, sptCoin.Height);
        }

        public override void Update(GameTime gameTime)
        {
            strCoin = "Coins: " + snake.coins;
            rectCoin.X = (int)vCoin.X + (int)font.MeasureString(strCoin).X + 3;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, strSword, vSword, clrSword);
            spriteBatch.DrawString(font, strCoin, vCoin, clrCoin);
            spriteBatch.Draw(sptCoin, rectCoin, Color.White);

            for (int i = 0; i < snake.swords; i++)
            {
                rectSword.Offset(20 * i, 0);
                spriteBatch.Draw(sptSword, rectSword, Color.White);
                rectSword.Offset(-(20 * i), 0);
            }

            spriteBatch.End(); 
        }
    }
}
