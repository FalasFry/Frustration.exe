using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{

    public class Player
    {
        public float speed = 5, rotation = 0,ammo = 2000;
        public Vector2 dir, position, offset, scale = new Vector2(0.07f, 0.07f);
        public Texture2D texture;
        public Rectangle rectangle;
        public Player(Texture2D Texture)
        {
            texture = Texture;
            offset = ((texture.Bounds.Size.ToVector2() * scale) / 2);
            position = new Vector2(50,50);
            rectangle = new Rectangle((position-offset).ToPoint(), (texture.Bounds.Size.ToVector2()*scale).ToPoint());
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 dir = new Vector2();

            rectangle.Location = (position).ToPoint();
            //rectangle.Offset(-offset);

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
            position += (dir * speed);
            if (keyState.IsKeyDown(Keys.R))
            {
                ammo = 20;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, offset, scale, SpriteEffects.None, 54);
            spriteBatch.Draw(texture, rectangle, Color.Black);

        }
    }
}