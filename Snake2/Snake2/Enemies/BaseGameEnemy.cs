using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snake2
{
    public abstract class BaseGameEnemy : BaseGameObject
    {
        public abstract override void SnakeCollision();

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public void HurtSnake(int damage)
        {
            Adventure.Current.snake.Pause(1);
            Adventure.Current.snake.Length -= damage;
        }
    }
}
