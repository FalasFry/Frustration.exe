using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Frustration
{
    public class GameState : States
    {

        float pressTimer = 0f;
        float timeElapsed;
        Player player;
        Texture2D bullet;
        Texture2D backSpace;
        List<Bullet> bullets;
        SpriteFont font;
        bool manualSpawning = true;
        List<int> posList = new List<int>();
        List<int> form1 = new List<int> { 4, 5, 0, 3, 6, 0, 3, 6, 0, 3, 6, 0, 3, 6, 3, 2, 7, 1, 8, 1, 0, 3, 6, 4, 7, 8, 1, 4, 5, 6, 0, 8, 2, 8, 3, 7,3, 0 };
        List <int> order = new List<int>();
        float delay = 2f;
        float remainingDelay = 1.5f;
        List<Enemy> enemies;
        float enemyCount = 3;
        float enemiesPerLine = 1;
        float speed = 1;
        float smartPercent;
        Random rnd = new Random();
        public float score;
        
        List<PowerUp> powerUps;



        // Contructor that makes a gamestate work with all variables and working funktions.
        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, bool easyMode) : base(Game, graphicsDevice, content)
        {
            posList = form1;
            
            player = new Player(game.Content.Load<Texture2D>("spaceship.1"),game)
            {
                difficulty = easyMode
            };

            font = content.Load<SpriteFont>("ammo");
            enemies = new List<Enemy>();
            backSpace = content.Load<Texture2D>("stars");
            bullet = game.bulletTexture;
            
            bullets = new List<Bullet>();
            powerUps = new List<PowerUp>
            {
                new PowerUp(2, game.Content.Load<Texture2D>("1-ball.svg"), new Vector2(800, rnd.Next(0, 400)), 1, player, game)
            };
        }

        #region Methods

        public void ReadPosition()
        {
            if (posList.Count == 0)
            {
                manualSpawning = false;
            }
            for (int i = 0; i < posList.Count;)
            {
                if (posList[i] == 0)
                {
                    posList.RemoveAt(i);
                    break;
                }
                else
                {
                    order.Add(posList[i]);
                    posList.RemoveAt(i);
                }
            }
        }

        public void GiveValues(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int x = rnd.Next(0, 10);
                if (!order.Contains(x))
                {
                    order.Add(x);
                }
                else i--;
            }
        }

        public float GivePosition(int num)
        {
            return num * 44 - 20;
        }

        public bool IsSmart()
        {
            if (rnd.Next(0, (int)smartPercent) > rnd.Next(0, 100))
            {
                return true;
            }
            else return false;
        }

        // Shoots when you have ammo and press left mouse.
        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), player.position+player.offset), bullet, (player.position+player.offset),1));
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

        public void EnemyShoot()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].FindIQ(i, enemies))
                {
                    bullets.Add(new Bullet(10f,GetDir(player.position, enemies[i].FindPos(i, enemies)), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i),2));
                }
                else bullets.Add(new Bullet(10f, new Vector2(-1, 0), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i),2));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(backSpace, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.DrawString(font, "ammo = " + player.ammo.ToString(), new Vector2(700, 10), Color.White);

            player.Draw(spriteBatch);

            #region Enemy

            for (int i = 0; i < enemies.Count; ++i)
            {
                enemies[i].DrawEnemy(spriteBatch);
            }

            for (int i = 0; i < bullets.Count; ++i)
            {
                bullets[i].DrawBullet(spriteBatch);
                if (bullets[i].rectangle.Intersects(player.rectangle) && bullets[i].owner !=1)
                {
                    player.hp -= bullets[i].damage;
                    bullets.RemoveAt(i);
                }

            }
            for (int i = 0; i < bullets.Count; ++i)
            {
                for (int k = 0; k < enemies.Count; ++k)
                {
                    if (bullets[i].rectangle.Intersects(enemies[k].rectangle) && bullets[i].owner != 2)
                    {
                        bullets.RemoveAt(i);
                        if (i == bullets.Count)
                        {
                            --i;
                        }
                        enemies.RemoveAt(k);
                        ++score;
                    }
                }
            }


            #endregion

            #region PowerUps

            for (int j = 0; j < powerUps.Count; j++)
            {
                if (powerUps[j].rectangle.Intersects(player.rectangle))
                {
                    powerUps.RemoveAt(j);
                }
            }
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(spriteBatch);
            }

            #endregion

            spriteBatch.DrawString(font, "score = " + score.ToString(), new Vector2(10, 10), Color.White);

            spriteBatch.End();
        }


        public override bool Update(GameTime gameTime)
        {
            
            KeyboardState pause = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouse = Mouse.GetState();
            timeElapsed += deltaTime;

            #region Game Over Screen

            if (player.dead == true)
            {
                game.ChangeState(new GameOverState(game, graphDevice, contentManager, score, (int)timeElapsed));
            }

            #endregion

            #region enemies

            for (int i = 0; i < order.Count;)
            {
                enemies.Add(new Enemy(game.enemyTexture, new Vector2(1000, GivePosition(order[i])), speed, new Vector2(0.2f, 0.2f), 0, Color.White, IsSmart()));
                order.RemoveAt(i);
            }

            player.Update();

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].FindPos(i,enemies).X < 0)
                {
                    enemies.RemoveAt(i);
                }
                enemies[i].Update();
            }

            remainingDelay -= deltaTime;
            if (remainingDelay <= 0)
            {
                if (enemiesPerLine < 3) enemiesPerLine += 0.02f;
                if (speed < 4) speed *= 1.02f;
                if (delay > 1f) delay -= 0.02f;
                if (smartPercent < 40) smartPercent += 0.4f;

                if (manualSpawning) ReadPosition();
                else GiveValues((int)enemyCount);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemyCount = enemiesPerLine;
                    EnemyShoot();
                }
                remainingDelay = delay;
            }
            #endregion
            
            #region For Shooting Bullets

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }
            for (int j = 0; j < powerUps.Count; j++)
            {
                powerUps[j].Update();
            }

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

        #endregion
    }
}
