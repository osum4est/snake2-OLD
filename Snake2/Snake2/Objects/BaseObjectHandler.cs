using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    class BaseObjectHandler : DrawableGameComponent
    {
        public static BaseObjectHandler Current { get; private set; }
        public Adventure a;
        public GameMain gm;

        public BaseObjectHandler() : base(GameMain.Current)
        {
            a = Adventure.Current;
            gm = GameMain.Current;
            Current = this;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < a.objects.Count; i++)
                if (a.objects[i].enabled)
                     a.objects[i].Update(gameTime);

            base.Update(gameTime);
        }

        public void DrawMain(GameTime gameTime)
        {
            gm.GraphicsDevice.SetRenderTarget(a.rtMain);
            gm.GraphicsDevice.Clear(Color.White);
            foreach (BaseGameObject obj in a.objects)
            {
                if (obj is ObjectApple)
                    Console.WriteLine();
                if (obj.visible)
                    obj.Draw();
            }

            Adventure.Current.levelType.Draw(gameTime);
            gm.GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawLight()
        {
            Texture2D back = new Texture2D(gm.GraphicsDevice, gm.Window.ClientBounds.Width, gm.Window.ClientBounds.Height);
            Color[] data = new Color[back.Width * back.Height];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            back.SetData<Color>(data);

            gm.GraphicsDevice.SetRenderTarget(a.rtLight);
            gm.GraphicsDevice.Clear(Color.White);
            gm.spriteBatch.Begin();
            gm.spriteBatch.Draw(back, new Vector2(0, 0), a.levelType.ambientLight);
            gm.spriteBatch.End();
            gm.GraphicsDevice.SetRenderTarget(null);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawMain(gameTime);
            DrawLight();
            gm.GraphicsDevice.Clear(Color.Black);

            gm.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            gm.fxLighting.Parameters["lightMask"].SetValue(a.rtLight);
            gm.fxLighting.CurrentTechnique.Passes[0].Apply();
            gm.spriteBatch.Draw(a.rtMain, new Vector2(0, 0), Color.White);
            gm.spriteBatch.End();

            //gm.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
            //gm.spriteBatch.Draw(gm.pixel, a.snake.snakeHead, a.snake.headColor);
            //foreach (Rectangle rect in a.snake.snakeBody)
            //{
            //    gm.spriteBatch.Draw(gm.pixel, rect, a.snake.headColor);
            //    gm.spriteBatch.Draw(gm.pixel, new Rectangle(rect.X + a.snake.bodySize / 4, rect.Y + a.snake.bodySize / 4, a.snake.bodySize / 2, a.snake.bodySize / 2), a.snake.appleColor);
            //}
            //gm.spriteBatch.End();

            base.Draw(gameTime);            
        }
    }
}
