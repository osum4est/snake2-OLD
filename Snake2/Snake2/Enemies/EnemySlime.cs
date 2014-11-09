using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class EnemySlime : BaseGameEnemy
    {
        int speed;
        int timer;
        float chanceToTurn;
        Vector2 direction;
        //Direction direction;

        public EnemySlime()
        {
            testForSnakeCollision = true;
            testForSwordCollision = true;

            Sprite = Sprites.slime;
            castsShadow = true;
            direction.X = 32;
            chanceToTurn = .25f;
            speed = 250;
            timer = 0;

            
        }

        public override void Update(GameTime gameTime)
        {
            if (timer == speed)
            {
                CreateTrail();
                rnd = new Random(Guid.NewGuid().GetHashCode());
                if (rnd.NextDouble() < chanceToTurn)
                {
                    int i = rnd.Next(1, 5);
                    switch (i)
                    {
                        case 1:
                            direction.Y = 0;
                            direction.X = 32;
                            break;
                        case 2:
                            direction.Y = 0;
                            direction.X = -32;
                            break;
                        case 3:
                            direction.Y = 32;
                            direction.X = 0;
                            break;
                        case 4:
                            direction.Y = -32;
                            direction.X = 0;
                            break;
                    }
                }

                Vector2 temp = position;
                if (!CollisionHandler.OutOfBounds(temp + direction, level))
                    position += direction;


                timer = 0;
            }
            else
                timer++;


            base.Update(gameTime);
        }
        
        public void CreateTrail()
        {
            ObjectSlimeTrail st = new ObjectSlimeTrail(position, level);
            Adventure.Current.objects.Add(st);
        }

        public override void SnakeCollision() { HurtSnake(1); }

        public override void HitBySword()
        {
            Console.WriteLine("Slime was hit");
            this.Delete();
        }
    }
}
