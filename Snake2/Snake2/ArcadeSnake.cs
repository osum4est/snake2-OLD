using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snake2
{
    public class ArcadeSnake : Snake
    {
        public ObjectApple apple;

        public ArcadeSnake(Game game, int p) : base(game, p)
        {
            apple = new ObjectApple(this);
            
        }

        public override bool CollisionCheck()
        {
            CollisionSelf();
            CollisionSides(headSize);
            return CollisionObjects();
        }

        public override Direction CollisionSides(int buffer = 0, int offsetX = 1, int offsetY = 1)
        {
            Direction d = base.CollisionSides(buffer);

            switch (d)
            {
                case Direction.Left:
                    rectangle.X = (int)gm.settings.width - buffer - headSize;
                    break;
                case Direction.Right:
                    rectangle.X = buffer;
                    break;
                case Direction.Up:
                    rectangle.Y = (int)gm.settings.height - buffer - headSize;
                    break;
                case Direction.Down:
                    rectangle.Y = buffer;
                    break;
                default:
                    return Direction.None;
            }
            return Direction.None;
        }

        public override void SetHeadPos()
        {
            bool flag = false;
            do
            {
                snakeRandom = new Random(Guid.NewGuid().GetHashCode());
                rectangle = new Rectangle(snakeRandom.Next(5, (int)gm.settings.width / 2 / headSize) * headSize, snakeRandom.Next(0, (int)gm.settings.height / headSize) * headSize, headSize, headSize);
                foreach (ArcadeSnake snake in Arcade.snakes)
                {
                    if (snake != this && rectangle.Intersects(snake.rectangle))
                        flag = true;
                }
            } while (flag);
        }

        public override void SnakeCollision(){}
    }
}
