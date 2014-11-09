using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Snake2
{
    public abstract class BaseGameObject
    {
        public bool enabled;
        public bool visible;

        // TODO: Make snake parts this
        public Color color { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Vector2 position;

        private Sprite sprite;
        public Sprite Sprite
        {
            get { return sprite; }
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
                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                shadowTexture = sprite.texture;
                shadowRectangle = rectangle;
                shadowOrigin = new Vector2(shadowRectangle.Width / 2, shadowRectangle.Height / 2);
            }
        }

        public Texture2D texture { get; set; }
        public char chr { get; set; }
        public Rectangle rectangle;
        public static GameMain gm;
        public Random rnd { get; set; }
        public static SpriteBatch spriteBatch;
        public Vector2 level { get; set; }
        public int spriteType { get; private set; }
        public Rectangle levelBounds { get; set; }
        public float rotation { get; set; }
        public Vector2 origin { get; set; }
        public bool testForSnakeCollision { get; set; }
        public bool testForSwordCollision { get; set; }
        public bool stopsSword { get; set; }
        public bool castsShadow { get; set; }

        public float scale = 1f;
        public Matrix transform;
        public Color[] colorData;

        Rectangle sourceRectangle;


        Texture2D shadowTexture;
        Rectangle shadowRectangle;
        Vector2 shadowOrigin;

        BaseLevelType type;

        // TODO: Make this not so ugly
        static bool f = true;

        public BaseGameObject()
        {
            enabled = true;
            visible = true;

            rotation = 0;
            stopsSword = true;
            
            gm = GameMain.Current;
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            origin = new Vector2(width / 2, height / 2);
            sourceRectangle = new Rectangle(0, 0, width, height);
            rnd = new Random();
            castsShadow = false;

            if (f)
            {
                spriteBatch = new SpriteBatch(GameMain.Current.GraphicsDevice);
                f = false;
            }
        }

        public static void CreateObjectFromChar(char c, int x, int y, int spriteType, Vector2 level, Adventure adventure)
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
            o.spriteType = spriteType;
            o.levelBounds = new Rectangle(gm.settings.width * (int)level.X, gm.settings.height * (int)level.Y, gm.settings.width, gm.settings.height);
            o.position = new Vector2(x * 32 + o.levelBounds.X, y * 32 + o.levelBounds.Y);
            adventure.objects.Add(o);
        }

        public abstract void SnakeCollision();

        public virtual void HitBySword() { }

        public virtual void Initialize()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Delete()
        {
            Adventure.Current.objects.Remove(this);
        }

        

        public virtual void Update(GameTime gameTime)
        {
            if (this is Snake && shadowRectangle.Width == 32)
                Console.WriteLine();

            if (Sprite != null && Sprite is AnimatedSprite)
            {
                (Sprite as AnimatedSprite).Update(gameTime);
                sourceRectangle = Sprite.sourceRectangle;
            }

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            shadowRectangle = rectangle;

            Rectangle bounds = CollisionHandler.CalculateBoundingRectangle(this);
            origin = new Vector2(bounds.Width / 2, bounds.Height / 2);

            transform = new Matrix();
            transform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                        Matrix.CreateScale(scale) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateTranslation(new Vector3(position, 0.0f));
        }

        RasterizerState shadowRasterizer = RasterizerState.CullClockwise;
        public Matrix CreateShadow(Vector2 p)
        {
            float ax = rectangle.X + origin.X;
            float ay = rectangle.Y + origin.Y;
            float bx = p.X;
            float by = p.Y;

            int height = 26;
            int sunHeight = 100;
            double shadowLength;
            double realShadowLength;
            double distance = Math.Sqrt(Math.Pow(p.X - ax, 2) + Math.Pow(p.Y - ay, 2));
            double angle = 0;

            shadowLength = (distance * height) / sunHeight;

            Matrix m = new Matrix();
            Matrix s = Matrix.Identity;
            if ((bx - ax > 0 && by - ay < 0) || (bx - ax > 0 && by - ay > 0))
            {
                angle = -Math.Atan(Math.Abs(bx - ax) / Math.Abs(by - ay));
            }
            else
            {
                angle = Math.Atan(Math.Abs(bx - ax) / Math.Abs(by - ay));
            }

            s.M12 = (float)Math.Tan(angle);

            double O = Math.Abs(angle);
            double SL = Math.PI / 2;
            double RSL = SL - O;
            double sl = shadowLength;

            realShadowLength = (sl / Math.Sin(SL)) * Math.Sin(RSL);
            int flip;
            if (by - ay < 0)
            {
                shadowRasterizer = RasterizerState.CullClockwise;
                flip = -1;
            }
            else
            {
                shadowRasterizer = RasterizerState.CullCounterClockwise;
                flip = 1;
            }
            
            m = Matrix.CreateTranslation(-shadowRectangle.X - shadowOrigin.X, -shadowRectangle.Y - shadowOrigin.Y, 0.0f) *
                Matrix.CreateScale(1, (float)(realShadowLength / height), 1) *
                Matrix.CreateRotationZ((float)Math.PI / 2) *
                s *
                Matrix.CreateScale(flip, 1, 0) *
                Matrix.CreateRotationZ(-((float)Math.PI / 2)) *
                Matrix.CreateTranslation(new Vector3(shadowRectangle.X + shadowOrigin.X * 2, shadowRectangle.Y + shadowOrigin.Y, 0f));

            if (this is ObjectSword && level == Adventure.Current.currentLevel && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine(shadowRectangle);
            }
            return m;
        }

        public virtual void Draw()
        {
            if (castsShadow && shadowTexture != null)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, shadowRasterizer, null, CreateShadow(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)) * gm.camera.transform);
                spriteBatch.Draw(shadowTexture, shadowRectangle, sourceRectangle, new Color(0, 0, 0, 150), 0f, new Vector2(16, 16), SpriteEffects.None, 0);
                spriteBatch.End();
            }
            if (this is ObjectApple)
                Console.WriteLine();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
            spriteBatch.Draw(texture, position + origin, sourceRectangle, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
            //spriteBatch.Draw(Sprites.pixel.texture, CollisionHandler.CalculateBoundingRectangle(this), new Color(255, 0, 0, 75));
            spriteBatch.End();
        }
    }
}