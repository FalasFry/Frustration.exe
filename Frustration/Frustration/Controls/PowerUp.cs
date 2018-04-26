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
        public float speed, rotation;
        public Vector2 dir = new Vector2(-1, 0), position, offset;
        public Texture2D texture;
        public Rectangle rectangle;
        public Color color = Color.White;
        public int powerType;
        Player player;
        Game1 game;
        float countDown = 10;
        bool wait = false;

        public PowerUp(float Speed, Texture2D Texture, Vector2 startPos,int PowerType,Player aPlayer,Game1 aGame)
        {
            speed = Speed;
            texture = Texture;
            position = startPos;
            powerType = PowerType;
            player = aPlayer;
            game = aGame;
            offset = ((texture.Bounds.Size.ToVector2()) / 2);
            rectangle = new Rectangle((offset - position).ToPoint(), (texture.Bounds.Size.ToVector2()).ToPoint());

        }
        public void Update(GameTime gameTime)
        {
            position += (dir * speed);
            rectangle.Location = (position).ToPoint();
            rotation = (float)Math.Atan2(dir.X, dir.Y) * -1;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (rectangle.Intersects(player.rectangle))
            {


                if (powerType == 1)
                {
                    game.attackSpeed = 0.1f;
                    player.ammo += 60;
                }
                if (powerType == 2)
                {
                    player.speed = player.speed * 2;
                }
                if (powerType == 3)
                {
                    //color = Color.Red;
                }
            }

            if (powerType == 1)
            {
                color = Color.Green;
            }
            if (powerType == 2)
            {
                //color = Color.Blue;
            }
            if (powerType == 3)
            {
                //color = Color.Red;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position+offset, null, color, rotation, offset, 1f, SpriteEffects.None, 1);
            // spriteBatch.Draw(texture,null, rectangle,null,offset,rotation,new Vector2(1,1), Color.Black,SpriteEffects.None,0);
            // spriteBatch.Draw(texture,rectangle,Color.Cyan);
        }
    }
}
