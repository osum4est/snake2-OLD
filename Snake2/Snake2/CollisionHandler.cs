using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    public static class CollisionHandler
    {
        static GameMain gm { get { return GameMain.Current; } }


        public static bool OutOfBounds(Vector2 pos, Vector2 level)
        {
            int width = gm.settings.width;
            int height = gm.settings.height;
            int x = (int)level.X * width;
            int y = (int)level.Y * height;

            if (pos.X <= x ||
                pos.Y <= y ||
                pos.X >= x + width ||
                pos.Y >= y + height)
                return true;

            return false;
        }

        public static bool PerPixelCollision(BaseGameObject objA, BaseGameObject objB)
        {
            Matrix transformAToB = objA.transform * Matrix.Invert(objB.transform);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < objA.height; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < objA.width; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < objB.width &&
                        0 <= yB && yB < objB.height)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = objA.colorData[xA + yA * objA.width];
                        Color colorB = objB.colorData[xB + yB * objB.width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }

        public static bool BoundingBoxIntersect(BaseGameObject objA, BaseGameObject objB)
        {
            Rectangle rectA = CalculateBoundingRectangle(objA);
            Rectangle rectB = CalculateBoundingRectangle(objB);

            if (rectA.Intersects(rectB))
                return true;

            return false;
        }

        public static Rectangle CalculateBoundingRectangle(BaseGameObject obj)
        {
            Rectangle r = new Rectangle(0, 0, obj.width, obj.height);
            Matrix transform = obj.transform;

            Vector2 leftTop = new Vector2(r.Left, r.Top);
            Vector2 rightTop = new Vector2(r.Right, r.Top);
            Vector2 leftBottom = new Vector2(r.Left, r.Bottom);
            Vector2 rightBottom = new Vector2(r.Right, r.Bottom);

            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);
            
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)min.X + (int)obj.origin.X, (int)min.Y + (int)obj.origin.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}
