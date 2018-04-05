using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class Bullet
    {
        public float speed;
        public Vector2 dir;
        public Texture2D texture;
        public Rectangle rectangle;
        
        public Bullet(float Speed,Vector2 Dir,Texture2D Texture, Rectangle Rectangle)
        {
            speed = Speed;
            dir = Dir;
            texture = Texture;
            rectangle = Rectangle;
        }
        public void Move()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle,Color.Black);
        }
    }
}