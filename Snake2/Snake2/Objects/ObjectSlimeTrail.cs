using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class ObjectSlimeTrail : BaseGameEnemy
    {
        public ObjectSlimeTrail(Vector2 position, Vector2 level) {
            this.position.X = position.X;
            this.position.Y = position.Y;
            this.level = level; }

        public override void Initialize()
        {
            testForSnakeCollision = true;
            testForSwordCollision = true;
            castsShadow = false;
            Sprite = Sprites.slimetrail;
            base.Initialize();
        }

        public override void SnakeCollision()
        {
            HurtSnake(1);
        }
    }
}
