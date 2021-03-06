﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class Bullet
    {
        #region Variables
        public float speed,rotation = 0;
        public Vector2 dir,position,offset,scale = new Vector2(0.07f,0.07f);
        public Texture2D texture;
        public Rectangle rectangle;
        public float damage = 1;
        public Color color;
        public float owner;
        #endregion


        public Bullet(float Speed,Vector2 Dir,Texture2D Texture, Vector2 startPos, float Owner, Color paint)
        {
            owner = Owner;
            speed = Speed;
            dir = Dir;
            texture = Texture;
            position = startPos;
            offset = ((texture.Bounds.Size.ToVector2())/2);
            rectangle = new Rectangle((offset - position).ToPoint(),new Point(20,20));
            color = paint;
        }

        public void Update()
        {
            
            position += (dir * speed);
            rectangle.Location = (position-(rectangle.Size.ToVector2()*0.5f)).ToPoint();
            rotation = (float)Math.Atan2(dir.X,dir.Y)*-1;

        }

        public void DrawBullet(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, 1f, SpriteEffects.None, 1);
        }
    }
}