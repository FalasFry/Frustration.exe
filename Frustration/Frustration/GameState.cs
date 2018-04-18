using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class GameState : States
    {
        KeyboardState pause = Keyboard.GetState();
        float pressTimer = 0f;
        Player player;
        Texture2D bullet;

        List<Bullet> bullets;

        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, bool ez) : base(Game, graphicsDevice, content)
        {
            player = new Player(game.Content.Load<Texture2D>("ball"))
            {
                difficulty = ez
            };

            bullet = game.bulletTexture;

            bullets = new List<Bullet>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            spriteBatch.Begin();

            spriteBatch.Draw(game.backGround, new Rectangle(0, 0, 800, 480), Color.White);

            player.Draw(spriteBatch);

            for (int i = 0; i < bullets.Count; ++i)
            {
                bullets[i].DrawBullet(spriteBatch);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime) { }
        

        public override bool Update(GameTime gameTime)
        {
            player.Update();
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }

            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (game.attackTimer <= 0)
                {
                    Shoot();
                    game.attackTimer = game.attackSpeed;
                }
            }
            game.attackTimer -= deltaTime;


            pressTimer += deltaTime;

            if(pause.IsKeyDown(Keys.Escape) && pressTimer > 1f)
            {
                game.ChangeState(new Pause_Menu(game, graphDevice, contentManager));
                pressTimer = 0f;
            }

            return true;
        }

        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), (player.position)), bullet, (player.position+player.offset)));
                player.ammo--;
            }

        }
        public Vector2 GetDir(Vector2 to, Vector2 from)
        {
            Vector2 dir = to - from;
            dir.Normalize();

            return dir;
        }

    }
}
