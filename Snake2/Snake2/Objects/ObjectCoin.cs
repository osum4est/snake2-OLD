using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class ObjectCoin : BaseGameObject
    {
        public ObjectCoin() : base() { }

        public override void Initialize()
        {
            testForSnakeCollision = true;
            testForSwordCollision = true;
            stopsSword = false;
            castsShadow = true;
            Sprite = Sprites.coin;
            width = texture.Width;
            height = texture.Height;
            color = Color.White;

            base.Initialize();
        }

        public override void HitBySword()
        {
            Collect();
            base.HitBySword();
        }

        public override void SnakeCollision()
        {
            Collect();
        }

        public void Collect()
        {
            Adventure.Current.snake.coins++;
            Delete();
        }
    }
}
