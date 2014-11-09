using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class ObjectSword : BaseGameObject
    {
        float speed = 2.5f;
        bool stopped;
        Vector2 direction;
        Adventure adventure;

        

        public ObjectSword(int x, int y, Vector2 target, Vector2 level)
        {
            adventure = Adventure.Current;
            testForSnakeCollision = true;
            Console.WriteLine(Adventure.Current.objects.Count);
            Console.WriteLine(gm.Components.Count);
            this.level = level;
            position.X = x;
            position.Y = y;
            width = 16;
            height = 32;
            Sprite = Sprites.sword;
            color = Color.White;
            origin = new Vector2(width / 2, height / 2);
            direction = target + new Vector2(level.X * gm.settings.width, level.Y * gm.settings.height) - position;
            direction.Normalize();

            rotation = (float)Math.Atan2(direction.X, -direction.Y); 
        }

        public override void Update(GameTime gameTime)
        {
            if (!stopped)
            {
                position += direction * speed;

                // TODO: Fix bounded box when collided with sides

                var objs = adventure.objects;

                for (int i = 0; i < objs.Count; i++)
                {
                    if (objs[i].level == level)
                    {  
                        if (objs[i].testForSwordCollision)
                        {
                            if (CollisionHandler.BoundingBoxIntersect(this, objs[i]))
                            {
                                //Console.WriteLine("Bounding boxes hit");
                                if (CollisionHandler.PerPixelCollision(this, objs[i]))
                                {
                                    objs[i].HitBySword();
                                    if (objs[i].stopsSword)
                                        stopped = true;
                                }
                            }
                        }
                    }
                }

                if (position.X - origin.X <= gm.settings.width * level.X || 
                    position.X + origin.X >= gm.settings.width * (level.X + 1) || 
                    position.Y - origin.Y <= gm.settings.height * level.Y || 
                    position.Y + origin.Y >= gm.settings.height * (level.Y + 1))
                    stopped = true;
            }

            base.Update(gameTime);
        }

        public override void SnakeCollision()
        {
            if (stopped)
            {
                this.Delete();
                adventure.snake.swords++;
            }
        }
    }
}
