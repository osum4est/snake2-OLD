using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snake2
{
    public class ObjectFallingRock : BaseGameObject
    {
        float speed = 0.1f;

        public ObjectFallingRock() : base ()
        {

        }

        public override void Initialize()
        {
            testForSnakeCollision = true;
            width = 16;
            height = 16;
            texture = gm.pixel;
            color = Color.RosyBrown;
            base.Initialize();
        }

        public override void SnakeCollision()
        {
            gm.ChangeGameState(GameState.GameOver);
        }

        public override void Update(GameTime gameTime)
        {
            if (position.Y + rectangle.Height < levelBounds.Y + levelBounds.Height)
                position.Y += speed;
            base.Update(gameTime);
        }
    }
}
