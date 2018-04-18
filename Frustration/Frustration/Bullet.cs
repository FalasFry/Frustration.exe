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
        public float speed,rotation = 0;
        public Vector2 dir,position,offset,scale = new Vector2(0.07f,0.07f);
        public Texture2D texture;
        public Rectangle rectangle;
        public Color color = Color.White;

        
        public Bullet(float Speed,Vector2 Dir,Texture2D Texture, Vector2 startPos)
        {
            speed = Speed;
            dir = Dir;
            texture = Texture;
            position = startPos;
            offset = ((texture.Bounds.Size.ToVector2())/2);
            rectangle = new Rectangle((offset - position).ToPoint(),(texture.Bounds.Size.ToVector2() * scale).ToPoint());

        }
        public void Update()
        {
            position += (dir * speed);
            rectangle.Location = (position).ToPoint();
            rotation = (float)Math.Atan2(dir.X,dir.Y)*-1;

        }
        public void DrawBullet(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 1);
           // spriteBatch.Draw(texture,null, rectangle,null,offset,rotation,new Vector2(1,1), Color.Black,SpriteEffects.None,0);

        }
    }
}