using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    public delegate void ClickEventHandler();
    class Button : DrawableGameComponent
    {
        public event ClickEventHandler Click;

        public static List<Button> buttons = new List<Button>();
        public InputHandler input;

        string text;
        int x, y, width, height;
        Rectangle rect;
        bool selected;
        Color defaultColor, selectedColor;
        SpriteFont font;
        SpriteBatch spriteBatch;

        public Button(Game game, ClickEventHandler Click, string text, int x, int y, SpriteFont font, Color? defaultColor = null, Color? selectedColor = null) : base (game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.text = text;
            this.x = (int)(x - font.MeasureString(text).X / 2);
            this.y = (int)(y - font.MeasureString(text).Y / 2);
            this.width = (int)font.MeasureString(text).X;
            this.height = (int)font.MeasureString(text).Y;
            this.font = font;

            if (defaultColor == null)
                defaultColor = Color.Black;
            if (selectedColor == null)
                selectedColor = Color.Blue;
            this.defaultColor = (Color)defaultColor;
            this.selectedColor = (Color)selectedColor;

            rect = new Rectangle(this.x, this.y, this.width, this.height);
            selected = false;

            buttons.Add(this);
            input = new InputHandler();

            this.Click = Click;
        }

        public void Delete()
        {
            buttons.Remove(this);
        }

        public static void DeleteAll()
        {
            for (int i = 0; i < GameMain.Current.Components.Count; i++ )
            {
                if (GameMain.Current.Components[i] is Button)
                    (GameMain.Current.Components[i] as DrawableGameComponent).Dispose();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (input.MouseIntersects(rect))
                selected = true;
            else
                selected = false;

            if (input.MouseIntersectsClick(rect))
                Click();

            input.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < buttons.Count; i++)
            {
                try
                {
                    Color color;
                    if (buttons[i].selected)
                        color = buttons[i].selectedColor;
                    else
                        color = buttons[i].defaultColor;

                    spriteBatch.DrawString(buttons[i].font, buttons[i].text, new Vector2(buttons[i].rect.X, buttons[i].rect.Y), color);
                }
                catch
                {
                    break;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
