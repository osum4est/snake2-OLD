using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Resources;

namespace Snake2
{
    public abstract class Snake : BaseGameObject
    {
        public int player;
        
        public int timer = 0;

        Rectangle testHead;
        public List<Rectangle> snakeBody;

        public Color headColor;
        public Color bodyColor;
        public Color appleColor;

        public int speed;
        public int headSize;
        public int bodySize;
        public int appleSize;
        
        public int increaseLength;
        public ControlMethod controlMethod;

        public Direction direction;

        public InputHandler input;

        public int score;

        private int length;
        public int Length
        {
            get { return length; }
            set
            {
                if (value < length)
                    removeBody = length - value;
                else if (value > length)
                    addBody = true;
                length = value;   
            }
        }

        public bool addBody;
        public int removeBody;

        Vector2 movement;

        //public bool instantDeath;

        bool border;

        public Random snakeRandom;

        public Keys[] keys;
        bool dontMove = false;

        public Snake(Game game, int player) : base()
        {
            Sprite = Sprites.pixel;
            width = 16;
            height = 16;
            castsShadow = true;
            testHead = new Rectangle();
            gm = GameMain.Current;
            this.player = player;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);


            // TODO: Apple Color??
            headColor = gm.data.snakeProps[player].headColor;
            bodyColor = gm.data.snakeProps[player].bodyColor;
            appleColor = gm.data.snakeProps[player].appleColor;
            

            speed = gm.data.snakeProps[player].speed;
            headSize = gm.data.snakeProps[player].headSize;
            bodySize = gm.data.snakeProps[player].bodySize;
            appleSize = gm.data.snakeProps[player].appleSize;
            length = gm.data.snakeProps[player].startingLength;
            increaseLength = gm.data.snakeProps[player].increaseLength;
            border = gm.data.snakeProps[player].borders;
            controlMethod = (ControlMethod)Enum.Parse(typeof(ControlMethod), gm.data.snakeProps[player].controlMethod);

            snakeRandom = new Random();

            GameMain.KeysAttribute a = (GameMain.KeysAttribute)typeof(ControlMethod).GetMember(controlMethod.ToString())[0].GetCustomAttributes(true)[0];
            keys = a.GetKeys();

            foreach (Keys k in keys)
                Console.WriteLine(k);
            SetHeadPos();
            snakeBody = new List<Rectangle>();
            for (int i = 0; i < length; i++)
                snakeBody.Add(new Rectangle(rectangle.X, rectangle.Y, bodySize, bodySize));


            position.X += 16;
            direction = Direction.Right;

            input = new InputHandler();

                    
        }

        public abstract void SetHeadPos();
        public abstract bool CollisionCheck();

        public override void Update(GameTime gameTime)
        {
            // TODO Check input for frequently and fix
            InputCheck();
            
            if (timer >= speed)
            {
                if (addBody)
                {
                    for (int i = 0; i < increaseLength; i++)
                        snakeBody.Add(new Rectangle(snakeBody[length - increaseLength - 1].X, snakeBody[length - increaseLength - 1].Y, bodySize, bodySize));
                    addBody = false;
                }

                if (removeBody > 0)
                {
                    for (int i = 0; i < removeBody; i++)
			        {

                        try
                        {
                            snakeBody.RemoveAt(length + removeBody - i - 1);
                        }
                        catch
                        {
                            gm.ChangeGameState(GameState.GameOver);
                        }
			        }
                    removeBody = 0;
                }

                movement = Vector2.Zero;
                switch (direction)
                {
                    case Direction.Up:
                        movement.Y = -headSize;
                        break;
                    case Direction.Down:
                        movement.Y = headSize;
                        break;
                    case Direction.Left:
                        movement.X = -headSize;
                        break;
                    case Direction.Right:
                        movement.X = headSize;
                        break;
                }

                if (!CollisionCheck())
                {
                    for (int i = length - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            Point p = rectangle.Location;
                            snakeBody[i] = rectangle;
                        }
                        else
                        {
                            snakeBody[i] = snakeBody[i - 1];
                        }
                    }

                    position.X += (int)movement.X;
                    position.Y += (int)movement.Y;
                }

                timer = 0;

                
            }
            else
            {
                timer++;
            }

            base.Update(gameTime);
        }

        public virtual bool CollisionObjects(Vector2? sLevel = null)
        {
            if (sLevel == null)
                sLevel = Vector2.Zero;
            List<BaseGameObject> objs = gm.Components.OfType<BaseGameObject>().ToList<BaseGameObject>();

            testHead = rectangle;
            testHead.X += (int)movement.X;
            testHead.Y += (int)movement.Y;

            for (int i = 0; i < objs.Count(); i++)
            {
                if(objs[i].testForSnakeCollision && objs[i].level == sLevel)
                    if (testHead.Intersects(objs.ElementAt(i).rectangle))
                    {
                        objs.ElementAt(i).SnakeCollision();
                        return true;
                    }
            }

            return false;
        }

        public void Pause(int time)
        {
            timer -= speed * time;
        }

        public void CollisionOtherSnake()
        {
            foreach (ArcadeSnake snake in Arcade.snakes)
            {
                for (int i = 0; i < snake.length; i++)
                    if (rectangle.Intersects(snake.snakeBody[i]))
                    {
                        GameMain.MessageBox(new IntPtr(0), string.Format("Your score was {0}", score), "Game Over!", 0);
                        gm.exitGame = true;
                    }
            }
        }

        public void CollisionSelf()
        {
            for (int i = 1; i < length; i++)
                    if (rectangle.Intersects(snakeBody[i]))
                    {
                        gm.ChangeGameState(GameState.GameOver);
                    }
        }

        public virtual Direction CollisionSides(int buffer = 0, int offsetX = 1, int offsetY = 1)
        {
            if (position.X < (int)gm.settings.width * (offsetX - 1) + buffer)
                return Direction.Left;
            else if (position.X >= (int)gm.settings.width * offsetX - buffer)
                return Direction.Right;
            else if (position.Y < (int)gm.settings.height * (offsetY - 1) + buffer)
                return Direction.Up;
            else if (position.Y >= (int)gm.settings.height * offsetY - buffer)
                return Direction.Down;
            else
                return Direction.None;
        }

           
        public virtual void InputCheck()
        {
            input.Update();

            if (input.IsPressed(keys[0]) && direction != Direction.Down)
                direction = Direction.Up;
            else if (input.IsPressed(keys[1]) && direction != Direction.Up)
                direction = Direction.Down;
            else if (input.IsPressed(keys[2]) && direction != Direction.Right)
                direction = Direction.Left;
            else if (input.IsPressed(keys[3]) && direction != Direction.Left)
                direction = Direction.Right;
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, gm.camera.transform);
            spriteBatch.Draw(gm.pixel, rectangle, headColor);

            foreach (Rectangle rect in snakeBody)
            {
                spriteBatch.Draw(gm.pixel, rect, headColor);
                spriteBatch.Draw(gm.pixel, new Rectangle(rect.X + bodySize / 4, rect.Y + bodySize / 4, bodySize / 2, bodySize / 2), appleColor);
            }

            spriteBatch.End();
        }
    }
}
