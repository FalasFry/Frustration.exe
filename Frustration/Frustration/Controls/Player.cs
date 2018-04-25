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
        public float speed = 5, rotation = 0,ammo = 20;
        public Vector2 dir, position, offset, scale = new Vector2(0.3f, 0.3f);
        public Texture2D texture;
        public Rectangle rectangle;
        public float hp = 10;
        public bool dead = false;
        Game1 game1;
        
        public bool difficulty = false;

        public Player(Texture2D Texture, Game1 game)
        {
            game1 = game;
            texture = Texture;
            offset = ((texture.Bounds.Size.ToVector2() * 0.5f));
            //offset = new Vector2(texture.Width/2,texture.Height/2)*scale;
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

            #region Controls
           
            if (keyState.IsKeyDown(Keys.R) && ammo < 20)
            {
                ammo = 20;
            }
            if (difficulty)
            {
                MovementNormal();
            }
            else
            {
                MovementHard();
            }

            #endregion
        }
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