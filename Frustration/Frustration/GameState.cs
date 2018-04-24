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

        float pressTimer = 0f;
        Player player;
        Texture2D bullet;
        Texture2D backSpace;
        List<Bullet> bullets;
        List<PowerUp> powerUps;
        Random rnd = new Random();



        // Contructor that makes a gamestate work with all variables and working funktions.
        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, bool easyMode) : base(Game, graphicsDevice, content)
        {
            player = new Player(game.Content.Load<Texture2D>("spaceship.1"))
            {
                difficulty = easyMode
            };

            backSpace = content.Load<Texture2D>("stars");
            bullet = game.bulletTexture;
            
            bullets = new List<Bullet>();
            powerUps.Add(new PowerUp(10,game.Content.Load<Texture2D>("ball"),new Vector2(800,rnd.Next(0,400)),rnd.Next(0,3),player,game));

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(backSpace, new Rectangle(0, 0, 800, 480), Color.White);

            player.Draw(spriteBatch);

            for (int i = 0; i < bullets.Count; ++i)
            {
                bullets[i].DrawBullet(spriteBatch);
                if (bullets[i].rectangle.Intersects(player.rectangle))
                {
                   // player.position = new Vector2(1000,1000);
                }
            }
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        

        public override bool Update(GameTime gameTime)
        {
            player.Update();
            KeyboardState pause = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region For Shooting Bullets

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

            #endregion

            #region Swap To Pause Menu

            pressTimer += deltaTime;
            if (pause.IsKeyDown(Keys.Escape) && pressTimer > 1f)
            {
                game.ChangeState(new PauseState(game, graphDevice, contentManager));
                pressTimer = 0f;
            }

            return true;

            #endregion
        }


        // Shoots when you have ammo and press left mouse.
        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), player.position+player.offset), bullet, (player.position+player.offset)));
                player.ammo--;
            }

        }

        // Gets the direction of the bullets.
        public Vector2 GetDir(Vector2 to, Vector2 from)
        {
            Vector2 dir = to - from;
            dir.Normalize();

            return dir;
        }

    }
}
