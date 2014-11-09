using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake2
{
    class MainMenu : DrawableGameComponent
    {
        static Button btnArcade;
        static Button btnAdventure;
        static Button btnOptions;
        static Button btnQuit;
        GameMain gm;
        Game game;

        public MainMenu(Game game) : base(game)
        {
            gm = GameMain.Current;
            this.game = game;
        }

        public override void Initialize()
        {
            gm.Components.Add(btnArcade = new Button(game, new ClickEventHandler(btnArcade_Click), "Arcade", gm.settings.width / 5, gm.settings.height - 200, gm.fontMain));
            gm.Components.Add(btnAdventure = new Button(game, new ClickEventHandler(btnAdventure_Click), "Adventure", gm.settings.width / 5 * 2, gm.settings.height - 200, gm.fontMain));
            gm.Components.Add(btnOptions = new Button(game, new ClickEventHandler(btnOptions_Click), "Options", gm.settings.width / 5 * 3, gm.settings.height - 200, gm.fontMain));
            gm.Components.Add(btnQuit = new Button(game, new ClickEventHandler(btnQuit_Click), "Quit", gm.settings.width / 5 * 4, gm.settings.height - 200, gm.fontMain));

            gm.IsMouseVisible = true;

            base.Initialize();
        }

        public void btnArcade_Click()
        {
            gm.ChangeGameState(GameState.Arcade); 
            Close();
        }

        public void btnAdventure_Click()
        {
            gm.ChangeGameState(GameState.AdventureWorld);
            Close();
        }

        public void btnOptions_Click()
        {
            Console.WriteLine("You clicked options. That's not implemented yet");
        }

        public void btnQuit_Click()
        {
            gm.exitGame = true;
        }

        public void Close()
        {
            Button.DeleteAll();
            Mouse.WindowHandle = gm.Window.Handle;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
