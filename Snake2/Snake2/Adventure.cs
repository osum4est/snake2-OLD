using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;

namespace Snake2
{
    public class Adventure : DrawableGameComponent
    {
    	// HIHIHIHIHIHIHI
    	
        public RenderTarget2D rtMain;
        public RenderTarget2D rtLight;

        public static Adventure Current;
        public AdventureSnake snake;
        AdventureHUD hud;
        SpriteBatch spriteBatch;
        GameMain gm;
        List<List<LevelLibrary>> levels;
        public Vector2 currentLevel;
        public BaseLevelType levelType;
        public BaseObjectList objects;

        // in levels
        public const int worldWidth = 3;
        public const int worldHeight = 2;

        public Adventure(Game game) : base(game)
        {
            gm = GameMain.Current;   
        }

        public override void Initialize()
        {
            Current = this;
            
            levels = new List<List<LevelLibrary>>();
            objects = new BaseObjectList();
            Adventure.Current.objects.Add(snake = new AdventureSnake(gm, 0, this));
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            currentLevel = new Vector2(0, 0);
            
            LoadLevels();
            LoadObjects();

            EnableLevel();

            hud = new AdventureHUD(this, gm);
            hud.DrawOrder = 1;
            gm.Components.Add(hud);
            new BaseObjectHandler();
            gm.Components.Add(BaseObjectHandler.Current);

            var pp = gm.GraphicsDevice.PresentationParameters;
            rtMain = new RenderTarget2D(gm.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            rtLight = new RenderTarget2D(gm.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

            base.Initialize();
        }

        private void LoadLevels()
	    {
            Console.WriteLine("Loaded levels");
            for (int x = 0; x < worldWidth; x++)
			{
                List<LevelLibrary> column = new List<LevelLibrary>();
                for (int y = 0; y < worldHeight; y++)
                {
                    string fileName = string.Format("levels/{0}-{1}", x, y);
                    column.Add(gm.Content.Load<LevelLibrary>(fileName));
                }
                levels.Add(column);
			}
        }

        public void ChangeLevel(Direction d)
        {
            Console.WriteLine("You are going {0}", d);
            switch(d)
            {
                case Direction.Up:
                    currentLevel.Y -= 1;
                    break;
                case Direction.Down:
                    currentLevel.Y += 1;
                    break;
                case Direction.Right:
                    currentLevel.X += 1;
                    break;
                case Direction.Left:
                    currentLevel.X -= 1;
                    break;
            }
            EnableLevel();
        }

        public void LoadObjects()
        {
            for (int column = 0; column < worldWidth; column++)
            {
                for (int row = 0; row < worldHeight; row++)
                {
                    LevelLibrary level = levels[column][row];
                    for (int x = 0; x < level.columns; x++)
                    {
                        for (int y = 0; y < level.rows; y++)
                        {
                            foreach (var v in typeof(ObjectChars).GetFields())
                            {
                                char c = (char)v.GetValue(typeof(ObjectChars));

                                if (level.GetChar(y, x) == c)
                                {
                                    Vector2 v2 = new Vector2(column, row);
                                    BaseGameObject.CreateObjectFromChar(c, x, y, (int)level.type, v2, this);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void EnableLevel()
        {
            foreach (BaseGameObject o in objects)
            {
                if (o.level == currentLevel)
                {
                    o.enabled = true;
                    o.visible = true;
                }
                else
                {
                    o.enabled = false;
                    o.visible = false;
                }
            }

            levelType = BaseLevelType.levelTypes[levels[(int)currentLevel.X][(int)currentLevel.Y].type];
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
