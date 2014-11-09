using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Snake2
{
    public abstract class BaseGameObject : DrawableGameComponent
    {
        // TODO: Make snake parts this
        public Color color { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Vector2 position;

        private Sprite sprite;
        public Sprite Sprite { get { return sprite; } 
            set 
            { 
                sprite = value; 
                texture = sprite.texture; 
                colorData = sprite.colorData; 
                width = sprite.width;
                height = sprite.height;
                color = Color.White;
                sourceRectangle = sprite.sourceRectangle;
                rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            }
        }

        public Texture2D texture { get; set; }
        public char chr { get; set; }
        public Rectangle rectangle;
        public static GameMain gm;
        public Random rnd { get; set; }
        public static SpriteBatch spriteBatch;
        public Vector2 level { get; set; }
        public Rectangle levelBounds { get; set; }
        public float rotation { get; set; }
        public Vector2 origin { get; set; }
        public bool testForSnakeCollision { get; set; }
        public bool testForSwordCollision { get; set; }
        public bool stopsSword { get; set; }

        public float scale = 1f;
        public Matrix transform;
        public Color[] colorData;

        Rectangle sourceRectangle;

        // TODO: Make this not so ugly
        static bool f = true;

        public BaseGameObject() : base(GameMain.Current)
        {
            rotation = 0;
            stopsSword = true;
            origin = Vector2.Zero;
            gm = GameMain.Current;
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            sourceRectangle = new Rectangle(0, 0, width, height);
            rnd = new Random();

            if (f)
            {
                spriteBatch = new SpriteBatch(GameMain.Current.GraphicsDevice);
                f = false;
            }
        }

        public static void CreateObjectFromChar(char c, int x, int y, Vector2 level, Adventure adventure)
        {
            BaseGameObject o;
            switch (c)
            {
                case ObjectChars.ROCK:
                    o = new ObjectRock();
                    break;
                case ObjectChars.APPLE:
                    o = new ObjectApple(Adventure.Current.snake);
                    break;
                case ObjectChars.FALLINGROCK:
                    o = new ObjectFallingRock();
                    break;
                case ObjectChars.COIN:
                    o = new ObjectCoin();
                    break;
                case ObjectChars.SLIME:
                    o = new EnemySlime();
                    break;
                default:
                    o = null;
                    break;
            }

            o.level = level;
            o.levelBounds = new Rectangle(gm.settings.width * (int)level.X, gm.settings.height * (int)level.Y, gm.settings.width, gm.settings.height);
            o.position = new Vector2(x * 32 + o.levelBounds.X, y * 32 + o.levelBounds.Y);
            adventure.objects.Add(o);
            gm.Components.Add(o);
        }

        public abstract void SnakeCollision();

        public virtual void HitBySword() { }

        public override void Initialize()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            base.Initialize();
        }

        public void Delete()
        {
            Adventure.Current.objects.Remove(this);
            this.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            if (Sprite != null && Sprite is AnimatedSprite)
            {
                (Sprite as AnimatedSprite).Update(gameTime);
                sourceRectangle = Sprite.sourceRectangle;
            }

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;

            transform = new Matrix();
            transform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                        Matrix.CreateScale(scale) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateTranslation(new Vector3(position, 0.0f));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //CHANGE ME//
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
            gm.superAwesome.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(texture, rectangle, sourceRectangle, color, rotation, origin, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
