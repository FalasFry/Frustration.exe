using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Frustration
{

    public class Player
    {
        #region Structs

        public float speed = 5, rotation = 0, ammo = 20, hp = 5;
        public bool dead = false, normalDiff = false;

        #endregion

        #region Classes

        public Vector2 dir, position, offset;
        public Texture2D texture;
        public Rectangle rectangle;
        Game1 game1;

        #endregion

        public Player(Texture2D Texture, Game1 game)
        {
            game1 = game;
            texture = Texture;
            offset = ((texture.Bounds.Size.ToVector2() * 0.5f));
            position = new Vector2(50,300);
            rectangle = new Rectangle((offset).ToPoint(), (texture.Bounds.Size.ToVector2()).ToPoint());
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            rectangle.Location = (position).ToPoint();

            if (hp <= 0)
            {
                dead = true;
            }
            // Makes it so you can't move outside the screen.
            #region WorldWalls
            if (position.X < 0)
            {
                position.X += speed;
            }
            if (position.X + texture.Width > 800)
            {
                position.X -= speed;
            }
            if (position.Y < 0)
            {
                position.Y += speed;
            }
            if (position.Y + texture.Height > 480)
            {
                position.Y -= speed;
            }
            #endregion

            // Changes control scheme based on difficulty.
            #region Controls

            if (normalDiff)
            {
                MovementNormal();
            }
            else
            {
                MovementHard();
            }

            #endregion
        }

        // Makes it so you move with wasd.
        public void MovementNormal()
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 dir = new Vector2();

            if (keyState.IsKeyDown(Keys.A))
            {
                dir.X = -1;

            }
            if (keyState.IsKeyDown(Keys.D))
            {
                dir.X = 1;

            }
            if (keyState.IsKeyDown(Keys.W))
            {
                dir.Y = -1;

            }
            if (keyState.IsKeyDown(Keys.S))
            {
                dir.Y = 1;

            }
            if (dir.X > 1f || dir.Y > 1f)
            {
                dir.Normalize();
            }

            if (dir == Vector2.Zero)
            {
                dir = new Vector2(1, 0);
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.A))
            {
                position += (dir * speed);
            }

            rotation = (float)Math.Atan2(dir.X, dir.Y) * -1;
            
        }

        // Player follows mouse to move.
        public void MovementHard()
        {
            Vector2 moveDir = new Vector2();
            MouseState mouse = Mouse.GetState();
            moveDir = mouse.Position.ToVector2() - (position+offset);
            moveDir.Normalize();
            position += (moveDir * speed);
            rotation = (float)Math.Atan2(moveDir.X, moveDir.Y)*-1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!dead)
            {
                spriteBatch.Draw(texture, position + offset, null, Color.White, rotation, offset, 1f, SpriteEffects.None, 0);
            }
        }
    }
}