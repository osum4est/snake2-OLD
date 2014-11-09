using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class AdventureSnake : Snake
    {
        // in levels

        Adventure adventure;
        int lowerSpeed;
        int increaseSpeed;

        bool shooting;
        Vector2 shootingAt;
        int shootingRate;
        int sTimer;

        float dummySwordRotation;
        Texture2D dummySwordTexture;
        Rectangle dummySwordRectangle;
        Vector2 dummySwordOrigin;
        public int swords;

        public int coins;

        public AdventureSnake(Game game, int p, Adventure adventure) : base(game, p)
        {
            this.adventure = adventure;
            

            swords = 4000;
            dummySwordTexture = Sprites.sword.texture;
            dummySwordOrigin = new Vector2(dummySwordTexture.Width / 2, dummySwordTexture.Height / 2);
            dummySwordRectangle = new Rectangle(rectangle.X, rectangle.Y, dummySwordTexture.Width, dummySwordTexture.Height);

            lowerSpeed = 4000000;
            increaseSpeed = 12;
            speed = lowerSpeed;
            shootingRate = 25;
        }

        public override void SetHeadPos()
        {
            rectangle = new Rectangle(32, 32, headSize, headSize); 
        }

        public override bool CollisionCheck()
        {
            CollisionSelf();
            CollisionSides();
            return CollisionObjects(adventure.currentLevel);
        }

        public override void InputCheck()
        {
            base.InputCheck();

            if (input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                speed = increaseSpeed;
            else if (input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                speed = 1000;
            else
                speed = lowerSpeed;

            Vector2 mouse;
            if ((mouse = input.IsMouseLeftDownCoords()) != Vector2.Zero)
            {
                shooting = true;
                shootingAt = mouse;
            }
            else
                shooting = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (swords != 0)
            {
                if (shooting)
                {
                    if (sTimer == 0)
                    {
                        ObjectSword o = new ObjectSword(rectangle.X + headSize / 2, rectangle.Y + headSize / 2, shootingAt, adventure.currentLevel);
                        adventure.objects.Add(o);
                        sTimer = shootingRate;
                        swords--;
                    }
                    else
                        sTimer--;
                }

                DummySwordUpdate();
            }

            if (sTimer != 0)
                sTimer--;

            
        }

        public void DummySwordUpdate()
        {
            if (swords != 0)
            {
                Vector2 target = input.GetMouseCoords();
                Vector2 direction = new Vector2(target.X + adventure.currentLevel.X * gm.settings.width, target.Y + adventure.currentLevel.Y * gm.settings.height) - new Vector2(rectangle.X, rectangle.Y);
                dummySwordRotation = (float)Math.Atan2(direction.X, -direction.Y);
                dummySwordRectangle.X = rectangle.X + rectangle.Width / 2;
                dummySwordRectangle.Y = rectangle.Y + rectangle.Height / 2 ;
            }
        }

        public override void Draw()
        {
            base.Draw();

            if (swords != 0)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
                spriteBatch.Draw(dummySwordTexture, dummySwordRectangle, null, Color.White, dummySwordRotation, dummySwordOrigin, SpriteEffects.None, 0);
                spriteBatch.End();
            }  
        }

        public override Direction CollisionSides(int x = 0, int offsetX = 1, int offsetY = 1)
        {
            if (base.CollisionSides() == Direction.Up ||
                base.CollisionSides() == Direction.Left ||
                base.CollisionSides(offsetY: Adventure.worldHeight) == Direction.Down ||
                base.CollisionSides(offsetX: Adventure.worldWidth) == Direction.Right)
                gm.ChangeGameState(GameState.GameOver);

            Direction d = base.CollisionSides(offsetX: (int)adventure.currentLevel.X + 1, offsetY: (int)adventure.currentLevel.Y + 1);

            if (d != Direction.None)
            {
                adventure.ChangeLevel(d);
                gm.camera.ChangeLevel(d);
            }
            
            return Direction.None; ;
        }

        public override void SnakeCollision(){}
    }
}
