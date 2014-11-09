using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Snake2
{
    public static class Sprites
    {
        public static Sprite pixel;
        public static Sprite coin;
        public static Sprite sword;
        public static Sprite[] rock;

        public static Sprite slime;
        public static Sprite slimetrail;

        static ContentManager c;

        static public void Load(GameMain gm)
        {
            c = gm.Content;
            Console.WriteLine(c.RootDirectory);

            pixel = new Sprite(new Texture2D(GameMain.Current.GraphicsDevice, 32, 32));
            pixel.colorData = new Color[32 * 32];
            for (int i = 0; i < pixel.colorData.Count(); i++)
			{
			    pixel.colorData[i] = Color.White;
			}
            pixel.texture.SetData(pixel.colorData);

            coin = new Sprite(c.Load<Texture2D>("Sprites/Coin"));
            sword = new Sprite(c.Load<Texture2D>("Sprites/Sword"));
            rock = new Sprite[] 
            { 
                new Sprite(c.Load<Texture2D>("Sprites/rock"), 0, 2),
                new Sprite(c.Load<Texture2D>("Sprites/rock"), 1, 2)
            };

            slime = new AnimatedSprite(c.Load<Texture2D>("Sprites/slime"), 1, 2, 150);
            slimetrail = new Sprite(c.Load<Texture2D>("Sprites/newslimetrail"));
        }

        
    }

    public class Sprite
    {
        public Rectangle sourceRectangle;
        public Texture2D texture;
        public Color[] colorData;

        public int width;
        public int height;

        public Sprite(Texture2D texture, int type = 0, int totalTypes = 1)
        {
            
            width = texture.Width;
            height = texture.Height / totalTypes;

            Texture2D newTexture = new Texture2D(GameMain.Current.GraphicsDevice, width, height);

            
            sourceRectangle = new Rectangle(0, height * type, width, height);

            colorData = new Color[width * height];
            texture.GetData(0, sourceRectangle, colorData, 0, colorData.Length);

            newTexture.SetData(colorData);
            this.texture = newTexture;
            sourceRectangle = new Rectangle(0, 0, width, height);
        }
    }

    public class AnimatedSprite : Sprite
    {
        int currentX;
        int currentY;
        int rows;
        int columns;
        int speed;
        int counter;

        public AnimatedSprite(Texture2D texture, int rows, int columns, int speed) : base (texture)
        {
            this.rows = rows;
            this.columns = columns;
            this.speed = speed;
            width = texture.Width / columns;
            height = texture.Height / rows;
            sourceRectangle = new Rectangle(0, 0, width, height);
        }

        public void Update(GameTime gameTime)
        {
            if (counter == speed)
            {
                if (currentY < rows - 1)
                    currentY++;
                else
                {
                    currentY = 0;
                    if (currentX < columns - 1)
                        currentX++;
                    else
                        currentX = 0;
                }

                sourceRectangle.X = currentX * width;
                sourceRectangle.Y = currentY * height;
                counter = 0;
            }
            else
                counter++;
        }
    }
}
