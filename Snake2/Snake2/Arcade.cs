using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    public class Arcade : DrawableGameComponent
    {
        public static List<ArcadeSnake> snakes;
        SpriteBatch spriteBatch;
        GameMain gm;
        
        public Arcade(Game game) : base(game)
        {
            gm = GameMain.Current;
        }

        public override void Initialize()
        {
            snakes = new List<ArcadeSnake>();

            // TODO: Multiplayer
            snakes.Add(new ArcadeSnake(gm, 0));
            snakes[0].apple.Generate();
            //gm.Components.Add(snakes[0]);
            Console.WriteLine(snakes[0].apple.rectangle.X);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Snake snake in snakes)
            {
                string text = string.Format("P{0}: {1}", snake.player + 1, snake.score);
                Vector2 textSize = gm.fontMain.MeasureString(text);
                Vector2 scoreLocation = new Vector2();

                switch (snake.player)
                {
                    case 0:
                        scoreLocation = new Vector2(50, 50);
                        break;
                    case 1:
                        scoreLocation = new Vector2(gm.settings.width - 50 - textSize.X, 50);
                        break;
                    case 2:
                        scoreLocation = new Vector2(50, gm.settings.height - 50 - textSize.Y);
                        break;
                    case 3:
                        scoreLocation = new Vector2(gm.settings.width - 50 - textSize.X, gm.settings.height - 50 - textSize.Y);
                        break;
                }

                spriteBatch.DrawString(gm.fontMain, text, scoreLocation, snake.headColor);
            }

            for (int i = 0; i <= gm.settings.width; i += snakes[0].headSize)
            {
                spriteBatch.Draw(gm.pixel, new Rectangle(i, 0, snakes[0].headSize, snakes[0].headSize), snakes[0].headColor);
            }
            for (int i = 0; i <= gm.settings.width; i += snakes[0].headSize)
            {
                spriteBatch.Draw(gm.pixel, new Rectangle(i, (int)gm.settings.height - snakes[0].headSize, snakes[0].headSize, snakes[0].headSize), snakes[0].headColor);
            }
            for (int i = 0; i <= gm.settings.height; i += snakes[0].headSize)
            {
                spriteBatch.Draw(gm.pixel, new Rectangle(0, i, snakes[0].headSize, snakes[0].headSize), snakes[0].headColor);
            }
            for (int i = 0; i <= gm.settings.height; i += snakes[0].headSize)
            {
                spriteBatch.Draw(gm.pixel, new Rectangle((int)gm.settings.width - snakes[0].headSize, i, snakes[0].headSize, snakes[0].headSize), snakes[0].headColor);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
