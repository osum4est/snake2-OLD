using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class Camera
    {
        public Matrix transform;
        Vector2 position;
        Viewport viewport;

        GameMain gm;

        public Camera(Viewport viewport, GameMain gm)
        {
            this.gm = gm;
            position = Vector2.Zero;
            this.viewport = viewport;
            //transform = Matrix.CreateRotationZ(1f) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(position.X, position.Y, 0);
            transform = Matrix.CreateTranslation(position.X, position.Y, 0);
        }

        public void ChangeLevel(Direction d)
        {
            switch (d)
            {
                // ADD OTHER DIRECTIONS
                // FIX VIEWPORT
                case Direction.Up:
                    position.Y += gm.settings.height;
                    break;
                case Direction.Down:
                    position.Y -= gm.settings.height;
                    break;
                case Direction.Right:
                    position.X -= gm.settings.width;
                    break;
                case Direction.Left:
                    position.X += gm.settings.width;
                    break;
            }
            transform = Matrix.CreateTranslation(position.X, position.Y, 0);
        }

    }
}
