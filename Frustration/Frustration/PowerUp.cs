using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class PowerUp
    {
        public float speed, rotation = 0;
        public Vector2 dir = new Vector2(-1,0), position, offset, scale = new Vector2(0.07f, 0.07f);
        public Texture2D texture;
        public Rectangle rectangle;
        public Color color = Color.White;
        public int powerType;
        Player player;
        Game1 game;

        public PowerUp(float Speed, Texture2D Texture, Vector2 startPos,int PowerType,Player aPlayer,Game1 aGame)
        {
            speed = Speed;
            texture = Texture;
            position = startPos;
            powerType = PowerType;
            player = aPlayer;
            game = aGame;
            offset = ((texture.Bounds.Size.ToVector2()) / 2);
            rectangle = new Rectangle((offset - position).ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());

        }
        public void Update()
        {
            position += (dir * speed);
            rectangle.Location = (position).ToPoint();
            rotation = (float)Math.Atan2(dir.X, dir.Y) * -1;
            if (rectangle.Intersects(player.rectangle))
            {
                if (powerType == 1)
                {
                    game.attackSpeed = 0.1f;
                }
                if (powerType == 2)
                {
                    color = Color.Blue;
                }
                if (powerType == 3)
                {
                    color = Color.Red;
                }
            }
            if (powerType == 1)
            {
                color = Color.Green;
            }
            if (powerType == 2)
            {
                color = Color.Blue;
            }
            if (powerType == 3)
            {
                color = Color.Red;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 1);
            // spriteBatch.Draw(texture,null, rectangle,null,offset,rotation,new Vector2(1,1), Color.Black,SpriteEffects.None,0);

        }
    }
}
