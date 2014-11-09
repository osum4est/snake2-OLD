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


public class FrameRateCounter : DrawableGameComponent
{
    ContentManager content;
    SpriteBatch spriteBatch;
    SpriteFont spriteFont;

    int frameRate = 0;
    int frameCounter = 0;
    TimeSpan elapsedTime = TimeSpan.Zero;

    public bool show = true;


    public FrameRateCounter(Game game, ContentManager setContent)
        : base(game)
    {
        content = setContent;
    }


    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteFont = content.Load<SpriteFont>("fpsFont");
    }


    protected override void UnloadContent()
    {
        content.Unload();
    }


    public override void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime;

        if (elapsedTime > TimeSpan.FromSeconds(1))
        {
            elapsedTime -= TimeSpan.FromSeconds(1);
            frameRate = frameCounter;
            frameCounter = 0;
        }
    }


    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont spriteFont)
    {
        frameCounter++;
        if (!show) return;

        string fps = string.Format("{0}", frameRate);

        //spriteBatch.Begin();

        spriteBatch.DrawString(spriteFont, fps, new Vector2(1200, 5), Color.Black);
        //spriteBatch.DrawString(spriteFont, fps, new Vector2(2, 2), Color.White);

        //spriteBatch.End();
    }
}