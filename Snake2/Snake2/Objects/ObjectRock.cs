using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class ObjectRock : BaseGameObject
    {
        public ObjectRock() : base() { }

        // TODO: Custom adding of objects, for falling rock delay

        public override void SnakeCollision()
        {
            gm.ChangeGameState(GameState.GameOver);
        }

        public override void Initialize()
        {
            testForSwordCollision = true;
            testForSnakeCollision = true;
            castsShadow = true;
            
            Sprite = Sprites.rock[spriteType];

            base.Initialize();
        }
    }
}
