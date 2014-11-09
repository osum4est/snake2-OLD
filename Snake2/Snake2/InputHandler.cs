using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Snake2
{
    public class InputHandler : GameMain
    {
        KeyboardState prevKeystate, keystate;
        public MouseState prevMouseState, mouseState;

        public void Update()
        {
            prevKeystate = keystate;
            keystate = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public Vector2 GetMouseCoords()
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public bool IsPressed(Keys key)
        {
            if (keystate.IsKeyDown(key) && prevKeystate.IsKeyUp(key))
                return true;
            return false;
        }

        public bool IsUnpressed(Keys key)
        {
            if (keystate.IsKeyUp(key) && prevKeystate.IsKeyDown(key))
                return true;
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return keystate.IsKeyDown(key);
        }

        public bool IsMouseLeftDown()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public Vector2 IsMouseLeftDownCoords()
        {
            if (IsMouseLeftDown())
                return new Vector2(mouseState.X, mouseState.Y);
            return Vector2.Zero;
        }

        public Vector2 IsMouseLeftClickedCoords()
        {
            if (IsMouseLeftClicked())
                return new Vector2(mouseState.X, mouseState.Y);
            return Vector2.Zero;
        }

        public bool IsMouseLeftClicked()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed)
                return true;
            return false;
        }

        public bool MouseIntersectsClick(Rectangle rect)
        {
            if (MouseIntersects(rect))
            {
                if (IsMouseLeftClicked())
                {
                    return true;
                }
            }

            return false;         
        }

        public bool MouseIntersects(Rectangle rect)
        {
            if (rect.Contains(new Point(mouseState.X, mouseState.Y)))
                return true;
            else
                return false;

        }
    }
}
