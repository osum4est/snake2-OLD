using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    public class ObjectApple : BaseGameObject
    {
        Snake snake;

        public ObjectApple(Snake snake = null) : base()
        {
            this.snake = snake;
        }

        public override void Initialize()
        {
            Sprite = Sprites.pixel;
            testForSnakeCollision = true;
            castsShadow = true;
            color = Color.Red;
            width = 16;
            height = 16;
            
            base.Initialize();
        }

        public override void SnakeCollision()
        {
            Eat();
        }

        public void Eat()
        {
            if (snake is ArcadeSnake)
                Generate();
            else
            {
                Delete();
            }
            snake.Length += snake.increaseLength;
            snake.addBody = true;
            snake.score++;
        }

        public void Generate()
        {
            bool flag = false;
            do
            {
                rnd = new Random(Guid.NewGuid().GetHashCode());
                // TODO: Fix for apple gen w/o borders
                rectangle = new Rectangle(rnd.Next(snake.headSize, (int)(gm.settings.width - snake.headSize) / width) * width, rnd.Next(snake.headSize, (int)(gm.settings.height - snake.headSize) / height) * height, width, height);

                foreach (ArcadeSnake aSnake in Arcade.snakes)
                {
                    foreach (Rectangle rect in aSnake.snakeBody)
                        if (rectangle.Intersects(rect))
                            flag = true;
                    if (rectangle.Intersects(aSnake.rectangle))
                        flag = true;
                    if (aSnake.apple != this && rectangle.Intersects(aSnake.apple.rectangle))
                        flag = true;
                }

            } while (flag);

            position = new Vector2(rectangle.X, rectangle.Y);
        }
    }
}
