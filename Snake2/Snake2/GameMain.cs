using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Resources;

namespace Snake2
{
    public enum GameState { MainMenu, Options, GameOver, Arcade, AdventureWorld };
    public enum Direction { Up, Down, Left, Right, None };
    public enum ControlMethod
    {
        [GameMain.KeysAttribute(Keys.Up, Keys.Down, Keys.Left, Keys.Right)]
        KeyboardArrows,
        [GameMain.KeysAttribute(Keys.W, Keys.S, Keys.A, Keys.D)]
        KeyboardWASD,
        [GameMain.KeysAttribute(Keys.Up, Keys.Down, Keys.Left, Keys.Right)]
        GamePad
    };

    public class GameMain : Microsoft.Xna.Framework.Game
    {
        // TODO: Fix xml settings
        // TODO: MENUS: Customize, Settings, Achievements/Unlocks
        // TODO: CUSTOMIZATION: Unlock colors, patterns, arcade modes
        // TODO: ARCADE: 1 - 4 people, play unlocked modes
        // TODO: Adventure: Open world, Twin Stick shooter!!

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public static GameMain Current { get; private set; }

        public bool exitGame;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public  SettingsLibrary settings;
        public DataLibrary data;
        StorageHandler storageHandler;

        public GameState gameState { get; private set; }
        
        public Texture2D pixel;

        public SpriteFont fontMain;

        FrameRateCounter fps;

        Arcade arcade;
        MainMenu mainMenu;
        
        Adventure adventure;

        public Camera camera;

        public Effect fxLighting;

        public GameMain()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = false;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 500.0f);
        }

        protected override void Initialize()
        {
            Current = this;
            Mouse.WindowHandle = Window.Handle;
            

            storageHandler = new StorageHandler();
            fps = new FrameRateCounter(this, Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            camera = new Camera(GraphicsDevice.Viewport, this);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            settings = storageHandler.Load<SettingsLibrary>("Settings.settings");
            data = storageHandler.Load<DataLibrary>("Data.sav");
            fontMain = Content.Load<SpriteFont>("Font");
            fxLighting = Content.Load<Effect>("Effects/lighting");

            Sprites.Load(this);
            
            pixel.SetData<Color>(new Color[] { Color.White });
            exitGame = false;
            ChangeGameState(GameState.MainMenu);

            graphics.PreferredBackBufferHeight = settings.height;
            graphics.PreferredBackBufferWidth = settings.width;
            graphics.ApplyChanges();

            Mouse.WindowHandle = Window.Handle;
        }

        protected override void UnloadContent()
        {
            storageHandler.Save<SettingsLibrary>("Settings.settings", settings);
            storageHandler.Save<DataLibrary>("Data.sav", data);
        }

        protected override void Update(GameTime gameTime)
        {
            if (exitGame)
                this.Exit();

            fps.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                ChangeGameState(GameState.GameOver);           
                 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleTurquoise);

            spriteBatch.Begin();

            fps.Draw(gameTime, spriteBatch, fontMain);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangeGameState(GameState gameState)
        {
            // TODO: Dispose compenents??

            Components.Clear();

            switch (gameState)
            {
                case GameState.MainMenu:
                    // TODO: Don't hard-call init
                    Components.Add(mainMenu = new MainMenu(this));
                    mainMenu.Initialize();
                    break;
                case GameState.GameOver:
                    // TODO: Actual game over
                    MessageBox(new IntPtr(0), "Game OVER!!", "Game Over...", 0);
                    this.Exit();
                    break;
                case GameState.Arcade:
                    Components.Add(arcade = new Arcade(this));
                    break;
                case GameState.AdventureWorld:
                    Components.Add(adventure = new Adventure(this));
                    break;
            }
        }

        public class KeysAttribute : Attribute
        {
            public Keys up { get; set;}
            public Keys down;
            public Keys left;
            public Keys right;

            public KeysAttribute(Keys up, Keys down, Keys left, Keys right)
            {
                this.up = up;
                this.down = down;
                this.left = left;
                this.right = right;
            }

            public Keys[] GetKeys()
            {
                return new Keys[] { up, down, left, right };
            }
        }
    }
}
